using GMLParserPL.Configuration;
using System.Collections.Generic;

namespace GMLParserPL.Translators.BDOT
{
    internal class PTRK_A : AreaToPointsTranslator
    {
        public PTRK_A(string bdotClass, string filePath, Config config) : base(bdotClass, filePath, config)
        {
            gridDist = 15f;
        }

        protected sealed override ObjectTypeEnum GetObjectType(IDictionary<string, object> objectAsDict)
        {
            return ObjectTypeEnum.Tree;
        }

        protected sealed override string GetObjectName(IDictionary<string, object> objectAsDict)
        {
            if (config.PTRK_A_IIPObj.ContainsKey(objectAsDict["idIIP"].ToString()))
            {
                return config.PTRK_A_IIPObj[objectAsDict["idIIP"].ToString()];
            }

            if (config.PTRK_A_Obj.ContainsKey(objectAsDict["x_kod"].ToString()))
            {
                return config.PTRK_A_Obj[objectAsDict["x_kod"].ToString()];
            }
            return null;
        }
    }
}
