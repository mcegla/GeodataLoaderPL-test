using System.Numerics;

namespace GMLParserPL.Logic
{
    internal static class CoordinatesCalc
    {
        private static Vector2 GameXY(Vector2 pointRealXY, Vector2 centerRealXY)
        {
            return new Vector2(-(pointRealXY.X - centerRealXY.X),
                -(pointRealXY.Y - centerRealXY.Y));
        }

        public static Vector2 GameXY(Vector2 pointRealXY)
            => GameXY(pointRealXY, GMLParserPL.CenterRealXY);

        private static bool IsInRange(Vector2 pointRealXY, float sideLength)
            => pointRealXY.X < sideLength / 2
            && pointRealXY.X > -sideLength / 2
            && pointRealXY.Y < sideLength / 2
            && pointRealXY.Y > -sideLength / 2;

        public static bool IsInRange(Vector2 pointRealXY)
            => IsInRange(pointRealXY, GMLParserPL.SideLength);

        //Mniejszy obszar dla surowców, o około 28-32m z każdej strony
        //---------------------------------------------------------
        //Smaller area for resources - around 28-32m from each side
        public static bool IsInResourceRange(Vector2 pointRealXY)
            => IsInRange(pointRealXY, GMLParserPL.SideLength - 32);
    }
}
