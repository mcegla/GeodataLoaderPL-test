using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeodataLoader.Source.Helpers;

namespace GeodataLoader.Source.Dictionaries
{
    //===============================================================
    //============= Słownik do klasy tłumaczącej SKJZ_L =============
    //---------------------------------------------------------------
    //=== Dictionary for class responsible for translating SKJZ_L ===
    //===============================================================
    //http://prawo.sejm.gov.pl/isap.nsf/download.xsp/WDU20112791642/O/D20111642-02.pdf
    class SKJZ_L_Dic
    {
        // ilość pasów jezdni, skrót typu nawierzchni, obiekt
        //---------------------------------------------------
        // number of lanes, abbreviation of the surface type, object
        public static MultiKeyDictionary<int, string, string> SegmentDic = new MultiKeyDictionary<int, string, string>
        {
            // wielu typów brakuje, zastosowano zamienniki / many pavement types missing, placeholders used instead
            { 0,"Bt","Basic Road"},//beton, concrete
            { 1,"Bt","HighwayRamp"},
            { 2,"Bt","Basic Road Decoration Grass"},
            { 3,"Bt","Medium Road"},
            { 4,"Bt","Medium Road Decoration Grass"},
            { 5,"Bt","Large Road"},
            { 6,"Bt","Large Road Decoration Grass"},
            { 0,"Br","Basic Road"},//bruk, paving
            { 1,"Br","HighwayRamp"},
            { 2,"Br","Basic Road Decoration Grass"},
            { 3,"Br","Medium Road"},
            { 4,"Br","Medium Road Decoration Grass"},
            { 5,"Br","Large Road"},
            { 6,"Br","Large Road"},
            { 0,"Kl","Basic Road"},//klinkier, clinker
            { 1,"Kl","HighwayRamp"},
            { 2,"Kl","Basic Road Decoration Grass"},
            { 3,"Kl","Medium Road"},
            { 4,"Kl","Medium Road Decoration Grass"},
            { 5,"Kl","Large Road"},
            { 6,"Kl","Large Road Decoration Grass"},
            { 0,"Kk","Basic Road"},//kostka kamienna, stone cubes
            { 1,"Kk","HighwayRamp"},
            { 2,"Kk","Basic Road Decoration Grass"},
            { 3,"Kk","Medium Road"},
            { 4,"Kk","Medium Road Decoration Grass"},
            { 5,"Kk","Large Road"},
            { 6,"Kk","Large Road Decoration Grass"},
            { 0,"Kp","Basic Road"},//kostka prefabrykowana, prefabricated cubes
            { 1,"Kp","HighwayRamp"},
            { 2,"Kp","Basic Road Decoration Grass"},
            { 3,"Kp","Medium Road"},
            { 4,"Kp","Medium Road Decoration Grass"},
            { 5,"Kp","Large Road"},
            { 6,"Kp","Large Road Decoration Grass"},
            { 0,"Mb","Basic Road"},//masa bitumiczna, bitumen
            { 1,"Mb","HighwayRamp"},
            { 2,"Mb","Basic Road Decoration Grass"},
            { 3,"Mb","Medium Road"},
            { 4,"Mb","Medium Road Decoration Grass"},
            { 5,"Mb","Large Road"},
            { 6,"Mb","Large Road Decoration Grass"},
            { 0,"Pb","Basic Road"},//płyty betonowe, concrete slabs
            { 1,"Pb","HighwayRamp"},
            { 2,"Pb","Basic Road Decoration Grass"},
            { 3,"Pb","Medium Road"},
            { 4,"Pb","Medium Road Decoration Grass"},
            { 5,"Pb","Large Road"},
            { 6,"Pb","Large Road Decoration Grass"},
            { 0,"Tl","Gravel Road"},//tłuczeń, broken stone
            { 1,"Tl","Gravel Road"},
            { 2,"Tl","Gravel Road"},
            { 3,"Tl","Gravel Road"},
            { 4,"Tl","Gravel Road"},
            { 5,"Tl","Gravel Road"},
            { 6,"Tl","Gravel Road"},
            { 0,"Zw","Gravel Road"},//żwir, gravel
            { 1,"Zw","Gravel Road"},
            { 2,"Zw","Gravel Road"},
            { 3,"Zw","Gravel Road"},
            { 4,"Zw","Gravel Road"},
            { 5,"Zw","Gravel Road"},
            { 6,"Zw","Gravel Road"},
            { 0,"Gr","Gravel Road"},//grunt naturalny, natural soil
            { 1,"Gr","Gravel Road"},
            { 2,"Gr","Gravel Road"},
            { 3,"Gr","Gravel Road"},
            { 4,"Gr","Gravel Road"},
            { 5,"Gr","Gravel Road"},
            { 6,"Gr","Gravel Road"},
            { 0,"Gz","Gravel Road"},//stabilizacja żwirem lub żużlem, stabilized with gravel or slag
            { 1,"Gz","Gravel Road"},
            { 2,"Gz","Gravel Road"},
            { 3,"Gz","Gravel Road"},
            { 4,"Gz","Gravel Road"},
            { 5,"Gz","Gravel Road"},
            { 6,"Gz","Gravel Road"},

            { 0,"T","Basic Road"},//nawierzchnia twarda, hard surface
            { 1,"T","HighwayRamp"},
            { 2,"T","Basic Road Decoration Grass"},
            { 3,"T","Medium Road"},
            { 4,"T","Medium Road Decoration Grass"},
            { 5,"T","Large Road"},
            { 6,"T","Large Road Decoration Grass"},
            { 0,"U","Gravel Road"},//nawierzchnia utwardzona, paved surface
            { 1,"U","Gravel Road"},
            { 2,"U","Gravel Road"},
            { 3,"U","Gravel Road"},
            { 4,"U","Gravel Road"},
            { 5,"U","Gravel Road"},
            { 6,"U","Gravel Road"},
            { 0,"G","Gravel Road"},//nawierzchnia gruntowa, ground surface (inapplicable)
        };
    }
}
