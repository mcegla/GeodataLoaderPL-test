using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeodataLoader.Source.Dictionaries
{
    //===============================================================
    //============= Słownik do klasy tłumaczącej OIOR_P =============
    //---------------------------------------------------------------
    //=== Dictionary for class responsible for translating OIOR_P ===
    //===============================================================
    //http://prawo.sejm.gov.pl/isap.nsf/download.xsp/WDU20112791642/O/D20111642-02.pdf
    class OIOR_P_Dic
    {
        public static Dictionary<string, string> BuildingXkodDic = new Dictionary<string, string>
        {
            { "OIOR07", "2x8 Fishing Pier" }, // line as point
            { "OIOR13", "RailwayElevatedPillar" },
        };

        public static Dictionary<string, string> PropXkodDic = new Dictionary<string, string>
        {
            { "OIOR01", "Bunker Ruins 01" },
            { "OIOR02", "Garden_pot" },
            { "OIOR03", "Large Fountain" },
            //OIOR04 - Line feature
            { "OIOR05", "Concrete support" },
            { "OIOR06", "Standing Stone 03" },//rider_statue
            { "OIOR08", "Castle Ruins 01" },
            { "OIOR09", "Rooftop window 02" },
            //OIOR10 - not existing 
            { "OIOR11", "Pavilion" },
            //OIOR12 - not existing
            { "OIOR14", "Light Pole Red" }
        };
    }
}
