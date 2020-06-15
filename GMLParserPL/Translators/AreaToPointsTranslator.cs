using GMLParserPL.Configuration;
using GMLParserPL.Logic;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Numerics;


namespace GMLParserPL.Translators
{

    internal abstract class AreaToPointsTranslator : Translator
    {
        protected float gridDist;
        protected AreaToPointsTranslator(string bdotClass, string filePath, Config config) : base(bdotClass, filePath, config)
        {
        }

        protected override void Translate(ExpandoObject objectToTranslate)
        {
            List<string> translatedObjects = new List<string>();
            var objectAsDict = (IDictionary<string, object>)objectToTranslate;

            HashSet<Vector2> pointsInArea = GetPointsInArea(objectAsDict);
            if (pointsInArea != null)
            {
                ObjectTypeEnum objectType = GetObjectType(objectAsDict);
                string objectName = GetObjectName(objectAsDict);
                if (string.IsNullOrEmpty(objectName))
                    return;
                string iIP = GetIIP(objectAsDict);
                string other = GetOther(objectAsDict);
                foreach (var point in pointsInArea)
                {
                    translatedObjects.Add($"{objectType};{objectName};{iIP};{other};{point.X} {point.Y};");
                }

                foreach (var obj in translatedObjects)
                {
                    if (!string.IsNullOrEmpty(obj))
                    {
                        Console.WriteLine(obj);
                    }
                }
            }
        }

        protected virtual HashSet<Vector2> GetPointsInArea(IDictionary<string, object> objectAsDict)
        {
            //implement DP reduction?
            if (objectAsDict.ContainsKey("posList"))
            {
                var lineList = (List<string>)objectAsDict["posList"];
                var exteriorV2 = TranslatorUtils.LineToVectorList(lineList[0]);
                //var exteriorV2 = TranslatorUtils.LineListToVector2Arr(lineList, 0);
                if (exteriorV2.Count < 3)
                    return null;

                var areaMinMax = Calculations.FindMaxMin(exteriorV2);
                var points = AdditionalPointsCreation.CreatePointArray(areaMinMax[0], areaMinMax[1], gridDist);
                List<List<Vector2>> interiors = new List<List<Vector2>>();
                if (lineList.Count > 1)
                {
                    for (int i = 1; i < lineList.Count; i++)
                    {
                        interiors.Add(TranslatorUtils.LineToVectorList(lineList[i]));
                    }
                }

                HashSet<Vector2> selectedPoints = new HashSet<Vector2>();
                foreach (var p in points)
                {
                    if (Calculations.PointInPoly(exteriorV2, p)
                        && (interiors == null || !interiors.Any(interior => Calculations.PointInPoly(interior, p))))
                    {
                        selectedPoints.Add(p);
                    }
                }
                return selectedPoints;
            }
            return null;
        }
    }
}
