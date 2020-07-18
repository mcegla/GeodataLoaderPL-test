using System;
using System.Collections.Generic;
using System.Numerics;

namespace GMLParserPL.Logic
{
    internal static class Calculations
    {
        /// <summary>
        ///     Finding max and min value for vectors set
        /// </summary>
        /// <param name="vectors"></param>
        /// <returns></returns>
        internal static Vector2[] FindMaxMin(IEnumerable<Vector2> vectors)
        {
            var max = new Vector2(float.MinValue, float.MinValue);
            var min = new Vector2(float.MaxValue, float.MaxValue);

            foreach (var v in vectors)
            {
                if (v.X < min.X)
                    min.X = v.X;
                if (v.X > max.X)
                    max.X = v.X;

                if (v.Y < min.Y)
                    min.Y = v.Y;
                if (v.Y > max.Y)
                    max.Y = v.Y;
            }
            return new[] { min, max };
        }

        /// <summary>
        ///     Algorithm checking if point is placed inside of the polygon
        /// </summary>
        /// <param name="vertices"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <see cref="https://wrf.ecse.rpi.edu//Research/Short_Notes/pnpoly.html"/>
        internal static bool PointInPoly(List<Vector2> vertices, Vector2 point)
        {
            int i, j;
            bool c = false;
            for (i = 0, j = vertices.Count - 1; i < vertices.Count; j = i++)
            {
                if (((vertices[i].Y > point.Y) != (vertices[j].Y > point.Y)) &&
                 (point.X < (vertices[j].X - vertices[i].X) * (point.Y - vertices[i].Y) / (vertices[j].Y - vertices[i].Y) + vertices[i].X))
                    c = !c;
            }
            return c;
        }

        /// <summary>
        ///     Algorithm calculating polygon area from coordinates
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        /// <see cref="https://stackoverflow.com/questions/2432428/is-there-any-algorithm-for-calculating-area-of-a-shape-given-co-ordinates-that-d"/>
        internal static float PolygonArea(List<Vector2> polygon)
        {
            int i, j;
            float area = 0;

            for (i = 0; i < polygon.Count; i++)
            {
                j = (i + 1) % polygon.Count;

                area += polygon[i].X * polygon[j].Y;
                area -= polygon[i].Y * polygon[j].X;
            }

            area /= 2;
            return (area < 0 ? -area : area);
        }

        /// <summary>
        ///     Simple center of gravity calculation
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        internal static Vector2 AvgPoint(List<Vector2> polygon)
        {
            float sumx = 0;
            float sumy = 0;
            foreach (var entity in polygon)
            {
                sumx = sumx + entity.X;
                sumy = sumy + entity.Y;
            }
            var x = sumx / polygon.Count;
            var y = sumy / polygon.Count;
            return new Vector2(x, y);
        }

        /// <summary>
        ///     Vector product calculation
        /// </summary>
        /// <param name="segmentP1"></param>
        /// <param name="segmentP2"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        internal static float VectorProduct(Vector2 segmentP1, Vector2 segmentP2, Vector2 point)
        {
            var vp = (segmentP2.X - segmentP1.X) * (point.Y - segmentP1.Y) - (point.X - segmentP1.X) * (segmentP2.Y - segmentP1.Y);
            return vp;
        }

        /// <summary>
        ///     Azimuth calucation
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        internal static float Azimuth(Vector2 point1, Vector2 point2)
        {
            double dX = point2.X - point1.X;
            double dY = point2.Y - point1.Y;
            // azimuth + pi because of game area rotation
            double phi = Math.Atan2(dY, dX) + Math.PI;
            return (float)phi;
        }
    }
}
