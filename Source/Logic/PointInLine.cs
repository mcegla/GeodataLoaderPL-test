using System;
using System.Collections.Generic;
using UnityEngine;

namespace GeodataLoader.Source.Logic
{
    //==============================================================
    //======== Klasa odpowiedzialna za obliczenia w liniach ========
    //--------------------------------------------------------------
    //=== Class responsible for additional calcualtions in lines ===
    //==============================================================

    class PointInLine
    {
        // tworzenie dodatkowych punktów w linii / creation of additional points in line
        public static List<Vector2> CreatePointsInLine(Vector2 P1, Vector2 P2, float PropSize)
        {
            float dX = P2.x - P1.x; // różnica współrzędnej x między punktami / x coordinate difference between points
            float dY = P2.y - P1.y; // różnica współrzędnej y między punktami / y coordinate difference between points
            float dP1P2 = (float)Math.Sqrt(Math.Pow(dX,2)+ Math.Pow(dY, 2)); // odległośc między punktami / distance between points
            int numberOfProps = (int) ((dP1P2 / PropSize) + 1); // ilość elementów które zmieszczą się w danej długości + 1 by domknąć / number of elements that could be placed in such distance + 1 to close up any gaps
            List<Vector2> PointsInLine = new List<Vector2>(numberOfProps); // lista wektorów współrzędnych nie bedzie dłuższa niż max ilość elementów / list of coordinates vectors won't be longer than max number of elements 
            
            for (float D = PropSize / 2; D <= (dP1P2-(PropSize/2)); D += PropSize) // pierwszy punkt będzie sie znajdował w połowie wielkości elementu / first point will be in half of the element size 
            {
                // proste przeliczenie współrzędnych w lini między dwoma punktami np. jak w metodzie domiarów prostokątnych
                //---------------------------------------------------------------------------------------------------
                // simple coordinates reclaculation in line between two points for example as in survey offset method
                float Xn = P1.x + (D * (dX / dP1P2)); 
                float Yn = P1.y + (D * (dY / dP1P2));
                PointsInLine.Add(new Vector2(Xn,Yn));
            }
            // jako, że ostatni element najprawdopodobniej nie wyląduje idealnie na końcu odcinka, dodajemy jeszcze 1 od końca
            //--------------------------------------------------------------------------------------------------------------
            // the last element most probably won't end ideally in segment end, so we add 1 more created from the segment end 
            float Xend = P2.x - ((PropSize / 2) * (dX / dP1P2));
            float Yend = P2.y - ((PropSize / 2) * (dY / dP1P2));
            PointsInLine.Add(new Vector2(Xend, Yend));
            return PointsInLine;
        }

        // obliczanie iloczynu wektorowego / vector product calculation
        public static float VectorProduct (Vector2 SegmentP1, Vector2 SegmentP2, Vector2 Point)
        {
            var vp = (SegmentP2.x - SegmentP1.x) * (Point.y - SegmentP1.y) - (Point.x - SegmentP1.x) * (SegmentP2.y - SegmentP1.y);
            return vp;
        }

        // obliczanie azymutu / calculating of azimuth
        public static float Azimuth (Vector2 P1, Vector2 P2)
        {
            double dX = P2.x - P1.x; // różnica współrzędnej x między punktami / x coordinate difference between points
            double dY = P2.y - P1.y; // różnica współrzędnej y między punktami / y coordinate difference between points
            double phi = Math.Atan2(dY , dX) + Math.PI; // azymut + pi ze względu na skręt obszaru gry / azimuth + pi because of game area rotation
            return (float)phi;
        }
    }
}
