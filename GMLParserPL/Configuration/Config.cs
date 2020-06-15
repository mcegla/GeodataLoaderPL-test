using GMLParserPL.Models;
using System;
using System.Collections.Generic;

namespace GMLParserPL.Configuration
{
    public class Config
    {
        public Config() // dla porównywarki z MultiKeyDictionaries / for comparer from MultiKeyDictionaries
        {
            BUBD_A_MKDObj.K2Comparer = (v1, v2, value) => Math.Abs(value - v1) > Math.Abs(value - v2) ? v2 : v1;
            BUBD_A_MKDObj.K3Comparer = (v1, v2, value) => Math.Abs(value - v1) > Math.Abs(value - v2) ? v2 : v1;

            BUIT_A_MKDObj.K2Comparer = (v1, v2, value) => Math.Abs(value - v1) > Math.Abs(value - v2) ? v2 : v1;

            SKJZ_L_MKDObj.K1Comparer = (v1, v2, value) => Math.Abs(value - v1) > Math.Abs(value - v2) ? v2 : v1;
        }

        public virtual List<string> BdotClassesUsed { get; } = new List<string> { };

        public virtual Dictionary<string, string> BUBD_A_IIPObj { get; } = new Dictionary<string, string> { };
        public virtual MultiKeyDictionary<string, int, int, string> BUBD_A_MKDObj { get; } = new MultiKeyDictionary<string, int, int, string> { };
        public virtual Dictionary<string, string> BUBD_A_FunObj { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> BUBD_A_Obj { get; } = new Dictionary<string, string> { };

        public virtual Dictionary<string, string> BUCM_A_IIPObj { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> BUCM_A_Obj { get; } = new Dictionary<string, string> { };

        public virtual Dictionary<string, string> BUHD_L_IIPObj_Net { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> BUHD_L_Obj_Net { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, Tuple<string, int>> BUHD_L_IIPObjSize_Prop { get; } = new Dictionary<string, Tuple<string, int>> { };
        public virtual Dictionary<string, Tuple<string, int>> BUHD_L_ObjSize_Prop { get; } = new Dictionary<string, Tuple<string, int>> { };

        public virtual Dictionary<string, string> BUIB_A_IIPObj_Building { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> BUIB_A_Obj_Building { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> BUIB_A_IIPObj_Prop { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> BUIB_A_Obj_Prop { get; } = new Dictionary<string, string> { };

        public virtual Dictionary<string, string> BUIB_L_IIPObj_Net { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> BUIB_L_Obj_Net { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, Tuple<string, int>> BUIB_L_IIPObjSize_Prop { get; } = new Dictionary<string, Tuple<string, int>> { };
        public virtual Dictionary<string, Tuple<string, int>> BUIB_L_ObjSize_Prop { get; } = new Dictionary<string, Tuple<string, int>> { };

        public virtual Dictionary<string, string> BUIT_A_IIPObj { get; } = new Dictionary<string, string> { };
        public virtual MultiKeyDictionary<string, int, string> BUIT_A_MKDObj { get; } = new MultiKeyDictionary<string, int, string> { };
        public virtual Dictionary<string, string> BUIT_A_Obj { get; } = new Dictionary<string, string> { };

        public virtual Dictionary<string, string> BUIT_P_IIPObj_Building { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> BUIT_P_Obj_Building { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> BUIT_P_IIPObj_Prop { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> BUIT_P_Obj_Prop { get; } = new Dictionary<string, string> { };

        public virtual Dictionary<string, string> BUSP_A_IIPObj { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> BUSP_A_Obj { get; } = new Dictionary<string, string> { };

        public virtual Dictionary<string, string> BUSP_L_IIPObj { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> BUSP_L_Obj { get; } = new Dictionary<string, string> { };

        public virtual Dictionary<string, string> BUTR_L_IIPObj_Net { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> BUTR_L_Obj_Net { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, Tuple<string, int>> BUTR_L_IIPObjSize_Prop { get; } = new Dictionary<string, Tuple<string, int>> { };
        public virtual Dictionary<string, Tuple<string, int>> BUTR_L_ObjSize_Prop { get; } = new Dictionary<string, Tuple<string, int>> { };

        public virtual Dictionary<string, string> BUTR_P_IIPObj_Building { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> BUTR_P_Obj_Building { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> BUTR_P_IIPObj_Prop { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> BUTR_P_Obj_Prop { get; } = new Dictionary<string, string> { };

        public virtual Dictionary<string, string> BUUO_L_IIPObj_Net { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> BUUO_L_Obj_Net { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, Tuple<string, int>> BUUO_L_IIPObjSize_Prop { get; } = new Dictionary<string, Tuple<string, int>> { };
        public virtual Dictionary<string, Tuple<string, int>> BUUO_L_ObjSize_Prop { get; } = new Dictionary<string, Tuple<string, int>> { };

        public virtual Dictionary<string, string> BUWT_A_IIPObj_Building { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> BUWT_A_Obj_Building { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> BUWT_A_IIPObj_Prop { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> BUWT_A_Obj_Prop { get; } = new Dictionary<string, string> { };

        public virtual Dictionary<string, string> BUWT_P_IIPObj_Building { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> BUWT_P_Obj_Building { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> BUWT_P_IIPObj_Prop { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> BUWT_P_Obj_Prop { get; } = new Dictionary<string, string> { };

        public virtual Dictionary<string, string> BUZT_A_IIPObj { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> BUZT_A_Obj { get; } = new Dictionary<string, string> { };

        public virtual Dictionary<string, string> BUZT_P_IIPObj_Building { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> BUZT_P_Obj_Building { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> BUZT_P_IIPObj_Prop { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> BUZT_P_Obj_Prop { get; } = new Dictionary<string, string> { };

        public virtual Dictionary<string, string> OIKM_A_IIPObj_Building { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> OIKM_A_Obj_Building { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> OIKM_A_IIPObj_Prop { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> OIKM_A_Obj_Prop { get; } = new Dictionary<string, string> { };

        public virtual Dictionary<string, string> OIKM_L_IIPObj_Net { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> OIKM_L_Obj_Net { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, Tuple<string, int>> OIKM_L_IIPObjSize_Prop { get; } = new Dictionary<string, Tuple<string, int>> { };
        public virtual Dictionary<string, Tuple<string, int>> OIKM_L_ObjSize_Prop { get; } = new Dictionary<string, Tuple<string, int>> { };

        public virtual Dictionary<string, string> OIKM_P_IIPObj_Building { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> OIKM_P_Obj_Building { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> OIKM_P_IIPObj_Prop { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> OIKM_P_Obj_Prop { get; } = new Dictionary<string, string> { };

        public virtual List<int> OIMK_A_FlowInOut { get; } = new List<int> { };

        public virtual Dictionary<string, string> OIOR_A_IIPObj_Building { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> OIOR_A_Obj_Building { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> OIOR_A_IIPObj_Prop { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> OIOR_A_Obj_Prop { get; } = new Dictionary<string, string> { };

        public virtual Dictionary<string, string> OIOR_L_IIPObj_Net { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> OIOR_L_Obj_Net { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, Tuple<string, int>> OIOR_L_IIPObjSize_Prop { get; } = new Dictionary<string, Tuple<string, int>> { };
        public virtual Dictionary<string, Tuple<string, int>> OIOR_L_ObjSize_Prop { get; } = new Dictionary<string, Tuple<string, int>> { };

        public virtual Dictionary<string, string> OIOR_P_IIPObj_Building { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> OIOR_P_Obj_Building { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> OIOR_P_IIPObj_Prop { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> OIOR_P_Obj_Prop { get; } = new Dictionary<string, string> { };

        public virtual Dictionary<string, string> OIPR_L_IIPObj_Net { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> OIPR_L_Obj_Net { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, Tuple<string, int>> OIPR_L_IIPObjSize_Prop { get; } = new Dictionary<string, Tuple<string, int>> { };
        public virtual Dictionary<string, Tuple<string, int>> OIPR_L_ObjSize_Prop { get; } = new Dictionary<string, Tuple<string, int>> { };
        public virtual Dictionary<string, Tuple<string, int>> OIPR_L_IIPObjSize_Tree { get; } = new Dictionary<string, Tuple<string, int>> { };
        public virtual Dictionary<string, Tuple<string, int>> OIPR_L_ObjSize_Tree { get; } = new Dictionary<string, Tuple<string, int>> { };

        public virtual Dictionary<string, string> OIPR_P_IIPObj_Building { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> OIPR_P_Obj_Building { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> OIPR_P_IIPObj_Prop { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> OIPR_P_Obj_Prop { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> OIPR_P_IIPObj_Tree { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> OIPR_P_Obj_Tree { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, int[]> OIPR_P_IIPFlowInOut { get; } = new Dictionary<string, int[]> { };
        public virtual Dictionary<string, int[]> OIPR_P_ObjFlowInOut { get; } = new Dictionary<string, int[]> { };

        public virtual Dictionary<string, string> OISZ_A_IIPObj { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> OISZ_A_Obj { get; } = new Dictionary<string, string> { };

        public virtual Dictionary<string, string> PTGN_A_IIPObj { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> PTGN_A_Obj { get; } = new Dictionary<string, string> { };

        public virtual Dictionary<string, string> PTLZ_A_IIPObj { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> PTLZ_A_SpeciesObj { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> PTLZ_A_Obj { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, float> PTLZ_A_GridSize { get; } = new Dictionary<string, float> { };

        public virtual Dictionary<string, string> PTNZ_A_IIPObj { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> PTNZ_A_Obj { get; } = new Dictionary<string, string> { };

        public virtual Dictionary<string, string> PTPL_A_IIPObj { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> PTPL_A_Obj { get; } = new Dictionary<string, string> { };

        public virtual Dictionary<string, string> PTRK_A_IIPObj { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> PTRK_A_Obj { get; } = new Dictionary<string, string> { };

        public virtual Dictionary<string, string> PTSO_A_IIPObj { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> PTSO_A_Obj { get; } = new Dictionary<string, string> { };

        public virtual Dictionary<string, string> PTTR_A_IIPObj { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> PTTR_A_Obj { get; } = new Dictionary<string, string> { };

        public virtual Dictionary<string, string> PTUT_A_IIPObj_Building { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> PTUT_A_Obj_Building { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> PTUT_A_IIPObj_Prop { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> PTUT_A_Obj_Prop { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, Tuple<string, int>> PTUT_A_IIPObj_PropGridSize { get; } = new Dictionary<string, Tuple<string, int>> { };
        public virtual Dictionary<string, Tuple<string, int>> PTUT_A_SpeciesObj_PropGridSize { get; } = new Dictionary<string, Tuple<string, int>> { };
        public virtual Dictionary<string, Tuple<string, int>> PTUT_A_Obj_PropGridSize { get; } = new Dictionary<string, Tuple<string, int>> { };
        public virtual Dictionary<string, Tuple<string, int>> PTUT_A_IIPObj_TreeGridSize { get; } = new Dictionary<string, Tuple<string, int>> { };
        public virtual Dictionary<string, Tuple<string, int>> PTUT_A_SpeciesObj_TreeGridSize { get; } = new Dictionary<string, Tuple<string, int>> { };
        public virtual Dictionary<string, Tuple<string, int>> PTUT_A_Obj_TreeGridSize { get; } = new Dictionary<string, Tuple<string, int>> { };

        public virtual List<int> PTWP_A_FlowInOut { get; } = new List<int> { };

        public virtual Dictionary<string, string> PTWZ_A_IIPObj { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> PTWZ_A_Obj { get; } = new Dictionary<string, string> { };

        public virtual Dictionary<string, string> SKJZ_L_IIPObj { get; } = new Dictionary<string, string> { };
        public virtual MultiKeyDictionary<int, string, string> SKJZ_L_MKDObj { get; } = new MultiKeyDictionary<int, string, string> { };
        public virtual Dictionary<string, string> SKJZ_L_Obj { get; } = new Dictionary<string, string> { };

        public virtual Dictionary<string, string> SKRP_L_IIPObj { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, Dictionary<string, string>> SKRP_L_BikesPavement { get; } = new Dictionary<string, Dictionary<string, string>> { };
        public virtual Dictionary<string, string> SKRP_L_Obj { get; } = new Dictionary<string, string> { };

        public virtual Dictionary<string, string> SKTR_L_IIPObj { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> SKTR_L_Obj { get; } = new Dictionary<string, string> { };

        public virtual Dictionary<string, string> SULN_L_IIPObj { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> SULN_L_Obj { get; } = new Dictionary<string, string> { };

        public virtual Dictionary<string, string> SUPR_L_IIPObj { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> SUPR_L_Obj { get; } = new Dictionary<string, string> { };

        public virtual Dictionary<string, string> SWKN_L_IIPObj { get; } = new Dictionary<string, string> { };
        public virtual Dictionary<string, string> SWKN_L_Obj { get; } = new Dictionary<string, string> { };
    }
}