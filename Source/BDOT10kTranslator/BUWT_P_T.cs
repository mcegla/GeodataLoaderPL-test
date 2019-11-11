﻿using System;
using System.IO;
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
    //=== Klasa odpowiedzialna za tłumaczenie klasy BDOT10k - BUWT_P na obiekty Unity ===
    //-----------------------------------------------------------------------------------
    //====== Class responsible for translating BDOT10k - BUWT_P class to Unity obj ======
    //===================================================================================
    //http://prawo.sejm.gov.pl/isap.nsf/download.xsp/WDU20112791642/O/D20111642-02.pdf
    class BUWT_P_T
    {
        public void BUWT_P(GeodataLoaderConfiguration config)
        {
            var type = "BUWT_P"; // końcówka nazwy pliku / end of file name
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
            var parser = new Parser_BUWT_P(); // wykorzystaj ten parser / use this parser
            parser.InitDocument(file);
            CoordinatesCalculator.InitializeCenter(config.ParsedCenterXY); // wczytaj centrum obszaru / load area center

            foreach (var entity in parser.GetBDOT10Ks()) // (gml featuremember)
            {
                var p = CoordinatesCalculator.GameXY(new Vector2 (entity.XYPoint[0], entity.XYPoint[1])); // oblicz wewnątrzgrowe współrzędne dla elementu / calculate ingame coordinates for entity
                if (CoordinatesCalculator.IsInRange(p) && entity.XKod != "BUWT06") // jeżeli w obszarze gry / if in range of game area
                {
                    var closest = RoadSegmentFinder.FindClosesToPoint(RoadFactory.Segments, p); // znajdź najbliższy segment drogi / find closest road segment 
                    var angle = PointInLine.Azimuth(closest.p1, closest.p2); // oblicz azymut do segmentu / calculate azimuth to segment

                    // oblicz iloczyn wektorowy by przekręcić obiekty z lewej strony wstawiane tyłem do segmentu 
                    // -----------------------------------------------------------------------------------------
                    // we calculate vector product because objects on the left side are placed with their back to the segment 
                    var vp = PointInLine.VectorProduct(closest.p1, closest.p2, p);
                    if (vp < 0)
                        angle = angle + (float)Math.PI;

                    if (BUWT_P_Dic.PropXkodDic.ContainsKey(entity.XKod))  // jeżeli xkod istnieje w danym słowniku / if xkod exists in dictionary
                        PropFactory.Create(p.x, p.y, angle, BUWT_P_Dic.PropXkodDic[entity.XKod]); // stwórz obiekt odpowiedniego typu / create object od specified type 
                    else if (BUWT_P_Dic.BuildingXkodDic.ContainsKey(entity.XKod))
                        BuildingFactory.Create(p.x, p.y, angle, BUWT_P_Dic.BuildingXkodDic[entity.XKod]);

                }
                else 
                {
                    // słupy elektryczne ustawiane są zgodnie z przebiegiem lini 
                    // --------------------------------------------------------- 
                    // electricity poles are placed in accordance with the course of the line 
                    if (CoordinatesCalculator.IsInRange(p) && entity.XKod == "BUWT06")
                    {
                        var closest = RoadSegmentFinder.FindClosesToPoint(GridFactory.Segments, p);
                        var angle = PointInLine.Azimuth(closest.p1, closest.p2);

                        var vp = PointInLine.VectorProduct(closest.p1, closest.p2, p);
                        if (vp < 0)
                            angle = angle + (float)Math.PI;

                        BuildingFactory.Create(p.x, p.y, angle + ((float)Math.PI / 2), "Electricity Pole");
                    }
                }
            }
        }
    }
}