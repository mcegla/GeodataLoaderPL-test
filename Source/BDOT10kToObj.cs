using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using ColossalFramework.Plugins;
using System.Collections;
using ColossalFramework;
using ColossalFramework.Math;
using System.Text;
using System.Xml.Serialization;
using GeodataLoader.Source.Parsers;
using GeodataLoader.Source.Helpers;


//=================================================================================
//=== This part of code is responsible for converting all BDOT10k to Unity obj. ===
//=================================================================================

namespace GeodataLoader.Source
{    
    //=============================================================================
    //=== This part contains classes responsible for creation of ingame objects ===
    public class CreateTree
    {
        public static void Start(float coordX, float coordY, string treeType)
        {
            TreeInfo tree = PrefabCollection<TreeInfo>.FindLoaded(treeType);
            if (tree == null)
            {
                Debug.LogError("Tree could not be found");
            }
            SimulationManager.instance.AddAction(AddTree(coordX, coordY, tree));
        }

        private static IEnumerator AddTree(float x, float y, TreeInfo tree)
        {
            uint treeNum;
            TreeManager.instance.CreateTree(out treeNum, ref SimulationManager.instance.m_randomizer, tree, new Vector3(x, 0, y), false);
            yield return null;
        }
    }

    public class CreateProp
    {
        public static void Start(float coordX, float coordY, float angle, string propType)
        {
            PropInfo prop = PrefabCollection<PropInfo>.FindLoaded(propType);
            if (prop == null)
            {
                Debug.LogError("Prop could not be found");
            }
            SimulationManager.instance.AddAction(AddProp(coordX, coordY, angle, prop));
        }

        private static IEnumerator AddProp(float x, float y, float angle, PropInfo prop)
        {
            ushort propNum;
            PropManager.instance.CreateProp(out propNum, ref SimulationManager.instance.m_randomizer, prop, new Vector3(x, 0, y), angle, false);
            yield return null;
        }
    }

    public class CreateNet
    {
        public static void Start(float x1, float y1, float x2, float y2, string netType)
        {
            //NetInfo droga = PrefabCollection<NetInfo>.FindLoaded("Basic Road");
            NetInfo net = PrefabCollection<NetInfo>.FindLoaded(netType);
            //NetInfo Prefab;
            //NetCollection tdr = GameObject.FindObjectOfType<NetCollection>();
            //Prefab = tdr.m_prefabs[0];
            if (net == null)
            {
                Debug.LogError("Network could not be found");
            }
            //float x11 = 0;
            //float y11 = 0;
            //float x22 = 50f;
            //float y22 = 50f;

            ushort startN;
            var z1 = Singleton<TerrainManager>.instance.SampleRawHeightSmoothWithWater(new Vector3(x1, 0, y1), false, 0f);
            NetManager.instance.CreateNode(out startN, ref SimulationManager.instance.m_randomizer, net, new Vector3(x1, z1, y1), Singleton<SimulationManager>.instance.m_currentBuildIndex);
            Singleton<SimulationManager>.instance.m_currentBuildIndex += 1u;
            ushort endN;
            var z2 = Singleton<TerrainManager>.instance.SampleRawHeightSmoothWithWater(new Vector3(x2, 0, y2), false, 0f);
            NetManager.instance.CreateNode(out endN, ref SimulationManager.instance.m_randomizer, net, new Vector3(x2, z2, y2), Singleton<SimulationManager>.instance.m_currentBuildIndex);
            Singleton<SimulationManager>.instance.m_currentBuildIndex += 1u;

            Vector3 pos1 = Singleton<NetManager>.instance.m_nodes.m_buffer[startN].m_position;
            Vector3 pos2 = Singleton<NetManager>.instance.m_nodes.m_buffer[endN].m_position;
            Vector3 pos = pos2 - pos1;
            pos = VectorUtils.NormalizeXZ(pos);

            ushort segmentID;
            NetManager.instance.CreateSegment(out segmentID, ref SimulationManager.instance.m_randomizer, net, startN, endN, pos, -pos, Singleton<SimulationManager>.instance.m_currentBuildIndex, Singleton<SimulationManager>.instance.m_currentBuildIndex, false);
            Singleton<SimulationManager>.instance.m_currentBuildIndex += 2u;
        }
    }

    public class CreateBuilding
    {
        public static void Start(float coordX, float coordY, float angle, int length, string BuildingType)
        {
            //BuildingInfo building = PrefabCollection<BuildingInfo>.FindLoaded("cathedral_of_cologne");
            BuildingInfo building = PrefabCollection<BuildingInfo>.FindLoaded(BuildingType);
            if (building == null)
            {
                Debug.LogError("Building could not be found");
            }
            SimulationManager.instance.AddAction(AddBuilding(coordX, coordY, angle, length, building));
        }
        private static IEnumerator AddBuilding(float coordX, float coordY, float angle, int length, BuildingInfo building)
        {
            ushort buildingNum;
            BuildingManager.instance.CreateBuilding(out buildingNum, ref SimulationManager.instance.m_randomizer, building, new Vector3(coordX, 0, coordY), angle, length, Singleton<SimulationManager>.instance.m_currentBuildIndex);
            Singleton<SimulationManager>.instance.m_currentBuildIndex += 1u;
            yield return null;
        }
    }

    //============================================
    //=== This part contains conversion to obj ===
    public class BDOT10kToObj
    {
        GeodataLoaderConfiguration config;
        public BDOT10kToObj()
        {
            //Loading configuration
            config = Configuration<GeodataLoaderConfiguration>.Load();
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class ADJA_A
        public void ADJA_A()
        {
            //var type = "ADJA_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_ADJA_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //    {

            //    }
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class ADMS_A
        public void ADMS_A()
        {
            //var type = "ADMS_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_ADMS_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //    {

            //    }
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO - BTW CO NIE TAK JEST Z TA GEOMETRIA
        //Converting BDOT10k class ADMS_P
        public void ADMS_P()
        {
            //var type = "ADMS_P";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_ADMS_P();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //    {

            //    }
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class BUBD_A
        public void BUBD_A()
        {
            //var type = "BUBD_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_BUBD_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    if (entity.xkod == "BUBD01")
            //    {
            //        //JAK DOSTAĆ ILOŚĆ ELEMENTÓW?
            //        float sumX = 0;
            //        float sumY = 0;
            //        int numberOfDifferentCoordinates = 1; //ŻLE
            //        foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //        {
            //            sumX = sumX + p[0];
            //            sumY = sumY + p[1];
            //            float buildingX = sumX / numberOfDifferentCoordinates;
            //            float buildingY = sumY / numberOfDifferentCoordinates;
            //            CreateBuilding.Start(buildingX, buildingY, 0, 0, "cathedral_of_cologne");

            //        }
            //    }
            //    //ITD
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class BUCM_A
        public void BUCM_A()
        {
            //var type = "BUCM_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_BUCM_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    if (entity.xkod == "BUCM01")
            //    {
            //        //JAK DOSTAĆ ILOŚĆ ELEMENTÓW?
            //        float sumX = 0;
            //        float sumY = 0;
            //        int numberOfDifferentCoordinates = 1; //ŻLE
            //        foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //        {
            //            sumX = sumX + p[0];
            //            sumY = sumY + p[1];
            //            float buildingX = sumX / numberOfDifferentCoordinates;
            //            float buildingY = sumY / numberOfDifferentCoordinates;
            //            CreateBuilding.Start(buildingX, buildingY, 0, 0, "cathedral_of_cologne");

            //        }
            //    }
            //    //ITD
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class BUHD_A
        public void BUHD_A() //Incomplete model
        {
            //var type = "BUHD_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_BUHD_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    if (entity.xkod == "BUHD01")
            //    {
            //        //JAK DOSTAĆ ILOŚĆ ELEMENTÓW?
            //        float sumX = 0;
            //        float sumY = 0;
            //        int numberOfDifferentCoordinates = 1; //ŻLE
            //        foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //        {
            //            sumX = sumX + p[0];
            //            sumY = sumY + p[1];
            //            float buildingX = sumX / numberOfDifferentCoordinates;
            //            float buildingY = sumY / numberOfDifferentCoordinates;
            //            CreateBuilding.Start(buildingX, buildingY, 0, 0, "cathedral_of_cologne");

            //        }
            //    }
            //    //ITD
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class BUHD_L
        public void BUHD_L()
        {
            //var type = "BUHD_L";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_BUHD_L();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //    {

            //    }
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class BUHD_P
        public void BUHD_P() //Incomplete model
        {
            //var type = "BUHD_P";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_BUHD_P();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //    {

            //    }
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class BUIB_A
        public void BUIB_A()
        {
            //var type = "BUIB_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_BUIB_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    if (entity.xkod == "BUIB01")
            //    {
            //        //JAK DOSTAĆ ILOŚĆ ELEMENTÓW?
            //        float sumX = 0;
            //        float sumY = 0;
            //        int numberOfDifferentCoordinates = 1; //ŻLE
            //        foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //        {
            //            sumX = sumX + p[0];
            //            sumY = sumY + p[1];
            //            float buildingX = sumX / numberOfDifferentCoordinates;
            //            float buildingY = sumY / numberOfDifferentCoordinates;
            //            CreateBuilding.Start(buildingX, buildingY, 0, 0, "cathedral_of_cologne");

            //        }
            //    }
            //    //ITD
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class BUIB_L
        public void BUIB_L()
        {
            //var type = "BUIB_L";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_BUIB_L();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //    {

            //    }
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class BUIN_L
        public void BUIN_L()
        {
            //var type = "BUIN_L";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_BUIN_L();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //    {

            //    }
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class BUIT_A
        public void BUIT_A()
        {
            //var type = "BUIT_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_BUIT_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    if (entity.xkod == "BUIT01")
            //    {
            //        //JAK DOSTAĆ ILOŚĆ ELEMENTÓW?
            //        float sumX = 0;
            //        float sumY = 0;
            //        int numberOfDifferentCoordinates = 1; //ŻLE
            //        foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //        {
            //            sumX = sumX + p[0];
            //            sumY = sumY + p[1];
            //            float buildingX = sumX / numberOfDifferentCoordinates;
            //            float buildingY = sumY / numberOfDifferentCoordinates;
            //            CreateBuilding.Start(buildingX, buildingY, 0, 0, "cathedral_of_cologne");

            //        }
            //    }
            //    //ITD
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class BUIT_P
        public void BUIT_P()
        {
            var type = "BUIT_P";
            string file = null;
            try
            {
                file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            }
            catch
            {
                CommonHelpers.Log($"Can not find file of type:{type}");
                return;
            }
            CommonHelpers.Log($"found file:{file}");
            var parser = new Parser_BUIT_P();
            parser.InitDocument(file);
            CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            foreach (var entity in parser.GetBDOT10Ks())
            {
                var p = CoordinatesCalculator.GameXY(entity.XYPoint[0], entity.XYPoint[1]);
                if (CoordinatesCalculator.AllInRange(p))
                {
                    if (entity.XKod == "BUIT01")
                    {
                        CreateBuilding.Start(p[0], p[1], 0, 0, "Oil Extractor");
                    }
                    //TEST - obj too big
                    else if (entity.XKod == "BUIT02")
                    {
                        CreateBuilding.Start(p[0], p[1], 0, 0, "Water Intake");
                    }
                    //TEST - BUIT03 and BUIT04 not existing in vanilla, same placeholder prop used instead
                    else if (entity.XKod == "BUIT03")
                    {
                        CreateProp.Start(p[0], p[1], 0, "Air Source Heat Pump 02");
                    }
                    else if (entity.XKod == "BUIT04")
                    {
                        CreateBuilding.Start(p[0], p[1], 0, 0, "H3 1x1 Facility05");
                    }
                    else if (entity.XKod == "BUIT05")
                    {
                        CreateBuilding.Start(p[0], p[1], 0, 0, "L3 1x1 Shop");
                    }
                    //TEST - BUIT06 not existing in vanilla, placeholder prop used instead
                    else if (entity.XKod == "BUIT06")
                    {
                        CreateProp.Start(p[0], p[1], 0, "ILS Building"); 
                    }
                    //TEST - BUIT07 not existing in vanilla, placeholder prop used instead
                    else if (entity.XKod == "BUIT07")
                    {
                        CreateBuilding.Start(p[0], p[1], 0, 0, "Oil 2x2 Processing");
                    }
                }
            }
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class BUSP_A
        public void BUSP_A()
        {
            //var type = "BUSP_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_BUSP_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    if (entity.xkod == "BUSP01")
            //    {
            //        //JAK DOSTAĆ ILOŚĆ ELEMENTÓW?
            //        float sumX = 0;
            //        float sumY = 0;
            //        int numberOfDifferentCoordinates = 1; //ŻLE
            //        foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //        {
            //            sumX = sumX + p[0];
            //            sumY = sumY + p[1];
            //            float buildingX = sumX / numberOfDifferentCoordinates;
            //            float buildingY = sumY / numberOfDifferentCoordinates;
            //            CreateBuilding.Start(buildingX, buildingY, 0, 0, "cathedral_of_cologne");

            //        }
            //    }
            //    //ITD
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class BUSP_L
        public void BUSP_L() //Incomplete model
        {
            //var type = "BUSP_L";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_BUSP_L();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //    {

            //    }
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class BUTR_L
        public void BUTR_L()
        {
            //var type = "BUTR_L";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_BUTR_L();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //    {

            //    }
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class BUTR_P
        public void BUTR_P() //Incomplete model
        {
            //var type = "BUTR_P";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_BUTR_P();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //    {

            //    }
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class BUUO_L
        public void BUUO_L()
        {
            //var type = "BUUO_L";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_BUUO_L();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //    {

            //    }
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class BUWT_A
        public void BUWT_A() //Incomplete model
        {
            //var type = "BUWT_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_BUWT_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    if (entity.xkod == "BUWT01")
            //    {
            //        //JAK DOSTAĆ ILOŚĆ ELEMENTÓW?
            //        float sumX = 0;
            //        float sumY = 0;
            //        int numberOfDifferentCoordinates = 1; //ŻLE
            //        foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //        {
            //            sumX = sumX + p[0];
            //            sumY = sumY + p[1];
            //            float buildingX = sumX / numberOfDifferentCoordinates;
            //            float buildingY = sumY / numberOfDifferentCoordinates;
            //            CreateBuilding.Start(buildingX, buildingY, 0, 0, "cathedral_of_cologne");

            //        }
            //    }
            //    //ITD
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class BUWT_P
        public void BUWT_P()
        {
            var type = "BUWT_P";
            string file = null;
            try
            {
                file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            }
            catch
            {
                CommonHelpers.Log($"Can not find file of type:{type}");
                return;
            }
            CommonHelpers.Log($"found file:{file}");
            var parser = new Parser_BUWT_P();
            parser.InitDocument(file);
            CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            foreach (var entity in parser.GetBDOT10Ks())
            {
                var p = CoordinatesCalculator.GameXY(entity.XYPoint[0], entity.XYPoint[1]);
                if (CoordinatesCalculator.AllInRange(p))
                {
                    //TEST - BUWT00 not existing in vanilla, placeholder prop used instead
                    if (entity.XKod == "BUWT00")
                    {
                        CreateBuilding.Start(p[0], p[1], 0, 0, "Wooden Footbridge Pillar 18");
                    }
                    //TEST - BUWT01 not existing in vanilla, placeholder prop used instead
                    else if (entity.XKod == "BUWT01")
                    {
                        CreateBuilding.Start(p[0], p[1], 0, 0, "Castle Ruins 02");
                    }
                    //TEST - BUWT02 not existing in vanilla, placeholder prop used instead
                    else if (entity.XKod == "BUWT02")
                    {
                        CreateBuilding.Start(p[0], p[1], 0, 0, "Castle Ruins 03");
                    }
                    //TEST - BUWT03 not existing in vanilla, placeholder prop used instead
                    else if (entity.XKod == "BUWT03")
                    {
                        CreateProp.Start(p[0], p[1], 0, "Airport Light");
                    }
                    //TEST - BUWT04 not existing in vanilla, placeholder prop used instead
                    else if (entity.XKod == "BUWT04")
                    {
                        CreateProp.Start(p[0], p[1], 0, "Wifi antenna");
                    }
                    else if (entity.XKod == "BUWT05")
                    {
                        CreateBuilding.Start(p[0], p[1], 0, 0, "Wind Turbine");
                    }
                    //TEST - maybe better to use network with this obj?
                    else if (entity.XKod == "BUWT06")
                    {
                        CreateBuilding.Start(p[0], p[1], 0, 0, "Electricity Pole");
                    }
                    //TEST - BUWT07 not existing in vanilla, placeholder prop used instead
                    else if (entity.XKod == "BUWT07")
                    {
                        CreateBuilding.Start(p[0], p[1], 0, 0, "RailwayElevatedPillar");
                    }
                    else if (entity.XKod == "BUWT08")
                    {
                        CreateBuilding.Start(p[0], p[1], 0, 0, "Water Tower");
                    }
                    //TEST - BUWT09 not existing in vanilla, placeholder prop used instead
                    else if (entity.XKod == "BUWT09")
                    {
                        CreateBuilding.Start(p[0], p[1], 0, 0, "RailwayElevatedPillar");
                    }
                    else if (entity.XKod == "BUWT10")
                    {
                        CreateBuilding.Start(p[0], p[1], 0, 0, "Ore 2x2 Extractor");
                    }
                    //TEST - BUWT11 not existing in vanilla, placeholder prop used instead
                    else if (entity.XKod == "BUWT11")
                    {
                        CreateProp.Start(p[0], p[1], 0, "Wifi antenna");
                    }
                    //TEST - BUWT12 not existing in vanilla, placeholder prop used instead
                    else if (entity.XKod == "BUWT12")
                    {
                        CreateBuilding.Start(p[0], p[1], 0, 0, "Wooden Footbridge Pillar 6");
                    }
                }
            }
         }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class BUZM_L
        public void BUZM_L()
        {
            //var type = "BUZM_L";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_BUZM_L();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //    {

            //    }
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class BUZT_A
        public void BUZT_A()
        {
            //var type = "BUZT_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_BUZT_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    if (entity.xkod == "BUZT01")
            //    {
            //        //JAK DOSTAĆ ILOŚĆ ELEMENTÓW?
            //        float sumX = 0;
            //        float sumY = 0;
            //        int numberOfDifferentCoordinates = 1; //ŻLE
            //        foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //        {
            //            sumX = sumX + p[0];
            //            sumY = sumY + p[1];
            //            float buildingX = sumX / numberOfDifferentCoordinates;
            //            float buildingY = sumY / numberOfDifferentCoordinates;
            //            CreateBuilding.Start(buildingX, buildingY, 0, 0, "cathedral_of_cologne");

            //        }
            //    }
            //    //ITD
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class BUZT_P
        public void BUZT_P()
        {
            var type = "BUZT_P";
            string file = null;
            try
            {
                file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            }
            catch
            {
                CommonHelpers.Log($"Can not find file of type:{type}");
                return;
            }
            CommonHelpers.Log($"found file:{file}");
            var parser = new Parser_BUZT_P();
            parser.InitDocument(file);
            CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            foreach (var entity in parser.GetBDOT10Ks())
            {
                var p = CoordinatesCalculator.GameXY(entity.XYPoint[0], entity.XYPoint[1]);
                if (CoordinatesCalculator.AllInRange(p))
                {
                    //TEST - not sure if correct
                    if (entity.XKod == "BUZT01")
                    {
                        CreateBuilding.Start(p[0], p[1], 0, 0, "Agricultural 1x1 processing 1");
                    }
                    else if (entity.XKod == "BUZT02")
                    {
                        CreateBuilding.Start(p[0], p[1], 0, 0, "Oil 1x1 processing");
                    }
                    else if (entity.XKod == "BUZT03")
                    {
                        CreateBuilding.Start(p[0], p[1], 0, 0, "H1 1x1 Facility02");
                    }
                    else if (entity.XKod == "BUZT04")
                    {
                        CreateBuilding.Start(p[0], p[1], 0, 0, "Ore 1x1 processing");
                    }
                }
            }
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class KUHO_A
        public void KUHO_A() //Error
        {
            //var type = "KUHO_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_KUHO_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    if (entity.xkod == "KUHO01")
            //    {
            //        //JAK DOSTAĆ ILOŚĆ ELEMENTÓW?
            //        float sumX = 0;
            //        float sumY = 0;
            //        int numberOfDifferentCoordinates = 1; //ŻLE
            //        foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //        {
            //            sumX = sumX + p[0];
            //            sumY = sumY + p[1];
            //            float buildingX = sumX / numberOfDifferentCoordinates;
            //            float buildingY = sumY / numberOfDifferentCoordinates;
            //            CreateBuilding.Start(buildingX, buildingY, 0, 0, "cathedral_of_cologne");

            //        }
            //    }
            //    //ITD
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class KUHU_A
        public void KUHU_A()
        {
            //var type = "KUHU_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_KUHU_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    if (entity.xkod == "KUHU01")
            //    {
            //        //JAK DOSTAĆ ILOŚĆ ELEMENTÓW?
            //        float sumX = 0;
            //        float sumY = 0;
            //        int numberOfDifferentCoordinates = 1; //ŻLE
            //        foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //        {
            //            sumX = sumX + p[0];
            //            sumY = sumY + p[1];
            //            float buildingX = sumX / numberOfDifferentCoordinates;
            //            float buildingY = sumY / numberOfDifferentCoordinates;
            //            CreateBuilding.Start(buildingX, buildingY, 0, 0, "cathedral_of_cologne");

            //        }
            //    }
            //    //ITD
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class KUHU_P
        public void KUHU_P()
        {
            var type = "KUHU_P";
            string file = null;
            try
            {
                file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            }
            catch
            {
                CommonHelpers.Log($"Can not find file of type:{type}");
                return;
            }
            CommonHelpers.Log($"found file:{file}");
            var parser = new Parser_KUHU_P();
            parser.InitDocument(file);
            CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            foreach (var entity in parser.GetBDOT10Ks())
            {
                var p = CoordinatesCalculator.GameXY(entity.XYPoint[0], entity.XYPoint[1]);
                if (CoordinatesCalculator.AllInRange(p))
                {
                    if (entity.XKod == "KUHU01")
                    {
                        CreateBuilding.Start(p[0], p[1], 0, 0, "hypermarket");
                    }
                    else if (entity.XKod == "KUHU02")
                    {
                        CreateBuilding.Start(p[0], p[1], 0, 0, "Winter Market 01");
                    }
                }
            }
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class KUIK_A
        public void KUIK_A() //Incomplete model
        {
            //var type = "KUIK_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_KUIK_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    if (entity.xkod == "KUIK01")
            //    {
            //        //JAK DOSTAĆ ILOŚĆ ELEMENTÓW?
            //        float sumX = 0;
            //        float sumY = 0;
            //        int numberOfDifferentCoordinates = 1; //ŻLE
            //        foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //        {
            //            sumX = sumX + p[0];
            //            sumY = sumY + p[1];
            //            float buildingX = sumX / numberOfDifferentCoordinates;
            //            float buildingY = sumY / numberOfDifferentCoordinates;
            //            CreateBuilding.Start(buildingX, buildingY, 0, 0, "cathedral_of_cologne");

            //        }
            //    }
            //    //ITD
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class KUKO_A
        public void KUKO_A()
        {
            //var type = "KUKO_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_KUKO_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    if (entity.xkod == "KUKO01")
            //    {
            //        //JAK DOSTAĆ ILOŚĆ ELEMENTÓW?
            //        float sumX = 0;
            //        float sumY = 0;
            //        int numberOfDifferentCoordinates = 1; //ŻLE
            //        foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //        {
            //            sumX = sumX + p[0];
            //            sumY = sumY + p[1];
            //            float buildingX = sumX / numberOfDifferentCoordinates;
            //            float buildingY = sumY / numberOfDifferentCoordinates;
            //            CreateBuilding.Start(buildingX, buildingY, 0, 0, "cathedral_of_cologne");

            //        }
            //    }
            //    //ITD
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class KUKO_P
        public void KUKO_P() //Error
        {
            //var type = "KUKO_P";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_KUKO_P();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    var p = CoordinatesCalculator.GameXY(entity.XYPoint[0], entity.XYPoint[1]);
            //    if (CoordinatesCalculator.AllInRange(p))
            //    {
            //        if (entity.XKod == "KUKO01")
            //        {
            //            CreateBuilding.Start(p[0], p[1], 0, 0, "Bus Station");
            //        }
            //        else if (entity.XKod == "KUKO02")
            //        {
            //            CreateBuilding.Start(p[0], p[1], 0, 0, "Airport");
            //        }
            //        //TEST - KUKO03 not used, hard tu classify
                    
            //        else if (entity.XKod == "KUKO04")
            //        {
            //            CreateBuilding.Start(p[0], p[1], 0, 0, "Parking Space");
            //        }
            //        else if (entity.XKod == "KUKO05")
            //        {
            //            CreateBuilding.Start(p[0], p[1], 0, 0, "Harbor");
            //        }
            //        else if (entity.XKod == "KUKO06")
            //        {
            //            CreateBuilding.Start(p[0], p[1], 0, 0, "Train Station");
            //        }
            //        else if (entity.XKod == "KUKO07")
            //        {
            //            CreateBuilding.Start(p[0], p[1], 0, 0, "Metro Entrance");
            //        }
            //        else if (entity.XKod == "KUKO08")
            //        {
            //            CreateBuilding.Start(p[0], p[1], 0, 0, "L1 3x3 Shop16");
            //        }
            //        //KUKO09 not used
            //        else if (entity.XKod == "KUKO10")
            //        {
            //            CreateBuilding.Start(p[0], p[1], 0, 0, "Bus Depot");
            //        }
            //    }
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class KUMN_A
        public void KUMN_A()
        {
            //var type = "KUMN_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_KUMN_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    if (entity.xkod == "KUMN01")
            //    {
            //        //JAK DOSTAĆ ILOŚĆ ELEMENTÓW?
            //        float sumX = 0;
            //        float sumY = 0;
            //        int numberOfDifferentCoordinates = 1; //ŻLE
            //        foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //        {
            //            sumX = sumX + p[0];
            //            sumY = sumY + p[1];
            //            float buildingX = sumX / numberOfDifferentCoordinates;
            //            float buildingY = sumY / numberOfDifferentCoordinates;
            //            CreateBuilding.Start(buildingX, buildingY, 0, 0, "cathedral_of_cologne");

            //        }
            //    }
            //    //ITD
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class KUOS_A
        public void KUOS_A()
        {
            //var type = "KUOS_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_KUOS_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    if (entity.xkod == "KUOS01")
            //    {
            //        //JAK DOSTAĆ ILOŚĆ ELEMENTÓW?
            //        float sumX = 0;
            //        float sumY = 0;
            //        int numberOfDifferentCoordinates = 1; //ŻLE
            //        foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //        {
            //            sumX = sumX + p[0];
            //            sumY = sumY + p[1];
            //            float buildingX = sumX / numberOfDifferentCoordinates;
            //            float buildingY = sumY / numberOfDifferentCoordinates;
            //            CreateBuilding.Start(buildingX, buildingY, 0, 0, "cathedral_of_cologne");

            //        }
            //    }
            //    //ITD
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class KUOZ_A
        public void KUOZ_A()
        {
            //var type = "KUOZ_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_KUOZ_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    if (entity.xkod == "KUOZ01")
            //    {
            //        //JAK DOSTAĆ ILOŚĆ ELEMENTÓW?
            //        float sumX = 0;
            //        float sumY = 0;
            //        int numberOfDifferentCoordinates = 1; //ŻLE
            //        foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //        {
            //            sumX = sumX + p[0];
            //            sumY = sumY + p[1];
            //            float buildingX = sumX / numberOfDifferentCoordinates;
            //            float buildingY = sumY / numberOfDifferentCoordinates;
            //            CreateBuilding.Start(buildingX, buildingY, 0, 0, "cathedral_of_cologne");

            //        }
            //    }
            //    //ITD
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class KUPG_A
        public void KUPG_A()
        {
            //var type = "KUPG_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_KUPG_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    if (entity.xkod == "KUPG01")
            //    {
            //        //JAK DOSTAĆ ILOŚĆ ELEMENTÓW?
            //        float sumX = 0;
            //        float sumY = 0;
            //        int numberOfDifferentCoordinates = 1; //ŻLE
            //        foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //        {
            //            sumX = sumX + p[0];
            //            sumY = sumY + p[1];
            //            float buildingX = sumX / numberOfDifferentCoordinates;
            //            float buildingY = sumY / numberOfDifferentCoordinates;
            //            CreateBuilding.Start(buildingX, buildingY, 0, 0, "cathedral_of_cologne");

            //        }
            //    }
            //    //ITD
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class KUPG_P
        public void KUPG_P() //Error
        {
            //var type = "KUPG_P";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_KUPG_P();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //    {

            //    }
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class KUSC_A
        public void KUSC_A()
        {
            //var type = "KUSC_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_KUSC_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    if (entity.xkod == "KUSC01")
            //    {
            //        //JAK DOSTAĆ ILOŚĆ ELEMENTÓW?
            //        float sumX = 0;
            //        float sumY = 0;
            //        int numberOfDifferentCoordinates = 1; //ŻLE
            //        foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //        {
            //            sumX = sumX + p[0];
            //            sumY = sumY + p[1];
            //            float buildingX = sumX / numberOfDifferentCoordinates;
            //            float buildingY = sumY / numberOfDifferentCoordinates;
            //            CreateBuilding.Start(buildingX, buildingY, 0, 0, "cathedral_of_cologne");

            //        }
            //    }
            //    //ITD
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class KUSK_A
        public void KUSK_A()
        {
            //var type = "KUSK_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_KUSK_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    if (entity.xkod == "KUSK01")
            //    {
            //        //JAK DOSTAĆ ILOŚĆ ELEMENTÓW?
            //        float sumX = 0;
            //        float sumY = 0;
            //        int numberOfDifferentCoordinates = 1; //ŻLE
            //        foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //        {
            //            sumX = sumX + p[0];
            //            sumY = sumY + p[1];
            //            float buildingX = sumX / numberOfDifferentCoordinates;
            //            float buildingY = sumY / numberOfDifferentCoordinates;
            //            CreateBuilding.Start(buildingX, buildingY, 0, 0, "cathedral_of_cologne");

            //        }
            //    }
            //    //ITD
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class KUZA_A
        public void KUZA_A() //Error
        {
            //var type = "KUZA_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_KUZA_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    if (entity.xkod == "KUZA01")
            //    {
            //        //JAK DOSTAĆ ILOŚĆ ELEMENTÓW?
            //        float sumX = 0;
            //        float sumY = 0;
            //        int numberOfDifferentCoordinates = 1; //ŻLE
            //        foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //        {
            //            sumX = sumX + p[0];
            //            sumY = sumY + p[1];
            //            float buildingX = sumX / numberOfDifferentCoordinates;
            //            float buildingY = sumY / numberOfDifferentCoordinates;
            //            CreateBuilding.Start(buildingX, buildingY, 0, 0, "cathedral_of_cologne");

            //        }
            //    }
            //    //ITD
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class OIKM_A
        public void OIKM_A() //Incomplete model
        {
            //var type = "OIKM_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_OIKM_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    if (entity.xkod == "OIKM01")
            //    {
            //        //JAK DOSTAĆ ILOŚĆ ELEMENTÓW?
            //        float sumX = 0;
            //        float sumY = 0;
            //        int numberOfDifferentCoordinates = 1; //ŻLE
            //        foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //        {
            //            sumX = sumX + p[0];
            //            sumY = sumY + p[1];
            //            float buildingX = sumX / numberOfDifferentCoordinates;
            //            float buildingY = sumY / numberOfDifferentCoordinates;
            //            CreateBuilding.Start(buildingX, buildingY, 0, 0, "cathedral_of_cologne");

            //        }
            //    }
            //    //ITD
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class OIKM_L
        public void OIKM_L() //Incomplete model
        {
            //var type = "OIKM_L";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_OIKM_L();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //    {

            //    }
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class OIKM_P
        public void OIKM_P()
        {
            var type = "OIKM_P";
            string file = null;
            try
            {
                file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            }
            catch
            {
                CommonHelpers.Log($"Can not find file of type:{type}");
                return;
            }
            CommonHelpers.Log($"found file:{file}");
            var parser = new Parser_OIKM_P();
            parser.InitDocument(file);
            CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            foreach (var entity in parser.GetBDOT10Ks())
            {
                var p = CoordinatesCalculator.GameXY(entity.XYPoint[0], entity.XYPoint[1]);
                if (CoordinatesCalculator.AllInRange(p))
                {
                    if (entity.XKod == "OIKM01")
                    {
                        CreateProp.Start(p[0], p[1], 0, "Billboard_medium_flat_bigbyte_01");
                    }
                    //OIKM02 - Line feature
                    //TEST - simple booth
                    else if (entity.XKod == "OIKM03")
                    {
                        CreateProp.Start(p[0], p[1], 0, "Toll Booth Large");
                    }
                    else if (entity.XKod == "OIKM04")
                    {
                        CreateProp.Start(p[0], p[1], 0, "Bus Stop Large");
                    }
                    else if (entity.XKod == "OIKM05")
                    {
                        CreateBuilding.Start(p[0], p[1], 0, 0, "Train Station");
                    }
                    //OIKM06 not existing
                    else if (entity.XKod == "OIKM07")
                    {
                        CreateProp.Start(p[0], p[1], 0, "Traffic Light European 01");
                    }
                    else if (entity.XKod == "OIKM08")
                    {
                        CreateBuilding.Start(p[0], p[1], 0, 0, "Metro Entrance");
                    }
                    else if (entity.XKod == "OIKM09")
                    {
                        CreateProp.Start(p[0], p[1], 0, "Toll Booth Large");
                    }
                }
            }
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class OIMK_A
        public void OIMK_A()
        {
            //var type = "OIMK_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_OIMK_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    if (entity.xkod == "OIMK01")
            //    {
            //        //JAK DOSTAĆ ILOŚĆ ELEMENTÓW?
            //        float sumX = 0;
            //        float sumY = 0;
            //        int numberOfDifferentCoordinates = 1; //ŻLE
            //        foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //        {
            //            sumX = sumX + p[0];
            //            sumY = sumY + p[1];
            //            float buildingX = sumX / numberOfDifferentCoordinates;
            //            float buildingY = sumY / numberOfDifferentCoordinates;
            //            CreateBuilding.Start(buildingX, buildingY, 0, 0, "cathedral_of_cologne");

            //        }
            //    }
            //    //ITD
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class OIOR_A
        public void OIOR_A()
        {
            //var type = "OIOR_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_OIOR_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    if (entity.xkod == "OIOR01")
            //    {
            //        //JAK DOSTAĆ ILOŚĆ ELEMENTÓW?
            //        float sumX = 0;
            //        float sumY = 0;
            //        int numberOfDifferentCoordinates = 1; //ŻLE
            //        foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //        {
            //            sumX = sumX + p[0];
            //            sumY = sumY + p[1];
            //            float buildingX = sumX / numberOfDifferentCoordinates;
            //            float buildingY = sumY / numberOfDifferentCoordinates;
            //            CreateBuilding.Start(buildingX, buildingY, 0, 0, "cathedral_of_cologne");

            //        }
            //    }
            //    //ITD
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class OIOR_L
        public void OIOR_L()
        {
            //var type = "OIOR_L";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_OIOR_L();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //    {

            //    }
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class OIOR_P
        public void OIOR_P()
        {
            var type = "OIOR_P";
            string file = null;
            try
            {
                file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            }
            catch
            {
                CommonHelpers.Log($"Can not find file of type:{type}");
                return;
            }
            CommonHelpers.Log($"found file:{file}");
            var parser = new Parser_OIOR_P();
            parser.InitDocument(file);
            CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            foreach (var entity in parser.GetBDOT10Ks())
            {
                var p = CoordinatesCalculator.GameXY(entity.XYPoint[0], entity.XYPoint[1]);
                if (CoordinatesCalculator.AllInRange(p))
                {
                    if (entity.XKod == "OIOR01")
                    {
                        CreateProp.Start(p[0], p[1], 0, "Bunker Ruins 01");
                    }
                    else if (entity.XKod == "OIOR02")
                    {
                        CreateProp.Start(p[0], p[1], 0, "Concrete support");
                    }
                    else if (entity.XKod == "OIOR03")
                    {
                        CreateProp.Start(p[0], p[1], 0, "Large Fountain");
                    }
                    //OIOR04 - Line feature
                    else if (entity.XKod == "OIOR05")
                    {
                        CreateProp.Start(p[0], p[1], 0, "Garden_pot");
                        CreateProp.Start(p[0], p[1], 0, "High Vegetation 08");
                    }
                    else if (entity.XKod == "OIOR06") 
                    {
                        CreateProp.Start(p[0], p[1], 0, "rider_statue");
                    }
                    else if (entity.XKod == "OIOR07")
                    {
                        CreateBuilding.Start(p[0], p[1], 0, 0, "2x8 Fishing Pier");
                    }
                    else if (entity.XKod == "OIOR08")
                    {
                        CreateProp.Start(p[0], p[1], 0, "Castle Ruins 01");
                    }
                    else if (entity.XKod == "OIOR09")
                    {
                        CreateProp.Start(p[0], p[1], 0, "Rooftop window 02");
                    }
                    //OIOR10 - not existing 
                    else if (entity.XKod == "OIOR11")
                    {
                        CreateProp.Start(p[0], p[1], 0, "Pavilion");
                    }
                    //OIOR12 - not existing
                    else if (entity.XKod == "OIOR13")
                    {
                        CreateBuilding.Start(p[0], p[1], 0, 0, "RailwayElevatedPillar");
                    }
                    else if (entity.XKod == "OIOR14")
                    {
                        CreateProp.Start(p[0], p[1], 0, "Light Pole Red");
                    }
                }
            }
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class OIPR_L

        public void OIPR_L()
        {
            var type = "OIPR_L";
            //var file = FileFinder.FindFileInFolder(@"C:\Users\DELL\Desktop\MGR\Zuromin\BDOT 10k\mazowieckie_pow_zurominski_1437\PL.PZGiK.330.1437\BDOT10K", type);
            string file = null;
            try
            {
                file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            }
            catch
            {
                CommonHelpers.Log($"Can not find file of type:{type}");
                return;
            }

            CommonHelpers.Log($"found file:{file}");

            //NEW CHANGES

            var parser = new Parser_OIPR_L();
            parser.InitDocument(file);

            CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            foreach (var entity in parser.GetBDOT10Ks())
            {

                //CommonHelpers.Log($"{entity.GeomType}, {entity.XKod}");

                foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
                {
                    //TEST is something more needed?
                    //OIPR05 not needed
                    //OIPR08 TEST - TO DO, Line
                    if (entity.XKod == "OIPR08")
                    {
                        CreateProp.Start(p[0], p[1], 0, "Hedge 02");
                    }
                    //OIPR10 TEST - TO DO, Line
                    if (entity.XKod == "OIPR10")
                    {
                        CreateTree.Start(p[0], p[1], "Conifer");
                    }
                    //CommonHelpers.Log($"-- {{{p.Aggregate(String.Empty, (x, y) => $"{x} {y}")}}}");                   
                    //var p1 = CoordinatesCalculator.GameXY(p);
                }

                //CommonHelpers.Log(String.Empty);
            }
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class OIPR_P
        public void OIPR_P()
        {
            var type = "OIPR_P";
            string file = null;
            try
            {
                file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            }
            catch
            {
                CommonHelpers.Log($"Can not find file of type:{type}");
                return;
            }
            CommonHelpers.Log($"found file:{file}");
            var parser = new Parser_OIPR_P();
            parser.InitDocument(file);
            CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            foreach (var entity in parser.GetBDOT10Ks())
            {
                var p = CoordinatesCalculator.GameXY(entity.XYPoint[0], entity.XYPoint[1]);
                if (CoordinatesCalculator.AllInRange(p))
                {
                    if (entity.XKod == "OIPR01")
                    {
                        CreateTree.Start(p[0], p[1], "Conifer");
                    }
                    else if (entity.XKod == "OIPR02")
                    {
                        CreateProp.Start(p[0], p[1], 0, "Boulder 01");
                    }
                    else if (entity.XKod == "OIPR03")
                    {
                        CreateTree.Start(p[0], p[1], "Bush01");
                    }
                    else if (entity.XKod == "OIPR04")
                    {
                        CreateTree.Start(p[0], p[1], "Tree Sapling 01");
                    }
                    //OIPR05 not needed
                    //OIPR06 TEST - Placeholder
                    else if (entity.XKod == "OIPR06")
                    {
                        CreateTree.Start(p[0], p[1], "Pine01low");
                    }
                    else if (entity.XKod == "OIPR07")
                    {
                        CreateProp.Start(p[0], p[1], 0, "Rock Formation 04 C");
                    }
                    //OIPR08 - Line Feature
                    //OIPR09 TEST - Line Feature?
                    else if (entity.XKod == "OIPR09")
                    {
                        CreateProp.Start(p[0], p[1], 0, "Cliff 12");
                    }
                    //OIPR10 - Line Feature
                    //OIPR11 TEST - Different approach?
                    else if (entity.XKod == "OIPR09")
                    {
                        CreateProp.Start(p[0], p[1], 0, "Cave 01");
                    }
                    //OIPR12 Different approach
                    //OIPR13 TEST - Water Source Missing
                }
            }
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class OISZ_A
        public void OISZ_A()
        {
            //var type = "OISZ_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_OISZ_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    if (entity.xkod == "OISZ01")
            //    {
            //        //JAK DOSTAĆ ILOŚĆ ELEMENTÓW?
            //        float sumX = 0;
            //        float sumY = 0;
            //        int numberOfDifferentCoordinates = 1; //ŻLE
            //        foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //        {
            //            sumX = sumX + p[0];
            //            sumY = sumY + p[1];
            //            float buildingX = sumX / numberOfDifferentCoordinates;
            //            float buildingY = sumY / numberOfDifferentCoordinates;
            //            CreateBuilding.Start(buildingX, buildingY, 0, 0, "cathedral_of_cologne");

            //        }
            //    }
            //    //ITD
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class PTGN_A
        public void PTGN_A()
        {
            //var type = "PTGN_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_PTGN_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    if (entity.xkod == "PTGN01")
            //    {
            //        //JAK DOSTAĆ ILOŚĆ ELEMENTÓW?
            //        float sumX = 0;
            //        float sumY = 0;
            //        int numberOfDifferentCoordinates = 1; //ŻLE
            //        foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //        {
            //            sumX = sumX + p[0];
            //            sumY = sumY + p[1];
            //            float buildingX = sumX / numberOfDifferentCoordinates;
            //            float buildingY = sumY / numberOfDifferentCoordinates;
            //            CreateBuilding.Start(buildingX, buildingY, 0, 0, "cathedral_of_cologne");

            //        }
            //    }
            //    //ITD
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class PTKM_A
        public void PTKM_A()
        {
            //var type = "PTKM_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_PTKM_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    if (entity.xkod == "PTKM01")
            //    {
            //        //JAK DOSTAĆ ILOŚĆ ELEMENTÓW?
            //        float sumX = 0;
            //        float sumY = 0;
            //        int numberOfDifferentCoordinates = 1; //ŻLE
            //        foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //        {
            //            sumX = sumX + p[0];
            //            sumY = sumY + p[1];
            //            float buildingX = sumX / numberOfDifferentCoordinates;
            //            float buildingY = sumY / numberOfDifferentCoordinates;
            //            CreateBuilding.Start(buildingX, buildingY, 0, 0, "cathedral_of_cologne");

            //        }
            //    }
            //    //ITD
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class PTLZ_A
        public void PTLZ_A()
        {
            //var type = "PTLZ_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_PTLZ_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    if (entity.xkod == "PTLZ01")
            //    {
            //        //JAK DOSTAĆ ILOŚĆ ELEMENTÓW?
            //        float sumX = 0;
            //        float sumY = 0;
            //        int numberOfDifferentCoordinates = 1; //ŻLE
            //        foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //        {
            //            sumX = sumX + p[0];
            //            sumY = sumY + p[1];
            //            float buildingX = sumX / numberOfDifferentCoordinates;
            //            float buildingY = sumY / numberOfDifferentCoordinates;
            //            CreateBuilding.Start(buildingX, buildingY, 0, 0, "cathedral_of_cologne");

            //        }
            //    }
            //    //ITD
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class PTNZ_A
        public void PTNZ_A()
        {
            //var type = "PTNZ_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_PTNZ_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    if (entity.xkod == "PTNZ01")
            //    {
            //        //JAK DOSTAĆ ILOŚĆ ELEMENTÓW?
            //        float sumX = 0;
            //        float sumY = 0;
            //        int numberOfDifferentCoordinates = 1; //ŻLE
            //        foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //        {
            //            sumX = sumX + p[0];
            //            sumY = sumY + p[1];
            //            float buildingX = sumX / numberOfDifferentCoordinates;
            //            float buildingY = sumY / numberOfDifferentCoordinates;
            //            CreateBuilding.Start(buildingX, buildingY, 0, 0, "cathedral_of_cologne");

            //        }
            //    }
            //    //ITD
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class PTPL_A
        public void PTPL_A()
        {
            //var type = "PTPL_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_PTPL_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    if (entity.xkod == "PTPL01")
            //    {
            //        //JAK DOSTAĆ ILOŚĆ ELEMENTÓW?
            //        float sumX = 0;
            //        float sumY = 0;
            //        int numberOfDifferentCoordinates = 1; //ŻLE
            //        foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //        {
            //            sumX = sumX + p[0];
            //            sumY = sumY + p[1];
            //            float buildingX = sumX / numberOfDifferentCoordinates;
            //            float buildingY = sumY / numberOfDifferentCoordinates;
            //            CreateBuilding.Start(buildingX, buildingY, 0, 0, "cathedral_of_cologne");

            //        }
            //    }
            //    //ITD
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class PTRK_A
        public void PTRK_A()
        {
            //var type = "PTRK_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_PTRK_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    if (entity.xkod == "PTRK01")
            //    {
            //        //JAK DOSTAĆ ILOŚĆ ELEMENTÓW?
            //        float sumX = 0;
            //        float sumY = 0;
            //        int numberOfDifferentCoordinates = 1; //ŻLE
            //        foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //        {
            //            sumX = sumX + p[0];
            //            sumY = sumY + p[1];
            //            float buildingX = sumX / numberOfDifferentCoordinates;
            //            float buildingY = sumY / numberOfDifferentCoordinates;
            //            CreateBuilding.Start(buildingX, buildingY, 0, 0, "cathedral_of_cologne");

            //        }
            //    }
            //    //ITD
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class PTSO_A
        public void PTSO_A()
        {
            //var type = "PTSO_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_PTSO_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    if (entity.xkod == "PTSO01")
            //    {
            //        //JAK DOSTAĆ ILOŚĆ ELEMENTÓW?
            //        float sumX = 0;
            //        float sumY = 0;
            //        int numberOfDifferentCoordinates = 1; //ŻLE
            //        foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //        {
            //            sumX = sumX + p[0];
            //            sumY = sumY + p[1];
            //            float buildingX = sumX / numberOfDifferentCoordinates;
            //            float buildingY = sumY / numberOfDifferentCoordinates;
            //            CreateBuilding.Start(buildingX, buildingY, 0, 0, "cathedral_of_cologne");

            //        }
            //    }
            //    //ITD
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class PTTR_A
        public void PTTR_A()
        {
            //var type = "PTTR_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_PTTR_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    if (entity.xkod == "PTTR01")
            //    {
            //        //JAK DOSTAĆ ILOŚĆ ELEMENTÓW?
            //        float sumX = 0;
            //        float sumY = 0;
            //        int numberOfDifferentCoordinates = 1; //ŻLE
            //        foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //        {
            //            sumX = sumX + p[0];
            //            sumY = sumY + p[1];
            //            float buildingX = sumX / numberOfDifferentCoordinates;
            //            float buildingY = sumY / numberOfDifferentCoordinates;
            //            CreateBuilding.Start(buildingX, buildingY, 0, 0, "cathedral_of_cologne");

            //        }
            //    }
            //    //ITD
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class PTUT_A
        public void PTUT_A()
        {
            //var type = "PTUT_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_PTUT_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    if (entity.xkod == "PTUT01")
            //    {
            //        //JAK DOSTAĆ ILOŚĆ ELEMENTÓW?
            //        float sumX = 0;
            //        float sumY = 0;
            //        int numberOfDifferentCoordinates = 1; //ŻLE
            //        foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //        {
            //            sumX = sumX + p[0];
            //            sumY = sumY + p[1];
            //            float buildingX = sumX / numberOfDifferentCoordinates;
            //            float buildingY = sumY / numberOfDifferentCoordinates;
            //            CreateBuilding.Start(buildingX, buildingY, 0, 0, "cathedral_of_cologne");

            //        }
            //    }
            //    //ITD
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class PTWP_A
        public void PTWP_A()
        {
            //var type = "PTWP_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_PTWP_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    if (entity.xkod == "PTWP01")
            //    {
            //        //JAK DOSTAĆ ILOŚĆ ELEMENTÓW?
            //        float sumX = 0;
            //        float sumY = 0;
            //        int numberOfDifferentCoordinates = 1; //ŻLE
            //        foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //        {
            //            sumX = sumX + p[0];
            //            sumY = sumY + p[1];
            //            float buildingX = sumX / numberOfDifferentCoordinates;
            //            float buildingY = sumY / numberOfDifferentCoordinates;
            //            CreateBuilding.Start(buildingX, buildingY, 0, 0, "cathedral_of_cologne");

            //        }
            //    }
            //    //ITD
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class PTWZ_A
        public void PTWZ_A()
        {
            //var type = "PTWZ_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_PTWZ_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    if (entity.xkod == "PTWZ01")
            //    {
            //        //JAK DOSTAĆ ILOŚĆ ELEMENTÓW?
            //        float sumX = 0;
            //        float sumY = 0;
            //        int numberOfDifferentCoordinates = 1; //ŻLE
            //        foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //        {
            //            sumX = sumX + p[0];
            //            sumY = sumY + p[1];
            //            float buildingX = sumX / numberOfDifferentCoordinates;
            //            float buildingY = sumY / numberOfDifferentCoordinates;
            //            CreateBuilding.Start(buildingX, buildingY, 0, 0, "cathedral_of_cologne");

            //        }
            //    }
            //    //ITD
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class PTZB_A
        public void PTZB_A()
        {
            //var type = "PTZB_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_PTZB_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    if (entity.xkod == "PTZB01")
            //    {
            //        //JAK DOSTAĆ ILOŚĆ ELEMENTÓW?
            //        float sumX = 0;
            //        float sumY = 0;
            //        int numberOfDifferentCoordinates = 1; //ŻLE
            //        foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //        {
            //            sumX = sumX + p[0];
            //            sumY = sumY + p[1];
            //            float buildingX = sumX / numberOfDifferentCoordinates;
            //            float buildingY = sumY / numberOfDifferentCoordinates;
            //            CreateBuilding.Start(buildingX, buildingY, 0, 0, "cathedral_of_cologne");

            //        }
            //    }
            //    //ITD
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class SKDR_L
        public void SKDR_L()
        {
            //var type = "SKDR_L";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_SKDR_L();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //    {

            //    }
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class SKJZ_L
        public void SKJZ_L()
        {
            //var type = "SKJZ_L";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_SKJZ_L();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //    {

            //    }
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class SKPP_L
        public void SKPP_L() //Incomplete model
        {
            //var type = "SKPP_L";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_SKPP_L();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //    {

            //    }
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class SKRP_L
        public void SKRP_L()
        {
            //var type = "SKRP_L";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_SKRP_L();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //    {

            //    }
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class SKRW_P
        public void SKRW_P() //TEST - not used
        {
            //var type = "SKRW_P";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_SKRW_P();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //    {

            //    }
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class SKTR_L
        public void SKTR_L() //Incomplete model
        {
            //var type = "SKTR_L";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_SKTR_L();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //    {

            //    }
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class SULN_L
        public void SULN_L()
        {
            //var type = "SULN_L";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_SULN_L();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //    {

            //    }
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class SUPR_L
        public void SUPR_L() //Incomplete model
        {
            //var type = "SUPR_L";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_SUPR_L();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //    {

            //    }
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class SWKN_L
        public void SWKN_L() //Incomplete model
        {
            //var type = "SWKN_L";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_SWKN_L();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //    {

            //    }
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class SWRM_L
        public void SWRM_L()
        {
            //var type = "SWRM_L";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_SWRM_L();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //    {

            //    }
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class SWRS_L
        public void SWRS_L()
        {
            //var type = "SWRS_L";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_SWRS_L();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //    {

            //    }
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class TCON_A
        public void TCON_A()
        {
            //var type = "TCON_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_TCON_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    if (entity.xkod == "TCON01")
            //    {
            //        //JAK DOSTAĆ ILOŚĆ ELEMENTÓW?
            //        float sumX = 0;
            //        float sumY = 0;
            //        int numberOfDifferentCoordinates = 1; //ŻLE
            //        foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //        {
            //            sumX = sumX + p[0];
            //            sumY = sumY + p[1];
            //            float buildingX = sumX / numberOfDifferentCoordinates;
            //            float buildingY = sumY / numberOfDifferentCoordinates;
            //            CreateBuilding.Start(buildingX, buildingY, 0, 0, "cathedral_of_cologne");

            //        }
            //    }
            //    //ITD
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class TCPK_A
        public void TCPK_A() //Error
        {
            //var type = "TCPK_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_TCPK_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    if (entity.xkod == "TCPK01")
            //    {
            //        //JAK DOSTAĆ ILOŚĆ ELEMENTÓW?
            //        float sumX = 0;
            //        float sumY = 0;
            //        int numberOfDifferentCoordinates = 1; //ŻLE
            //        foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //        {
            //            sumX = sumX + p[0];
            //            sumY = sumY + p[1];
            //            float buildingX = sumX / numberOfDifferentCoordinates;
            //            float buildingY = sumY / numberOfDifferentCoordinates;
            //            CreateBuilding.Start(buildingX, buildingY, 0, 0, "cathedral_of_cologne");

            //        }
            //    }
            //    //ITD
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class TCPN_A
        public void TCPN_A() //Incomplete model
        {
            //var type = "TCPN_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_TCPN_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    if (entity.xkod == "TCPN01")
            //    {
            //        //JAK DOSTAĆ ILOŚĆ ELEMENTÓW?
            //        float sumX = 0;
            //        float sumY = 0;
            //        int numberOfDifferentCoordinates = 1; //ŻLE
            //        foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //        {
            //            sumX = sumX + p[0];
            //            sumY = sumY + p[1];
            //            float buildingX = sumX / numberOfDifferentCoordinates;
            //            float buildingY = sumY / numberOfDifferentCoordinates;
            //            CreateBuilding.Start(buildingX, buildingY, 0, 0, "cathedral_of_cologne");

            //        }
            //    }
            //    //ITD
            //}
        }

        //TO DO, TO DO, TODO TODO TODO, TODO, TODOOO
        //Converting BDOT10k class TCRZ_A
        public void TCRZ_A() //Error
        {
            //var type = "TCRZ_A";
            //string file = null;
            //try
            //{
            //    file = FileFinder.FindFileInFolder(config.BDOT10k, type);
            //}
            //catch
            //{
            //    CommonHelpers.Log($"Can not find file of type:{type}");
            //    return;
            //}
            //CommonHelpers.Log($"found file:{file}");
            //var parser = new Parser_TCRZ_A();
            //parser.InitDocument(file);
            //CoordinatesCalculator.InitializeCenter(config.ParsedCenterX, config.ParsedCenterY);

            //foreach (var entity in parser.GetBDOT10Ks())
            //{
            //    if (entity.xkod == "TCRZ01")
            //    {
            //        //JAK DOSTAĆ ILOŚĆ ELEMENTÓW?
            //        float sumX = 0;
            //        float sumY = 0;
            //        int numberOfDifferentCoordinates = 1; //ŻLE
            //        foreach (var p in entity.XYLine.Select(CoordinatesCalculator.GameXY).Where(CoordinatesCalculator.AllInRange))
            //        {
            //            sumX = sumX + p[0];
            //            sumY = sumY + p[1];
            //            float buildingX = sumX / numberOfDifferentCoordinates;
            //            float buildingY = sumY / numberOfDifferentCoordinates;
            //            CreateBuilding.Start(buildingX, buildingY, 0, 0, "cathedral_of_cologne");

            //        }
            //    }
            //    //ITD
            //}
        }
    }
}



// Dane do testów - środek posiadanego obszaru, ścieżka do plików, po zestawie współrzędnych dla odpowiedniego typu obiektów.
//folder = @"D:\PW\Praca mgr\Zuromin\BDOT 10k\mazowieckie_pow_zurominski_1437\PL.PZGiK.330.1437\BDOT10K";
//centerX = "560730.55";
//centerY = "576278.95";

//string drzewoXY = "561253.7 576286.4";
//string domekXY = "560688.42 578200.87 560692.45 578198.8 560689.5 578193.95 560685.54 578196.11 560679.12 578199.96 560681.89 578204.54 560688.42 578200.87";
//string frDrogiXY = "561191.18 576356.62 561222.95 576336.38 561234.06 576330.79 561237.43 576329.81 561241.42 576329.48";


//WHAT DO U WANT?
//IN  > @folder, center;  static KOD BDOT x74? (A,L,P),
//OUT > Obj
//UWAGA NA ILOŚĆ OBIEKTOW - OGRANICZENIA
