using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeodataLoader.Source.Helpers;

//============================================================================
//=== This part of code is a standard model for parsing BDOT10k .gml files ===
//============================================================================

namespace GeodataLoader.Source.Models
{
    public class BDOT10k
    {
        private string xkod1;
        private string _xkod1;
        private string geomtype1;
        private string _geomtype1;

        public virtual string XKod
        {
            get { return _xkod1; }
        }
        public virtual string GeomType
        {
            get { return _geomtype1; }
        }

        public virtual string xkod
        {
            get
            {
                return xkod1;
            }
            set
            {
                xkod1 = value;
                if (!String.IsNullOrEmpty(value))
                    _xkod1 = value;
                else
                    CommonHelpers.Log("xkod - Null Or Empty: " + xkod1);
            }
        }

        public virtual string geomtype
        {
            get
            {
                return geomtype1;
            }
            set
            {
                geomtype1 = value;
                if (!String.IsNullOrEmpty(value))
                    _geomtype1 = value;
                else
                    CommonHelpers.Log("geomtype - Null Or Empty: " + geomtype1);
            }
        }
    }
}
