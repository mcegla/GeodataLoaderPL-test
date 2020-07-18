using ColossalFramework;
using System.Collections;
using UnityEngine;

namespace GeodataLoaderPL.Factories
{
    public class BuildingFactory
    {
        /// <summary>
        ///     Uses in-game methods for buildings creation
        /// </summary>
        public int Temp { get; private set; }

        public BuildingFactory()
        {
            Temp = 0;
        }

        public void Create(Vector2 point, float angle, string BuildingType)
        {
            if (Temp < BuildingManager.MAX_BUILDING_COUNT)
            {
                int length = 0;
                BuildingInfo building = PrefabCollection<BuildingInfo>.FindLoaded(BuildingType);
                if (building == null)
                {
                    Debug.LogError($"Building {BuildingType} could not be found");
                }
                else
                {
                    SimulationManager.instance.AddAction(AddBuilding(point, angle, length, building));
                    Temp++;

                }
            }
            else
                Debug.Log($"MAX_BUILDING_COUNT reached");
        }

        private static IEnumerator AddBuilding(Vector2 point, float angle, int length, BuildingInfo building)
        {
            ushort buildingNum;
            var coordZ = Singleton<TerrainManager>.instance.SampleRawHeightSmoothWithWater(new Vector3(point.x, 0, point.y), false, 0f);
            BuildingManager.instance.CreateBuilding(out buildingNum, ref SimulationManager.instance.m_randomizer,
                building, new Vector3(point.x, coordZ, point.y), angle, length, Singleton<SimulationManager>.instance.m_currentBuildIndex);
            Singleton<SimulationManager>.instance.m_currentBuildIndex += 1u;
            yield return null;
        }
    }
}
