using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using GeodataLoader.Source.GMLModels;
using GeodataLoader.Source.Models;

//=============================================================================================================
//==== Ta część kodu jest odpowiedzialna za parsowanie danych z klas BDOT10k zgodnie z utworzonymi modelami ===
//-------------------------------------------------------------------------------------------------------------
//=== This part of code is responsible for parsinging data from BDOT10k classes according to created models ===
//=============================================================================================================

// pierwotnie oparty o (duże zmiany): / primarily based on (major changes):
//https://stackoverflow.com/questions/1818147/parsing-gml-data-using-c-sharp-linq-to-xml

// podstawowy schemat wykonany przez / basic scheme by:
// jaggi

// prawie wszystkie kalsy: / almost all classes:
// mcegla

namespace GeodataLoader.Source.Parsers
{
    public class BDOT10kParser<TBdot, TGml>
        where TBdot : BDOT10k, new()
        where TGml : class, IGML
    {
        protected internal TGml Document { get; set; }

        public virtual IEnumerable<TBdot> GetBDOT10Ks()
        {
            return new TBdot[0];
        }

        public void InitDocument(string path)
        {
            var serializer = new XmlSerializer(typeof(TGml));
            using (var fs = File.OpenRead(path))
            {
                Document = serializer.Deserialize(fs) as TGml;
            }
        }
    }

    //===================================== ADJA_A =====================================
    public class Parser_ADJA_A : BDOT10kParser<BDOT10k_AL, ADJA_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    XKod = x.OT_ADJA_A.x_kod,
                    GeomType = x.OT_ADJA_A.x_rodzajReprGeom,
                    IDIIP = x.OT_ADJA_A.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_ADJA_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== ADMS_A =====================================
    public class Parser_ADMS_A : BDOT10kParser<BDOT10k_AL, ADMS_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    XKod = x.OT_ADMS_A.x_kod,
                    GeomType = x.OT_ADMS_A.x_rodzajReprGeom,
                    IDIIP = x.OT_ADMS_A.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_ADMS_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== ADMS_P =====================================
    public class Parser_ADMS_P : BDOT10kParser<BDOT10k_AL, ADMS_P.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    XKod = x.OT_ADMS_P.x_kod,
                    GeomType = x.OT_ADMS_P.x_rodzajReprGeom,
                    IDIIP = x.OT_ADMS_P.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_ADMS_P.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== BUBD_A =====================================
    public class Parser_BUBD_A : BDOT10kParser<Models.BUBD_A, GMLModels.BUBD_A.FeatureCollection>
    {
        public override IEnumerable<Models.BUBD_A> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new Models.BUBD_A
                {
                    XKod = x.OT_BUBD_A.x_kod,
                    GeomType = x.OT_BUBD_A.x_rodzajReprGeom,
                    IDIIP = x.OT_BUBD_A.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_BUBD_A.geometria.Polygon.exterior.LinearRing.posList,
                    FunSzczegolowaBudynku = x.OT_BUBD_A.funSzczegolowaBudynku.FirstOrDefault(),
                    //liczbaKondygnacji = int.Parse(x.OT_BUBD_A.liczbaKondygnacji.Value)
                    LiczbaKondygnacji =
                        x.OT_BUBD_A.liczbaKondygnacji?.Value != null
                            ? int.Parse(x.OT_BUBD_A.liczbaKondygnacji?.Value)
                            : 1
                    //interiorLines = x.OT_BUBD_A.geometria.Polygon.interior?.Select(line => line?.LinearRing?.posList).ToList(),

                }) ;
        }
    }

    //===================================== BUCM_A =====================================
    public class Parser_BUCM_A : BDOT10kParser<BDOT10k_AL, BUCM_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    XKod = x.OT_BUCM_A.x_kod,
                    GeomType = x.OT_BUCM_A.x_rodzajReprGeom,
                    IDIIP = x.OT_BUCM_A.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_BUCM_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== BUHD_A =====================================
    //NIEPEŁNY MODEL

    //public class Parser_BUHD_A : BDOT10kParser<BDOT10k_AL, BUHD_A.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k_AL
    //            {
    //                xkod = x.OT_BUHD_A.x_kod,
    //                geomtype = x.OT_BUHD_A.x_rodzajReprGeom,
    //                idIIP = x.OT_BUHD_A.idIIP.BT_Identyfikator.lokalnyId,
    //                xyline = x.OT_BUHD_A.geometria.Polygon.exterior.LinearRing.posList
    //            });
    //    }
    //}

    //===================================== BUHD_L =====================================
    public class Parser_BUHD_L : BDOT10kParser<BDOT10k_AL, BUHD_L.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    XKod = x.OT_BUHD_L.x_kod,
                    GeomType = x.OT_BUHD_L.x_rodzajReprGeom,
                    IDIIP = x.OT_BUHD_L.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_BUHD_L.geometria.Curve.segments.LineStringSegment.posList
                });
        }
    }

    //===================================== BUHD_P =====================================
    //NIEPEŁNY MODEL

    //public class Parser_BUHD_P : BDOT10kParser<BDOT10k_P, BUHD_P.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k_P> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k_P
    //            {
    //                xkod = x.OT_BUHD_P.x_kod,
    //                geomtype = x.OT_BUHD_P.x_rodzajReprGeom,
    //                idIIP = x.OT_BUHD_P.idIIP.BT_Identyfikator.lokalnyId,
    //                xypoint = x.OT_BUHD_P.geometria.Point.pos
    //            });
    //    }
    //}

    //===================================== BUIB_A =====================================
    public class Parser_BUIB_A : BDOT10kParser<BDOT10k_AL, BUIB_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    XKod = x.OT_BUIB_A.x_kod,
                    GeomType = x.OT_BUIB_A.x_rodzajReprGeom,
                    IDIIP = x.OT_BUIB_A.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_BUIB_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== BUIB_L =====================================
    public class Parser_BUIB_L : BDOT10kParser<BDOT10k_AL, BUIB_L.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    XKod = x.OT_BUIB_L.x_kod,
                    GeomType = x.OT_BUIB_L.x_rodzajReprGeom,
                    IDIIP = x.OT_BUIB_L.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_BUIB_L.geometria.Curve.segments.LineStringSegment.posList
                });
        }
    }

    //===================================== BUIN_L =====================================
    public class Parser_BUIN_L : BDOT10kParser<BDOT10k_AL, BUIN_L.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    XKod = x.OT_BUIN_L.x_kod,
                    GeomType = x.OT_BUIN_L.x_rodzajReprGeom,
                    IDIIP = x.OT_BUIN_L.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_BUIN_L.geometria.Curve.segments.LineStringSegment.posList
                });
        }
    }

    //===================================== BUIT_A =====================================
    public class Parser_BUIT_A : BDOT10kParser<BDOT10k_AL, BUIT_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    XKod = x.OT_BUIT_A.x_kod,
                    GeomType = x.OT_BUIT_A.x_rodzajReprGeom,
                    IDIIP = x.OT_BUIT_A.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_BUIT_A.geometria.Polygon.exterior.LinearRing.posList
                    //interiorLines = x.OT_BUIT_A.geometria.Polygon.interior?.Select(line => line?.LinearRing?.posList).ToList()
                });
        }
    }

    //===================================== BUIT_P =====================================
    public class Parser_BUIT_P : BDOT10kParser<BDOT10k_P, BUIT_P.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_P> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_P
                {
                    XKod = x.OT_BUIT_P.x_kod,
                    GeomType = x.OT_BUIT_P.x_rodzajReprGeom,
                    IDIIP = x.OT_BUIT_P.idIIP.BT_Identyfikator.lokalnyId,
                    xypoint = x.OT_BUIT_P.geometria.Point.pos
                });
        }
    }

    //===================================== BUSP_A =====================================
    public class Parser_BUSP_A : BDOT10kParser<BDOT10k_A, BUSP_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_A> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_A
                {
                    XKod = x.OT_BUSP_A.x_kod,
                    GeomType = x.OT_BUSP_A.x_rodzajReprGeom,
                    IDIIP = x.OT_BUSP_A.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_BUSP_A.geometria.Polygon.exterior.LinearRing.posList,
                    //interiorLines = x.OT_BUSP_A.geometria.Polygon.interior?.Select(line => line?.LinearRing?.posList).ToList()
                });
        }
    }

    //===================================== BUSP_L =====================================
    //NIEPEŁNY MODEL

    //public class Parser_BUSP_L : BDOT10kParser<BDOT10k_AL, BUSP_L.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k_AL
    //            {
    //                xkod = x.OT_BUSP_L.x_kod,
    //                geomtype = x.OT_BUSP_L.x_rodzajReprGeom,
    //                idIIP = x.OT_BUSP_L.idIIP.BT_Identyfikator.lokalnyId,
    //                xyline = x.OT_BUSP_L.geometria.Curve.segments.LineStringSegment.posList
    //            });
    //    }
    //}

    //===================================== BUTR_L =====================================
    public class Parser_BUTR_L : BDOT10kParser<BDOT10k_AL, BUTR_L.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    XKod = x.OT_BUTR_L.x_kod,
                    GeomType = x.OT_BUTR_L.x_rodzajReprGeom,
                    IDIIP = x.OT_BUTR_L.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_BUTR_L.geometria.Curve.segments.LineStringSegment.posList
                });
        }
    }

    //===================================== BUTR_P =====================================
    //NIEPEŁNY MODEL

    //public class Parser_BUTR_P : BDOT10kParser<BDOT10k_P, BUTR_P.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k_P> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k_P
    //            {
    //                xkod = x.OT_BUTR_P.x_kod,
    //                geomtype = x.OT_BUTR_P.x_rodzajReprGeom,
    //                idIIP = x.OT_BUTR_P.idIIP.BT_Identyfikator.lokalnyId,
    //                xypoint = x.OT_BUTR_P.geometria.geometria.Point.pos
    //            });
    //    }
    //}

    //===================================== BUUO_L =====================================
    public class Parser_BUUO_L : BDOT10kParser<BDOT10k_AL, BUUO_L.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    XKod = x.OT_BUUO_L.x_kod,
                    GeomType = x.OT_BUUO_L.x_rodzajReprGeom,
                    IDIIP = x.OT_BUUO_L.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_BUUO_L.geometria.Curve.segments.LineStringSegment.posList
                });
        }
    }

    //===================================== BUWT_A =====================================
    //NIEPEŁNY MODEL

    //public class Parser_BUWT_A : BDOT10kParser<BDOT10k_AL, BUWT_A.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k_AL
    //            {
    //                xkod = x.OT_BUWT_A.x_kod,
    //                geomtype = x.OT_BUWT_A.x_rodzajReprGeom,
    //                idIIP = x.OT_BUWT_A.idIIP.BT_Identyfikator.lokalnyId,
    //                xyline = x.OT_BUWT_A.geometria.Polygon.exterior.LinearRing.posList
    //            });
    //    }
    //},

    //===================================== BUWT_P =====================================
    public class Parser_BUWT_P : BDOT10kParser<BDOT10k_P, BUWT_P.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_P> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_P
                {
                    XKod = x.OT_BUWT_P.x_kod,
                    GeomType = x.OT_BUWT_P.x_rodzajReprGeom,
                    IDIIP = x.OT_BUWT_P.idIIP.BT_Identyfikator.lokalnyId,
                    xypoint = x.OT_BUWT_P.geometria.Point.pos
                });
        }
    }

    //===================================== BUZM_L =====================================
    public class Parser_BUZM_L : BDOT10kParser<BDOT10k_AL, BUZM_L.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    XKod = x.OT_BUZM_L.x_kod,
                    GeomType = x.OT_BUZM_L.x_rodzajReprGeom,
                    IDIIP = x.OT_BUZM_L.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_BUZM_L.geometria.Curve.segments.LineStringSegment.posList
                });
        }
    }

    //===================================== BUZT_A =====================================
    public class Parser_BUZT_A : BDOT10kParser<BDOT10k_AL, BUZT_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    XKod = x.OT_BUZT_A.x_kod,
                    GeomType = x.OT_BUZT_A.x_rodzajReprGeom,
                    IDIIP = x.OT_BUZT_A.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_BUZT_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== BUZT_P =====================================
    public class Parser_BUZT_P : BDOT10kParser<BDOT10k_P, BUZT_P.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_P> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_P
                {
                    XKod = x.OT_BUZT_P.x_kod,
                    GeomType = x.OT_BUZT_P.x_rodzajReprGeom,
                    IDIIP = x.OT_BUZT_P.idIIP.BT_Identyfikator.lokalnyId,
                    xypoint = x.OT_BUZT_P.geometria.Point.pos
                });
        }
    }

    //===================================== KUHO_A =====================================
    //COŚ NIE TAK Z OBIEKTAMI WEWNĄTRZ LUB Z ODWOŁANIEM DO NICH!!!

    //public class Parser_KUHO_A : BDOT10kParser<BDOT10k_AL, KUHO_A.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k_AL
    //            {
    //                xkod = x.OT_KUHO_A.x_kod,
    //                geomtype = x.OT_KUHO_A.x_rodzajReprGeom,
    //                idIIP = x.OT_KUHO_A.idIIP.BT_Identyfikator.lokalnyId,
    //                xyline = x.OT_KUHO_A.geometria.Polygon.exterior.LinearRing.posList
    //            });
    //    }
    //}

    //===================================== KUHU_A =====================================
    public class Parser_KUHU_A : BDOT10kParser<BDOT10k_AL, KUHU_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    XKod = x.OT_KUHU_A.x_kod,
                    GeomType = x.OT_KUHU_A.x_rodzajReprGeom,
                    IDIIP = x.OT_KUHU_A.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_KUHU_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== KUHU_P =====================================
    public class Parser_KUHU_P : BDOT10kParser<BDOT10k_P, KUHU_P.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_P> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_P
                {
                    XKod = x.OT_KUHU_P.x_kod,
                    GeomType = x.OT_KUHU_P.x_rodzajReprGeom,
                    IDIIP = x.OT_KUHU_P.idIIP.BT_Identyfikator.lokalnyId,
                    xypoint = x.OT_KUHU_P.geometria.Point.pos
                });
        }
    }

    //===================================== KUIK_A =====================================
    //NIEPEŁNY MODEL

    //public class Parser_KUIK_A : BDOT10kParser<BDOT10k_AL, KUIK_A.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k_AL
    //            {
    //                xkod = x.OT_KUIK_A.x_kod,
    //                geomtype = x.OT_KUIK_A.x_rodzajReprGeom,
    //                idIIP = x.OT_KUIK_A.idIIP.BT_Identyfikator.lokalnyId,
    //                xyline = x.OT_KUIK_A.geometria.Polygon.exterior.LinearRing.posList
    //            });
    //    }
    //}

    //===================================== KUKO_A =====================================
    public class Parser_KUKO_A : BDOT10kParser<BDOT10k_AL, KUKO_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    XKod = x.OT_KUKO_A.x_kod,
                    GeomType = x.OT_KUKO_A.x_rodzajReprGeom,
                    IDIIP = x.OT_KUKO_A.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_KUKO_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== KUKO_P =====================================
    //COŚ NIE TAK Z OBIEKTAMI WEWNĄTRZ LUB Z ODWOŁANIEM DO NICH!!!

    //public class Parser_KUKO_P : BDOT10kParser<BDOT10k_P, KUKO_P.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k_P> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k_P
    //            {
    //                xkod = x.OT_KUKO_P.x_kod,
    //                geomtype = x.OT_KUKO_P.x_rodzajReprGeom,
    //                idIIP = x.OT_KUKO_P.idIIP.BT_Identyfikator.lokalnyId,
    //                xypoint = x.OT_KUKO_P.geometria.Point.pos
    //            });
    //    }
    //}

    //===================================== KUMN_A =====================================
    public class Parser_KUMN_A : BDOT10kParser<BDOT10k_AL, KUMN_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    XKod = x.OT_KUMN_A.x_kod,
                    GeomType = x.OT_KUMN_A.x_rodzajReprGeom,
                    IDIIP = x.OT_KUMN_A.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_KUMN_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== KUOS_A =====================================
    public class Parser_KUOS_A : BDOT10kParser<BDOT10k_AL, KUOS_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    XKod = x.OT_KUOS_A.x_kod,
                    GeomType = x.OT_KUOS_A.x_rodzajReprGeom,
                    IDIIP = x.OT_KUOS_A.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_KUOS_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== KUOZ_A =====================================
    public class Parser_KUOZ_A : BDOT10kParser<BDOT10k_AL, KUOZ_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    XKod = x.OT_KUOZ_A.x_kod,
                    GeomType = x.OT_KUOZ_A.x_rodzajReprGeom,
                    IDIIP = x.OT_KUOZ_A.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_KUOZ_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== KUPG_A =====================================
    public class Parser_KUPG_A : BDOT10kParser<BDOT10k_AL, KUPG_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    XKod = x.OT_KUPG_A.x_kod,
                    GeomType = x.OT_KUPG_A.x_rodzajReprGeom,
                    IDIIP = x.OT_KUPG_A.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_KUPG_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== KUPG_P =====================================
    //COŚ NIE TAK Z OBIEKTAMI WEWNĄTRZ LUB Z ODWOŁANIEM DO NICH!!!

    //public class Parser_KUPG_P : BDOT10kParser<BDOT10k_P, KUPG_P.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k_P> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k_P
    //            {
    //                xkod = x.OT_KUPG_P.x_kod,
    //                geomtype = x.OT_KUPG_P.x_rodzajReprGeom,
    //                idIIP = x.OT_KUPG_P.idIIP.BT_Identyfikator.lokalnyId,
    //                xypoint = x.OT_KUPG_P.geometria.Point.pos
    //            });
    //    }
    //}

    //===================================== KUSC_A =====================================
    public class Parser_KUSC_A : BDOT10kParser<BDOT10k_AL, KUSC_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    XKod = x.OT_KUSC_A.x_kod,
                    GeomType = x.OT_KUSC_A.x_rodzajReprGeom,
                    IDIIP = x.OT_KUSC_A.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_KUSC_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== KUSK_A =====================================
    public class Parser_KUSK_A : BDOT10kParser<BDOT10k_AL, KUSK_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    XKod = x.OT_KUSK_A.x_kod,
                    GeomType = x.OT_KUSK_A.x_rodzajReprGeom,
                    IDIIP = x.OT_KUSK_A.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_KUSK_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== KUZA_A =====================================
    //COŚ NIE TAK Z OBIEKTAMI WEWNĄTRZ LUB Z ODWOŁANIEM DO NICH!!!

    //public class Parser_KUZA_A : BDOT10kParser<BDOT10k_AL, KUZA_A.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k_AL
    //            {
    //                xkod = x.OT_KUZA_A.x_kod,
    //                geomtype = x.OT_KUZA_A.x_rodzajReprGeom,
    //                idIIP = x.OT_KUZA_A.idIIP.BT_Identyfikator.lokalnyId,
    //                xyline = x.OT_KUZA_A.geometria.Polygon.exterior.LinearRing.posList
    //            });
    //    }
    //}

    //===================================== OIKM_A =====================================
    //NIEPEŁNY MODEL

    //public class Parser_OIKM_A : BDOT10kParser<BDOT10k_AL, OIKM_A.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k_AL
    //            {
    //                xkod = x.OT_OIKM_A.x_kod,
    //                geomtype = x.OT_OIKM_A.x_rodzajReprGeom,
    //                idIIP = x.OT_OIKM_A.idIIP.BT_Identyfikator.lokalnyId,
    //                xyline = x.OT_OIKM_A.geometria.Polygon.exterior.LinearRing.posList
    //            });
    //    }
    //}

    //===================================== OIKM_L =====================================
    //NIEPEŁNY MODEL

    //public class Parser_OIKM_L : BDOT10kParser<BDOT10k_AL, OIKM_L.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k_AL
    //            {
    //                xkod = x.OT_OIKM_L.x_kod,
    //                geomtype = x.OT_OIKM_L.x_rodzajReprGeom,
    //                idIIP = x.OT_OIKM_L.idIIP.BT_Identyfikator.lokalnyId
    //                xyline = x.OT_OIKM_L.geometria.Curve.segments.LineStringSegment.posList
    //            });
    //    }
    //}

    //===================================== OIKM_P =====================================
    public class Parser_OIKM_P : BDOT10kParser<BDOT10k_P, OIKM_P.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_P> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_P
                {
                    XKod = x.OT_OIKM_P.x_kod,
                    GeomType = x.OT_OIKM_P.x_rodzajReprGeom,
                    IDIIP = x.OT_OIKM_P.idIIP.BT_Identyfikator.lokalnyId,
                    xypoint = x.OT_OIKM_P.geometria.Point.pos
                });
        }
    }

    //===================================== OIMK_A =====================================
    public class Parser_OIMK_A : BDOT10kParser<BDOT10k_A, OIMK_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_A> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_A
                {
                    XKod = x.OT_OIMK_A.x_kod,
                    GeomType = x.OT_OIMK_A.x_rodzajReprGeom,
                    IDIIP = x.OT_OIMK_A.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_OIMK_A.geometria.Polygon.exterior.LinearRing.posList,
                    interiorLines = x.OT_OIMK_A.geometria.Polygon.interior?.Select(line => line?.LinearRing?.posList).ToList()
                });
        }
    }

    //===================================== OIOR_A =====================================
    public class Parser_OIOR_A : BDOT10kParser<BDOT10k_AL, OIOR_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    XKod = x.OT_OIOR_A.x_kod,
                    GeomType = x.OT_OIOR_A.x_rodzajReprGeom,
                    IDIIP = x.OT_OIOR_A.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_OIOR_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== OIOR_L =====================================
    public class Parser_OIOR_L : BDOT10kParser<BDOT10k_AL, OIOR_L.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    XKod = x.OT_OIOR_L.x_kod,
                    GeomType = x.OT_OIOR_L.x_rodzajReprGeom,
                    IDIIP = x.OT_OIOR_L.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_OIOR_L.geometria.Curve.segments.LineStringSegment.posList
                });
        }
    }

    //===================================== OIOR_P =====================================
    public class Parser_OIOR_P : BDOT10kParser<BDOT10k_P, OIOR_P.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_P> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_P
                {
                    XKod = x.OT_OIOR_P.x_kod,
                    GeomType = x.OT_OIOR_P.x_rodzajReprGeom,
                    IDIIP = x.OT_OIOR_P.idIIP.BT_Identyfikator.lokalnyId,
                    xypoint = x.OT_OIOR_P.geometria.Point.pos
                });
        }
    }

    //===================================== OIPR_L =====================================
    public class Parser_OIPR_L : BDOT10kParser<BDOT10k_AL, OIPR_L.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    XKod = x.OT_OIPR_L.x_kod,
                    GeomType = x.OT_OIPR_L.x_rodzajReprGeom,
                    IDIIP = x.OT_OIPR_L.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_OIPR_L.geometria.Curve.segments.LineStringSegment.posList
                });
        }
    }

    //===================================== OIPR_P =====================================
    public class Parser_OIPR_P : BDOT10kParser<BDOT10k_P, OIPR_P.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_P> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_P
                {
                    XKod = x.OT_OIPR_P.x_kod,
                    GeomType = x.OT_OIPR_P.x_rodzajReprGeom,
                    IDIIP = x.OT_OIPR_P.idIIP.BT_Identyfikator.lokalnyId,
                    xypoint = x.OT_OIPR_P.geometria.Point.pos
                });
        }
    }

    //===================================== OISZ_A =====================================
    public class Parser_OISZ_A : BDOT10kParser<BDOT10k_A, OISZ_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_A> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_A
                {
                    XKod = x.OT_OISZ_A.x_kod,
                    GeomType = x.OT_OISZ_A.x_rodzajReprGeom,
                    IDIIP = x.OT_OISZ_A.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_OISZ_A.geometria.Polygon.exterior.LinearRing.posList,
                    //interiorLines = x.OT_OISZ_A.geometria.Polygon.interior?.Select(line => line?.LinearRing?.posList).ToList()
                });
        }
    }

    //===================================== PTGN_A =====================================
    public class Parser_PTGN_A : BDOT10kParser<BDOT10k_A, PTGN_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_A> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_A
                {
                    XKod = x.OT_PTGN_A.x_kod,
                    GeomType = x.OT_PTGN_A.x_rodzajReprGeom,
                    IDIIP = x.OT_PTGN_A.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_PTGN_A.geometria.Polygon.exterior.LinearRing.posList, //v--- Select problem
                    //interiorLines = x.OT_PTGN_A.geometria.Polygon.interior?.Select(line => line?.LinearRing?.posList).ToList(),

                });
        }
    }

    //===================================== PTKM_A =====================================
    public class Parser_PTKM_A : BDOT10kParser<BDOT10k_AL, PTKM_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    XKod = x.OT_PTKM_A.x_kod,
                    GeomType = x.OT_PTKM_A.x_rodzajReprGeom,
                    IDIIP = x.OT_PTKM_A.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_PTKM_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== PTLZ_A =====================================
    public class Parser_PTLZ_A : BDOT10kParser<Models.PTLZ_A, GMLModels.PTLZ_A.FeatureCollection>
    {
        public override IEnumerable<Models.PTLZ_A> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new Models.PTLZ_A
                {
                    XKod = x.OT_PTLZ_A.x_kod,
                    GeomType = x.OT_PTLZ_A.x_rodzajReprGeom,
                    IDIIP = x.OT_PTLZ_A.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_PTLZ_A.geometria.Polygon.exterior.LinearRing.posList,
                    interiorLines = x.OT_PTLZ_A.geometria.Polygon.interior?.Select(line => line?.LinearRing?.posList).ToList(),
                    GatunekDrzew = x.OT_PTLZ_A.gatunekDrzew.FirstOrDefault()
                });
        }
    }

    //===================================== PTNZ_A =====================================
    public class Parser_PTNZ_A : BDOT10kParser<BDOT10k_A, PTNZ_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_A> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_A
                {
                    XKod = x.OT_PTNZ_A.x_kod,
                    GeomType = x.OT_PTNZ_A.x_rodzajReprGeom,
                    IDIIP = x.OT_PTNZ_A.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_PTNZ_A.geometria.Polygon.exterior.LinearRing.posList,
                    //interiorLines = x.OT_PTNZ_A.geometria.Polygon.interior?.Select(line => line?.LinearRing?.posList).ToList()
                });
        }
    }

    //===================================== PTPL_A =====================================
    public class Parser_PTPL_A : BDOT10kParser<BDOT10k_A, PTPL_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_A> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_A
                {
                    XKod = x.OT_PTPL_A.x_kod,
                    GeomType = x.OT_PTPL_A.x_rodzajReprGeom,
                    IDIIP = x.OT_PTPL_A.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_PTPL_A.geometria.Polygon.exterior.LinearRing.posList,
                    interiorLines = x.OT_PTPL_A.geometria.Polygon.interior?.Select(line => line?.LinearRing?.posList).ToList()
                });
        }
    }

    //===================================== PTRK_A =====================================
    public class Parser_PTRK_A : BDOT10kParser<BDOT10k_A, PTRK_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_A> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_A
                {
                    XKod = x.OT_PTRK_A.x_kod,
                    GeomType = x.OT_PTRK_A.x_rodzajReprGeom,
                    IDIIP = x.OT_PTRK_A.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_PTRK_A.geometria.Polygon.exterior.LinearRing.posList,
                    interiorLines = x.OT_PTRK_A.geometria.Polygon.interior?.Select(line => line?.LinearRing?.posList).ToList()
                });
        }
    }

    //===================================== PTSO_A =====================================
    public class Parser_PTSO_A : BDOT10kParser<BDOT10k_AL, PTSO_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    XKod = x.OT_PTSO_A.x_kod,
                    GeomType = x.OT_PTSO_A.x_rodzajReprGeom,
                    IDIIP = x.OT_PTSO_A.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_PTSO_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== PTTR_A =====================================
    public class Parser_PTTR_A : BDOT10kParser<BDOT10k_A, PTTR_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_A> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_A
                {
                    XKod = x.OT_PTTR_A.x_kod,
                    GeomType = x.OT_PTTR_A.x_rodzajReprGeom,
                    IDIIP = x.OT_PTTR_A.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_PTTR_A.geometria.Polygon.exterior.LinearRing.posList,
                    interiorLines = x.OT_PTTR_A.geometria.Polygon.interior?.Select(line => line?.LinearRing?.posList).ToList()
                });
        }
    }

    //===================================== PTUT_A =====================================
    public class Parser_PTUT_A : BDOT10kParser<Models.PTUT_A, GMLModels.PTUT_A.FeatureCollection>
    {
        public override IEnumerable<Models.PTUT_A> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new Models.PTUT_A
                {
                    XKod = x.OT_PTUT_A.x_kod,
                    GeomType = x.OT_PTUT_A.x_rodzajReprGeom,
                    IDIIP = x.OT_PTUT_A.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_PTUT_A.geometria.Polygon.exterior.LinearRing.posList,
                    GatunekUpraw = 
                        x.OT_PTUT_A.gatunek?.Value != null
                            ? x.OT_PTUT_A.gatunek?.Value.ToString()
                            : "other"
                });
        }
    }

    //===================================== PTWP_A =====================================
    public class Parser_PTWP_A : BDOT10kParser<BDOT10k_A, PTWP_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_A> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_A
                {
                    XKod = x.OT_PTWP_A.x_kod,
                    GeomType = x.OT_PTWP_A.x_rodzajReprGeom,
                    IDIIP = x.OT_PTWP_A.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_PTWP_A.geometria.Polygon.exterior.LinearRing.posList,
                    interiorLines = x.OT_PTWP_A.geometria.Polygon.interior?.Select(line => line?.LinearRing?.posList).ToList()
                });
        }
    }

    //===================================== PTWZ_A =====================================
    public class Parser_PTWZ_A : BDOT10kParser<BDOT10k_A, PTWZ_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_A> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_A
                {
                    XKod = x.OT_PTWZ_A.x_kod,
                    GeomType = x.OT_PTWZ_A.x_rodzajReprGeom,
                    IDIIP = x.OT_PTWZ_A.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_PTWZ_A.geometria.Polygon.exterior.LinearRing.posList,
                    interiorLines = x.OT_PTWZ_A.geometria.Polygon.interior?.Select(line => line?.LinearRing?.posList).ToList()

                });
        }
    }

    //===================================== PTZB_A =====================================
    public class Parser_PTZB_A : BDOT10kParser<BDOT10k_AL, PTZB_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    XKod = x.OT_PTZB_A.x_kod,
                    GeomType = x.OT_PTZB_A.x_rodzajReprGeom,
                    IDIIP = x.OT_PTZB_A.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_PTZB_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== SKDR_L =====================================
    public class Parser_SKDR_L : BDOT10kParser<BDOT10k_AL, SKDR_L.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    XKod = x.OT_SKDR_L.x_kod,
                    GeomType = x.OT_SKDR_L.x_rodzajReprGeom,
                    IDIIP = x.OT_SKDR_L.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_SKDR_L.geometria.Curve.segments.LineStringSegment.posList
                });
        }
    }

    //===================================== SKJZ_L =====================================
    public class Parser_SKJZ_L : BDOT10kParser<Models.SKJZ_L, GMLModels.SKJZ_L.FeatureCollection>
    {
        public override IEnumerable<Models.SKJZ_L> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new Models.SKJZ_L
                {
                    XKod = x.OT_SKJZ_L.x_kod,
                    GeomType = x.OT_SKJZ_L.x_rodzajReprGeom,
                    IDIIP = x.OT_SKJZ_L.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_SKJZ_L.geometria.Curve.segments.LineStringSegment.posList,
                    MaterialNawierzchni = x.OT_SKJZ_L.materialNawierzchni,
                    LiczbaPasow =
                        x.OT_SKJZ_L.liczbaPasow?.Value != null
                            ? int.Parse(x.OT_SKJZ_L.liczbaPasow?.Value)
                            : 0
                });
        }
    }

    //===================================== SKPP_L =====================================
    //NIEKOMPLETNY MODEL

    //public class Parser_SKPP_L : BDOT10kParser<BDOT10k_AL, SKPP_L.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k_AL
    //            {
    //                xkod = x.OT_SKPP_L.x_kod,
    //                geomtype = x.OT_SKPP_L.x_rodzajReprGeom,
    //                idIIP = x.OT_SKPP_L.idIIP.BT_Identyfikator.lokalnyId,
    //                xyline = x.OT_SKPP_L.geometria.Curve.segments.LineStringSegment.posList
    //            });
    //    }
    //}

    //===================================== SKRP_L =====================================
    public class Parser_SKRP_L : BDOT10kParser<Models.SKRP_L, GMLModels.SKRP_L.FeatureCollection>
    {
        public override IEnumerable<Models.SKRP_L> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new Models.SKRP_L
                {
                    XKod = x.OT_SKRP_L.x_kod,
                    GeomType = x.OT_SKRP_L.x_rodzajReprGeom,
                    IDIIP = x.OT_SKRP_L.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_SKRP_L.geometria.Curve.segments.LineStringSegment.posList,
                    MaterialNawierzchni = x.OT_SKRP_L.materialNawierzchni?.Any() != null
                    ? x.OT_SKRP_L.materialNawierzchni
                    : "other",
                    RuchRowerowy = x.OT_SKRP_L.ruchRowerowy
                });
        }
    }

    //===================================== SKRW_P =====================================
    public class Parser_SKRW_P : BDOT10kParser<BDOT10k_P, SKRW_P.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_P> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_P
                {
                    XKod = x.OT_SKRW_P.x_kod,
                    GeomType = x.OT_SKRW_P.x_rodzajReprGeom,
                    IDIIP = x.OT_SKRW_P.idIIP.BT_Identyfikator.lokalnyId,
                    xypoint = x.OT_SKRW_P.geometria.Point.pos
                });
        }
    }

    //===================================== SKTR_L =====================================
    //NIEKOMPLETNY MODEL

    //public class Parser_SKTR_L : BDOT10kParser<BDOT10k_AL, SKTR_L.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k_AL
    //            {
    //                xkod = x.OT_SKTR_L.x_kod,
    //                geomtype = x.OT_SKTR_L.x_rodzajReprGeom,
    //                idIIP = x.OT_SKTR_L.idIIP.BT_Identyfikator.lokalnyId,
    //                xyline = x.OT_SKTR_L.geometria.Curve.segments.LineStringSegment.posList
    //            });
    //    }
    //}

    //===================================== SULN_L =====================================
    public class Parser_SULN_L : BDOT10kParser<BDOT10k_AL, SULN_L.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    XKod = x.OT_SULN_L.x_kod,
                    GeomType = x.OT_SULN_L.x_rodzajReprGeom,
                    IDIIP = x.OT_SULN_L.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_SULN_L.geometria.Curve.segments.LineStringSegment.posList
                });
        }
    }

    //===================================== SUPR_L =====================================
    //NIEKOMPLETNY MODEL

    //public class Parser_SUPR_L : BDOT10kParser<BDOT10k_AL, SUPR_L.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k_AL
    //            {
    //                xkod = x.OT_SUPR_L.x_kod,
    //                geomtype = x.OT_SUPR_L.x_rodzajReprGeom,
    //                idIIP = x.OT_SUPR_L.idIIP.BT_Identyfikator.lokalnyId,
    //                xyline = x.OT_SUPR_L.geometria.Curve.segments.LineStringSegment.posList
    //            });
    //    }
    //}

    //===================================== SWKN_L =====================================
    //NIEKOMPLETNY MODEL

    //public class Parser_SWKN_L : BDOT10kParser<BDOT10k_AL, SWKN_L.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k_AL
    //            {
    //                xkod = x.OT_SWKN_L.x_kod,
    //                geomtype = x.OT_SWKN_L.x_rodzajReprGeom,
    //                idIIP = x.OT_SWKN_L.idIIP.BT_Identyfikator.lokalnyId,
    //                xyline = x.OT_SWKN_L.geometria.Curve.segments.LineStringSegment.posList
    //            });
    //    }
    //}

    //===================================== SWRM_L =====================================
    public class Parser_SWRM_L : BDOT10kParser<BDOT10k_AL, SWRM_L.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    XKod = x.OT_SWRM_L.x_kod,
                    GeomType = x.OT_SWRM_L.x_rodzajReprGeom,
                    IDIIP = x.OT_SWRM_L.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_SWRM_L.geometria.Curve.segments.LineStringSegment.posList
                });
        }
    }

    //===================================== SWRS_L =====================================
    public class Parser_SWRS_L : BDOT10kParser<BDOT10k_AL, SWRS_L.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    XKod = x.OT_SWRS_L.x_kod,
                    GeomType = x.OT_SWRS_L.x_rodzajReprGeom,
                    IDIIP = x.OT_SWRS_L.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_SWRS_L.geometria.Curve.segments.LineStringSegment.posList
                });
        }
    }

    //===================================== TCON_A =====================================
    public class Parser_TCON_A : BDOT10kParser<BDOT10k_AL, TCON_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    XKod = x.OT_TCON_A.x_kod,
                    GeomType = x.OT_TCON_A.x_rodzajReprGeom,
                    IDIIP = x.OT_TCON_A.idIIP.BT_Identyfikator.lokalnyId,
                    xyline = x.OT_TCON_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== TCPK_A =====================================
    //COŚ NIE TAK Z OBIEKTAMI WEWNĄTRZ LUB Z ODWOŁANIEM DO NICH!!!

    //public class Parser_TCPK_A : BDOT10kParser<BDOT10k_AL, TCPK_A.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k_AL
    //            {
    //                xkod = x.OT_TCPK_A.x_kod,
    //                geomtype = x.OT_TCPK_A.x_rodzajReprGeom,
    //                idIIP = x.OT_TCPK_A.idIIP.BT_Identyfikator.lokalnyId,
    //                xyline = x.OT_TCPK_A.geometria.Polygon.exterior.LinearRing.posList
    //            });
    //    }
    //}

    //===================================== TCPN_A =====================================
    //NIEKOMPLETNY MODEL

    //public class Parser_TCPN_A : BDOT10kParser<BDOT10k_AL, TCPN_A.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k_AL
    //            {
    //                xkod = x.OT_TCPN_A.x_kod,
    //                geomtype = x.OT_TCPN_A.x_rodzajReprGeom,
    //                idIIP = x.OT_TCPN_A.idIIP.BT_Identyfikator.lokalnyId,
    //                xyline = x.OT_TCPN_A.geometria.Polygon.exterior.LinearRing.posList
    //            });
    //    }
    //}

    //===================================== TCRZ_A =====================================
    //COŚ NIE TAK Z OBIEKTAMI WEWNĄTRZ LUB Z ODWOŁANIEM DO NICH!!!

    //public class Parser_TCRZ_A : BDOT10kParser<BDOT10k_AL, TCRZ_A.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k_AL
    //            {
    //                xkod = x.OT_TCRZ_A.x_kod,
    //                geomtype = x.OT_TCRZ_A.x_rodzajReprGeom,
    //                idIIP = x.OT_TCRZ_A.idIIP.BT_Identyfikator.lokalnyId,
    //                xyline = x.OT_TCRZ_A.geometria.Polygon.exterior.LinearRing.posList
    //            });
    //    }
    //}
}