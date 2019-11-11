using System.Globalization;
using UnityEngine;

//============================================================================
//=== Część kodu odpowiedzialna za tworzenie i przechowywanie konfiguracji ===
//----------------------------------------------------------------------------
//=== Part of the code responsible for configuration creation and storage  ===
//============================================================================

// bazuje na samouczku użytkownika forum simtropolis boformer: / based on simtropolis forum user boformer tutorial:
//https://community.simtropolis.com/forums/topic/73487-modding-tutorial-2-road-tree-replacer/

namespace GeodataLoader.Source
{
    // standardowa ścieżka dla pliku konfiguracyjnego / default configuration file path
    [ConfigurationPath("GeodataLoader.xml")]
    public class GeodataLoaderConfiguration
    {
        // standardowa konfiguracja ustawień dla plików / default setings configuration for files
        public string inputCenterX { get; set; } = @""; // X obszaru w układzie PUWG1992 / area X in PUWG1992 coordinate system
        public string inputCenterY { get; set; } = @""; // Y obszaru w układzie PUWG1992 / area Y in PUWG1992 coordinate system
        public string BDOT10k { get; set; } = @""; // ścieżka do folderu z plikami .xml(gml) / path to folder containing .xml(gml) files
        public string DEM { get; set; } = @""; // ścieżka do folderu z plikami ASCII / path to folder containing ASCII files

        // parsowanie podanych współrzędnych do liczb i wektora / parsing input coordinates to floats and vector
        public float ParsedCenterX
        {
            get
            {
                return float.TryParse(inputCenterX, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out var res) ? res : 0f;
            }
        }

        public float ParsedCenterY
        {
            get
            {
                return float.TryParse(inputCenterY, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out var res) ? res : 0f;
            }
        }

        public Vector2 ParsedCenterXY
        {
            get { return new Vector2(ParsedCenterX, ParsedCenterY); }
        }

        public static readonly int DEMRange = (17296 / 2);
        public static readonly int DEMRangepx = 1081;

        // lista końcówek plików .xml(gml), które zostaną uwzględnione przy wczytywaniu: *{końcówka}.xml
        //------------------------------------------------------------------------------------
        // list of .xml(gml) files endings that will be considerd while loading: *{ending}.xml
        public static readonly string[] gmlFilesEnding =
        {
            "SWRS_L",
            "SWKN_L",
            "SWRM_L",
            "SKJZ_L",
            "SKDR_L",
            "SKRW_P",
            "SKRP_L",
            "SKTR_L",
            "SKPP_L",
            "SULN_L",
            "SUPR_L",
            "PTWP_A",
            "PTZB_A",
            "PTLZ_A",
            "PTRK_A",
            "PTUT_A",
            "PTTR_A",
            "PTKM_A",
            "PTGN_A",
            "PTPL_A",
            "PTSO_A",
            "PTWZ_A",
            "PTNZ_A",
            "BUBD_A",
            "BUIN_L",

            "BUHD_A",
            "BUHD_L",
            "BUHD_P",

            "BUSP_A",
            "BUSP_L",

            "BUWT_A",
            "BUWT_P",

            "BUZT_A",
            "BUZT_P",

            "BUUO_L",
            "BUZM_L",

            "BUTR_L",
            "BUTR_P",

            "BUIT_A",
            "BUIT_P",

            "BUCM_A",

            "BUIB_A",
            "BUIB_L",

            "KUMN_A",

            "KUPG_A",
            "KUPG_P",

            "KUHU_A",
            "KUHU_P",

            "KUKO_A",
            "KUKO_P",

            "KUSK_A",
            "KUHO_A",
            "KUOS_A",
            "KUOZ_A",
            "KUZA_A",
            "KUSC_A",
            "KUIK_A",
            "TCON_A",
            "TCPK_A",
            "TCPN_A",
            "TCRZ_A",
            "ADJA_A",

            "ADMS_A",
            "ADMS_P",

            "OIPR_L",
            "OIPR_P",

            "OIKM_A",
            "OIKM_L",
            "OIKM_P",

            "OIOR_A",
            "OIOR_L",
            "OIOR_P",

            "OIMK_A",
            "OISZ_A",
        };
    }
}
