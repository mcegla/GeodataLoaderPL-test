using ColossalFramework;
using System.Collections;
using UnityEngine;
using GeodataLoader.Source.Helpers;

namespace GeodataLoader.Source.Factories
{
    //=============================================================
    //=== Klasa odpowiedzialna za tworzenie i usuwanie budynków ===
    //-------------------------------------------------------------
    //=== Class responsible for creating and deleting buildings ===
    //=============================================================

    // tworzenie i usuwanie bazuje na: / creating and deleting is based on:
    //https://github.com/tomarus/cs-terraingen/blob/master/TerrainGen.cs
    public class BuildingFactory
    {
        public static int temp = 0; // licznik budynków / building counter

        // tworzenie / creating
        public static void Create(float coordX, float coordY, float angle, string BuildingType) // budynek wstawiany jest jako punkt i obrócony / building placed as point and then rotated by a given angle
        {
            if (temp < BuildingManager.MAX_BUILDING_COUNT)
            {
                int length = 0; // zmienna niezbędna do stworzenia budynku, ciężko powiedzeić za co jest odpowiedzialna / needed for building creation, don't know why
                BuildingInfo building = PrefabCollection<BuildingInfo>.FindLoaded(BuildingType); // znajdź budynek danego typu / find building of given type
                if (building == null)
                {
                    Debug.LogError($"Building could not be found: {BuildingType}"); // co jeśli się nie powiedzie / what when failed
                }
                else
                {
                    SimulationManager.instance.AddAction(AddBuilding(coordX, coordY, angle, length, building)); // dodanie akcji - stworzenia budynku / adding building creating action
                    temp++;
                }
            }
            else
                CommonHelpers.Log($"MAX_BUILDING_COUNT reached");
        }


        // IEnumerator odpowiedzialny za dodawanie budynków / IEnumerator responsible for adding new buildings
        private static IEnumerator AddBuilding(float coordX, float coordY, float angle, int length, BuildingInfo building)
        {
            ushort buildingNum; // pewien numer dla budynku / some number for building
            var coordZ = Singleton<TerrainManager>.instance.SampleRawHeightSmoothWithWater(new Vector3(coordX, 0, coordY), false, 0f); // wyliczenie współrzędnej z / calculation of z coordinate
            // wszystkie parametry i metoda potrzebna do stworzenia budynku / all parameters and method needed for building creation
            BuildingManager.instance.CreateBuilding(out buildingNum, ref SimulationManager.instance.m_randomizer, building, new Vector3(coordX, coordZ, coordY), angle, length, Singleton<SimulationManager>.instance.m_currentBuildIndex);
            Singleton<SimulationManager>.instance.m_currentBuildIndex += 1u; // zwiększenie indeksu o 1 / increasing building index by 1
            yield return null;
        }


        // usuwanie / deleting
        public static void DeleteAllBuildings()
        {
            int br = BuildingManager.BUILDINGGRID_RESOLUTION;
            BuildingManager bm = BuildingManager.instance;

            if (bm.m_buildingCount == 0) // jeżeli ilość budynków jest równa 0, wyjdź / if number of buildings equals 0, return
                return;

            uint tot = 0;
            for (int i = 0; i < br * br; i++) // dla każdego z punktów siatki / for each of grid points
            {
                ushort id = bm.m_buildingGrid[i]; // id budynku to wartość dla punku z siatki / building id it's value for building grid
                if (id != 0)
                {
                    // dodawaj akcje usunięcia jeśli id jest różne od 0, a suma budynków nie przekroczyła maksymalnej ilości na mapie
                    //---------------------------------------------------------------------------------------------------------------
                    // add another delete action, as long as id is different from 0 and total buildings count is lower then max buildings count
                    while (id != 0 && tot++ < BuildingManager.MAX_MAP_BUILDINGS)
                    {
                        ushort next = bm.m_buildings.m_buffer[id].m_nextGridBuilding;
                        SimulationManager.instance.AddAction(DelBuilding(id));
                        id = next;
                    };
                }
            }
            // sprawdzenie, zwraca ilość budynków do konsoli / check returns number of buildings to console
            int bc = BuildingManager.instance.m_buildingCount;
            int mbc = BuildingManager.MAX_BUILDING_COUNT;
            CommonHelpers.Log($"\nBuildings: {bc} / {mbc}");
            temp = 0;
        }

        // IEnumerator odpowiedzialny za usuwanie budynków po id / IEnumerator responsible for deleting buildings by id
        private static IEnumerator DelBuilding(ushort id)
        {
            BuildingManager.instance.ReleaseBuilding(id);
            yield return null;
        }
    }
}