using ColossalFramework;
using System.Collections.Generic;
using UnityEngine;

namespace GeodataLoader.Source.Factories
{
    //==================================================================
    //==== Klasa odpowiedzialna za tworzenie i usuwanie źródeł wody ====
    //------------------------------------------------------------------
    //=== Class responsible for creating and deleting water sources ====
    //==================================================================

    // tworzenie i usuwanie bazuje na: / creating and deleting is based on:
    //https://github.com/yenyang/rainfall/blob/master/Source/Hydrology.cs
    public class WaterFactory
    {
        private static WaterSimulation _waterSimulation = Singleton<TerrainManager>.instance.WaterSimulation;
        private static List<ushort> _waterSourceIDs = new List<ushort> { };


        // tworzenie / creating
        public WaterSource newWaterSource(float x, float y, uint flow, uint inrate, uint outrate)
        {
            ushort sourceNum;
            WaterSource defaultSource = default(WaterSource);
            var z = Singleton<TerrainManager>.instance.SampleRawHeightSmoothWithWater(new Vector3(x, 0, y), false, 0f);
            var pos = new Vector3(x, z, y);
            // ustawienia dla źródła / settings for source
            defaultSource.m_flow = flow;
            defaultSource.m_inputRate = inrate;
            defaultSource.m_outputRate = outrate;
            defaultSource.m_outputPosition = pos;
            defaultSource.m_inputPosition = pos;
            defaultSource.m_type = 2;
            defaultSource.m_water = 8000000u;
            _waterSimulation.CreateWaterSource(out sourceNum, defaultSource);
            _waterSourceIDs.Add(sourceNum); // dodawanie źródła do listy źródeł / adding source to source list
            return defaultSource;
        }

        // usuwanie / deleting
        public static void DeleteAllSources()
        {
            _waterSourceIDs.ForEach(DelSource);
            _waterSourceIDs.Clear();
        }

        public static void DelSource(ushort id)
        {
            _waterSimulation.ReleaseWaterSource(id);
        }

        public static void setSeaLevelTo0()
        {
            _waterSimulation.m_nextSeaLevel = 0;
        }
    }
}
