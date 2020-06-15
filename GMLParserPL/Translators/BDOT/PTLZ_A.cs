using GMLParserPL.Configuration;
using GMLParserPL.Parsers;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Numerics;

namespace GMLParserPL.Translators.BDOT
{
    internal class PTLZ_A : AreaToPointsTranslator
    {
        public PTLZ_A(string bdotClass, string filePath, Config config) : base(bdotClass, filePath, config)
        {
            gridDist = 20f;
        }

        protected sealed override ObjectTypeEnum GetObjectType(IDictionary<string, object> objectAsDict)
        {
            return ObjectTypeEnum.Tree;
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
            GMLAttrWCoord.Add("gatunekDrzew");

            GMLParser parser = new GMLParser(file, bdotClass, GMLAttrWCoord);
            return parser.ParseObjects();
        }

        protected override void Translate(ExpandoObject objectToTranslate)
        {
            List<string> translatedObjects = new List<string>();
            var objectAsDict = (IDictionary<string, object>)objectToTranslate;

            if (config.PTLZ_A_GridSize.ContainsKey(objectAsDict["x_kod"].ToString()))
                gridDist = config.PTLZ_A_GridSize[objectAsDict["x_kod"].ToString()];

            HashSet<Vector2> pointsInArea = GetPointsInArea(objectAsDict);
            if (pointsInArea == null)
                return;

            ObjectTypeEnum objectType = GetObjectType(objectAsDict);
            string objectName = GetObjectName(objectAsDict);
            if (string.IsNullOrEmpty(objectName))
                return;
            string iIP = GetIIP(objectAsDict);
            string other = GetOther(objectAsDict);
            foreach (var point in pointsInArea)
            {
                translatedObjects.Add($"{objectType};{objectName};{iIP};{other};{point.X} {point.Y};");
            }

            foreach (var obj in translatedObjects)
            {
                if (!string.IsNullOrEmpty(obj))
                {
                    Console.WriteLine(obj);
                }
            }           
        }

        protected sealed override string GetObjectName(IDictionary<string, object> objectAsDict)
        {
            if (config.PTLZ_A_IIPObj.ContainsKey(objectAsDict["idIIP"].ToString()))
            {
                return config.PTLZ_A_IIPObj[objectAsDict["idIIP"].ToString()];
            }

            if (config.PTLZ_A_SpeciesObj.ContainsKey(objectAsDict["gatunekDrzew"].ToString()))
            {
                return config.PTLZ_A_SpeciesObj[objectAsDict["gatunekDrzew"].ToString()];
            }

            if (config.PTLZ_A_Obj.ContainsKey(objectAsDict["x_kod"].ToString()))
            {
                return config.PTLZ_A_Obj[objectAsDict["x_kod"].ToString()];
            }
            return null;
        }
    }
}
