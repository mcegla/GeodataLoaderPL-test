using ColossalFramework;
using System.Collections.Generic;
using UnityEngine;

namespace GeodataLoaderPL.Factories
{
    public class WaterFactory
    {
        private static WaterSimulation _waterSimulation = Singleton<TerrainManager>.instance.WaterSimulation;
        private static List<ushort> _waterSourceIDs = new List<ushort> { };
        public int Temp { get; private set; }

        public WaterFactory()
        {
            Temp = 0;
        }

        public WaterSource NewWaterSource(Vector2 point, uint flow, uint inrate, uint outrate)
        {
            ushort sourceNum;
            WaterSource defaultSource = default(WaterSource);
            var z = Singleton<TerrainManager>.instance.SampleRawHeightSmoothWithWater(new Vector3(point.x, 0, point.y), false, 0f);
            var pos = new Vector3(point.x, z, point.y);
            // ustawienia dla źródła / settings for source
            defaultSource.m_flow = flow;
            defaultSource.m_inputRate = inrate;
            defaultSource.m_outputRate = outrate;
            defaultSource.m_outputPosition = pos;
            defaultSource.m_inputPosition = pos;
            defaultSource.m_type = 2;
            defaultSource.m_water = 8000000u;
            _waterSimulation.CreateWaterSource(out sourceNum, defaultSource);
            _waterSourceIDs.Add(sourceNum);
            Temp++;
            return defaultSource;
        }

        public void SetSeaLevelTo0()
        {
            _waterSimulation.m_nextSeaLevel = 0;
        }
    }
}
