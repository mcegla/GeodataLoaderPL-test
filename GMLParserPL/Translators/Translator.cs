using GMLParserPL.Configuration;
using GMLParserPL.Parsers;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Numerics;

namespace GMLParserPL.Translators
{
    internal abstract class Translator
    {
        #region fields
        protected string bdotClass;
        protected string filePath;
        protected Config config;
        private List<string> _parsedGMLAttr = new List<string>
        {
            "x_kod",
            "idIIP"
        };
        #endregion


        protected List<string> ParsedGMLAttr { get => _parsedGMLAttr; set => _parsedGMLAttr = value; }


        protected Translator(string bdotClass, string filePath, Config config)
        {
            this.bdotClass = bdotClass;
            this.filePath = filePath;
            this.config = config;
        }


        #region methods
        internal void ParseAndTranslate()
        {
            var parsedObjects = LoadObjects(filePath, bdotClass);
            foreach (var obj in parsedObjects)
            {
                Translate(obj);
            }
        }


        protected virtual List<ExpandoObject> LoadObjects(string file, string bdotClass)
        {
            if (string.IsNullOrEmpty(file))
                throw new FileNotFoundException(file);
            // Zakończenie P,L,A klasy BDOT10k odpowiada jej reprezentacji geometrycznej
            //
            // The ending letter of BDOT10k class corresponds to its geometric representation
            List<string> GMLAttrWCoord = ParsedGMLAttr;
            if (bdotClass.EndsWith("P"))
                GMLAttrWCoord.Add("pos");
            else if (bdotClass.EndsWith("A") || bdotClass.EndsWith("L"))
                GMLAttrWCoord.Add("posList");

            return new GMLParser(file, bdotClass, GMLAttrWCoord).ParseObjects();
        }

        protected virtual void Translate(ExpandoObject objectToTranslate)
        {
            throw new NotImplementedException();
        }


        protected virtual ObjectTypeEnum GetObjectType(IDictionary<string, object> objectAsDict)
        {
            throw new NotImplementedException();
        }

        protected virtual string GetObjectName(IDictionary<string, object> objectAsDict)
        {
            throw new NotImplementedException();
        }

        protected string GetIIP(IDictionary<string, object> objectAsDict)
        {
            string idIIP = objectAsDict["idIIP"].ToString();
            //"PL" ending not needed
            return idIIP.Substring(0, idIIP.IndexOf(".") - 2);
        }

        protected virtual string GetOther(IDictionary<string, object> objectAsDict)
        {
            return "null";
        }
        protected virtual string GetOther(IDictionary<string, object> objectAsDict, Vector2 point)
        {
            return "null";
        }
        #endregion
    }
}
