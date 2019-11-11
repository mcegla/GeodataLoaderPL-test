using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeodataLoader.Source.Dictionaries
{
    //===============================================================
    //============= Słownik do klasy tłumaczącej BUHD_L =============
    //---------------------------------------------------------------
    //=== Dictionary for class responsible for translating BUHD_L ===
    //===============================================================
    //http://prawo.sejm.gov.pl/isap.nsf/download.xsp/WDU20112791642/O/D20111642-02.pdf
    class BUHD_L_Dic
    {
        public static Dictionary<string, string> PropXkodDic = new Dictionary<string, string>
        {
            // rzeczywiste obiekty nie posiadają odpowiedników w grze, zastosowano zamienniki 
            //--------------------------------------------------------------------------------
            // props representing realworld data not existing in vanilla, placeholders used
            { "BUHD01", "Modern Fence 02" },
            { "BUHD02", "Modern Fence 02" },
            { "BUHD51", "Modern Fence 02" },
            { "BUHD03", "Modern Fence 01" }
        };
    }
}
