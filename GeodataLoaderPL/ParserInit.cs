using ColossalFramework;
using GeodataLoaderPL.Factories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using UnityEngine;

namespace GeodataLoaderPL
{
    public class ParserInit
    {
        private int timesNetUsed;
        private int timesBuildingUsed;
        private int timesPropUsed;
        private int timesResourceUsed;
        private int timesTreeUsed;
        private int timesWaterUsed;
        private string modDirectory;

        public ParserInit()
        {
            var currentDir = Environment.CurrentDirectory;
            var steamAppsDir = System.IO.Directory.GetParent(System.IO.Directory.GetParent(currentDir).ToString()).ToString();
            modDirectory = $"{steamAppsDir}\\workshop\\content\\255710\\2131751315\\";
        }

        internal string[] Arguments { get; set; } = new string[5]; // X,Y,AreaSideLength,TDBPath,DEMPath

        internal void ProcessTDBExternally()
        {

            UnityEngine.Debug.Log(modDirectory);
            var psi = new ProcessStartInfo();
            // !!! STEAM PATH CHECK NEEDED && CLEAR ARGS !!!
            psi.FileName = $"{modDirectory}GMLParserPL.exe";
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardInput = true;
            psi.RedirectStandardError = true;
            psi.UseShellExecute = false;

            psi.Arguments = "\"" + Arguments[0] + "\"" + " " + "\"" + Arguments[1] + "\"" + " " + "\"" + Arguments[2] + "\""
                + " " + "\"" + Arguments[3] + "\"";
            var p = new Process();
            p.StartInfo = psi;
            p.Start();

            NetFactory netFactory = new NetFactory();
            BuildingFactory buildingFactory = new BuildingFactory();
            PropFactory propFactory = new PropFactory();
            ResourceFactory resourceFactory = new ResourceFactory();
            TreeFactory treeFactory = new TreeFactory();
            WaterFactory waterFactory = new WaterFactory();

            waterFactory.SetSeaLevelTo0();

            while (!p.StandardOutput.EndOfStream)
            {
                string line = p.StandardOutput.ReadLine();
                SelectFactory(line, netFactory, buildingFactory, propFactory, resourceFactory, treeFactory, waterFactory);
            }
            netFactory.UpdateSegments();

            UnityEngine.Debug.Log("TDB loaded");
            UnityEngine.Debug.Log($"Buildings loaded/called/max: {buildingFactory.Temp}/{timesBuildingUsed}/{BuildingManager.MAX_BUILDING_COUNT}");
            UnityEngine.Debug.Log($"Segments loaded/called/max: {netFactory.TempS}/{timesNetUsed}/{NetManager.MAX_SEGMENT_COUNT}");
            UnityEngine.Debug.Log($"Net nodes loaded/max: {netFactory.TempN}/{NetManager.MAX_NODE_COUNT}");
            UnityEngine.Debug.Log($"Props loaded/called/max: {propFactory.Temp}/{timesPropUsed}/{PropManager.MAX_PROP_COUNT}");
            UnityEngine.Debug.Log($"Trees loaded/called/max: {treeFactory.Temp}/{timesTreeUsed}/{TreeManager.MAX_TREE_COUNT}");
            UnityEngine.Debug.Log($"Resources loaded/called: {resourceFactory.Temp}/{timesResourceUsed}");
            UnityEngine.Debug.Log($"Resources loaded/called: {waterFactory.Temp}/{timesWaterUsed}");
        }

        internal void ProcessDEMExternally()
        {
            var psi = new ProcessStartInfo();
            // !!! STEAM PATH CHECK NEEDED && CLEAR ARGS !!!
            psi.FileName = $"{modDirectory}ASCIIParserPL.exe";
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardInput = true;
            psi.RedirectStandardError = true;
            psi.UseShellExecute = false;

            psi.Arguments = "\"" + Arguments[0] + "\"" + " " + "\"" + Arguments[1] + "\"" + " " + "\"" + Arguments[4] + "\"";
            var p = new Process();
            p.StartInfo = psi;
            p.Start();

            TerrainFactory terrainFactory = new TerrainFactory();
            WaterFactory waterFactory = new WaterFactory();

            waterFactory.SetSeaLevelTo0();

            while (!p.StandardOutput.EndOfStream)
            {
                string line = p.StandardOutput.ReadLine();
                List<string> arguments = line.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                if (arguments[0] == "Terrain")
                {
                    byte[] map = System.Convert.FromBase64String(arguments[1]);
                    terrainFactory.SetTerrain(map);
                    UnityEngine.Debug.Log("DEM imported");
                }
                if (arguments[0] == "Error")
                {
                    UnityEngine.Debug.Log(arguments[1]);
                }
            }
        }

        private void SelectFactory(string line, NetFactory netFactory, BuildingFactory buildingFactory, PropFactory propFactory,
            ResourceFactory resourceFactory, TreeFactory treeFactory, WaterFactory waterFactory)
        {
            if (!line.IsNullOrWhiteSpace())
            {
                List<string> arguments = line.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                float[] points;
                float angle;
                switch (arguments[0])
                {
                    case "Resource":
                        points = ReturnFloatsFromArg(arguments);
                        NaturalResourceManager.Resource resource = (NaturalResourceManager.Resource)Enum
                            .Parse(typeof(NaturalResourceManager.Resource), arguments[1], true);
                        resourceFactory.CreateResource(new Vector2(points[0], points[1]), resource);
                        timesResourceUsed++;
                        break;
                    case "Water":
                        points = ReturnFloatsFromArg(arguments);
                        var flowInOut = arguments[3].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(x => uint.Parse(x, CultureInfo.InvariantCulture))
                            .ToArray();
                        waterFactory.NewWaterSource(new Vector2(points[0], points[1]), flowInOut[0], flowInOut[1], flowInOut[2]);
                        timesWaterUsed++;
                        break;
                    case "Net":
                        points = ReturnFloatsFromArg(arguments);
                        netFactory.Create(new Vector2(points[0], points[1]), new Vector2(points[2], points[3]), arguments[1]);
                        timesNetUsed++;
                        break;
                    case "Building":
                        points = ReturnFloatsFromArg(arguments);
                        angle = float.Parse(arguments[3], CultureInfo.InvariantCulture);
                        buildingFactory.Create(new Vector2(points[0], points[1]), angle, arguments[1]);
                        timesBuildingUsed++;
                        break;
                    case "Tree":
                        points = ReturnFloatsFromArg(arguments);
                        treeFactory.Create(new Vector2(points[0], points[1]), arguments[1]);
                        timesTreeUsed++;
                        break;
                    case "Prop":
                        points = ReturnFloatsFromArg(arguments);
                        angle = float.Parse(arguments[3], CultureInfo.InvariantCulture);
                        propFactory.Create(new Vector2(points[0], points[1]), angle, arguments[1]);
                        timesPropUsed++;
                        break;
                    case "Error":
                        UnityEngine.Debug.Log(arguments[1]);
                        break;
                    default:
                        UnityEngine.Debug.Log(arguments[0]);
                        break;
                }
            }
        }

        private static float[] ReturnFloatsFromArg(List<string> arguments)
        {
            return arguments[4].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                    .Select(x => float.Parse(x, CultureInfo.InvariantCulture))
                                    .ToArray();
        }
    }
}
