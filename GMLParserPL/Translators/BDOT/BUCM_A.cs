﻿using GMLParserPL.Configuration;
using System.Collections.Generic;

namespace GMLParserPL.Translators.BDOT
{
    internal class BUCM_A : BuildingTranslator
    {
        public BUCM_A(string bdotClass, string filePath, Config config) : base(bdotClass, filePath, config)
        {
        }

        protected sealed override string GetObjectName(IDictionary<string, object> objectAsDict)
        {
            if (config.BUCM_A_IIPObj.ContainsKey(objectAsDict["idIIP"].ToString()))
            {
                return config.BUCM_A_IIPObj[objectAsDict["idIIP"].ToString()];
            }

            if (config.BUCM_A_Obj.ContainsKey(objectAsDict["x_kod"].ToString()))
                return config.BUCM_A_Obj[objectAsDict["x_kod"].ToString()];
            return null;
        }
    }
}
