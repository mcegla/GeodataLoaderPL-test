using System;
using System.Collections.Generic;
using System.Linq;

namespace GMLParserPL.Logic
{
    public static class EnumerableExtension
    {
        // rozdzielanie listy na mniejsze listy / spliting list to smaller lists
        public static IEnumerable<T[]> Split<T>(this IEnumerable<T> enumerable, int size)
        {
            var count = 0;
            var length = enumerable.Count();
            var res = new List<T[]>((int)Math.Ceiling((double)length / size));
            while (count * size < length)
            {
                res.Add(enumerable.Skip(count * size).Take(size).ToArray());
                count++;
            }
            return res;
        }
    }
}
