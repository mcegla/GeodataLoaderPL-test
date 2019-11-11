using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using GeodataLoader.Source.Parsers;
using GeodataLoader.Source.Helpers;
using GeodataLoader.Source.Dictionaries;
using GeodataLoader.Source.Logic;
using GeodataLoader.Source.Factories;


namespace GeodataLoader.Source.BDOT10kTranslator
{
    //===================================================================================
    //=== Klasa odpowiedzialna za tłumaczenie klasy BDOT10k - BUUO_L na obiekty Unity ===
    //-----------------------------------------------------------------------------------
    //====== Class responsible for translating BDOT10k - BUUO_L class to Unity obj ======
    //===================================================================================
    //http://prawo.sejm.gov.pl/isap.nsf/download.xsp/WDU20112791642/O/D20111642-02.pdf
    class BUUO_L_T
    {
        public void BUUO_L(GeodataLoaderConfiguration config)
        {
            var type = "BUUO_L"; // końcówka nazwy pliku / end of file name
            string file = null;
            try
            {
                file = FileFinder.FindFileInFolder(config.BDOT10k, type); // odnajdywanie pliku w folderze / file finding in folder
            }
            catch
            {
                CommonHelpers.Log($"Can not find file of type:{type}"); // co jeśli się nie powiedzie / what when failed
                return;
            }
            CommonHelpers.Log($"found file:{file}"); // jeśli się powiedzie poinformuj / if succeeded send message
            var parser = new Parser_BUUO_L(); // wykorzystaj ten parser / use this parser
            parser.InitDocument(file);
            CoordinatesCalculator.InitializeCenter(config.ParsedCenterXY); // wczytaj centrum obszaru / load area center

            foreach (var entity in parser.GetBDOT10Ks()) // (gml featuremember)
            {
                // stwórz listę wektorów zawierających współrzędne x,y krańców segmentów w obszarze gry (współrzędne już w układzie gry)
                //------------------------------------------------------------------------------------------------------------
                // create list containing x,y vectors for ends of segments inside game area (coordinates already in ingame system)
                var vectorList =
                    entity.XYLine
                        .Select(point => CoordinatesCalculator.GameXY(new Vector2(point[0], point[1])))
                        .Where(CoordinatesCalculator.IsInRange)
                        .ToList();

                for (int i = 0; i < vectorList.Count - 1; i++) // dla wszystkich wektorów z listy / for all vectors from the list
                {
                    if (BUUO_L_Dic.PropXkodDic.ContainsKey(entity.XKod))  // jeżeli xkod istnieje w danym słowniku / if xkod exists in dictionary
                    {
                        var pointsList = PointInLine.CreatePointsInLine(vectorList[i], vectorList[i + 1], 4); // stwórz listę punktów w danym segmencie / create points list inside of said segment
                        var pointsAzimuth = PointInLine.Azimuth(vectorList[i], vectorList[i + 1]); // oblicz azymut dla krańców segmentu / calculate azimuth between ends of segment
                        foreach (var point in pointsList)
                        {
                            try
                            {
                                // spróbuj stworzyć obiekt dla danego xkod w słowniku / try creating object for certain xkod in dictionary
                                PropFactory.Create(point.x, point.y, pointsAzimuth, BUUO_L_Dic.PropXkodDic[entity.XKod]);
                            }
                            catch
                            {
                                // jeżeli nie uda sie stworzyć obiektu zwróc komunikat / if object could not be created show message
                                CommonHelpers.Log($"Could not create prop");
                            }
                        }
                    }
                    else if (entity.XKod == "BUUO04")
                        // spróbuj stworzyć obiekt dla danego xkod / try creating object for certain xkod
                        NetFactory.Create(vectorList[i].x, vectorList[i].y, vectorList[i + 1].x, vectorList[i + 1].y, "Quay");
                }
            }
        }
    }
}
