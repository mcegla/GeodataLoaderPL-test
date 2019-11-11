using ColossalFramework;
using ColossalFramework.Math;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeodataLoader.Source.Helpers;
using GeodataLoader.Source.Models;

namespace GeodataLoader.Source.Factories
{
    //========================================================================
    //=== Klasa odpowiedzialna za tworzenie i usuwanie obiektów sieciowych ===
    //------------------------------------------------------------------------
    //====== Class responsible for creating and deleting net structures ======
    //========================================================================

    // tworzenie oparte o: / creating based on:
    //https://github.com/rdiekema/Cities-Skylines-Mapper/tree/master/Mapper

    // usuwanie oparte o: / deleting based on:
    //https://github.com/tomarus/cs-terraingen/blob/master/TerrainGen.cs

    // fragmenty odpowiedzialne za wyrównywanie do segmentów wykonane przez jaggi 
    //--------------------------------------------------------
    // code fragments responsible for segment alignment - jaggi
    public class RoadFactory // podklasa dróg / roads subclass
    {
        private static NetFactoryBase _instance;
        static RoadFactory()
        {
            _instance = new NetFactoryBase();
        }

        public static void Create(float x1, float y1, float x2, float y2, string netType)
            => _instance.Create(x1, y1, x2, y2, netType);

        public static void DeleteAllNodes()
            => _instance.DeleteAllNodes();

        public static HashSet<Segment> Segments
            => _instance.Segments;

        public static List<ushort> SegmentIds
            => _instance.SegmentIds;
    }

    public class GridFactory // podklasa sieci / grid subclass
    {
        private static NetFactoryBase _instance;
        static GridFactory()
        {
            _instance = new NetFactoryBase();
        }

        public static void Create(float x1, float y1, float x2, float y2, string netType)
            => _instance.Create(x1, y1, x2, y2, netType);

        public static void DeleteAllNodes()
            => _instance.DeleteAllNodes();

        public static HashSet<Segment> Segments
            => _instance.Segments;

        public static List<ushort> SegmentIds
            => _instance.SegmentIds;
    }

    public class NetFactory
    {
        private static NetFactoryBase _instance;
        static NetFactory()
        {
            _instance = new NetFactoryBase();
        }

        public static void Create(float x1, float y1, float x2, float y2, string netType)
            => _instance.Create(x1, y1, x2, y2, netType);

        public static void DeleteAllNodes()
            => _instance.DeleteAllNodes();

        public static HashSet<Segment> Segments
            => _instance.Segments;

        public static List<ushort> SegmentIds
            => _instance.SegmentIds;
    }

    public class NetFactoryBase
    {
        public static int tempN = 0; // licznik węzłów / node counter
        public static int tempS = 0; // licznik segmentów / segment counter
        private readonly Dictionary<string, NetInfo> nets = new Dictionary<string, NetInfo>();
        private readonly Dictionary<Vector2, ushort> nodes = new Dictionary<Vector2, ushort>();
        public readonly HashSet<Segment> Segments = new HashSet<Segment>();
        public readonly List<ushort> SegmentIds = new List<ushort>();

        public void Create(float x1, float y1, float x2, float y2, string netType)
        {
            ushort startN;
            ushort endN;

            //NetInfo droga = PrefabCollection<NetInfo>.FindLoaded("Basic Road");
            NetInfo net = null;
            if (!nets.TryGetValue(netType, out net))
            {
                net = PrefabCollection<NetInfo>.FindLoaded(netType);
                if (net == null)
                {
                    Debug.LogError("Prop could not be found");
                    return;
                }
                nets.Add(netType, net);
            }

            //ushort startN;
            var z1 = Singleton<TerrainManager>.instance.SampleRawHeightSmoothWithWater(new Vector3(x1, 0, y1), false, 0f);

            var p1 = new Vector2(x1, y1);
            if (!nodes.ContainsKey(p1))
            {
                NetManager.instance.CreateNode(out startN, ref SimulationManager.instance.m_randomizer, net,
                    new Vector3(x1, z1, y1), Singleton<SimulationManager>.instance.m_currentBuildIndex);
                Singleton<SimulationManager>.instance.m_currentBuildIndex += 1u;
                nodes.Add(p1, startN);
                tempN++;
            }
            else
            {
                startN = nodes[p1];
            }
            //ushort endN;
            var z2 = Singleton<TerrainManager>.instance.SampleRawHeightSmoothWithWater(new Vector3(x2, 0, y2), false, 0f);

            var p2 = new Vector2(x2, y2);
            if (!nodes.ContainsKey(p2))
            {
                NetManager.instance.CreateNode(out endN, ref SimulationManager.instance.m_randomizer, net,
                    new Vector3(x2, z2, y2), Singleton<SimulationManager>.instance.m_currentBuildIndex);
                Singleton<SimulationManager>.instance.m_currentBuildIndex += 1u;
                nodes.Add(p2, endN);
                tempN++;
            }
            else
            {
                endN = nodes[p2];
            }

            Vector3 pos1 = Singleton<NetManager>.instance.m_nodes.m_buffer[startN].m_position;
            Vector3 pos2 = Singleton<NetManager>.instance.m_nodes.m_buffer[endN].m_position;
            Vector3 pos = pos2 - pos1;
            pos = VectorUtils.NormalizeXZ(pos);

            Segments.Add(new Segment { p1 = p1, p2 = p2 });

            ushort segmentID;
            NetManager.instance.CreateSegment(out segmentID, ref SimulationManager.instance.m_randomizer,
                net, startN, endN, pos, -pos, Singleton<SimulationManager>.instance.m_currentBuildIndex,
                Singleton<SimulationManager>.instance.m_currentBuildIndex, false);
            SegmentIds.Add(segmentID);
            Singleton<SimulationManager>.instance.m_currentBuildIndex += 2u; // was 2u, why?
            tempS++;
        }

        // usuwanie / deleting 
        public void DeleteAllNodes() //Is it all that can be done?
        {
            int r = NetManager.NODEGRID_RESOLUTION; // 540
            NetManager nm = NetManager.instance;

            if (nm.m_nodeCount == 0)
                return;

            uint tot = 0;
            for (int i = 0; i < r * r; i++)
            {
                ushort id = nm.m_nodeGrid[i];
                if (id != 0)
                {
                    while (id != 0 && tot++ < NetManager.MAX_MAP_NODES)
                    {
                        ushort next = nm.m_nodes.m_buffer[id].m_nextGridNode;
                        SimulationManager.instance.AddAction(DelNode(id));
                        id = next;
                    };
                }
            }
            int nc = NetManager.instance.m_nodeCount;
            int mnc = NetManager.MAX_NODE_COUNT;
            CommonHelpers.Log($"\nNodes: {nc} / {mnc}");
            //tempN = 0;
        }

        public void DelAllSegmentsNew_ToTest()
        {
            SegmentIds.ForEach(DelSegment);
            SegmentIds.Clear();
        }

        private static IEnumerator DelNode(ushort id)
        {
            NetManager.instance.ReleaseNode(id);
            yield return null;
        }

        private static void DelSegment(ushort id)
        {
            NetManager.instance.ReleaseSegment(id, true);
        }
    }
}
