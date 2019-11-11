using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using GeodataLoader.Source.Parsers;
using GeodataLoader.Source.Helpers;
using GeodataLoader.Source.Dictionaries;
using GeodataLoader.Source.Logic;
using GeodataLoader.Source.Factories;


namespace GeodataLoader.Source.BDOT10kTranslator
{
    //===================================================================================
    //=== Klasa odpowiedzialna za tłumaczenie klasy BDOT10k - SKJZ_L na obiekty Unity ===
    //-----------------------------------------------------------------------------------
    //====== Class responsible for translating BDOT10k - SKJZ_L class to Unity obj ======
    //===================================================================================
    //http://prawo.sejm.gov.pl/isap.nsf/download.xsp/WDU20112791642/O/D20111642-02.pdf
    class SKJZ_L_T
    {
        public void SKJZ_L(GeodataLoaderConfiguration config)
        {
            var type = "SKJZ_L"; // końcówka nazwy pliku / end of file name
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
            var parser = new Parser_SKJZ_L(); // wykorzystaj ten parser / use this parser
            parser.InitDocument(file);
            CoordinatesCalculator.InitializeCenter(config.ParsedCenterXY); // wczytaj centrum obszaru / load area center

            foreach (var entity in parser.GetBDOT10Ks())
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
                        // spróbuj stworzyć obiekt dla danego xkod w słowniku; drogi opisane są oddzielnie w NetFactory jako RoadFactory ze względu na wyrównywanie do nich budynków
                        //----------------------------------------------------------------------------------------------------------------------------------------------------------
                        // try creating object for certain xkod in dictionary; roads are called separately in NetFactory as RoadFactory, becouse of building alignment
                        RoadFactory.Create(line[i].x, line[i].y, line[i + 1].x, line[i + 1].y, SKJZ_L_Dic.SegmentDic[entity.LiczbaPasow, entity.MaterialNawierzchni]);
                        //NetFactory.Create(line[i].x, line[i].y, line[i + 1].x, line[i + 1].y, SKJZ_L_Dic.SegmentXkodDic[entity.XKod]);
                    }
                    catch (KeyNotFoundException)
                    {
                        // jeżeli nie uda sie znaleźc klucza zwróc komunikat / catch key not found exception, and show message
                        CommonHelpers.Log($"Key = {entity.XKod} is not found.");
                    }
                }
            }
        }
    }
}
