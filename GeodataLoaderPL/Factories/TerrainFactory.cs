using ColossalFramework;
using System.Collections;

namespace GeodataLoaderPL.Factories
{
    public class TerrainFactory
    {
        public void FlattenTerrain()
        {
            byte[] map = new byte[1081 * 1081 * 2];
            for (int i = 0; i < 1081 * 1081 * 2; i += 2)
            {
                map[i] = 0;
                map[i + 1] = 0;
            }
            SimulationManager.instance.AddAction(SetHeightMap(map));
        }

        public void SetTerrain(byte[] map)
        {
            SimulationManager.instance.AddAction(SetHeightMap(map));
        }

        private static IEnumerator SetHeightMap(byte[] map)
        {
            Singleton<TerrainManager>.instance.SetRawHeightMap(map);
            yield return null;
        }
    }
}
