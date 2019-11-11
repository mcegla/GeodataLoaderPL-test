using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;

//==============================================================================================
//=== This part of code is responsible for calculating in-game coordinates from given center ===
//==============================================================================================

namespace GeodataLoader.Source
{
    static class CoordinatesCalculator
    {
        // Real coordinates of area center
        static float Cx;
        static float Cy;

        public static readonly float LowerBoundary = -8640;
        public static readonly float UpperBoundary = 8640;

        public static void InitializeCenter(float cx, float cy)
        {
            Cx = cx;
            Cy = cy;
        }

        // Cache empty array so new one is not created every time
        private static float[] _empty = new float[0];

        public static float[] GameXY(float[] coordXY)
        {
            // If length is not 2 this is not array of known coordinates so we've no idea what to do with it
            if (coordXY.Length != 2)
                return _empty;
            return new[] { -(coordXY[0] - Cx), -(coordXY[1] - Cy) };
        }

        public static bool AllInRange(float[] nums) { return nums.All(IsInRange); }

        public static bool IsInRange(float num) => num < UpperBoundary && num > LowerBoundary;

        public static float[] GameXY(float coordX, float coordY)
        {
            // To recalcualte coordinates to in-game system we subtract center real XY from each object real XY
            return new[] { -(coordX - Cx), -(coordY - Cy) };
        }
    }
}