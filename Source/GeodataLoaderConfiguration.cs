namespace GeodataLoader//.Source
{
    // Default configuration path
    [ConfigurationPath("GeodataLoader.xml")]
    public class GeodataLoaderConfiguration
    {
        // Default configuration for setings XML file
        public string inputCenterX { get; set; } = @"560730.55";
        public string inputCenterY { get; set; } = @"576278.95";
        public string BDOT10k { get; set; } = @"C:\Users\DELL\Desktop\MGR\Zuromin\BDOT 10k\mazowieckie_pow_zurominski_1437\PL.PZGiK.330.1437\BDOT10K\";
        public string NMT { get; set; } = @"";
        public string Budynki3D { get; set; } = @"";

        public float ParsedCenterX
        {
            get
            {
                return float.TryParse(inputCenterX, out var res) ? res : 0f;
            }
        }

        public float ParsedCenterY
        {
            get
            {
                return float.TryParse(inputCenterY, out var res) ? res : 0f;
            }
        }

        // List of .gml files endings that will be considerd: *{ ending}.xml
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
