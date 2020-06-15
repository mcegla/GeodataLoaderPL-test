using UnityEngine;


namespace GeodataLoaderPL.Factories
{
    class ResourceFactory
    {
        public int Temp { get; private set; }

        public ResourceFactory()
        {
            Temp = 0;
        }
        NaturalResourceManager _naturalRM = NaturalResourceManager.instance;
        public void CreateResource(Vector2 Point, NaturalResourceManager.Resource resource)
        {
            var y = (int)((Point.x + 8608) / 33.625); // pixel y; 8608=(17280-32*2)/2; 33.625=17216/512
            var x = (int)((Point.y + 8608) / 33.625); // pixel x; 8608=(17280-32*2)/2; 33.625=17216/512
            if (x < 512 && y < 512 && x >= 0 && y >= 0)
            {
                var cellpos = x * 512 + y;

                switch (resource)
                {
                    case NaturalResourceManager.Resource.Fertility:
                        _naturalRM.m_naturalResources[cellpos].m_fertility = 255;
                        _naturalRM.m_naturalResources[cellpos].m_modified = 0xff;
                        break;

                    case NaturalResourceManager.Resource.Sand:
                        _naturalRM.m_naturalResources[cellpos].m_sand = 255;
                        _naturalRM.m_naturalResources[cellpos].m_modified = 0xff;
                        break;

                    case NaturalResourceManager.Resource.Oil:
                        _naturalRM.m_naturalResources[cellpos].m_oil = 255;
                        _naturalRM.m_naturalResources[cellpos].m_modified = 0xff;
                        break;

                    case NaturalResourceManager.Resource.Ore:
                        _naturalRM.m_naturalResources[cellpos].m_ore = 255;
                        _naturalRM.m_naturalResources[cellpos].m_modified = 0xff;
                        break;

                    default:
                        _naturalRM.m_naturalResources[cellpos].m_oil = 0;
                        _naturalRM.m_naturalResources[cellpos].m_ore = 0;
                        _naturalRM.m_naturalResources[cellpos].m_fertility = 0;
                        _naturalRM.m_naturalResources[cellpos].m_forest = 0;
                        _naturalRM.m_naturalResources[cellpos].m_tree = 0;
                        _naturalRM.m_naturalResources[cellpos].m_sand = 0;
                        _naturalRM.m_naturalResources[cellpos].m_shore = 0;
                        break;
                }
                Temp++;
            }
        }
    }
}
