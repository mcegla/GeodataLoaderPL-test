using ColossalFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeodataLoaderPL.Factories
{
    public class TreeFactory
    {
        public int Temp { get; private set; }

        public TreeFactory()
        {
            Temp = 0;
        }
        public void Create(Vector2 point, string treeType)
        {
            if (Temp < TreeManager.MAX_TREE_COUNT)
            {
                TreeInfo tree = PrefabCollection<TreeInfo>.FindLoaded(treeType);
                if (tree == null)
                {
                    Debug.LogError($"Tree {treeType} could not be found");
                }
                SimulationManager.instance.AddAction(AddTree(point, tree));
                Temp++;
            }
            else
                Debug.Log($"MAX_TREE_COUNT reached");
        }

        private static HashSet<uint> TreeIds = new HashSet<uint>();

        private static IEnumerator AddTree(Vector2 point, TreeInfo tree)
        {
            uint treeNum; //HMM
            var z = Singleton<TerrainManager>.instance.SampleRawHeightSmoothWithWater(new Vector3(point.x, 0, point.y), false, 0f);

            TreeManager.instance.CreateTree(out treeNum, ref SimulationManager.instance.m_randomizer, tree, new Vector3(point.x, z, point.y), false);
            TreeIds.Add(treeNum);
            yield return null;
        }

    }
}
