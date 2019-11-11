using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using GeodataLoader.Source.Parsers;
using GeodataLoader.Source.Helpers;
using GeodataLoader.Source.Dictionaries;
using GeodataLoader.Source.Logic;
using GeodataLoader.Source.Factories;


namespace GeodataLoader.Source.BDOT10kTranslator
{
    //===================================================================================
    //=== Klasa odpowiedzialna za tłumaczenie klasy BDOT10k - BUIB_L na obiekty Unity ===
    //-----------------------------------------------------------------------------------
    //====== Class responsible for translating BDOT10k - BUIB_L class to Unity obj ======
    //===================================================================================
    //http://prawo.sejm.gov.pl/isap.nsf/download.xsp/WDU20112791642/O/D20111642-02.pdf
    class BUIB_L_T
    {
        public void BUIB_L(GeodataLoaderConfiguration config)
        {
            var type = "BUIB_L"; // końcówka nazwy pliku / end of file name
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
            var parser = new Parser_BUIB_L(); // wykorzystaj ten parser / use this parser
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
                        .Where(CoordinatesCalculator.IsInRangeSmall)
                        .ToList();

                for (int i = 0; i < vectorList.Count - 1; i++) // dla wszystkich wektorów z listy / for all vectors from the list
                {
                    if (entity.XKod == "BUIB02")
                    {
                        // z użyciem assetu z warsztatu steam / with use of steam workshop asset

                        try
                        {
                            // spróbuj stworzyć dany obiekt; drogi opisane są oddzielnie w NetFactory jako RoadFactory ze względu na wyrównywanie do nich budynków
                            //----------------------------------------------------------------------------------------------------------------------------------------------------------
                            // try creating certain object; roads are called separately in NetFactory as RoadFactory, becouse of building alignment
                            NetFactory.Create(vectorList[i].x, vectorList[i].y, vectorList[i + 1].x, vectorList[i + 1].y, "1394735871.Privacy Fence_Data");
                        }
                        catch (KeyNotFoundException)
                        {
                            // jeżeli nie uda sie stworzyć obiektu zwróc komunikat / if object could not be created show message
                            CommonHelpers.Log($"Could not create fence");
                        }


                        //var pointsList = PointInLine.CreatePointsInLine(vectorList[i], vectorList[i + 1], 4); // stwórz listę punktów w danym segmencie / create points list inside of said segment
                        //var pointsAzimuth = PointInLine.Azimuth(vectorList[i], vectorList[i + 1]); // oblicz azymut dla krańców segmentu / calculate azimuth between ends of segment
                        //foreach (var point in pointsList)
                        //{
                        //    try
                        //    {
                        //        // spróbuj stworzyć dany obiekt / try creating certain object
                        //        PropFactory.Create(point.x, point.y, pointsAzimuth, "Modern Fence 02");
                        //    }
                        //    catch
                        //    {
                        //        // jeżeli nie uda sie stworzyć obiektu zwróc komunikat / if object could not be created show message
                        //        CommonHelpers.Log($"Could not create fence");
                        //    }
                        //}
                    }
                }
            }
        }
    }
}
