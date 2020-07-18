using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Xml.Linq;

namespace GMLParserPL.Parsers
{
    /// <summary>
    ///     Main parser returns objects list with attributes compatible with those from given list
    /// </summary>
    internal class GMLParser
    {
        #region fields
        private static readonly XNamespace gml = "http://www.opengis.net/gml/3.2";
        private static readonly XNamespace ot = "urn:gugik:specyfikacje:gmlas:bazaDanychObiektowTopograficznych10k:1.0";

        private readonly string filePath;
        private readonly string bdotClass;
        private List<string> gmlAttributesList;

        private dynamic objAttributes;
        private List<ExpandoObject> selectedBDOTObj;
        #endregion


        internal GMLParser(string filePath, string bdotClass, List<string> gmlAttributesList)
        {
            this.filePath = filePath;
            this.bdotClass = bdotClass;
            this.gmlAttributesList = gmlAttributesList;
        }


        #region methods
        internal List<ExpandoObject> ParseObjects()
        {
            try
            {
                XDocument xmlFile = XDocument.Load(filePath);
                selectedBDOTObj = new List<ExpandoObject>();
                foreach (var cos in xmlFile.Descendants(ot + "OT_" + bdotClass))
                {
                    objAttributes = new ExpandoObject();
                    var newObjAtributes = (IDictionary<string, object>)objAttributes;
                    foreach (var attribute in gmlAttributesList)
                    {
                        if (attribute == "posList")
                        {
                            newObjAtributes[attribute] = cos.Descendants(NamespaceCheck(attribute) + attribute)
                                .Select(m => m?.Value)
                                .ToList();
                        }
                        else
                            newObjAtributes[attribute] = cos.Descendants(NamespaceCheck(attribute) + attribute)
                                .FirstOrDefault()?.Value;

                    }
                    objAttributes = newObjAtributes;
                    selectedBDOTObj.Add(objAttributes);
                }
                return selectedBDOTObj;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{ObjectTypeEnum.Error};{e}");
                return null;
            }
        }

        private XNamespace NamespaceCheck(string attributeToCheck)
        {
            // those are the only important attributes using the gml namespace  
            if (attributeToCheck == "pos" || attributeToCheck == "posList")
                return gml;
            else
                return ot;
        }
        #endregion
    }
}
