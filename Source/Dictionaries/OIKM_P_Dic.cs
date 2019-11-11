using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeodataLoader.Source.Dictionaries
{
    //===============================================================
    //============= Słownik do klasy tłumaczącej OIKM_P =============
    //---------------------------------------------------------------
    //=== Dictionary for class responsible for translating OIKM_P ===
    //===============================================================
    //http://prawo.sejm.gov.pl/isap.nsf/download.xsp/WDU20112791642/O/D20111642-02.pdf
    class OIKM_P_Dic
    {
        public static Dictionary<string, string> BuildingXkodDic = new Dictionary<string, string>
        {
            { "OIKM02", "Airport Apron" }, //TEST - probably line feature
            { "OIKM03", "Oneway Toll Booth Large 01" },
            { "OIKM05", "Train Station" },
            { "OIKM08", "Metro Entrance" },
        };

        public static Dictionary<string, string> PropXkodDic = new Dictionary<string, string>
        {
            { "OIKM01", "Billboard_medium_flat_bigbyte_01" }, 
            { "OIKM04", "Bus Stop Large" },
            //OIKM06 - not existing
            { "OIKM07", "Traffic Light European 01" }, 
            { "OIKM09", "Toll Booth Large"}
        };


    }                              
}