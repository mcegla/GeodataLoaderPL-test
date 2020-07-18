using GMLParserPL.Configuration;
using GMLParserPL.Logic;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Numerics;

namespace GMLParserPL.Translators
{
    /// <summary>
    ///     basic translator for BDOT10k classes composed of point objects
    /// </summary>
    internal abstract class BPTranslator : Translator
    {
        private Vector2 currentPoint;
        protected bool isBuilding;
        public BPTranslator(string bdotClass, string filePath, Config config) : base(bdotClass, filePath, config)
        {
        }

        protected override void Translate(ExpandoObject objectToTranslate)
        {
            var objectAsDict = (IDictionary<string, object>)objectToTranslate;

            string gamePosition = GetPointPosition(objectAsDict);
            if (string.IsNullOrEmpty(gamePosition))
                return;
            string objectName = GetObjectName(objectAsDict);
            if (string.IsNullOrEmpty(objectName))
                return;
            ObjectTypeEnum objectType = GetObjectType(objectAsDict);
            string iIP = GetIIP(objectAsDict);
            string other = GetOther(objectAsDict, currentPoint);

            TranslatorUtils.WriteAttrToConsole(objectType, objectName, iIP, other, gamePosition);
        }

        protected virtual string GetPointPosition(IDictionary<string, object> objectAsDict)
        {
            if (objectAsDict.ContainsKey("pos"))
            {
                string objPoint = objectAsDict["pos"].ToString();
                var point = objPoint.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(num => float.Parse(num, System.Globalization.CultureInfo.InvariantCulture))
                    .ToArray();
                currentPoint = CoordinatesCalc.GameXY(new Vector2(point[0], point[1]));
                if (CoordinatesCalc.IsInRange(currentPoint))
                {
                    string pointStr = "";
                    pointStr += $"{currentPoint.X} {currentPoint.Y} ";
                    return pointStr;
                }
                return null;
            }

            else if (objectAsDict.ContainsKey("posList"))
            {
                var lineList = (List<string>)objectAsDict["posList"];
                List<List<Vector2>> lineV2List = new List<List<Vector2>>();
                for (int i = 0; i < lineList.Count; i++)
                {
                    lineV2List.Add(TranslatorUtils.LineToVectorList(lineList[i]));
                }
                var avgPoint = Calculations.AvgPoint(lineV2List[0]);
                string avgPointString = "";
                if (!TranslatorUtils.IsNaNVector2(avgPoint))
                {
                    currentPoint = avgPoint;
                    avgPointString = $"{avgPoint.X} {avgPoint.Y}";
                }

                if (!string.IsNullOrEmpty(avgPointString))
                {
                    return avgPointString;
                }
            }
            return null;
        }

        /// <summary>
        ///     In case of buildings and props the most important thing are angles to road segments
        /// </summary>
        /// <param name="objectAsDict"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        protected override string GetOther(IDictionary<string, object> objectAsDict, Vector2 point)
        {
            if (TranslatorInitiator.RoadSegment == null)
                return "0";
            var angleToSegment = TranslatorUtils.AngleToSegment(TranslatorInitiator.RoadSegment, point);
            return angleToSegment.ToString();
        }

        protected override ObjectTypeEnum GetObjectType(IDictionary<string, object> objectAsDict)
        {
            return !isBuilding ? ObjectTypeEnum.Prop : ObjectTypeEnum.Building;
        }
    }
}