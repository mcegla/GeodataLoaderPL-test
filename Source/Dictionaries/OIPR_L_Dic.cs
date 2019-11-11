using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeodataLoader.Source.Dictionaries
{
    //===============================================================
    //============= Słownik do klasy tłumaczącej OIPR_L =============
    //---------------------------------------------------------------
    //=== Dictionary for class responsible for translating OIPR_L ===
    //===============================================================
    //http://prawo.sejm.gov.pl/isap.nsf/download.xsp/WDU20112791642/O/D20111642-02.pdf
    class OIPR_L_Dic
    {
        public static Dictionary<string, string> PropXkodDic = new Dictionary<string, string>
        {
            //OIPR05 not needed
            {"OIPR08", "Hedge 02" }
        };

        public static Dictionary<string, string> TreeXkodDic = new Dictionary<string, string>
        {
            {"OIPR10", "Conifer2" } // nie zdefiniowano gatunku / species not defined
        };
    }
}
