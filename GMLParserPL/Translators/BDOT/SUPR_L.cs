using GMLParserPL.Configuration;
using System.Collections.Generic;

namespace GMLParserPL.Translators.BDOT
{
    internal class SUPR_L : NetTranslator
    {
        public SUPR_L(string bdotClass, string filePath, Config config) : base(bdotClass, filePath, config)
        {
        }

        protected sealed override string GetObjectName(IDictionary<string, object> objectAsDict)
        {
            if (config.SUPR_L_IIPObj.ContainsKey(objectAsDict["idIIP"].ToString()))
                return config.SUPR_L_IIPObj[objectAsDict["idIIP"].ToString()];

            if (config.SUPR_L_Obj.ContainsKey(objectAsDict["x_kod"].ToString()))
                return config.SUPR_L_Obj[objectAsDict["x_kod"].ToString()];
            return null;
        }
    }
}
