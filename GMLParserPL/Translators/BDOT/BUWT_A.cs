using GMLParserPL.Configuration;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace GMLParserPL.Translators.BDOT
{
    internal class BUWT_A : BPTranslator
    {
        private string currentXkod = "";
        public BUWT_A(string bdotClass, string filePath, Config config) : base(bdotClass, filePath, config)
        {
        }

        protected sealed override string GetObjectName(IDictionary<string, object> objectAsDict)
        {
            isBuilding = false;
            currentXkod = objectAsDict["x_kod"].ToString();
            if (config.BUWT_A_IIPObj_Building.ContainsKey(objectAsDict["idIIP"].ToString()))
            {
                isBuilding = true;
                return config.BUWT_A_IIPObj_Building[objectAsDict["idIIP"].ToString()];
            }

            if (config.BUWT_A_Obj_Building.ContainsKey(objectAsDict["x_kod"].ToString()))
            {
                isBuilding = true;
                return config.BUWT_A_Obj_Building[objectAsDict["x_kod"].ToString()];
            }

            if (config.BUWT_A_IIPObj_Prop.ContainsKey(objectAsDict["idIIP"].ToString()))
            {
                return config.BUWT_A_IIPObj_Prop[objectAsDict["idIIP"].ToString()];
            }

            if (config.BUWT_A_Obj_Prop.ContainsKey(objectAsDict["x_kod"].ToString()))
            {
                return config.BUWT_A_Obj_Prop[objectAsDict["x_kod"].ToString()];
            }
            return null;
        }

        protected sealed override string GetOther(IDictionary<string, object> objectAsDict, Vector2 point)
        {
            if (currentXkod == "BUWT06" && TranslatorInitiator.GridSegment != null)
            {
                //Electricity pole, front to electricity line direction
                var angleToSegment = TranslatorUtils.AngleToSegment(TranslatorInitiator.RoadSegment, point) + (float)Math.PI / 2;
                return angleToSegment.ToString();
            }
            else if (TranslatorInitiator.RoadSegment != null)
            {
                var angleToSegment = TranslatorUtils.AngleToSegment(TranslatorInitiator.RoadSegment, point);
                return angleToSegment.ToString();
            }
            return "0";
        }
    }
}
