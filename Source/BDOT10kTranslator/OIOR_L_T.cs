using System;
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
    //=== Klasa odpowiedzialna za tłumaczenie klasy BDOT10k - OIOR_L na obiekty Unity ===
    //-----------------------------------------------------------------------------------
    //====== Class responsible for translating BDOT10k - OIOR_L class to Unity obj ======
    //===================================================================================
    //http://prawo.sejm.gov.pl/isap.nsf/download.xsp/WDU20112791642/O/D20111642-02.pdf
    class OIOR_L_T
    {
        public void OIOR_L(GeodataLoaderConfiguration config)
        {
            var type = "OIOR_L"; // końcówka nazwy pliku / end of file name
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
            var parser = new Parser_OIOR_L(); // wykorzystaj ten parser / use this parser
            parser.InitDocument(file);
            CoordinatesCalculator.InitializeCenter(config.ParsedCenterXY); // wczytaj centrum obszaru / load area center

            foreach (var entity in parser.GetBDOT10Ks()) // (gml featuremember)
            {
                

                if (entity.XKod == "OIOR04")
                {
                    // stwórz listę wektorów zawierających współrzędne x,y krańców segmentów w obszarze gry (współrzędne już w układzie gry)
                    //----------------------------------------------------------------------------------------------------------------------
                    // create list containing x,y vectors for ends of segments inside game area (coordinates already in ingame system)
                    var vectorList =
                        entity.XYLine
                            .Select(point => CoordinatesCalculator.GameXY(new Vector2(point[0], point[1])))
                            .Where(CoordinatesCalculator.IsInRange)
                            .ToList();
                    var line = DouglasPointsReduction.Reduct(vectorList, 3); // wykorzystaj algorytm Douglasa do redukcji punktów / use the Douglas Point Reduction algorithm
                    for (int i = 0; i < line.Count - 1; i++) // dla każdej pary punktów po redukcji stwórz segment drogi / for each point pair, after the reduction, create road segment
                    {
                        try
                        {
                            // spróbuj stworzyć obiekt dla danego xkod w słowniku
                            //---------------------------------------------------
                            // try creating object for certain xkod in dictionary
                            NetFactory.Create(line[i].x, line[i].y, line[i + 1].x, line[i + 1].y, "Castle Wall 2");
                        }
                        catch
                        {
                            // jeżeli nie uda sie stworzyć obiektu/ if object could not be created
                            CommonHelpers.Log($"Key = {entity.XKod} is not found.");
                        }
                    }
                }
                else
                {
                    if (entity.XKod == "OIOR07")
                    {
                        var polygon =
                            entity.XYLine
                            .Select(point => CoordinatesCalculator.GameXY(new Vector2(point[0], point[1])))
                            .Where(CoordinatesCalculator.IsInRange)
                            .ToArray();
                        var avgPoint = PointInPoly.AvgPoint(polygon);
                        var closest = RoadSegmentFinder.FindClosesToPoint(RoadFactory.Segments, avgPoint); // znajdź najbliższy segment drogi / find closest road segment 
                        var angle = (float)PointInLine.Azimuth(closest.p1, closest.p2); // oblicz azymut do segmentu / calculate azimuth to segment

                        // oblicz iloczyn wektorowy by przekręcić obiekty z lewej strony wstawiane tyłem do segmentu 
                        // -----------------------------------------------------------------------------------------
                        // we calculate vector product because objects on the left side are placed with their back to the segment 
                        var vp = PointInLine.VectorProduct(closest.p1, closest.p2, avgPoint);
                        if (vp < 0)
                            angle = angle + (float)Math.PI;

                        BuildingFactory.Create(avgPoint.x, avgPoint.y, angle, "2x8_FishingPier"); // stwórz obiekt odpowiedniego typu / create object of specified type 
                    }
                }
            }
        }
    }
}
