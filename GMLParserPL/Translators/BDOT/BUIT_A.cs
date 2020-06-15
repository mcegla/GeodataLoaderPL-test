using GMLParserPL.Configuration;
using System.Collections.Generic;

namespace GMLParserPL.Translators.BDOT
{
    internal class BUIT_A : BuildingTranslator
    {
        public BUIT_A(string bdotClass, string filePath, Config config) : base(bdotClass, filePath, config)
        {
        }

        protected sealed override string GetObjectName(IDictionary<string, object> objectAsDict)
        {
            if (config.BUIT_A_IIPObj.ContainsKey(objectAsDict["idIIP"].ToString()))
            {
                return config.BUIT_A_IIPObj[objectAsDict["idIIP"].ToString()];
            }

            if (config.BUIT_A_MKDObj.ContainsKey(objectAsDict["x_kod"].ToString()))
            {
                try
                {
                    return config.BUIT_A_MKDObj.GetByKeyOrClosest(objectAsDict["x_kod"].ToString(), pArea64);
                }
                catch (KeyNotFoundException) { }
            }

            if (config.BUIT_A_Obj.ContainsKey(objectAsDict["x_kod"].ToString()))
            {
                return config.BUIT_A_Obj[objectAsDict["x_kod"].ToString()];
            }
            return null;
        }
    }
}
