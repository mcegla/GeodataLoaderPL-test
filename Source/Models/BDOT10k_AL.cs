using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using GeodataLoader.Source.Helpers;

//===========================================================================================
//=== This part of code is a modified BDOT10k model for parsing OIPR(xkod) point features ===
//===========================================================================================

namespace GeodataLoader.Source.Models
{
    public class BDOT10k_AL : BDOT10k
    {
        private string xyline1;
        private List<float[]> _xyline1;
        public virtual List<float[]> XYLine
        {
            get { return _xyline1; }
        }
        public virtual string xyline
        {
            get { return xyline1; }
            set
            {
                xyline1 = value;
                if (!String.IsNullOrEmpty(xyline1))
                    _xyline1 = xyline1
                        .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => float.Parse(x, CultureInfo.InvariantCulture))
                        .Split(2)
                        .ToList();
                else
                    CommonHelpers.Log("xyline - Null Or Empty: " + xyline1);
            }
        }
    }
    // public class BDOT10k_LA : BDOT10k_AL { }
}
