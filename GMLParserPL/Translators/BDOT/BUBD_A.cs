using GMLParserPL.Configuration;
using GMLParserPL.Parsers;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;

namespace GMLParserPL.Translators.BDOT
{
    class BUBD_A : BuildingTranslator
    {
        public BUBD_A(string bdotClass, string filePath, Config config) : base(bdotClass, filePath, config)
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
            GMLAttrWCoord.Add("funSzczegolowaBudynku");
            GMLAttrWCoord.Add("liczbaKondygnacji");
            GMLParser parser = new GMLParser(file, bdotClass, GMLAttrWCoord);
            return parser.ParseObjects();
        }

        protected sealed override string GetObjectName(IDictionary<string, object> objectAsDict)
        {
            if (config.BUBD_A_IIPObj.ContainsKey(objectAsDict["idIIP"].ToString()))
            {
                return config.BUBD_A_IIPObj[objectAsDict["idIIP"].ToString()];
            }

            int buildingFloors = 0;
            string buildingFunction = "";

            if (objectAsDict.ContainsKey("liczbaKondygnacji") && objectAsDict["liczbaKondygnacji"] != null && objectAsDict["liczbaKondygnacji"].ToString() != "")
            {
                buildingFloors = int.Parse(objectAsDict["liczbaKondygnacji"].ToString(), CultureInfo.InvariantCulture);
            }

            if (objectAsDict.ContainsKey("funSzczegolowaBudynku") && objectAsDict["funSzczegolowaBudynku"] != null && objectAsDict["funSzczegolowaBudynku"].ToString() != "")
            {
                buildingFunction = objectAsDict["funSzczegolowaBudynku"].ToString();
            }

            if (config.BUBD_A_MKDObj.ContainsKey(buildingFunction))
            {
                try
                {
                    return config.BUBD_A_MKDObj.GetByKeyOrClosest(buildingFunction, pArea64, buildingFloors);
                }
                catch (KeyNotFoundException) { }
            }

            if (config.BUBD_A_FunObj.ContainsKey(objectAsDict["funSzczegolowaBudynku"].ToString()))
            {
                return config.BUBD_A_FunObj[objectAsDict["funSzczegolowaBudynku"].ToString()];
            }

            if (config.BUBD_A_Obj.ContainsKey(objectAsDict["x_kod"].ToString()))
            {
                return config.BUBD_A_Obj[objectAsDict["x_kod"].ToString()];
            }
            return null;
        }
    }
}
