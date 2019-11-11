using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeodataLoader.Source.Dictionaries
{
    //===============================================================
    //============= Słownik do klasy tłumaczącej BUTR_L =============
    //---------------------------------------------------------------
    //=== Dictionary for class responsible for translating BUTR_L ===
    //===============================================================
    //http://prawo.sejm.gov.pl/isap.nsf/download.xsp/WDU20112791642/O/D20111642-02.pdf
    class BUTR_L_Dic
    {
        public static Dictionary<string, string> PropXkodDic = new Dictionary<string, string>
        {
            // wielu obiektów brakuje, zastosowano zamienniki / many objects missing, placeholders used instead
            { "BUTR01", "swing-set" },
            { "BUTR04", "Roof Walkway 01" },
            { "BUTR05", "swing-set" }
        };
    }
}
