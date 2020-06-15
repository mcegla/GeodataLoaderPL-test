using GMLParserPL.Configuration;
using System.Collections.Generic;

namespace GMLParserPL.Translators.BDOT
{
    internal class OIPR_L : NPTranslator
    {
        private bool isTree;
        public OIPR_L(string bdotClass, string filePath, Config config) : base(bdotClass, filePath, config)
        {
        }

        protected override ObjectTypeEnum GetObjectType(IDictionary<string, object> objectAsDict)
        {
            return isTree ? ObjectTypeEnum.Tree : (!isNet ? ObjectTypeEnum.Prop : ObjectTypeEnum.Net);
        }

        protected sealed override string GetObjectName(IDictionary<string, object> objectAsDict)
        {
            isNet = false;
            isTree = false;
            if (config.OIPR_L_IIPObj_Net.ContainsKey(objectAsDict["idIIP"].ToString()))
            {
                isNet = true;
                return config.OIPR_L_IIPObj_Net[objectAsDict["idIIP"].ToString()];
            }
            if (config.OIPR_L_Obj_Net.ContainsKey(objectAsDict["x_kod"].ToString()))
            {
                isNet = true;
                return config.OIPR_L_Obj_Net[objectAsDict["x_kod"].ToString()];
            }

            if (config.OIPR_L_IIPObjSize_Prop.ContainsKey(objectAsDict["idIIP"].ToString()))
            {
                propSize = config.OIPR_L_IIPObjSize_Prop[objectAsDict["idIIP"].ToString()].Item2;
                return config.OIPR_L_IIPObjSize_Prop[objectAsDict["idIIP"].ToString()].Item1;
            }
            if (config.OIPR_L_ObjSize_Prop.ContainsKey(objectAsDict["x_kod"].ToString()))
            {
                propSize = config.OIPR_L_ObjSize_Prop[objectAsDict["x_kod"].ToString()].Item2;
                return config.OIPR_L_ObjSize_Prop[objectAsDict["x_kod"].ToString()].Item1;
            }

            if (config.OIPR_L_IIPObjSize_Tree.ContainsKey(objectAsDict["idIIP"].ToString()))
            {
                isTree = true;
                propSize = config.OIPR_L_IIPObjSize_Tree[objectAsDict["idIIP"].ToString()].Item2;
                return config.OIPR_L_IIPObjSize_Tree[objectAsDict["idIIP"].ToString()].Item1;
            }
            if (config.OIPR_L_ObjSize_Tree.ContainsKey(objectAsDict["x_kod"].ToString()))
            {
                isTree = true;
                propSize = config.OIPR_L_ObjSize_Tree[objectAsDict["x_kod"].ToString()].Item2;
                return config.OIPR_L_ObjSize_Tree[objectAsDict["x_kod"].ToString()].Item1;
            }
            return null;
        }
    }
}
