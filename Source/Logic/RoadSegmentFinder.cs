using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using GeodataLoader.Source.Models;

namespace GeodataLoader.Source.Logic
{
    //===========================================================
    //=== Klasa odpowiedzialna za wyszukiwanie segmentów dróg ===
    //-----------------------------------------------------------
    //======= Class responsible for finding road segments =======
    //===========================================================

    // FindClosesToPoint - jaggi

    public static class RoadSegmentFinder
    {
        // odległość do segmentu intensywnie bazuje na: / segment distance is intensely based on:
        //https://stackoverflow.com/questions/849211/shortest-distance-between-a-point-and-a-line-segment
        static float sqr(float x) { return x * x; }
        static float dist2(Vector2 v, Vector2 w) { return sqr(v.x - w.x) + sqr(v.y - w.y); }
        static float distToSegmentSquared(Vector2 p, Vector2 v, Vector2 w)
        {
            var l2 = dist2(v, w);
            if (l2 == 0) return dist2(p, v);
            var t = ((p.x - v.x) * (w.x - v.x) + (p.y - v.y) * (w.y - v.y)) / l2;
            t = Math.Max(0, Math.Min(1, t));
            return dist2(p, new Vector2(v.x + t * (w.x - v.x),
                    v.y + t * (w.y - v.y)));
        }

        public static Segment FindClosesToPoint(IEnumerable<Segment> segments, Vector2 point)
        {
            if (!segments.Any())
                return default(Segment);

            Segment closest = new Segment();
            double distance = double.MaxValue;

            foreach(var s in segments)
            {
                var d = distToSegmentSquared(point, s.p1, s.p2);

                if (d < distance)
                {
                    distance = d;
                    closest = s;
                }
            }
            return closest;
        }
    }
}