using GMLParserPL.Configuration;
using GMLParserPL.Parsers;
using System.Collections.Generic;
using System.Dynamic;

namespace GMLParserPL.Translators.BDOT
{
    internal class SKRP_L : NetTranslator
    {
        public SKRP_L(string bdotClass, string filePath, Config config) : base(bdotClass, filePath, config)
        {
        }

        protected sealed override List<ExpandoObject> LoadObjects(string file, string bdotClass)
        {
            if (string.IsNullOrEmpty(file))
                return null;
            // Zakończenie P,L,A klasy BDOT10k odpowiada jej reprezentacji geometrycznej
            //
            // The ending letter of BDOT10k class corresponds to its geometric representation
            List<string> GMLAttrWCoord = ParsedGMLAttr;
            if (bdotClass.EndsWith("P"))
                GMLAttrWCoord.Add("pos");
            else if (bdotClass.EndsWith("A") || bdotClass.EndsWith("L"))
                GMLAttrWCoord.Add("posList");
            GMLAttrWCoord.Add("materialNawierzchni");
            GMLAttrWCoord.Add("ruchRowerowy");
            GMLParser parser = new GMLParser(file, bdotClass, GMLAttrWCoord);
            return parser.ParseObjects();
        }

        protected override string GetObjectName(IDictionary<string, object> objectAsDict)
        {

            if (config.SKRP_L_IIPObj.ContainsKey(objectAsDict["idIIP"].ToString()))
                return config.SKRP_L_IIPObj[objectAsDict["idIIP"].ToString()];

            if (config.SKRP_L_BikesPavement.Count > 0)
            {
                string material = "";
                string bikesAlowed = "";

                if (objectAsDict.ContainsKey("ruchRowerowy") && objectAsDict["ruchRowerowy"] != null)
                    bikesAlowed = objectAsDict["ruchRowerowy"].ToString();

                if (objectAsDict.ContainsKey("materialNawierzchni") && objectAsDict["materialNawierzchni"] != null)
                    material = objectAsDict["materialNawierzchni"].ToString();

                try
                {
                    return config.SKRP_L_BikesPavement[bikesAlowed][material];
                }
                catch (KeyNotFoundException)
                {
                }
            }

            if (config.SKRP_L_Obj.ContainsKey(objectAsDict["x_kod"].ToString()))
                return config.SKRP_L_Obj[objectAsDict["x_kod"].ToString()];
            return null;
        }
    }
}
