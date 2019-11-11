using UnityEngine;

//=============================================================
//=== Klasa odpowiedzialna za tworzenie i usuwanie surowców ===
//-------------------------------------------------------------
//=== Class responsible for creating and deleting resources ===
//=============================================================

// tworzenie i usuwanie bazuje na: / creating and deleting is based on:
//https://github.com/tomarus/cs-terraingen/blob/master/TerrainGen.cs
    
namespace GeodataLoader.Source.Factories
{ 
    public class ResourceFactory
    {
        NaturalResourceManager _naturalRM = NaturalResourceManager.instance;

        public void CreateFertileLand(Vector2 Point)
        {
            var y = (int)((Point.x + 8608) / 33.625); // pixel y; 8608=(17280-32*2)/2; 33.625=17216/512
            var x = (int)((Point.y + 8608) / 33.625); // pixel x; 8608=(17280-32*2)/2; 33.625=17216/512
            if (x < 512 && y < 512 && x >= 0 && y >= 0)
            {
                var cellpos = x * 512 + y;
                _naturalRM.m_naturalResources[cellpos].m_fertility = 255;

                _naturalRM.m_naturalResources[cellpos].m_modified = 0xff;
            }
        }
        public void CreateSand(Vector2 Point)
        {
            var y = (int)((Point.x + 8608) / 33.625); // pixel y; 8608=(17280-32*2)/2; 33.625=17216/512
            var x = (int)((Point.y + 8608) / 33.625); // pixel x; 8608=(17280-32*2)/2; 33.625=17216/512
            if (x < 512 && y < 512 && x >= 0 && y >= 0)
            {
                var cellpos = x * 512 + y;
                _naturalRM.m_naturalResources[cellpos].m_sand = 255;

                _naturalRM.m_naturalResources[cellpos].m_modified = 0xff;
            }
        }
        public void CreateOil(Vector2 Point)
        {
            var y = (int)((Point.x + 8608) / 33.625); // pixel y; 8608=(17280-32*2)/2; 33.625=17216/512
            var x = (int)((Point.y + 8608) / 33.625); // pixel x; 8608=(17280-32*2)/2; 33.625=17216/512
            if (x < 512 && y < 512 && x >= 0 && y >= 0)
            {
                var cellpos = x * 512 + y;
                _naturalRM.m_naturalResources[cellpos].m_oil = 255;

                _naturalRM.m_naturalResources[cellpos].m_modified = 0xff;
            }
        }
        public void CreateOre(Vector2 Point)
        {
            var y = (int)((Point.x + 8608) / 33.625); // pixel y; 8608=(17280-32*2)/2; 33.625=17216/512
            var x = (int)((Point.y + 8608) / 33.625); // pixel x; 8608=(17280-32*2)/2; 33.625=17216/512
            if (x < 512 && y < 512 && x >= 0 && y >= 0)
            {
                var cellpos = x * 512 + y;
                _naturalRM.m_naturalResources[cellpos].m_ore = 255;

                _naturalRM.m_naturalResources[cellpos].m_modified = 0xff;
            }
        }

        public void DropResources()
        {
            for (int i = 0; i < 512; i++) // 512 = grid max
            {
                for (int j = 0; j < 512; j++) // 512 = grid max
                {
                    var cellpos = j * 512 + i;
                    _naturalRM.m_naturalResources[cellpos].m_oil = 0;
                    _naturalRM.m_naturalResources[cellpos].m_ore = 0;
                    _naturalRM.m_naturalResources[cellpos].m_fertility = 0;
                    _naturalRM.m_naturalResources[cellpos].m_forest = 0;
                    _naturalRM.m_naturalResources[cellpos].m_tree = 0;
                    _naturalRM.m_naturalResources[cellpos].m_sand = 0;
                    _naturalRM.m_naturalResources[cellpos].m_shore = 0;

                    _naturalRM.m_naturalResources[cellpos].m_modified = 0xff;
                }
            }
        }
    }
}
