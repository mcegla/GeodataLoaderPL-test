using GMLParserPL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace GMLParserPL.Logic
{
    internal static class RoadSegmentFinder
    {
        /// <summary>
        ///     Odległość do segmentu
        ///     <para />
        ///     Distance to segment
        /// </summary>
        /// <see cref="https://stackoverflow.com/questions/849211/shortest-distance-between-a-point-and-a-line-segment"/>
        static float sqr(float x) { return x * x; }
        static float dist2(Vector2 v, Vector2 w) { return sqr(v.X - w.X) + sqr(v.Y - w.Y); }
        static float distToSegmentSquared(Vector2 p, Vector2 v, Vector2 w)
        {
            var l2 = dist2(v, w);
            if (l2 == 0) return dist2(p, v);
            var t = ((p.X - v.X) * (w.X - v.X) + (p.Y - v.Y) * (w.Y - v.Y)) / l2;
            t = Math.Max(0, Math.Min(1, t));
            return dist2(p, new Vector2(v.X + t * (w.X - v.X),
                    v.Y + t * (w.Y - v.Y)));
        }

        // by jaggi
        public static Segment FindClosesToPoint(IEnumerable<Segment> segments, Vector2 point)
        {
            if (!segments.Any())
                return default(Segment);

            Segment closest = new Segment();
            double distance = double.MaxValue;

            foreach (var s in segments)
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