using System;
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
    //=== Klasa odpowiedzialna za tłumaczenie klasy BDOT10k - BUZT_P na obiekty Unity ===
    //-----------------------------------------------------------------------------------
    //====== Class responsible for translating BDOT10k - BUZT_P class to Unity obj ======
    //===================================================================================
    //http://prawo.sejm.gov.pl/isap.nsf/download.xsp/WDU20112791642/O/D20111642-02.pdf
    class BUZT_P_T
    {
        public void BUZT_P(GeodataLoaderConfiguration config)
        {
            var type = "BUZT_P"; // końcówka nazwy pliku / end of file name
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
            var parser = new Parser_BUZT_P(); // wykorzystaj ten parser / use this parser
            parser.InitDocument(file);
            CoordinatesCalculator.InitializeCenter(config.ParsedCenterXY); // wczytaj centrum obszaru / load area center

            foreach (var entity in parser.GetBDOT10Ks()) // (gml featuremember)
            {
                var p = CoordinatesCalculator.GameXY(new Vector2(entity.XYPoint[0], entity.XYPoint[1])); // oblicz wewnątrzgrowe współrzędne dla elementu / calculate ingame coordinates for entity
                if (CoordinatesCalculator.IsInRange(p)) // jeżeli w obszarze gry / if in range of game area
                {
                    var closest = RoadSegmentFinder.FindClosesToPoint(RoadFactory.Segments, p); // znajdź najbliższy segment drogi / find closest road segment 
                    var angle = PointInLine.Azimuth(closest.p1, closest.p2); // oblicz azymut do segmentu / calculate azimuth to segment

                    // oblicz iloczyn wektorowy by przekręcić obiekty z lewej strony wstawiane tyłem do segmentu 
                    // -----------------------------------------------------------------------------------------
                    // we calculate vector product because objects on the left side are placed with their back to the segment 
                    var vp = PointInLine.VectorProduct(closest.p1, closest.p2, p);
                    if (vp < 0)
                        angle = angle + (float)Math.PI;

                    try
                    {
                        // spróbuj stworzyć obiekt dla danego xkod w słowniku / try creating object for certain xkod in dictionary
                        BuildingFactory.Create(p[0], p[1], angle, BUZT_P_Dic.BuildingXkodDic[entity.XKod]);
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
