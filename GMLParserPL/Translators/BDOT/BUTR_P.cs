using GMLParserPL.Configuration;
using System.Collections.Generic;

namespace GMLParserPL.Translators.BDOT
{
    internal class BUTR_P : BPTranslator
    {
        public BUTR_P(string bdotClass, string filePath, Config config) : base(bdotClass, filePath, config)
        {
        }

        protected sealed override string GetObjectName(IDictionary<string, object> objectAsDict)
        {
            isBuilding = false;
            if (config.BUTR_P_IIPObj_Building.ContainsKey(objectAsDict["idIIP"].ToString()))
            {
                isBuilding = true;
                return config.BUTR_P_IIPObj_Building[objectAsDict["idIIP"].ToString()];
            }

            if (config.BUTR_P_Obj_Building.ContainsKey(objectAsDict["x_kod"].ToString()))
            {
                isBuilding = true;
                return config.BUTR_P_Obj_Building[objectAsDict["x_kod"].ToString()];
            }

            if (config.BUTR_P_IIPObj_Prop.ContainsKey(objectAsDict["idIIP"].ToString()))
            {
                return config.BUTR_P_IIPObj_Prop[objectAsDict["idIIP"].ToString()];
            }

            if (config.BUTR_P_Obj_Prop.ContainsKey(objectAsDict["x_kod"].ToString()))
            {
                return config.BUTR_P_Obj_Prop[objectAsDict["x_kod"].ToString()];
            }
            return null;
        }
    }
}
