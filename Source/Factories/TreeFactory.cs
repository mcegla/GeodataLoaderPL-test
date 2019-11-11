using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeodataLoader.Source.Helpers;
using ColossalFramework;

namespace GeodataLoader.Source.Factories
{
    //==========================================================
    //=== Klasa odpowiedzialna za tworzenie i usuwanie drzew ===
    //----------------------------------------------------------
    //=== Class responsible for creating and deleting trees ====
    //==========================================================

    // tworzenie i usuwanie bazuje na: / creating and deleting is based on:
    //https://github.com/tomarus/cs-terraingen/blob/master/TerrainGen.cs
    public class TreeFactory
    {
        public static int temp = 0; // licznik drzew / tree counter

        // tworzenie / creating
        public static void Create(float coordX, float coordY, string treeType)
        {
            if (temp < TreeManager.MAX_TREE_COUNT)
            {
                TreeInfo tree = PrefabCollection<TreeInfo>.FindLoaded(treeType); // znajdź drzewo danego typu / find tree of given type
                if (tree == null)
                {
                    Debug.LogError("Tree could not be found"); // co jeśli się nie powiedzie / what when failed
                }
                SimulationManager.instance.AddAction(AddTree(coordX, coordY, tree));  // dodanie akcji - stworzenia drzewa / adding tree creating action
                temp++;
            }
            else
                CommonHelpers.Log($"MAX_TREE_COUNT reached");
        }

        private static HashSet<uint> TreeIds = new HashSet<uint>();

        // IEnumerator odpowiedzialny za dodawanie drzew / IEnumerator responsible for adding new trees
        private static IEnumerator AddTree(float x, float y, TreeInfo tree)
        {
            uint treeNum; // pewien numer dla drzewa / some number for tree
            var z = Singleton<TerrainManager>.instance.SampleRawHeightSmoothWithWater(new Vector3(x, 0, y), false, 0f); // wyliczenie współrzędnej z / calculation of z coordinate

            // wszystkie parametry i metoda potrzebna do stworzenia drzewa / all parameters and method needed for tree creation
            TreeManager.instance.CreateTree(out treeNum, ref SimulationManager.instance.m_randomizer, tree, new Vector3(x, z, y), false);
            TreeIds.Add(treeNum);
            yield return null;
        }

        // usuwanie / deleting
        public static void DeleteAllTrees()
        {
            int tr = TreeManager.TREEGRID_RESOLUTION;
            TreeManager tm = TreeManager.instance;

            if (tm.m_treeCount == 0) // jeżeli ilość drzew jest równa 0, wyjdź / if number of trees equals 0, return
                return;

            uint tot = 0;
            for (int i = 0; i < tr * tr; i++) // dla każdego z punktów siatki / for each of grid points
            {
                uint id = tm.m_treeGrid[i];  // id drzewa to wartość dla punku z siatki / tree id it's value for tree grid
                if (id != 0)
                {
                    // dodawaj akcje usunięcia jeśli id jest różne od 0, a suma drzew nie przekroczyła maksymalnej ilości na mapie
                    //---------------------------------------------------------------------------------------------------------------
                    // add another delete action, as long as id is different from 0 and total trees count is lower then max trees count
                    while (id != 0 && tot++ < TreeManager.MAX_MAP_TREES)
                    {
                        uint next = tm.m_trees.m_buffer[id].m_nextGridTree;
                        SimulationManager.instance.AddAction(DelTree(id));
                        id = next;
                    };
                }
            }
            // sprawdzenie, zwraca ilość drzew do konsoli / check returns number of trees to console
            int tc = TreeManager.instance.m_treeCount;
            int mtc = TreeManager.MAX_TREE_COUNT;
            CommonHelpers.Log($"\nTrees: {tc} / {mtc}");
        }

        // IEnumerator odpowiedzialny za usuwanie drzew po id / IEnumerator responsible for deleting trees by id
        private static IEnumerator DelTree(uint id)
        {
            TreeManager.instance.ReleaseTree(id);
            yield return null;
        }
    }
}