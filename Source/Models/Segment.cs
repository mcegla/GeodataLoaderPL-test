using System;
using UnityEngine;

//================================================
//=== Ta część kodu przechowuje model segmentu ===
//------------------------------------------------
//=== This part of code contains segment model ===
//================================================

// wykonane przez: / made by:
// jaggi

namespace GeodataLoader.Source.Models
{
    public struct Segment : IEquatable<Segment>
    {
        public Vector2 p1;
        public Vector2 p2;

        public override bool Equals(object obj)
        {
            if(obj is Segment s)
            {
                return
                    (p1 == s.p1 && p2 == s.p2)
                    || (p1 == s.p2 && p2 == s.p1);
            }
            return false;
        }

        public bool Equals(Segment s)
        {
            return
                (p1 == s.p1 && p2 == s.p2)
                || (p1 == s.p2 && p2 == s.p1);
        }

        public override int GetHashCode()
        {
            var hashCode = 1502939027;
            hashCode = hashCode * -1521134295 + this.p1.GetHashCode();
            hashCode = hashCode * -1521134295 + this.p2.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}