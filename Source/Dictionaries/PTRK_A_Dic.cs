using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeodataLoader.Source.Dictionaries
{
    //===============================================================
    //============= Słownik do klasy tłumaczącej PTRK_A =============
    //---------------------------------------------------------------
    //=== Dictionary for class responsible for translating PTRK_A ===
    //===============================================================
    //http://prawo.sejm.gov.pl/isap.nsf/download.xsp/WDU20112791642/O/D20111642-02.pdf
    class PTRK_A_Dic
    {
        public static Dictionary<string, string> TreeXkodDic = new Dictionary<string, string>
        {
            { "PTRK01", "Tree Sapling 01" },
            { "PTRK02", "Bush01" },
        };
    }
}
