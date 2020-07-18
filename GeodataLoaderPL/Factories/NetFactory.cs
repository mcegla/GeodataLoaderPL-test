using ColossalFramework;
using ColossalFramework.Math;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GeodataLoaderPL.Factories
{
    /// <summary>
    ///     Uses in-game methods for net-type objects creation, includes skipping of repeated nodes in close distance
    /// </summary>
    public class NetFactory
    {
        private readonly Dictionary<string, NetInfo> nets;
        private readonly Dictionary<Vector2, ushort> nodes;
        private readonly List<ushort> segmentIds = new List<ushort>();

        public int TempN { get; private set; }
        public int TempS { get; private set; }

        public NetFactory()
        {
            TempN = 0;
            TempS = 0;
            nets = new Dictionary<string, NetInfo>();
            nodes = new Dictionary<Vector2, ushort>();
            segmentIds = new List<ushort>();
        }

        public void Create(Vector2 point1, Vector2 point2, string netType)
        {
            float minNodeDist = 0.2f;
            ushort startN;
            ushort endN;

            NetInfo net = null;
            if (!nets.TryGetValue(netType, out net))
            {
                net = PrefabCollection<NetInfo>.FindLoaded(netType);
                if (net == null)
                {
                    Debug.LogError($"Net {netType} could not be found");
                    return;
                }
                nets.Add(netType, net);
            }

            var z1 = Singleton<TerrainManager>.instance.SampleRawHeightSmoothWithWater(new Vector3(point1.x, 0, point1.y), false, 0f);

            if (!nodes.ContainsKey(point1))
            {
                var closestStartNode = CheckNodeDistance(point1, minNodeDist);
                if (closestStartNode == Vector2.zero)
                {
                    NetManager.instance.CreateNode(out startN, ref SimulationManager.instance.m_randomizer, net,
                        new Vector3(point1.x, z1, point1.y), Singleton<SimulationManager>.instance.m_currentBuildIndex);
                    Singleton<SimulationManager>.instance.m_currentBuildIndex += 1u;
                    nodes.Add(point1, startN);
                    TempN++;
                }
                else
                {
                    startN = nodes[closestStartNode];
                }
            }
            else
            {
                startN = nodes[point1];
            }

            var z2 = Singleton<TerrainManager>.instance.SampleRawHeightSmoothWithWater(new Vector3(point2.x, 0, point2.y), false, 0f);
            if (!nodes.ContainsKey(point2))
            {
                var closestEndNode = CheckNodeDistance(point2, minNodeDist);
                if (closestEndNode == Vector2.zero)
                {
                    NetManager.instance.CreateNode(out endN, ref SimulationManager.instance.m_randomizer, net,
                        new Vector3(point2.x, z2, point2.y), Singleton<SimulationManager>.instance.m_currentBuildIndex);
                    Singleton<SimulationManager>.instance.m_currentBuildIndex += 1u;
                    nodes.Add(point2, endN);
                    TempN++;
                }
                else
                {
                    endN = nodes[closestEndNode];
                }
            }
            else
            {
                endN = nodes[point2];
            }

            Vector3 pos1 = Singleton<NetManager>.instance.m_nodes.m_buffer[startN].m_position;
            Vector3 pos2 = Singleton<NetManager>.instance.m_nodes.m_buffer[endN].m_position;
            Vector3 pos = pos2 - pos1;
            pos = VectorUtils.NormalizeXZ(pos);

            ushort segmentID;
            NetManager.instance.CreateSegment(out segmentID, ref SimulationManager.instance.m_randomizer,
                net, startN, endN, pos, -pos, Singleton<SimulationManager>.instance.m_currentBuildIndex,
                Singleton<SimulationManager>.instance.m_currentBuildIndex, false);
            segmentIds.Add(segmentID);
            Singleton<SimulationManager>.instance.m_currentBuildIndex += 2u;
            TempS++;
        }

        //Thanks to guys from GeoSkylines mod for their source code
        public void UpdateSegments()
        {
            for (ushort i = 0; i < NetManager.instance.m_segments.m_buffer.Length; i++)
            {

                var seg = NetManager.instance.m_segments.m_buffer[i];
                if (seg.m_startNode == 0 || seg.m_endNode == 0)
                    continue;
                NetManager.instance.UpdateSegment(i);
            }
        }

        private Vector2 CheckNodeDistance(Vector2 point2Check, float dist)
        {
            Vector2 closestPoint = Vector2.zero;
            var cpXDist = float.MaxValue;
            var cpYDist = float.MaxValue;
            foreach (var valuePair in nodes)
            {
                var xDist = Math.Abs(valuePair.Key.x - point2Check.x);
                var yDist = Math.Abs(valuePair.Key.y - point2Check.y);
                if (xDist < dist && yDist < dist)
                {
                    if (xDist < cpXDist && yDist < cpYDist)
                    {
                        cpXDist = xDist;
                        cpXDist = yDist;
                        closestPoint = valuePair.Key;
                    }
                }
            }
            return closestPoint;
        }
    }
}
