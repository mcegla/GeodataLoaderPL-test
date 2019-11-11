using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeodataLoader.Source.Dictionaries
{
    //===============================================================
    //============= Słownik do klasy tłumaczącej BUIB_A =============
    //---------------------------------------------------------------
    //=== Dictionary for class responsible for translating BUIB_A ===
    //===============================================================
    //http://prawo.sejm.gov.pl/isap.nsf/download.xsp/WDU20112791642/O/D20111642-02.pdf
    class BUIB_A_Dic
    {
        public static Dictionary<string, string> BuildingXkodDic = new Dictionary<string, string>
        {
            // wielu obiektów brakuje, zastosowano zamienniki / many objects missing, placeholders used instead
            { "BUIB01", "Tropical Garden" },
            //BUIB02 - line feature 
            { "BUIB03", "Train Station" },
            { "BUIB04", "1x1_terrace01" },
            { "BUIB05", "shopping_cart_shelter" },
            { "BUIB06", "Football Stadium Front" }
        };
    }
}
