using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeodataLoader.Source.Helpers;

namespace GeodataLoader.Source.Factories
{
    //=================================================================
    //=== Klasa odpowiedzialna za tworzenie i usuwanie "rekwizytów" ===
    //-----------------------------------------------------------------
    //======= Class responsible for creating and deleting props =======
    //=================================================================

    // tworzenie i usuwanie bazuje na: / creating and deleting is based on:
    //https://github.com/tomarus/cs-terraingen/blob/master/TerrainGen.cs
    public class PropFactory
    {
        private static Dictionary<string, PropInfo> props = new Dictionary<string, PropInfo>(); // po co?
        public static int temp = 0; // licznik "rekwizytów" / prop counter

        // tworzenie / creating
        public static void Create(float coordX, float coordY, float angle, string propType)
        {
            if (temp < PropManager.MAX_PROP_COUNT)
            {
                if (!props.ContainsKey(propType))
                {
                    var prop = PrefabCollection<PropInfo>.FindLoaded(propType);
                    if (prop == null)
                    {
                        Debug.LogError("Prop could not be found");
                        return;
                    }
                    props.Add(propType, prop);
                }
                SimulationManager.instance.AddAction(AddProp(coordX, coordY, angle, props[propType]));
                temp++;
            }
            else
                CommonHelpers.Log($"MAX_PROP_COUNT reached");
        }

        // IEnumerator odpowiedzialny za dodawanie "rekwizytów" / IEnumerator responsible for adding new props
        private static IEnumerator AddProp(float x, float y, float angle, PropInfo prop)
        {
            ushort propNum; // pewien numer dla "rekwizytu" / some number for prop
            // wszystkie parametry i metoda potrzebna do stworzenia "rekwizytu" / all parameters and method needed for prop creation
            PropManager.instance.CreateProp(out propNum, ref SimulationManager.instance.m_randomizer, prop, new Vector3(x, 0, y), angle, false);
            yield return null;
        }

        // usuwanie / deleting
        public static void DeleteAllProps()
        {
            int pr = PropManager.PROPGRID_RESOLUTION;
            PropManager pm = PropManager.instance;

            if (pm.m_propCount == 0) // jeżeli ilość "rekwizytów" jest równa 0, wyjdź / if number of props equals 0, return
                return;

            uint tot = 0;
            for (int i = 0; i < pr * pr; i++) // dla każdego z punktów siatki / for each of grid points
            {
                ushort id = pm.m_propGrid[i]; // id "rekwizytu" to wartość dla punku z siatki / prop id it's value for props grid
                if (id != 0)
                {
                    // dodawaj akcje usunięcia jeśli id jest różne od 0, a suma "rekwizytów" nie przekroczyła maksymalnej ilości na mapie
                    //---------------------------------------------------------------------------------------------------------------
                    // add another delete action, as long as id is different from 0 and total props count is lower then max props count
                    while (id != 0 && tot++ < PropManager.MAX_MAP_PROPS)
                    {
                        ushort next = pm.m_props.m_buffer[id].m_nextGridProp;
                        SimulationManager.instance.AddAction(DelProp(id));
                        id = next;
                    };
                }
            }
            // sprawdzenie, zwraca ilość "rekwizytów" do konsoli / check returns number of props to console
            int pc = PropManager.instance.m_propCount;
            int mpc = PropManager.MAX_PROP_COUNT;
            CommonHelpers.Log($"\nProps: {pc} / {mpc}");
        }

        // IEnumerator odpowiedzialny za usuwanie "rekwizytów" po id / IEnumerator responsible for deleting props by id
        private static IEnumerator DelProp(ushort id)
        {
            PropManager.instance.ReleaseProp(id);
            yield return null;
        }
    }
}
