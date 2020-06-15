using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace GMLParserPL.Logic
{
    /// <summary>
    ///     Klasa odpowiedzialna za implementacje algorytmu Douglas'a–Peucker'a
    ///     <para />
    ///     Class responsible for implementation of Douglas–Peucker algorithm
    ///     <see cref="https://www.codeproject.com/Articles/18936/A-C-Implementation-of-Douglas-Peucker-Line-Appro"/>
    /// </summary>
    internal static class DouglasPointsReduction
    {
        /// <summary>
        ///     Uses the Douglas Peucker algorithm to reduce the number of points.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <returns></returns>
        public static List<Vector2> Reduct(List<Vector2> Points, Double Tolerance)
        {
            double Tolerancesqrd = Tolerance * Tolerance;
            if (Points == null || Points.Count < 3)
                return Points;

            Int32 firstPoint = 0;
            Int32 lastPoint = Points.Count - 1;
            List<Int32> pointIndexsToKeep = new List<Int32>();

            //Add the first and last index to the keepers
            pointIndexsToKeep.Add(firstPoint);
            pointIndexsToKeep.Add(lastPoint);


            //The first and the last point can not be the same
            while (lastPoint >= 0 && Points[firstPoint].Equals(Points[lastPoint]))
            {
                lastPoint--;
            }
            if (lastPoint == 0) { return Points; }

            SortedDictionary<Int32, Int32> PairsIndexesToCheck = new SortedDictionary<Int32, Int32>();
            PairsIndexesToCheck.Add(firstPoint, lastPoint);
            Int32 checkcnt = 0;
            while (PairsIndexesToCheck.Count > 0 && checkcnt < lastPoint)
            {
                Double maxDistancesqrd = 0;
                Int32 indexFarthest = 0, currentFirstPoint = PairsIndexesToCheck.First().Key, currentLastPoint = PairsIndexesToCheck.First().Value;
                double deltax = Points[currentFirstPoint].X - Points[currentLastPoint].X,
                deltay = Points[currentFirstPoint].Y - Points[currentLastPoint].Y;
                Double oneoverbottomsqrd = 1 / (deltax * deltax + deltay * deltay),
                 x1y2 = Points[currentFirstPoint].X * Points[currentLastPoint].Y,
                 x2y1 = Points[currentFirstPoint].Y * Points[currentLastPoint].X,
                 x1y2_diff_x2y1 = x1y2 - x2y1;
                ;
                for (Int32 index = currentFirstPoint + 1; index < currentLastPoint; index++)
                {
                    Double distancesqrd = PerpendicularDistance(Points[currentFirstPoint], Points[currentLastPoint], Points[index], oneoverbottomsqrd, x1y2_diff_x2y1, deltax, deltay);
                    if (distancesqrd > maxDistancesqrd)
                    {
                        maxDistancesqrd = distancesqrd;
                        indexFarthest = index;
                    }
                }

                if (maxDistancesqrd > Tolerancesqrd)
                {
                    //Add the largest point that exceeds the tolerance
                    pointIndexsToKeep.Add(indexFarthest);
                    //split current pair
                    PairsIndexesToCheck[currentFirstPoint] = indexFarthest;
                    PairsIndexesToCheck.Add(indexFarthest, currentLastPoint);

                }
                else
                {
                    //pair is checked
                    PairsIndexesToCheck.Remove(currentFirstPoint);
                }
                checkcnt++;
            }
            if (checkcnt == lastPoint) { return Points; }


            List<Vector2> returnPoints = new List<Vector2>();
            pointIndexsToKeep.Sort();

            foreach (Int32 index in pointIndexsToKeep)
            {
                returnPoints.Add(Points[index]);
            }

            return returnPoints;
        }


        public static Double PerpendicularDistance(Vector2 Point1, Vector2 Point2, Vector2 Point, Double oneoverbottomsqrd, Double x1y2_diff_x2y1, Double deltax, Double deltay)
        {
            //Area = |(1/2)(x1y2 + x2y3 + x3y1 - x2y1 - x3y2 - x1y3)|   *Area of triangle
            //Base = √((x1-x2)²+(x1-x2)²)                               *Base of Triangle*
            //Area = .5*Base*H                                          *Solve for height
            //Height = Area/.5/Base

            Double area = Math.Abs(x1y2_diff_x2y1 - Point.Y * deltax + Point.X * deltay);
            Double areasqrd = area * area;

            Double heightsqrd = areasqrd * oneoverbottomsqrd;

            return heightsqrd;
        }
    }
}
