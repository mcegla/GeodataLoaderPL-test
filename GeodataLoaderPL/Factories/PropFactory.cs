using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeodataLoaderPL.Factories
{
    public class PropFactory
    {
        private readonly Dictionary<string, PropInfo> props;

        public int Temp { get; private set; }

        public PropFactory()
        {
            Temp = 0;
            props = new Dictionary<string, PropInfo>();
        }

        public void Create(Vector2 point, float angle, string propType)
        {
            if (Temp < PropManager.MAX_PROP_COUNT)
            {
                if (!props.ContainsKey(propType))
                {
                    var prop = PrefabCollection<PropInfo>.FindLoaded(propType);
                    if (prop == null)
                    {
                        Debug.LogError($"Prop {propType} could not be found");
                        return;
                    }
                    props.Add(propType, prop);
                }
                SimulationManager.instance.AddAction(AddProp(point, angle, props[propType]));
                Temp++;
            }
            else
                Debug.Log($"MAX_PROP_COUNT reached");
        }

        private static IEnumerator AddProp(Vector2 point, float angle, PropInfo prop)
        {
            ushort propNum;
            PropManager.instance.CreateProp(out propNum, ref SimulationManager.instance.m_randomizer, prop, new Vector3(point.x, 0, point.y), angle, false);
            yield return null;
        }

    }
}
