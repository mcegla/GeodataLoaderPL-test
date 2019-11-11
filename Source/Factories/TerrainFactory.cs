using System.Collections;
using ColossalFramework;
using GeodataLoader.Source.Parsers;


namespace GeodataLoader.Source.Factories
{
    //==================================================
    //=== Klasa odpowiedzialna za wczytywanie terenu ===
    //--------------------------------------------------
    //====== Class responsible for terrain loading =====
    //==================================================

    // wczytywanie terenu bazuje na: / terrain loading is based on:
    //https://github.com/tomarus/cs-terraingen/blob/master/TerrainGen.cs
    public class TerrainFactory
    {
        // wygładzanie terenu / flattening the area
        public static void FlattenTerrain()
        {
            byte[] map = new byte[1081 * 1081 * 2];
            for (int i = 0; i < 1081 * 1081 * 2; i += 2)
            {
                map[i] = 0;
                map[i + 1] = 0;
            }
            SimulationManager.instance.AddAction(LoadHeightMap(map));
        }

        // ładowanie NMT / loading DEM
        public static void LoadDEM()
        {
            var config = Configuration<GeodataLoaderConfiguration>.Load();
            var data = ASCII_GRID_Container.Test(config);
            SimulationManager.instance.AddAction(LoadHeightMap(data));
        }

        private static IEnumerator LoadHeightMap(byte[] map)
        {
            Singleton<TerrainManager>.instance.SetRawHeightMap(map);
            yield return null;
        }
    }
}
