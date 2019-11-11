//======================================================================
//=== Ta część kodu przechowuje standardowy model dla danych BDOT10k ===
//----------------------------------------------------------------------
//===== This part of code contains standard model for BDOT10k data =====
//======================================================================

namespace GeodataLoader.Source.Models
{
    public class BDOT10k
    {
        public virtual string XKod { get; set; }

        public virtual string GeomType { get; set; }

        public virtual string IDIIP { get; set; }
    }
}