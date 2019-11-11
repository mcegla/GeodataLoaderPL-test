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
    //=== Klasa odpowiedzialna za tłumaczenie klasy BDOT10k - PTGN_A na obiekty Unity ===
    //-----------------------------------------------------------------------------------
    //====== Class responsible for translating BDOT10k - PTGN_A class to Unity obj ======
    //===================================================================================
    //http://prawo.sejm.gov.pl/isap.nsf/download.xsp/WDU20112791642/O/D20111642-02.pdf
    class PTGN_A_T
    {
        public void PTGN_A(GeodataLoaderConfiguration config)
        {
            var type = "PTGN_A"; // końcówka nazwy pliku / end of file name
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
            var parser = new Parser_PTGN_A(); // wykorzystaj ten parser / use this parser
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
                    .Where(CoordinatesCalculator.IsInRangeResources)
                    .ToArray();

                // obiekty tego typu nie występują w danych wykorzystanych w modelu / objects this type does not exist in data used in model creation
                // stwórz tablicę wektorów zawierających współrzędne x,y krańców pustych wycinków poligonów w obszarze gry (współrzędne już w układzie gry)
                //------------------------------------------------------------------------------------------------------------
                // create array containing x,y vectors for vertexs of empty places inside polygon inside game area (coordinates already in ingame system)

                //var interiors =
                //    entity.InteriorLines?
                //        .Select(line => line?
                //            .Select(point => CoordinatesCalculator.GameXY(new Vector2(point[0], point[1])))
                //            .Where(CoordinatesCalculator.IsInRangeResources)
                //            .ToArray())
                //        .Where(x => x != null);

                // jeśli okaże się, że poligon reprezentowany jest mniej niż 3 wierzchołkami kontynuuj / pomiń
                //--------------------------------------------------------------------------------------------
                // if the plygon some how will be represented by less then 3 vertexes continue / skip
                if (polygon.Length < 3)
                    continue;

                // stwórz tablicę punktów wewnątrz prostokąta ograniczającego / create point array inside of bounding rectangle
                var minMax = PointInPoly.FindMaxMin(polygon);
                var points = PointInPoly.CreatePointArray(minMax[0], minMax[1], 33.625f); //(17280-32*2)/512

                foreach (var p in points) // sprawdź czy każdy ze stworzonych punktów jest wewnątrz poligonu / for each point check if it lies inside of polygon  
                {
                    if (PointInPoly.pnpoly(polygon, p.x, p.y))
                        //&& (interiors == null || !interiors.Any(interior => PointInPoly.pnpoly(interior, p.x, p.y))))
                    {
                        if (entity.XKod == "PTGN03")
                            new ResourceFactory().CreateSand(p);
                        else if (entity.XKod == "PTGN02" || entity.XKod == "PTGN01")
                            new ResourceFactory().CreateOre(p);
                    }
                }
            }
        }
    }
}
