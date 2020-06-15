using GMLParserPL.Configuration;
using System.Collections.Generic;

namespace GMLParserPL.Translators.BDOT
{
    internal class BUSP_A : BuildingTranslator
    {
        public BUSP_A(string bdotClass, string filePath, Config config) : base(bdotClass, filePath, config)
        {
        }

        protected sealed override string GetObjectName(IDictionary<string, object> objectAsDict)
        {
            if (config.BUSP_A_IIPObj.ContainsKey(objectAsDict["idIIP"].ToString()))
            {
                return config.BUSP_A_IIPObj[objectAsDict["idIIP"].ToString()];
            }

            if (config.BUSP_A_Obj.ContainsKey(objectAsDict["x_kod"].ToString()))
                return config.BUSP_A_Obj[objectAsDict["x_kod"].ToString()];
            return null;
        }
    }
}
