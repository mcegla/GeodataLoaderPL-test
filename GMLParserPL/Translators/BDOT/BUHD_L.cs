using GMLParserPL.Configuration;
using System.Collections.Generic;



namespace GMLParserPL.Translators.BDOT
{
    internal class BUHD_L : NPTranslator
    {
        public BUHD_L(string bdotClass, string filePath, Config config) : base(bdotClass, filePath, config)
        {
        }

        protected sealed override string GetObjectName(IDictionary<string, object> objectAsDict)
        {
            isNet = false;
            if (config.BUHD_L_IIPObj_Net.ContainsKey(objectAsDict["idIIP"].ToString()))
            {
                isNet = true;
                return config.BUHD_L_IIPObj_Net[objectAsDict["idIIP"].ToString()];
            }
            if (config.BUHD_L_Obj_Net.ContainsKey(objectAsDict["x_kod"].ToString()))
            {
                isNet = true;
                return config.BUHD_L_Obj_Net[objectAsDict["x_kod"].ToString()];
            }

            if (config.BUHD_L_IIPObjSize_Prop.ContainsKey(objectAsDict["idIIP"].ToString()))
            {
                propSize = config.BUHD_L_IIPObjSize_Prop[objectAsDict["idIIP"].ToString()].Item2;
                return config.BUHD_L_IIPObjSize_Prop[objectAsDict["idIIP"].ToString()].Item1;
            }
            if (config.BUHD_L_ObjSize_Prop.ContainsKey(objectAsDict["x_kod"].ToString()))
            {
                propSize = config.BUHD_L_ObjSize_Prop[objectAsDict["x_kod"].ToString()].Item2;
                return config.BUHD_L_ObjSize_Prop[objectAsDict["x_kod"].ToString()].Item1;
            }
            return null;
        }
    }
}
