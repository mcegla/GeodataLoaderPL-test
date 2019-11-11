using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeodataLoader.Source.Dictionaries
{
    //===============================================================
    //============= Słownik do klasy tłumaczącej OIPR_P =============
    //---------------------------------------------------------------
    //=== Dictionary for class responsible for translating OIPR_P ===
    //===============================================================
    //http://prawo.sejm.gov.pl/isap.nsf/download.xsp/WDU20112791642/O/D20111642-02.pdf
    class OIPR_P_Dic
    {
        public static Dictionary<string, string> BuildingXkodDic = new Dictionary<string, string>
        {
            { "OIPR02", "Boulder 01" },
            { "OIPR06", "Forestry 3x3 Forest" },
            { "OIPR07", "Rock Formation 04 C" },
            { "OIPR09", "Cliff 12" },
            { "OIPR11", "Cave 01"}
        };

        public static Dictionary<string, string> TreeXkodDic = new Dictionary<string, string>
        {
            { "OIPR01", "Conifer2" }, // nie zdefiniowano gatunku / species not defined
            { "OIPR03", "Bush01" },
            { "OIPR04", "Tree Sapling 01" },
        };

            //OIPR05 not needed
            //OIPR08 Line Feature
            //OIPR10 Line Feature
            //OIPR12 Different approach - DEM
            //OIPR13 Water Source
    }
}
