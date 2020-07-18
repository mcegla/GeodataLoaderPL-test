using GMLParserPL.Configuration;
using GMLParserPL.Logic;
using GMLParserPL.Models;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace GMLParserPL.Translators
{
    /// <summary>
    ///     basic translator for BDOT10k classes composed of linear (net) objects
    /// </summary>
    internal abstract class NetTranslator : Translator
    {
        private int toleranceDP; // 3 looks good, but might cause troubles with intersections
        public NetTranslator(string bdotClass, string filePath, Config config) : base(bdotClass, filePath, config)
        {
            toleranceDP = TranslatorInitiator.ToleranceDP;
        }


        #region methods
        protected override void Translate(ExpandoObject objectToTranslate)
        {
            var objectAsDict = (IDictionary<string, object>)objectToTranslate;

            List<Segment> segmentList = GetSegmentList(objectAsDict);
            if (segmentList != null)
            {
                ObjectTypeEnum objectType = GetObjectType(objectAsDict);
                string objectName = GetObjectName(objectAsDict);
                if (string.IsNullOrEmpty(objectName))
                    return;
                string iIP = GetIIP(objectAsDict);
                string other = GetOther(objectAsDict);
                TranslatorUtils.WriteAttrToConsole(segmentList, objectType, objectName, iIP, other);
            }
        }

        protected virtual List<Segment> GetSegmentList(IDictionary<string, object> objectAsDict)
        {
            if (!objectAsDict.ContainsKey("posList"))
                return null;
            var lineList = (List<string>)objectAsDict["posList"];
            // extrior (GML name) is the only one in this case
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
            return ObjectTypeEnum.Net;
        }
        #endregion
    }
}
