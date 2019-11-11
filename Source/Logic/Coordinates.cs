using UnityEngine;

namespace GeodataLoader.Source.Logic
{
    //============================================================================
    //=== Klasa odpowiedzialna za przeliczanie współrzędnych do układu growego ===
    //----------------------------------------------------------------------------
    //==== Class responsible for coordinates recalculation to in-game system =====
    //============================================================================

    static class CoordinatesCalculator
    {
        // rzeczywiste współrzędne centrum obszaru - w grze 0,0 / real coordinates of area center - in-game 0,0
        static Vector2 Cv;

        // rozmiar granicy obszaru gry / game area boundary
        public static readonly float LowerBoundary = -8640;
        public static readonly float UpperBoundary = 8640;

        public static void InitializeCenter(Vector2 center)
        {
            Cv = center;
        }

        public static Vector2 GameXY(Vector2 coordXY)
        {
            // by przeliczyć współrzędne do układu wewnątrz growego należy odjąć rzeczywiste współrzedne środka od każdej pary współrzędnych, a następnie przemnożyć przez -1 ze względu skręt obszaru
            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // to recalcualte coordinates to in-game system we subtract center real XY from each object real XY, because of rotation of the game environment we also multiply each coordinate by -1
            return new Vector2(-(coordXY.x - Cv.x), -(coordXY.y - Cv.y));
        }

        // punkt jest wewnątrz obszaru gry, jeśli przeliczone współrzędne mieszczą sie w granicy
        //--------------------------------------------------------------------------------------
        //point is inside the game area if recalculated coordinates are inside game boundary
        public static bool IsInRange(Vector2 num) => num.x < UpperBoundary && num.x > LowerBoundary && num.y < UpperBoundary && num.y > LowerBoundary;

        // mniejszy obszar w przypadku przekroczenia liczby assetów
        //----------------------------------------------------------
        // smaller area in case of hiting asset limit
        public static bool IsInRangeSmall(Vector2 num) => num.x < (UpperBoundary - 1920) && num.x > (LowerBoundary + 1920) && num.y < (UpperBoundary - 1920) && num.y > (LowerBoundary + 1920);

        // mniejszy obszar dla surowców, o około 28-32m z każdej strony
        //----------------------------------------------------------
        // smaller area for resources - around 28-32m from each side
        public static bool IsInRangeResources(Vector2 num) => num.x < (UpperBoundary - 32) && num.x > (LowerBoundary - 32) && num.y < (UpperBoundary + 32) && num.y > (LowerBoundary - 32);
    }
}