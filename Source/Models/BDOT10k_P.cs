using System;
using System.Linq;
using GeodataLoader.Source.Helpers;

//======================================================================
//=== Ta część kodu przechowuje model danych o charakterze punktowym ===
//----------------------------------------------------------------------
//======== This part of code contains model for point-type data ========
//======================================================================

// wykonane przez: / made by:
// jaggi

namespace GeodataLoader.Source.Models
{
    public class BDOT10k_P : BDOT10k
    {
        private string xypoint1; // LINQ element
        private float[] _xypoint1; // Array with that element
        public virtual float[] XYPoint
        {
            get { return _xypoint1; }
        }

        public virtual string xypoint
        {
            get
            {
                return xypoint1;
            }
            set
            {
                xypoint1 = value;
                if (!String.IsNullOrEmpty(xypoint1))
                    _xypoint1 = xypoint1.Split(' ').Select(x => float.Parse(x)).ToArray();
                else
                    CommonHelpers.Log("xypoint - Null Or Empty: " + xypoint1);
            }
        }
    }
}
