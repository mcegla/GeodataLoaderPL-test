using GMLParserPL.Configuration;
using GMLParserPL.Logic;
using GMLParserPL.Parsers;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Numerics;

namespace GMLParserPL.Translators.BDOT
{
    internal class PTUT_A : AreaToPointsTranslator
    {
        private Vector2 currentPoint = new Vector2(0, 0);
        private bool isBuilding;
        private bool isTree;
        private bool isPropArea;
        public PTUT_A(string bdotClass, string filePath, Config config) : base(bdotClass, filePath, config)
        {
            gridDist = 10;
        }

        protected sealed override List<ExpandoObject> LoadObjects(string file, string bdotClass)
        {
            if (string.IsNullOrEmpty(file))
                return null;
            // The ending letter of BDOT10k class corresponds to its geometric representation
            List<string> GMLAttrWCoord = ParsedGMLAttr;
            if (bdotClass.EndsWith("P"))
                GMLAttrWCoord.Add("pos");
            else if (bdotClass.EndsWith("A") || bdotClass.EndsWith("L"))
                GMLAttrWCoord.Add("posList");
            GMLAttrWCoord.Add("gatunek");

            GMLParser parser = new GMLParser(file, bdotClass, GMLAttrWCoord);
            return parser.ParseObjects();
        }


        protected sealed override void Translate(ExpandoObject objectToTranslate)
        {
            List<string> translatedObjects = new List<string>();
            var objectAsDict = (IDictionary<string, object>)objectToTranslate;

            string objectName = GetObjectName(objectAsDict);
            ObjectTypeEnum objectType = GetObjectType(objectAsDict);
            HashSet<Vector2> pointsInArea = GetPointsInArea(objectAsDict);
            if (pointsInArea == null)
                return;
            if (string.IsNullOrEmpty(objectName))
                return;
            string iIP = GetIIP(objectAsDict);
            string other = GetOther(objectAsDict, currentPoint);
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

        protected sealed override HashSet<Vector2> GetPointsInArea(IDictionary<string, object> objectAsDict)
        {
            if (!isTree && !isPropArea)
            {
                if (!objectAsDict.ContainsKey("posList"))
                    return null;           
                var lineList = (List<string>)objectAsDict["posList"];
                List<List<Vector2>> lineV2List = new List<List<Vector2>>();
                foreach (var line in lineList)
                {
                    var polygon = TranslatorUtils.LineToVectorList(line);
                    lineV2List.Add(polygon);
                }
                var avgPoint = Calculations.AvgPoint(lineV2List[0]);
                if (!TranslatorUtils.IsNaNVector2(avgPoint))
                {
                    currentPoint = avgPoint;
                    return new HashSet<Vector2> { avgPoint };
                }
            }
            return base.GetPointsInArea(objectAsDict);
        }

        protected sealed override ObjectTypeEnum GetObjectType(IDictionary<string, object> objectAsDict)
        {
            return isTree ? ObjectTypeEnum.Tree : (!isBuilding ? ObjectTypeEnum.Prop : ObjectTypeEnum.Building);
        }

        protected sealed override string GetObjectName(IDictionary<string, object> objectAsDict)
        {
            isBuilding = false;
            isPropArea = false;
            isTree = false;
            if (config.PTUT_A_IIPObj_Building.ContainsKey(objectAsDict["idIIP"].ToString()))
            {
                isBuilding = true;
                return config.PTUT_A_IIPObj_Building[objectAsDict["idIIP"].ToString()];
            }
            if (config.PTUT_A_Obj_Building.ContainsKey(objectAsDict["idIIP"].ToString()))
            {
                isBuilding = true;
                return config.PTUT_A_Obj_Building[objectAsDict["idIIP"].ToString()];
            }

            if (config.PTUT_A_IIPObj_Prop.ContainsKey(objectAsDict["idIIP"].ToString()))
            {
                return config.PTUT_A_IIPObj_Prop[objectAsDict["idIIP"].ToString()];
            }
            if (config.PTUT_A_Obj_Prop.ContainsKey(objectAsDict["idIIP"].ToString()))
            {
                return config.PTUT_A_Obj_Prop[objectAsDict["idIIP"].ToString()];
            }

            if (config.PTUT_A_IIPObj_PropGridSize.ContainsKey(objectAsDict["idIIP"].ToString()))
            {
                isPropArea = true;
                gridDist = config.PTUT_A_IIPObj_PropGridSize[objectAsDict["idIIP"].ToString()].Item2;
                return config.PTUT_A_IIPObj_PropGridSize[objectAsDict["idIIP"].ToString()].Item1;
            }
            if (objectAsDict["gatunek"] != null && objectAsDict["gatunek"].ToString() != "" && config.PTUT_A_SpeciesObj_PropGridSize.ContainsKey(objectAsDict["gatunek"].ToString()))
            {
                isPropArea = true;
                gridDist = config.PTUT_A_SpeciesObj_PropGridSize[objectAsDict["gatunek"].ToString()].Item2;
                return config.PTUT_A_SpeciesObj_PropGridSize[objectAsDict["gatunek"].ToString()].Item1;
            }
            if (config.PTUT_A_Obj_PropGridSize.ContainsKey(objectAsDict["idIIP"].ToString()))
            {
                isPropArea = true;
                gridDist = config.PTUT_A_Obj_PropGridSize[objectAsDict["idIIP"].ToString()].Item2;
                return config.PTUT_A_Obj_PropGridSize[objectAsDict["idIIP"].ToString()].Item1;
            }

            if (config.PTUT_A_IIPObj_TreeGridSize.ContainsKey(objectAsDict["idIIP"].ToString()))
            {
                isTree = true;
                gridDist = config.PTUT_A_IIPObj_TreeGridSize[objectAsDict["idIIP"].ToString()].Item2;
                return config.PTUT_A_IIPObj_TreeGridSize[objectAsDict["idIIP"].ToString()].Item1;
            }
            if (objectAsDict["gatunek"] != null && objectAsDict["gatunek"].ToString() != "" && config.PTUT_A_SpeciesObj_TreeGridSize.ContainsKey(objectAsDict["gatunek"].ToString()))
            {
                isTree = true;
                gridDist = config.PTUT_A_SpeciesObj_TreeGridSize[objectAsDict["gatunek"].ToString()].Item2;
                return config.PTUT_A_SpeciesObj_TreeGridSize[objectAsDict["gatunek"].ToString()].Item1;
            }
            if (config.PTUT_A_Obj_TreeGridSize.ContainsKey(objectAsDict["x_kod"].ToString()))
            {
                isTree = true;
                gridDist = config.PTUT_A_Obj_TreeGridSize[objectAsDict["x_kod"].ToString()].Item2;
                return config.PTUT_A_Obj_TreeGridSize[objectAsDict["x_kod"].ToString()].Item1;
            }
            return null;
        }

        protected sealed override string GetOther(IDictionary<string, object> objectAsDict, Vector2 point)
        {
            if (!isTree && !isPropArea)
            {
                if (TranslatorInitiator.RoadSegment != null)
                {
                    var angleToSegment = TranslatorUtils.AngleToSegment(TranslatorInitiator.RoadSegment, point);
                    return angleToSegment.ToString();
                }
                return "0";
            }
            return isPropArea ? "0" : base.GetOther(objectAsDict, point);
        }
    }
}
