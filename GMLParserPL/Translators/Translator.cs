using GMLParserPL.Configuration;
using GMLParserPL.Parsers;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Numerics;

namespace GMLParserPL.Translators
{
    /// <summary>
    ///     abstract class contains typical translator fields and necessary methods
    /// </summary>
    internal abstract class Translator
    {
        protected string bdotClass;
        protected string filePath;
        protected Config config;
        private List<string> _parsedGMLAttr = new List<string>
        {
            "x_kod",
            "idIIP",
        };


        protected List<string> ParsedGMLAttr { get => _parsedGMLAttr; set => _parsedGMLAttr = value; }


        protected Translator(string bdotClass, string filePath, Config config)
        {
            this.bdotClass = bdotClass;
            this.filePath = filePath;
            this.config = config;
        }


        #region methods
        /// <summary>
        ///     calls parser, loads objects and than translates them
        /// </summary>
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

        /// <summary>
        ///     selects type from enum
        /// </summary>
        /// <param name="objectAsDict"></param>
        /// <returns></returns>
        protected virtual ObjectTypeEnum GetObjectType(IDictionary<string, object> objectAsDict)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     selects objects name which is later returned to console for further use
        /// </summary>
        /// <param name="objectAsDict"></param>
        /// <returns></returns>
        protected virtual string GetObjectName(IDictionary<string, object> objectAsDict)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     returns objects id
        /// </summary>
        /// <param name="objectAsDict"></param>
        /// <returns></returns>
        protected string GetIIP(IDictionary<string, object> objectAsDict)
        {
            string idIIP = objectAsDict["idIIP"].ToString();
            //"PL" ending not needed
            return idIIP.Substring(0, idIIP.IndexOf(".") - 2);
        }

        /// <summary>
        ///     used to return other important data e.g. angles  
        /// </summary>
        /// <param name="objectAsDict"></param>
        /// <returns></returns>
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
