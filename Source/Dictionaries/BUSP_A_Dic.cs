using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeodataLoader.Source.Dictionaries
{
    //===============================================================
    //============= Słownik do klasy tłumaczącej BUSP_A =============
    //---------------------------------------------------------------
    //=== Dictionary for class responsible for translating BUSP_A ===
    //===============================================================
    //http://prawo.sejm.gov.pl/isap.nsf/download.xsp/WDU20112791642/O/D20111642-02.pdf
    class BUSP_A_Dic
    {
        public static Dictionary<string, string> BuildingXkodDic = new Dictionary<string, string>
        {
            // wielu obiektów brakuje, zastosowano zamienniki / many objects missing, placeholders used instead
            { "BUSP01", "2x2_PrivatePool" },
            { "BUSP02", "2x2_PrivatePool" },
            //BUSP03 - line feature
            { "BUSP04", "Tennis_Court_EU" },
            { "BUSP05", "Tennis_Court_EU" },
            { "BUSP06", "Regular Playground" },
            { "BUSP07", "2x2_Gym" },
            { "BUSP08", "bouncer_castle" },     //placeholder
            { "BUSP09", "Football Stadium Field" },
            { "BUSP10", "bouncer_castle" },     //placeholder
            { "BUSP11", "bouncer_castle" },     //placeholder
            { "BUSP12", "bouncer_castle" },     //placeholder
            { "BUSP13", "bouncer_castle" }      //placeholder
        };
    }
}
