using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeodataLoader.Source.Dictionaries
{
    //===============================================================
    //============= Słownik do klasy tłumaczącej PTUT_A =============
    //---------------------------------------------------------------
    //=== Dictionary for class responsible for translating PTUT_A ===
    //===============================================================
    //http://prawo.sejm.gov.pl/isap.nsf/download.xsp/WDU20112791642/O/D20111642-02.pdf
    class PTUT_A_Dic
    {
        // skrót typu uprawy, obiekt / abbreviation of the cultivation type, object
        public static Dictionary<string, string> TreeGatunekDic = new Dictionary<string, string>
        {
            // wielu typów drzew brakuje, zastosowano zamienniki / many tree types missing, placeholders used instead
            { "Brz", "Orange Tree 01" },
            { "Czr", "Flower Tree 02" },
            { "Grs", "Pear Tree 01" },
            { "Jbl", "Apple Tree 01" },
            { "Mrl", "Orange Tree 01" },
            { "Slw", "Orange Tree 01" },
            { "Wsn", "Flower Tree 02" },
            { "Dig", "Conifer2" },
            { "Dls", "Orange Tree 01" },
            { "Dms", "Orange Tree 01" }, // mieszane / mixed
            { "other", "Orange Tree 01" }
        };

        // tak jak powyżej / same as above
        public static Dictionary<string, string> PropGatunekDic = new Dictionary<string, string>
        {
            // wielu typów roślin brakuje, zastosowano zamienniki / many plant types missing, placeholders used instead
            { "Agr", "Roof Vegetation 07" },
            { "Aro", "Roof Vegetation 07" },
            { "Chm", "Wall Vegetation 03" },
            { "Prz", "Roof Vegetation 07" },
            { "Wkl", "Roof Vegetation 07" },
            { "Win", "Wall Vegetation 03" },
            { "other", "Roof Vegetation 07" }
        };

        // dystans na siatce [m] / grid distance [m]
        public static Dictionary<string, float> XkodDic = new Dictionary<string, float>
        {
            { "PTUT02", 5 },
            { "PTUT03", 10 },
            { "PTUT04", 18 },
            { "PTUT05", 10 },
        };
    }
}
