using GMLParserPL.Configuration;
using GMLParserPL.Logic;
using GMLParserPL.Models;
using System.Collections.Generic;
using System.Linq;

namespace GMLParserPL.Translators.BDOT
{
    internal class SULN_L : NetTranslator
    {
        private int toleranceDP; // 3 looks good, but might cause troubles with intersections

        public SULN_L(string bdotClass, string filePath, Config config) : base(bdotClass, filePath, config)
        {
            toleranceDP = TranslatorInitiator.ToleranceDP;
        }

        /// <summary>
        ///     Nearly identical to base, override needed because other classes use road segments
        /// </summary>
        /// <param name="objectAsDict"></param>
        /// <returns></returns>
        protected sealed override List<Segment> GetSegmentList(IDictionary<string, object> objectAsDict)
        {
            if (!objectAsDict.ContainsKey("posList"))
                return null;            
            var lineList = (List<string>)objectAsDict["posList"];
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
                Segment newSegment = new Segment { p1 = reductedPoints[i], p2 = reductedPoints[i + 1] };
                netSegments.Add(newSegment);
                TranslatorInitiator.GridSegment.Add(newSegment);
            }
            return netSegments; 
        }

        protected sealed override string GetObjectName(IDictionary<string, object> objectAsDict)
        {
            if (config.SULN_L_IIPObj.ContainsKey(objectAsDict["idIIP"].ToString()))
            {
                return config.SULN_L_IIPObj[objectAsDict["idIIP"].ToString()];
            }

            if (config.SULN_L_Obj.ContainsKey(objectAsDict["x_kod"].ToString()))
                return config.SULN_L_Obj[objectAsDict["x_kod"].ToString()];
            return null;
        }
    }
}
