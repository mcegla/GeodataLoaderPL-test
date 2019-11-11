using GeodataLoader.Source.BDOT10kTranslator;

//====================================================================================================
//=== Ta część kodu jest odpowiedzialna za przywoływanie klas konwertujących dane na obiekty Unity ===
//----------------------------------------------------------------------------------------------------
//===== This part of code is responsible for calling all classes converting BDOT10k to Unity obj. ====
//====================================================================================================
//http://prawo.sejm.gov.pl/isap.nsf/download.xsp/WDU20112791642/O/D20111642-02.pdf

namespace GeodataLoader.Source
{
    public class BDOT10kToObj
    {
        // wczytanie konfiguracji z przygotowanego .xml / loading configuration from .xml
        GeodataLoaderConfiguration config;
        public BDOT10kToObj()
        {
            config = Configuration<GeodataLoaderConfiguration>.Load();
        }

        // odwołanie do wszystkich niezbędnych translatorów / calls to all needed translators
        public void BDOT10k()
        {
            SKJZ_L_T sKJZ_L_T = new SKJZ_L_T();
            sKJZ_L_T.SKJZ_L(config);
            SKRP_L_T sKRP_L_T = new SKRP_L_T();
            sKRP_L_T.SKRP_L(config);
            SULN_L_T sULN_L_T = new SULN_L_T();
            sULN_L_T.SULN_L(config);
            BUIB_L_T bUIB_L_T = new BUIB_L_T();
            bUIB_L_T.BUIB_L(config);

            BUHD_L_T bUHD_L_T = new BUHD_L_T();
            bUHD_L_T.BUHD_L(config);
            BUUO_L_T bUUO_L_T = new BUUO_L_T();
            bUUO_L_T.BUUO_L(config);
            OIOR_L_T oIOR_L_T = new OIOR_L_T();
            oIOR_L_T.OIOR_L(config);

            BUTR_L_T bUTR_L_T = new BUTR_L_T();
            bUTR_L_T.BUTR_L(config);
            OIPR_L_T oIPR_L_T = new OIPR_L_T();
            oIPR_L_T.OIPR_L(config);


            BUBD_A_T bUBD_A_T = new BUBD_A_T();
            bUBD_A_T.BUBD_A(config);
            PTLZ_A_T pTLZ_A_T = new PTLZ_A_T();
            pTLZ_A_T.PTLZ_A(config);
            PTUT_A_T pTUT_A_T = new PTUT_A_T();
            pTUT_A_T.PTUT_A(config);
            PTRK_A_T pTRK_A_T = new PTRK_A_T();
            pTRK_A_T.PTRK_A(config);
            OIMK_A_T oIMK_A_T = new OIMK_A_T();
            oIMK_A_T.OIMK_A(config);
            BUSP_A_T bUSP_A_T = new BUSP_A_T();
            bUSP_A_T.BUSP_A(config);
            BUCM_A_T bUCM_A_T = new BUCM_A_T();
            bUCM_A_T.BUCM_A(config);
            PTSO_A_T pTSO_A_T = new PTSO_A_T();
            pTSO_A_T.PTSO_A(config);
            BUIB_A_T bUIB_A_T = new BUIB_A_T();
            bUIB_A_T.BUIB_A(config);
            BUIT_A_T bUIT_A_T = new BUIT_A_T();
            bUIT_A_T.BUIT_A(config);
            OIOR_A_T oIOR_A_T = new OIOR_A_T();
            oIOR_A_T.OIOR_A(config);


            BUIT_P_T bUIT_P_T = new BUIT_P_T();
            bUIT_P_T.BUIT_P(config);
            BUWT_P_T bUWT_P_T = new BUWT_P_T();
            bUWT_P_T.BUWT_P(config);
            BUZT_P_T bUZT_P_T = new BUZT_P_T();
            bUZT_P_T.BUZT_P(config);
            OIKM_P_T oIKM_P_T = new OIKM_P_T();
            oIKM_P_T.OIKM_P(config);
            OIOR_P_T oIOR_P_T = new OIOR_P_T();
            oIOR_P_T.OIOR_P(config);
            OIPR_P_T oIPR_P_T = new OIPR_P_T();
            oIPR_P_T.OIPR_P(config);


            PTTR_A_T pTTR_A_T = new PTTR_A_T();
            pTTR_A_T.PTTR_A(config);
            PTGN_A_T pTGN_A_T = new PTGN_A_T();
            pTGN_A_T.PTGN_A(config);
            PTWZ_A_T pTWZ_A_T = new PTWZ_A_T();
            pTWZ_A_T.PTWZ_A(config);
            PTNZ_A_T pTNZ_A_T = new PTNZ_A_T();
            pTNZ_A_T.PTNZ_A(config);
            PTPL_A_T pTPL_A_T = new PTPL_A_T();
            pTPL_A_T.PTPL_A(config);

            PTWP_A_T pTWP_A_T = new PTWP_A_T();
            pTWP_A_T.PTWP_A(config);
        }
    }
}