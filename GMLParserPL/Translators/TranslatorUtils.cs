using GMLParserPL.Logic;
using GMLParserPL.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;

namespace GMLParserPL.Translators
{
    /// <summary>
    ///     Useful translating methods
    /// </summary>
    internal static class TranslatorUtils
    {
        internal static bool IsNaNVector2(Vector2 point)
        {
            return (point.X != point.X && point.Y != point.Y);
        }

        internal static void WriteAttrToConsole(ObjectTypeEnum objectType, string objectName, string iIP, string other, string gamePosition)
        {
            List<string> translatedAttr = new List<string>();
            translatedAttr.Add(objectType.ToString());
            translatedAttr.Add(objectName);
            translatedAttr.Add(iIP);
            translatedAttr.Add(other);
            translatedAttr.Add(gamePosition);

            if (translatedAttr != null)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var el in translatedAttr)
                    sb.Append($"{el};");
                Console.WriteLine(sb.ToString());
            }
        }

        internal static void WriteAttrToConsole(List<Segment> segmentList, ObjectTypeEnum objectType, string objectName, string iIP, string other)
        {
            List<string> translatedObjects = new List<string>();
            foreach (var segment in segmentList)
            {
                translatedObjects.Add($"{objectType};{objectName};{iIP};{other};{segment};");
            }

            foreach (var obj in translatedObjects)
            {
                if (!string.IsNullOrEmpty(obj))
                {
                    Console.WriteLine(obj);
                }
            }
        }

        internal static List<Vector2> LineToVectorList(string line, Func<Vector2, bool> rangeCheck = null)
        {
            var lineCoord = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => float.Parse(x, CultureInfo.InvariantCulture))
                .Split(2)
                .ToList();
            var lineV2 = lineCoord.Select(point => CoordinatesCalc.GameXY(new Vector2(point[0], point[1])))
                .Where(rangeCheck ?? CoordinatesCalc.IsInRange)
                .ToList();
            return lineV2;
        }

        internal static float AngleToSegment(HashSet<Segment> setOfSegments, Vector2 point)
        {
            var closestSegment = RoadSegmentFinder.FindClosesToPoint(setOfSegments, point);
            var angleToSegment = Calculations.Azimuth(closestSegment.p1, closestSegment.p2);
            //Checks if object is on the right or on the left side of the segment
            var vp = Calculations.VectorProduct(closestSegment.p1, closestSegment.p2, point);
            angleToSegment = vp < 0 ? angleToSegment + (float)Math.PI : angleToSegment;
            return angleToSegment;
        }
    }
}
