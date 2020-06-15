using GMLParserPL.Configuration;
using GMLParserPL.Logic;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Numerics;

namespace GMLParserPL.Translators
{
    internal abstract class BuildingTranslator : Translator
    {
        private Vector2 currentPoint;
        protected int pArea64; //Area using base of 64


        public BuildingTranslator(string bdotClass, string filePath, Config config) : base(bdotClass, filePath, config)
        {
        }


        #region methods
        protected override void Translate(ExpandoObject objectToTranslate)
        {
            var objectAsDict = (IDictionary<string, object>)objectToTranslate;

            string gamePosition = GetPointPosition(objectAsDict);
            if (string.IsNullOrEmpty(gamePosition))
                return;
            ObjectTypeEnum objectType = GetObjectType(objectAsDict);
            string objectName = GetObjectName(objectAsDict);
            if (string.IsNullOrEmpty(objectName))
                return;
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
                    avgPointString = $"{avgPoint.X} {avgPoint.Y}";

                if (!string.IsNullOrEmpty(avgPointString))
                {
                    var pArea = Calculations.PolygonArea(lineV2List[0]);
                    if (lineV2List.Count > 1)
                    {
                        for (int i = 1; i < lineV2List.Count; i++)
                        {
                            pArea -= Calculations.PolygonArea(lineV2List[i]);
                        }
                    }
                    pArea64 = ((int)(pArea / 64)) * 64;
                    currentPoint = avgPoint;
                    return avgPointString;
                }
            }
            return null;
        }

        /// <summary>
        ///     W przypadku budynków chodzi głównie o kąt obrotu do drogi
        ///     <para />
        ///     I case of buildings the most important thing are angles to road segments
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
            return ObjectTypeEnum.Building;
        }
        #endregion
    }
}
