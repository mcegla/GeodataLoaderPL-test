using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeodataLoader.Source.Dictionaries
{
    //===============================================================
    //============= Słownik do klasy tłumaczącej PTLZ_A =============
    //---------------------------------------------------------------
    //=== Dictionary for class responsible for translating PTLZ_A ===
    //===============================================================
    //http://prawo.sejm.gov.pl/isap.nsf/download.xsp/WDU20112791642/O/D20111642-02.pdf
    class PTLZ_A_Dic
    {
        // skrót typu drzewa, obiekt / abbreviation of the tree type, object
        public static Dictionary<string, string> TreeGatunekDic = new Dictionary<string, string>
        {
            // wielu typów drzew brakuje, zastosowano zamienniki / many tree types missing, placeholders used instead
            { "Akc", "Yew 01" },        //akacja, acacia
            { "Brz", "Yew 01" },        //brzoza, birch-tree
            { "Buk", "Beech01low" },    //buk, beech
            { "Dgl", "Conifer2" },      //daglezja, douglas
            { "Dab", "Tree3variant" },  //dąb, oak
            { "Grb", "Yew 01" },        //grab, hornbeam
            { "Jwr", "Yew 01" },        //jawor, sycamore
            { "Jsn", "Yew 01" },        //jesion, ash
            { "Jdl", "Conifer2" },      //jodła, fir
            { "Kln", "Sugar Maple 01" },//klon, maple
            { "Lmb", "Conifer2" },      //limba, pine cembra L.
            { "Lpa", "Yew 01" },        //lipa, linden
            { "Mdr", "Conifer2" },      //moderzew, larch
            { "Olc", "Alder01" },       //olcha, alder
            { "Osk", "Yew 01" },        //osika, aspen
            { "Ssn", "Pine01low" },     //sosna, pine
            { "Swr", "Conifer" },       //świerk, spruce
            { "Tpl", "Yew 01" },        //topola, poplar
            { "Wiz", "Yew 01" },        //wiąz, elm
            { "Wib", "Yew 01" },        //wierzba, willow
            { "", "Yew 01" }
        };

        // dystans na siatce [m] / grid distance [m]
        public static Dictionary<string, float> XkodDic = new Dictionary<string, float>
        {
            { "PTLZ01", 20 },
            { "PTLZ02", 25 },
            { "PTLZ03", 30 }
        };
    }
}
