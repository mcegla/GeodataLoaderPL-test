using GMLParserPL.Configuration;
using System.Collections.Generic;

namespace GMLParserPL.Translators.BDOT
{
    internal class PTWP_A : AreaToPointsTranslator
    {
        public PTWP_A(string bdotClass, string filePath, Config config) : base(bdotClass, filePath, config)
        {
            gridDist = 25;
        }

        protected sealed override ObjectTypeEnum GetObjectType(IDictionary<string, object> objectAsDict)
        {
            return ObjectTypeEnum.Water;
        }

        protected sealed override string GetObjectName(IDictionary<string, object> objectAsDict)
        {
            return "WaterSource";
        }

        protected sealed override string GetOther(IDictionary<string, object> objectAsDict)
        {
            //parameters for waterSource cration
            var waterParams = config.PTWP_A_FlowInOut;
            return $"{waterParams[0]} {waterParams[1]} {waterParams[2]}";
        }
    }
}
