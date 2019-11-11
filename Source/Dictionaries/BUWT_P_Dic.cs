using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeodataLoader.Source.Dictionaries
{
    //===============================================================
    //============= Słownik do klasy tłumaczącej BUWT_P =============
    //---------------------------------------------------------------
    //=== Dictionary for class responsible for translating BUWT_P ===
    //===============================================================
    //http://prawo.sejm.gov.pl/isap.nsf/download.xsp/WDU20112791642/O/D20111642-02.pdf
    class BUWT_P_Dic
    {
        public static Dictionary<string, string> BuildingXkodDic = new Dictionary<string, string>
        {
            // wielu obiektów brakuje, zastosowano zamienniki / many objects missing, placeholders used instead
            { "BUWT00", "Wooden Footbridge Pillar 18" },
            { "BUWT01", "Castle Ruins 02" },
            { "BUWT02", "Castle Ruins 03" },
            { "BUWT05", "Wind Turbine" },
            //{ "BUWT06", "Electricity Pole" }, //different case
            { "BUWT07", "RoadSmallBridgePillar" }, 
            { "BUWT08", "Water Tower" },
            { "BUWT09", "RailwayElevatedPillar" }, 
            { "BUWT10", "Ore 2x2 Extractor" },
            { "BUWT11", "ChirpX Launch Tower" },
            { "BUWT12", "Wooden Footbridge Pillar 6"}
        };

        public static Dictionary<string, string> PropXkodDic = new Dictionary<string, string>
        {
            { "BUWT03", "Airport Light" },
            { "BUWT04", "Wifi antenna" }
            
        };
    }
}
