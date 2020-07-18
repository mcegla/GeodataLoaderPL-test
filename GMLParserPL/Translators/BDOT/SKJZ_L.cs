using GMLParserPL.Configuration;
using GMLParserPL.Logic;
using GMLParserPL.Models;
using GMLParserPL.Parsers;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;


namespace GMLParserPL.Translators.BDOT
{
    internal class SKJZ_L : NetTranslator
    {
        private int toleranceDP;
        public SKJZ_L(string bdotClass, string filePath, Config config) : base(bdotClass, filePath, config)
        {
            toleranceDP = TranslatorInitiator.ToleranceDP;
        }

        /// <summary>
        ///     Nearly identical to base Net, override needed because other classes use road segments
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
                TranslatorInitiator.RoadSegment.Add(newSegment);
            }
            return netSegments;
        }

        protected sealed override List<ExpandoObject> LoadObjects(string file, string bdotClass)
        {
            if (string.IsNullOrEmpty(file))
                return null;
            // The ending letter of BDOT10k class corresponds to its geometric representation
            List<string> GMLAttrWCoord = ParsedGMLAttr;
            if (bdotClass.EndsWith("P"))
                GMLAttrWCoord.Add("pos");
            else if (bdotClass.EndsWith("A") || bdotClass.EndsWith("L"))
                GMLAttrWCoord.Add("posList");
            GMLAttrWCoord.Add("materialNawierzchni");
            GMLAttrWCoord.Add("liczbaPasow");
            GMLParser parser = new GMLParser(file, bdotClass, GMLAttrWCoord);
            return parser.ParseObjects();
        }

        protected override string GetObjectName(IDictionary<string, object> objectAsDict)
        {

            if (config.SKJZ_L_IIPObj.ContainsKey(objectAsDict["idIIP"].ToString()))
            {
                return config.SKJZ_L_IIPObj[objectAsDict["idIIP"].ToString()];
            }

            if (config.SKJZ_L_MKDObj.Count > 0)
            {
                int numberOfLines = 0;
                string material = "";

                if (objectAsDict.ContainsKey("liczbaPasow") && objectAsDict["liczbaPasow"] != null && objectAsDict["liczbaPasow"].ToString() != "")
                {
                    numberOfLines = int.Parse(objectAsDict["liczbaPasow"].ToString(), CultureInfo.InvariantCulture);
                }


                if (objectAsDict.ContainsKey("materialNawierzchni") && objectAsDict["materialNawierzchni"] != null)
                    material = objectAsDict["materialNawierzchni"].ToString();

                try
                {
                    return config.SKJZ_L_MKDObj[numberOfLines, material];
                }
                catch (KeyNotFoundException)
                {
                }
            }

            if (config.SKJZ_L_Obj.ContainsKey(objectAsDict["x_kod"].ToString()))
            {
                return config.SKJZ_L_Obj[objectAsDict["x_kod"].ToString()];
            }
            return null;
        }
    }
}
