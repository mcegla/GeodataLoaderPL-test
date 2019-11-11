using System.Collections.Generic;
using UnityEngine;

namespace GeodataLoader.Source.Logic
{
    //=================================================================
    //===== Klasa odpowiedzialna za obliczenia wewnątrz poligonów =====
    //-----------------------------------------------------------------
    //=== Class responsible for calculations inside of the polygons ===
    //=================================================================

    // FindMaxMin - jaggi

    class PointInPoly
    {
        // tworzenie tablicy odpowiednio rozłożonych punktów wewnątrz prostokąta ograniczającego
        //---------------------------------------------------------------------------
        // creation of point array with specified space, inside of bounding rectangle
        public static List<Vector2> CreatePointArray(Vector2 min, Vector2 max, float Dist)
        {
            var listSize = 1 + (int)((max.x - min.x + 1) * (max.y - min.y + 1) / (Dist * Dist)); // obliczenie potrzebnego wymiaru listy / calculation of needed list size
            List<Vector2> PointArray = new List<Vector2>(listSize);

            // tak długo jak wartości x i y znajdują się wewnątrz prostokąta, dodawaj kolejne wektory współrzędnych
            //--------------------------------------------------------------------------------------------
            // as long as x and y coordinates are inside the bouding rectangle add new vectors to the list
            for (float x = max.x; x > min.x; x -= Dist)
            {
                for (float y = max.y; y > min.y; y -= Dist)
                {
                    PointArray.Add(new Vector2(x, y));
                }
            }
            return PointArray;
        }

        // odnajdywanie wartości najmniejszej i największej w ramach zestawu wektorów
        //------------------------------------------
        // finding max and min value for vectors set
        public static Vector2[] FindMaxMin(IEnumerable<Vector2> vectors)
        {
            var max = new Vector2(float.MinValue, float.MinValue); // ustaw największą wartość x,y jako najmniejszą możliwą / set max x,y value as minimal possible
            var min = new Vector2(float.MaxValue, float.MaxValue); // a najmniejszą jako największą / and min as maximal possible
            
            // dla każdej wartości w zestawie wektorów sprawdź czy jest mniejsza (większa) od do tej pory istniejącej w min (max)
            //-------------------------------------------------------------------------------------------
            // foreach value in vectors set check if it is lower (higher) then current value in min (max)
            foreach (var v in vectors) 
            {
                if (v.x < min.x)
                    min.x = v.x;
                if (v.x > max.x)
                    max.x = v.x;

                if (v.y < min.y)
                    min.y = v.y;
                if (v.y > max.y)
                    max.y = v.y;
            }
            return new[] { min, max }; // zwróć najmniejsze i największe wartości x,y w zestawie wektorów / return the lowest and the highest values in vector set
        }

        // algorytm sprawdzający położenie punktu w poligonie
        //---------------------------------------------------
        // algorithm checking if point is placed inside of the polygon
        //https://wrf.ecse.rpi.edu//Research/Short_Notes/pnpoly.html
        public static bool pnpoly(Vector2[] vert,  float testx, float testy)
        {
            int i, j;
            bool c = false;
            for (i = 0, j = vert.Length - 1; i < vert.Length; j = i++)
            {
                if (((vert[i].y > testy) != (vert[j].y > testy)) &&
                 (testx < (vert[j].x - vert[i].x) * (testy - vert[i].y) / (vert[j].y - vert[i].y) + vert[i].x))
                    c = !c;
            }
            return c;
        }

        // algorytm obliczający pole poligou ze współrzędnych
        //---------------------------------------------------
        // algorithm calculating field of a polygon from coordinates
        //https://stackoverflow.com/questions/2432428/is-there-any-algorithm-for-calculating-area-of-a-shape-given-co-ordinates-that-d
        public static float PolygonArea(Vector2[] polygon)
        {
            int i, j;
            float area = 0;

            for (i = 0; i < polygon.Length; i++)
            {
                j = (i + 1) % polygon.Length;

                area += polygon[i].x * polygon[j].y;
                area -= polygon[i].y * polygon[j].x;
            }

            area /= 2;
            return (area < 0 ? -area : area);
        }

        // proste obliczanie środka ciężkości / simple center of gravity calculation
        public static Vector2 AvgPoint(Vector2[] polygon)
        {
            float sx = 0; // suma X / sum X
            float sy = 0; // suma Y / sum Y
            foreach (var entity in polygon)
            {
                sx = sx + entity.x;
                sy = sy + entity.y;
            }
            var x = sx / polygon.Length;
            var y = sy / polygon.Length;
            return new Vector2(x,y);
        }
    }
}
