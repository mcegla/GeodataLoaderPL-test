using System;
using System.Numerics;

namespace GMLParserPL.Models
{
    // by jaggi
    /// <summary>
    ///     segment containing 2 vectors - points
    /// </summary>
    internal struct Segment : IEquatable<Segment>
    {
        public Vector2 p1;
        public Vector2 p2;

        public override bool Equals(object obj)
        {
            if (obj is Segment s)
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
            //return base.ToString();
            return $"{p1.X} {p1.Y} {p2.X} {p2.Y}";
        }
    }
}