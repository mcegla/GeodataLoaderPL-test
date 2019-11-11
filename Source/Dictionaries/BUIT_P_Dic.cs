using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeodataLoader.Source.Dictionaries
{
    //===============================================================
    //============= Słownik do klasy tłumaczącej BUIT_P =============
    //---------------------------------------------------------------
    //=== Dictionary for class responsible for translating BUIT_P ===
    //===============================================================
    //http://prawo.sejm.gov.pl/isap.nsf/download.xsp/WDU20112791642/O/D20111642-02.pdf
    class BUIT_P_Dic
    {
        public static Dictionary<string, string> BuildingXkodDic = new Dictionary<string, string>
        {
            // wielu obiektów brakuje, zastosowano zamienniki / many objects missing, placeholders used instead
            { "BUIT01", "Oil 3x2 Extractor02" },
            { "BUIT02", "Water Intake" }, // zbyt duże / too big
            //{ "BUIT04", "H3 1x1 Facility05" },
            { "BUIT05", "L3 1x1 Shop" },
            { "BUIT07","Oil 4x2 Processing03" } 
        };

        public static Dictionary<string, string> PropXkodDic = new Dictionary<string, string>
        {
            { "BUIT03", "Air Source Heat Pump 02" },
            { "BUIT06", "ILS Building" } 
        };
    }
}
