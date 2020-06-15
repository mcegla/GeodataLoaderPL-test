using System;
using System.Collections.Generic;
using System.Linq;

namespace GMLParserPL.Models
{
    // bazuje na: / based on:
    //https://stackoverflow.com/questions/1171812/multi-key-dictionary-in-c

    // GetByKeyOrClosest - jaggi, 
    // Refactoring is needed
    public class MultiKeyDictionary<K1, K2, V> : Dictionary<K1, Dictionary<K2, V>>
    {
        public V this[K1 key1, K2 key2]
        {
            get
            {
                return ContainsKey(key1) ? this[key1][key2] : default(V);
            }
            set
            {
                if (!ContainsKey(key1))
                    this[key1] = new Dictionary<K2, V>();
                this[key1][key2] = value;
            }
        }
        protected Dictionary<K2, V> GetByKeyOrClosest(K1 key1)
        {
            if (ContainsKey(key1)) return this[key1];
            else if (K1Comparer == null) throw new KeyNotFoundException("Key not found and comparer not set [k1]");
            var closest = Keys.Aggregate((x, y) => K1Comparer(x, y, key1));
            return this[closest];
        }

        public V GetByKeyOrClosest(K1 key1, K2 key2)
        {
            var byK1 = GetByKeyOrClosest(key1);
            if (byK1.ContainsKey(key2)) return byK1[key2];
            else if (K2Comparer == null) throw new KeyNotFoundException("Key not found and comparer not set [k2]");
            var closest = byK1.Keys.Aggregate((x, y) => K2Comparer(x, y, key2));
            return byK1[closest];
        }

        public Func<K1, K1, K1, K1> K1Comparer { get; set; }
        public Func<K2, K2, K2, K2> K2Comparer { get; set; } // = (v1, v2, value) => Math.Abs(value - v1) > Math.Abs(value - v2) ? v2 : v1;


        public void Add(K1 key1, K2 key2, V value)
        {
            if (!ContainsKey(key1))
                this[key1] = new Dictionary<K2, V>();
            this[key1][key2] = value;
        }

        public bool ContainsKey(K1 key1, K2 key2)
        {
            return base.ContainsKey(key1) && this[key1].ContainsKey(key2);
        }


        public new IEnumerable<V> Values
        {
            get
            {
                return Keys.SelectMany(k => this[k].Select(k1 => k1.Value));
            }
        }
    }

    public class MultiKeyDictionary<K1, K2, K3, V> : Dictionary<K1, MultiKeyDictionary<K2, K3, V>>
    {
        public V this[K1 key1, K2 key2, K3 key3]
        {
            get
            {
                return ContainsKey(key1) ? this[key1][key2, key3] : default(V);
            }
            set
            {
                if (!ContainsKey(key1))
                    this[key1] = new MultiKeyDictionary<K2, K3, V>();
                this[key1][key2, key3] = value;
            }
        }

        protected MultiKeyDictionary<K2, K3, V> GetByKeyOrClosest(K1 key1)
        {
            if (ContainsKey(key1)) return this[key1];
            else if (K1Comparer == null) throw new Exception();
            var closest = Keys.Aggregate((x, y) => K1Comparer(x, y, key1));
            return this[closest];
        }

        protected Dictionary<K3, V> GetByKeyOrClosest(K1 key1, K2 key2)
        {
            var byK1 = GetByKeyOrClosest(key1);
            if (byK1.ContainsKey(key2)) return byK1[key2];
            else if (K2Comparer == null) throw new KeyNotFoundException("Key not found and comparer not set [k2]");
            var closest = byK1.Keys.Aggregate((x, y) => K2Comparer(x, y, key2));
            return byK1[closest];
        }

        public V GetByKeyOrClosest(K1 key1, K2 key2, K3 key3)
        {
            var byK2 = GetByKeyOrClosest(key1, key2);
            if (byK2.ContainsKey(key3)) return byK2[key3];
            else if (K3Comparer == null) throw new KeyNotFoundException("Key not found and comparer not set [k3]");
            var closest = byK2.Keys.Aggregate((x, y) => K3Comparer(x, y, key3));
            return byK2[closest];
        }

        public Func<K1, K1, K1, K1> K1Comparer { get; set; }
        public Func<K2, K2, K2, K2> K2Comparer { get; set; } // = (v1, v2, value) => Math.Abs(value - v1) > Math.Abs(value - v2) ? v2 : v1;
        public Func<K3, K3, K3, K3> K3Comparer { get; set; } // = (v1, v2, value) => Math.Abs(value - v1) > Math.Abs(value - v2) ? v2 : v1;

        public void Add(K1 key1, K2 key2, K3 key3, V value)
        {
            if (!ContainsKey(key1))
                this[key1] = new MultiKeyDictionary<K2, K3, V>();
            this[key1].Add(key2, key3, value);
        }

        public bool ContainsKey(K1 key1, K2 key2, K3 key3)
        {
            return base.ContainsKey(key1) && this[key1].ContainsKey(key2, key3);
        }
        public new IEnumerable<V> Values
        {
            get
            {
                return Keys.SelectMany(k => this[k].SelectMany(k1 => k1.Value).Select(x => x.Value));
            }
        }
    }
}
