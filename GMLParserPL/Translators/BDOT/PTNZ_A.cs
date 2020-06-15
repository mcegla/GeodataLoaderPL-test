using GMLParserPL.Configuration;
using System.Collections.Generic;

namespace GMLParserPL.Translators.BDOT
{
    internal class PTNZ_A : AreaToPointsTranslator
    {
        public PTNZ_A(string bdotClass, string filePath, Config config) : base(bdotClass, filePath, config)
        {
            gridDist = 12;
        }

        protected sealed override ObjectTypeEnum GetObjectType(IDictionary<string, object> objectAsDict)
        {
            return ObjectTypeEnum.Prop;
        }

        protected sealed override string GetObjectName(IDictionary<string, object> objectAsDict)
        {
            if (config.PTNZ_A_IIPObj.ContainsKey(objectAsDict["idIIP"].ToString()))
            {
                return config.PTNZ_A_IIPObj[objectAsDict["idIIP"].ToString()];
            }

            if (config.PTNZ_A_Obj.ContainsKey(objectAsDict["x_kod"].ToString()))
            {
                return config.PTNZ_A_Obj[objectAsDict["x_kod"].ToString()];
            }
            return null;
        }

        protected sealed override string GetOther(IDictionary<string, object> objectAsDict)
        {
            return "0";
        }
    }
}