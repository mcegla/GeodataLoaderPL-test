using GMLParserPL.Configuration;
using System.Collections.Generic;

namespace GMLParserPL.Translators.BDOT
{
    internal class BUUO_L : NPTranslator
    {
        public BUUO_L(string bdotClass, string filePath, Config config) : base(bdotClass, filePath, config)
        {
        }

        protected sealed override string GetObjectName(IDictionary<string, object> objectAsDict)
        {
            isNet = false;
            if (config.BUUO_L_IIPObj_Net.ContainsKey(objectAsDict["idIIP"].ToString()))
            {
                isNet = true;
                return config.BUUO_L_IIPObj_Net[objectAsDict["idIIP"].ToString()];
            }
            if (config.BUUO_L_Obj_Net.ContainsKey(objectAsDict["x_kod"].ToString()))
            {
                isNet = true;
                return config.BUUO_L_Obj_Net[objectAsDict["x_kod"].ToString()];
            }

            if (config.BUUO_L_IIPObjSize_Prop.ContainsKey(objectAsDict["idIIP"].ToString()))
            {
                propSize = config.BUUO_L_IIPObjSize_Prop[objectAsDict["idIIP"].ToString()].Item2;
                return config.BUUO_L_IIPObjSize_Prop[objectAsDict["idIIP"].ToString()].Item1;
            }
            if (config.BUUO_L_ObjSize_Prop.ContainsKey(objectAsDict["x_kod"].ToString()))
            {
                propSize = config.BUUO_L_ObjSize_Prop[objectAsDict["x_kod"].ToString()].Item2;
                return config.BUUO_L_ObjSize_Prop[objectAsDict["x_kod"].ToString()].Item1;
            }
            return null;
        }
    }
}
