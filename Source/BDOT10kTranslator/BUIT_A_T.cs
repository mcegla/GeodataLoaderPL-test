using System;
using System.Linq;
using UnityEngine;
using GeodataLoader.Source.Parsers;
using GeodataLoader.Source.Helpers;
using GeodataLoader.Source.Dictionaries;
using GeodataLoader.Source.Logic;
using GeodataLoader.Source.Factories;


namespace GeodataLoader.Source.BDOT10kTranslator
{
    //===================================================================================
    //=== Klasa odpowiedzialna za tłumaczenie klasy BDOT10k - BUIT_A na obiekty Unity ===
    //-----------------------------------------------------------------------------------
    //====== Class responsible for translating BDOT10k - BUIT_A class to Unity obj ======
    //===================================================================================
    //http://prawo.sejm.gov.pl/isap.nsf/download.xsp/WDU20112791642/O/D20111642-02.pdf
    class BUIT_A_T
    {
        public void BUIT_A(GeodataLoaderConfiguration config)
        {
            var type = "BUIT_A"; // końcówka nazwy pliku / end of file name
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
            var parser = new Parser_BUIT_A(); // wykorzystaj ten parser / use this parser
            parser.InitDocument(file);
            CoordinatesCalculator.InitializeCenter(config.ParsedCenterXY); // wczytaj centrum obszaru / load area center

            foreach (var entity in parser.GetBDOT10Ks()) // (gml featuremember)
            {
                // stwórz tablicę wektorów zawierających współrzędne x,y krańców poligonu w obszarze gry (współrzędne już w układzie gry)
                //------------------------------------------------------------------------------------------------------------
                // create array containing x,y vectors for vertexs of polygon inside game area (coordinates already in ingame system)
                var polygon =
                    entity.XYLine
                    .Select(point => CoordinatesCalculator.GameXY(new Vector2(point[0], point[1])))
                    .Where(CoordinatesCalculator.IsInRange)
                    .ToArray();

                // obiekty tego typu nie występują w danych wykorzystanych w modelu / objects this type does not exist in data used in model creation
                // stwórz tablicę wektorów zawierających współrzędne x,y krańców pustych wycinków poligonów w obszarze gry (współrzędne już w układzie gry)
                //------------------------------------------------------------------------------------------------------------
                // create array containing x,y vectors for vertexs of empty places inside polygon inside game area (coordinates already in ingame system)

                //var interiors =
                //    entity.InteriorLines?
                //        .Select(line => line?
                //            .Select(point => CoordinatesCalculator.GameXY(new Vector2(point[0], point[1])))
                //            .Where(CoordinatesCalculator.IsInRange)
                //            .ToArray())
                //        .Where(x => x != null);

                var avgPoint = PointInPoly.AvgPoint(polygon);
                var closest = RoadSegmentFinder.FindClosesToPoint(RoadFactory.Segments, avgPoint); // znajdź najbliższy segment drogi / find closest road segment 
                var angle = PointInLine.Azimuth(closest.p1, closest.p2); // oblicz azymut do segmentu / calculate azimuth to segment

                // oblicz iloczyn wektorowy by przekręcić obiekty z lewej strony wstawiane tyłem do segmentu 
                // -----------------------------------------------------------------------------------------
                // we calculate vector product because objects on the left side are placed with their back to the segment 
                var vp = PointInLine.VectorProduct(closest.p1, closest.p2, avgPoint);
                if (vp < 0)
                    angle = angle + (float)Math.PI;

                if (entity.XKod == "BUIT04") // dla danego XKod / for certain XKod
                    BuildingFactory.Create(avgPoint.x, avgPoint.y, angle, "H3 1x1 Facility05"); // stwórz obiekt odpowiedniego typu / create object od specified type
                else
                {
                    if(entity.XKod == "BUIT05")
                    {
                        var parea = PointInPoly.PolygonArea(polygon);
                        if(parea <= 320f) //(576-64)/2+64 - w połowie drogi pomiędzy powierzchniami obiektów / half way between ingame objects area
                            BuildingFactory.Create(avgPoint.x, avgPoint.y, angle, "L3 1x1 Shop"); // stwórz obiekt odpowiedniego typu / create object of specified type
                        else if(parea > 320f && parea <= 672f) //(768-576)/2+576 - podobnie jak wyżej / same as above
                            BuildingFactory.Create(avgPoint.x, avgPoint.y, angle, "L1 3x3 Shop16");
                        else if (parea > 672f) // i więcej / and more
                            BuildingFactory.Create(avgPoint.x, avgPoint.y, angle, "L2 4x3 Shop 11");
                    }
                }
            }
        }
    }
}
