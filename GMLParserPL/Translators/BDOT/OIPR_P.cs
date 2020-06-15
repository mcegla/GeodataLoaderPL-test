using GMLParserPL.Configuration;
using System.Collections.Generic;
using System.Numerics;

namespace GMLParserPL.Translators.BDOT
{
    internal class OIPR_P : BPTranslator
    {
        private bool isTree;
        private bool isWaterSource;
        public OIPR_P(string bdotClass, string filePath, Config config) : base(bdotClass, filePath, config)
        {
        }

        protected override ObjectTypeEnum GetObjectType(IDictionary<string, object> objectAsDict)
        {
            return isWaterSource ? ObjectTypeEnum.Water : (isTree ? ObjectTypeEnum.Tree :
                (!isBuilding ? ObjectTypeEnum.Prop : ObjectTypeEnum.Building));
        }

        protected sealed override string GetObjectName(IDictionary<string, object> objectAsDict)
        {
            isBuilding = false;
            isTree = false;
            isWaterSource = false;
            if (config.OIPR_P_IIPFlowInOut.ContainsKey(objectAsDict["idIIP"].ToString()))
            {
                isWaterSource = true;
                return "WaterSource";
            }
            if (config.OIPR_P_ObjFlowInOut.ContainsKey(objectAsDict["x_kod"].ToString()))
            {
                isWaterSource = true;
                return "WaterSource";
            }

            if (config.OIPR_P_IIPObj_Building.ContainsKey(objectAsDict["idIIP"].ToString()))
            {
                isBuilding = true;
                return config.OIPR_P_IIPObj_Building[objectAsDict["idIIP"].ToString()];
            }

            if (config.OIPR_P_Obj_Building.ContainsKey(objectAsDict["x_kod"].ToString()))
            {
                isBuilding = true;
                return config.OIPR_P_Obj_Building[objectAsDict["x_kod"].ToString()];
            }

            if (config.OIPR_P_IIPObj_Prop.ContainsKey(objectAsDict["idIIP"].ToString()))
            {
                return config.OIPR_P_IIPObj_Prop[objectAsDict["idIIP"].ToString()];
            }

            if (config.OIPR_P_Obj_Prop.ContainsKey(objectAsDict["x_kod"].ToString()))
            {
                return config.OIPR_P_Obj_Prop[objectAsDict["x_kod"].ToString()];
            }

            if (config.OIPR_P_IIPObj_Tree.ContainsKey(objectAsDict["idIIP"].ToString()))
            {
                isTree = true;
                return config.OIPR_P_IIPObj_Tree[objectAsDict["idIIP"].ToString()];
            }

            if (config.OIPR_P_Obj_Tree.ContainsKey(objectAsDict["x_kod"].ToString()))
            {
                isTree = true;
                return config.OIPR_P_Obj_Tree[objectAsDict["x_kod"].ToString()];
            }
            return null;
        }

        protected sealed override string GetOther(IDictionary<string, object> objectAsDict, Vector2 point)
        {
            if (isWaterSource)
            {
                var waterParams = config.OIPR_P_IIPFlowInOut[objectAsDict["idIIP"].ToString()];
                if (waterParams == null)
                    waterParams = config.OIPR_P_ObjFlowInOut[objectAsDict["x_kod"].ToString()];
                return $"{waterParams[0]} {waterParams[1]} {waterParams[3]}";
            }
            else
                return base.GetOther(objectAsDict, point);
        }
    }
}
