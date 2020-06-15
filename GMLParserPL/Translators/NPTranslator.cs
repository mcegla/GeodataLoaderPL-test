using GMLParserPL.Configuration;
using GMLParserPL.Logic;
using GMLParserPL.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace GMLParserPL.Translators
{
    internal abstract class NPTranslator : Translator
    {
        private int toleranceDP; //3 looks good, but might cause troubles with intersections
        protected int propSize = 4; //4 - not all props are the same!!!!
        protected bool isNet;

        public NPTranslator(string bdotClass, string filePath, Config config) : base(bdotClass, filePath, config)
        {
            toleranceDP = TranslatorInitiator.ToleranceDP;
        }

        protected override void Translate(ExpandoObject objectToTranslate)
        {
            var objectAsDict = (IDictionary<string, object>)objectToTranslate;

            List<Segment> segmentList = GetSegmentList(objectAsDict);
            if (segmentList == null)
                return;

            string objectName = GetObjectName(objectAsDict);
            if (string.IsNullOrEmpty(objectName))
                return;
            ObjectTypeEnum objectType = GetObjectType(objectAsDict);
            string iIP = GetIIP(objectAsDict);
            string other;

            if (!isNet)
            {
                List<string> translatedObjects = new List<string>();
                foreach (var segment in segmentList)
                {

                    var pointsList = AdditionalPointsCreation.CreatePointsInLine(segment.p1, segment.p2, propSize);
                    other = Calculations.Azimuth(segment.p1, segment.p2).ToString();

                    foreach (var point in pointsList)
                    {
                        translatedObjects.Add($"{objectType};{objectName};{iIP};{other};{point.X} {point.Y};");
                    }
                }

                foreach (var obj in translatedObjects)
                {
                    if (!string.IsNullOrEmpty(obj))
                    {
                        Console.WriteLine(obj);
                    }
                }
            }
            else
            {
                other = GetOther(objectAsDict);
                TranslatorUtils.WriteAttrToConsole(segmentList, objectType, objectName, iIP, other);
            }
        }

        protected virtual List<Segment> GetSegmentList(IDictionary<string, object> objectAsDict)
        {
            if (!objectAsDict.ContainsKey("posList"))
                return null;
            var lineList = (List<string>)objectAsDict["posList"];
            //extrior (GML name) is the only one in this case
            var exteriorLine = lineList.First();

            if (string.IsNullOrEmpty(exteriorLine))
                return null;

            List<Segment> netSegments = new List<Segment>();
            var exteriorLinePoints = TranslatorUtils.LineToVectorList(exteriorLine);
            var reductedPoints = DouglasPointsReduction.Reduct(exteriorLinePoints, toleranceDP);
            if (reductedPoints.Count <= 1)
                return null;
            for (int i = 0; i < reductedPoints.Count - 1; i++)
            {
                netSegments.Add(new Segment { p1 = reductedPoints[i], p2 = reductedPoints[i + 1] });
            }
            return netSegments;
        }

        protected override ObjectTypeEnum GetObjectType(IDictionary<string, object> objectAsDict)
        {
            return !isNet ? ObjectTypeEnum.Prop : ObjectTypeEnum.Net;
        }
    }
}
