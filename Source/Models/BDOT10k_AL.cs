using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using GeodataLoader.Source.Helpers;

//=======================================================================================
//=== Ta część kodu przechowuje model danych o charakterze powierzchniowym i liniowym ===
//---------------------------------------------------------------------------------------
//============== This part of code contains model for area and linear data ==============
//=======================================================================================

// wykonane przez: / made by:
// jaggi

// zmiany: / edits:
// mcegla

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
    public class BDOT10k_A : BDOT10k_AL
    {
        private List<string> xyline1;
        private List<List<float[]>> _xyline1;
        public virtual List<List<float[]>> InteriorLines
        {
            get { return _xyline1; }
        }
        public virtual List<string> interiorLines
        {
            get { return xyline1; }
            set
            {
                xyline1 = value;
                _xyline1 = xyline1
                    ?.Where(line => !String.IsNullOrEmpty(line))
                    .Select(line =>
                        line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(x => float.Parse(x, CultureInfo.InvariantCulture))
                            .Split(2)
                            .ToList())
                    .ToList();
            }
        }
    }



    public class BUBD_A : BDOT10k_AL
    {
        public virtual string FunSzczegolowaBudynku { get; set; }
        public virtual int LiczbaKondygnacji { get; set; }
    }

    public class PTLZ_A : BDOT10k_A
    {
        public virtual string GatunekDrzew { get; set; }
    }

    public class PTUT_A : BDOT10k_A
    {
        public virtual string GatunekUpraw { get; set; }
    }

    public class SKJZ_L : BDOT10k_AL
    {
        public virtual string MaterialNawierzchni { get; set; }
        public virtual int LiczbaPasow { get; set; }
    }

    public class SKRP_L : BDOT10k_AL
    {
        public virtual string MaterialNawierzchni { get; set; }
        public virtual string RuchRowerowy { get; set; }
    }
}
