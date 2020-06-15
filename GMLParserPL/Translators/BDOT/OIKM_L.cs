using GMLParserPL.Configuration;
using System.Collections.Generic;

namespace GMLParserPL.Translators.BDOT
{
    internal class OIKM_L : NPTranslator
    {
        public OIKM_L(string bdotClass, string filePath, Config config) : base(bdotClass, filePath, config)
        {
        }

        protected sealed override string GetObjectName(IDictionary<string, object> objectAsDict)
        {
            isNet = false;
            if (config.OIKM_L_IIPObj_Net.ContainsKey(objectAsDict["idIIP"].ToString()))
            {
                isNet = true;
                return config.OIKM_L_IIPObj_Net[objectAsDict["idIIP"].ToString()];
            }
            if (config.OIKM_L_Obj_Net.ContainsKey(objectAsDict["x_kod"].ToString()))
            {
                isNet = true;
                return config.OIKM_L_Obj_Net[objectAsDict["x_kod"].ToString()];
            }

            if (config.OIKM_L_IIPObjSize_Prop.ContainsKey(objectAsDict["idIIP"].ToString()))
            {
                propSize = config.OIKM_L_IIPObjSize_Prop[objectAsDict["idIIP"].ToString()].Item2;
                return config.OIKM_L_IIPObjSize_Prop[objectAsDict["idIIP"].ToString()].Item1;
            }
            if (config.OIKM_L_ObjSize_Prop.ContainsKey(objectAsDict["x_kod"].ToString()))
            {
                propSize = config.OIKM_L_ObjSize_Prop[objectAsDict["x_kod"].ToString()].Item2;
                return config.OIKM_L_ObjSize_Prop[objectAsDict["x_kod"].ToString()].Item1;
            }
            return null;
        }
    }
}
