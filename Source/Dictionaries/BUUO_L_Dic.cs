using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeodataLoader.Source.Dictionaries
{
    //===============================================================
    //============= Słownik do klasy tłumaczącej BUUO_L =============
    //---------------------------------------------------------------
    //=== Dictionary for class responsible for translating BUUO_L ===
    //===============================================================
    //http://prawo.sejm.gov.pl/isap.nsf/download.xsp/WDU20112791642/O/D20111642-02.pdf
    class BUUO_L_Dic
    {
        public static Dictionary<string, string> PropXkodDic = new Dictionary<string, string>
        {
            // wielu obiektów brakuje, zastosowano zamienniki / many objects missing, placeholders used instead
            { "BUUO01", "Standing Stone 06" },
            { "BUUO02", "Standing Stone 06" },
            { "BUUO03", "Organic Shop Medium Billboard 01" }
        };
    }
}
