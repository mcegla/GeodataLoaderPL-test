using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeodataLoader.Source.Dictionaries
{
    //===============================================================
    //============= Słownik do klasy tłumaczącej BUZT_P =============
    //---------------------------------------------------------------
    //=== Dictionary for class responsible for translating BUZT_P ===
    //===============================================================
    //http://prawo.sejm.gov.pl/isap.nsf/download.xsp/WDU20112791642/O/D20111642-02.pdf
    class BUZT_P_Dic
    {
        public static Dictionary<string, string> BuildingXkodDic = new Dictionary<string, string>
        {
            { "BUZT01", "Agricultural 1x1 processing 1" },
            { "BUZT02", "Oil 1x1 processing" },
            { "BUZT03", "H1 1x1 Facility01" },
            { "BUZT04", "H1 1x1 FarmingFacility03" }
        };
    }
}
