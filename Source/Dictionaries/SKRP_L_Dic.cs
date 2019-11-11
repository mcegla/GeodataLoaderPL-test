using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeodataLoader.Source.Helpers;

namespace GeodataLoader.Source.Dictionaries
{
    //===============================================================
    //============= Słownik do klasy tłumaczącej SKRP_L =============
    //---------------------------------------------------------------
    //=== Dictionary for class responsible for translating SKRP_L ===
    //===============================================================
    //http://prawo.sejm.gov.pl/isap.nsf/download.xsp/WDU20112791642/O/D20111642-02.pdf
    class SKRP_L_Dic
    {
        // skrót typu nawierzchni, obiekt / abbreviation of the surface type, object
        public static Dictionary<string, string> SegmentmatNawDic = new Dictionary<string, string>
        {
            // wielu typów brakuje, zastosowano zamienniki / many pavement types missing, placeholders used instead
            { "Bt","Pedestrian Pavement"},  //beton, concrete
            { "Br","Pedestrian Pavement"},  //bruk, paving
            { "Kl","Pedestrian Pavement"},  //klinkier, clinker
            { "Kk","Pedestrian Pavement"},  //kostka kamienna, stone cubes
            { "Kp","Pedestrian Pavement"},  //kostka prefabrykowana, prefabricated cubes
            { "Mb","Pedestrian Pavement"},  //masa bitumiczna, bitumen
            { "Pb","Pedestrian Pavement"},  //płyty betonowe, concrete slabs
            { "Tl","Pedestrian Gravel"},    //tłuczeń, broken stone
            { "Zw","Pedestrian Gravel"},    //żwir, gravel
            { "Gr","Pedestrian Gravel"},    //grunt naturalny, natural soil
            { "Gz","Pedestrian Gravel"},    //stabilizacja żwirem lub żużlem, stabilized with gravel or slag

            { "T","Pedestrian Pavement"},   //nawierzchnia twarda, hard surface
            { "U","Pedestrian Gravel"},     //nawierzchnia utwardzona, paved surface
            { "G","Pedestrian Gravel"},     //nawierzchnia gruntowa, ground surface (inapplicable)

            { "other", "Pedestrian Gravel" }
        };
    }
}
