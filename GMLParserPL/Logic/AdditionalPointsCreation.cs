using System;
using System.Collections.Generic;
using System.Numerics;

namespace GMLParserPL.Logic
{
    internal static class AdditionalPointsCreation
    {
        /// <summary>
        ///     Metoda odpowiedzialna za tworzenie dodatkowych punktów w lnii np. płotu, wykorzystywana gdy obiekty liniowe reprezentowane są punktami
        ///     <para />
        ///     Method responsible for creating additional points in line e.g. fences, used when linear object is represented by points
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <param name="propSize"></param>
        /// <returns></returns>
        public static List<Vector2> CreatePointsInLine(Vector2 point1, Vector2 point2, float propSize)
        {
            float dX = point2.X - point1.X;
            float dY = point2.Y - point1.Y;
            float dP1P2 = (float)Math.Sqrt(Math.Pow(dX, 2) + Math.Pow(dY, 2));
            // ilość elementów które zmieszczą się w danej długości + 1 by domknąć / number of elements that could be placed in such distance + 1 to close up any gaps
            int numberOfProps = (int)((dP1P2 / propSize) + 1);
            List<Vector2> PointsInLine = new List<Vector2>(numberOfProps);

            // pierwszy punkt będzie sie znajdował w połowie wielkości elementu / first point will be placed in half of the element size 
            for (float dist = propSize / 2; dist <= (dP1P2 - (propSize / 2)); dist += propSize)
            {
                // proste przeliczenie współrzędnych w lini między dwoma punktami np. jak w metodzie domiarów prostokątnych
                //---------------------------------------------------------------------------------------------------
                // simple coordinates reclaculation in line between two points for example as in survey offset method
                float xn = point1.X + (dist * (dX / dP1P2));
                float yn = point1.Y + (dist * (dY / dP1P2));
                PointsInLine.Add(new Vector2(xn, yn));
            }
            // jako, że ostatni element najprawdopodobniej nie "wyląduje" idealnie na końcu odcinka, dodajemy jeszcze 1 od końca
            //--------------------------------------------------------------------------------------------------------------
            // as the last element most probably won't end in the segment end, we need to add 1 more element from that segment end 
            float xend = point2.X - ((propSize / 2) * (dX / dP1P2));
            float yend = point2.Y - ((propSize / 2) * (dY / dP1P2));
            PointsInLine.Add(new Vector2(xend, yend));
            return PointsInLine;
        }

        /// <summary>
        ///     Tworzenie tablicy odpowiednio rozłożonych punktów wewnątrz prostokąta ograniczającego
        ///     <para />
        ///     Creation of point array with specified space, inside of bounding rectangle
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="dist"></param>
        /// <returns></returns>
        public static List<Vector2> CreatePointArray(Vector2 min, Vector2 max, float dist)
        {
            var listSize = 1 + (int)((max.X - min.X + 1) * (max.Y - min.Y + 1) / (dist * dist));
            List<Vector2> PointArray = new List<Vector2>(listSize);

            // tak długo jak wartości x i y znajdują się wewnątrz prostokąta, dodawaj kolejne wektory współrzędnych
            //--------------------------------------------------------------------------------------------
            // as long as x and y coordinates are inside the bouding rectangle add new vectors to the list
            for (float x = max.X; x > min.X; x -= dist)
            {
                for (float y = max.Y; y > min.Y; y -= dist)
                {
                    PointArray.Add(new Vector2(x, y));
                }
            }
            return PointArray;
        }
    }
}