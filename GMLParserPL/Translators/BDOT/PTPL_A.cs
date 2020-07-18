using GMLParserPL.Configuration;
using GMLParserPL.Logic;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace GMLParserPL.Translators.BDOT
{
    internal class PTPL_A : AreaToPointsTranslator
    {
        public PTPL_A(string bdotClass, string filePath, Config config) : base(bdotClass, filePath, config)
        {
            gridDist = 33.625f; //(17280-32*2)/512
        }

        protected sealed override ObjectTypeEnum GetObjectType(IDictionary<string, object> objectAsDict)
        {
            return ObjectTypeEnum.Resource;
        }

        protected sealed override string GetObjectName(IDictionary<string, object> objectAsDict)
        {
            if (config.PTPL_A_IIPObj.ContainsKey(objectAsDict["idIIP"].ToString()))
            {
                return config.PTPL_A_IIPObj[objectAsDict["idIIP"].ToString()];
            }

            if (config.PTPL_A_Obj.ContainsKey(objectAsDict["x_kod"].ToString()))
            {
                return config.PTPL_A_Obj[objectAsDict["x_kod"].ToString()];
            }
            return null;
        }

        protected override HashSet<Vector2> GetPointsInArea(IDictionary<string, object> objectAsDict)
        {
            if (!objectAsDict.ContainsKey("posList"))
                return null;
            var lineList = (List<string>)objectAsDict["posList"];
            var exteriorV2 = TranslatorUtils.LineToVectorList(lineList[0], CoordinatesCalc.IsInResourceRange);
            if (exteriorV2.Count < 3)
                return null;

            var areaMinMax = Calculations.FindMaxMin(exteriorV2);
            var points = AdditionalPointsCreation.CreatePointArray(areaMinMax[0], areaMinMax[1], gridDist);
            List<List<Vector2>> interiors = new List<List<Vector2>>();
            if (lineList.Count > 1)
            {
                for (int i = 1; i < lineList.Count; i++)
                {
                    interiors.Add(TranslatorUtils.LineToVectorList(lineList[i], CoordinatesCalc.IsInResourceRange));
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
    }
}
