using System;
using System.Linq;
using System.Collections.Generic;
using GeodataLoader.Source.Helpers;

//===========================================================================================
//=== This part of code is a modified BDOT10k model for parsing OIPR(xkod) point features ===
//===========================================================================================

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
