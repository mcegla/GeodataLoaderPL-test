using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using GeodataLoader.Source.GMLModels;
using GeodataLoader.Source.Models;

//===========================================================================================
//=== This part of code contains a standard parser for BDOT10k area and line gml features ===
//===========================================================================================

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
    //PROBLEM Z AREA???
    public class Parser_ADJA_A_types : BDOT10kParser<BDOT10k, ADJA_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_ADJA_A.x_kod,
                    geomtype = x.OT_ADJA_A.x_rodzajReprGeom
                });
        }
    }
    public class Parser_ADJA_A : BDOT10kParser<BDOT10k_AL, ADJA_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_ADJA_A.x_kod,
                    geomtype = x.OT_ADJA_A.x_rodzajReprGeom,
                    xyline = x.OT_ADJA_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== ADMS_A =====================================
    public class Parser_ADMS_A_types : BDOT10kParser<BDOT10k, ADMS_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_ADMS_A.x_kod,
                    geomtype = x.OT_ADMS_A.x_rodzajReprGeom
                });
        }
    }
    public class Parser_ADMS_A : BDOT10kParser<BDOT10k_AL, ADMS_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_ADMS_A.x_kod,
                    geomtype = x.OT_ADMS_A.x_rodzajReprGeom,
                    xyline = x.OT_ADMS_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== ADMS_P =====================================
    public class Parser_ADMS_P_types : BDOT10kParser<BDOT10k, ADMS_P.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_ADMS_P.x_kod,
                    geomtype = x.OT_ADMS_P.x_rodzajReprGeom
                });
        }
    }
    public class Parser_ADMS_P : BDOT10kParser<BDOT10k_AL, ADMS_P.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_ADMS_P.x_kod,
                    geomtype = x.OT_ADMS_P.x_rodzajReprGeom,
                    xyline = x.OT_ADMS_P.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== BUBD_A =====================================
    public class Parser_BUBD_A_types : BDOT10kParser<BDOT10k, BUBD_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_BUBD_A.x_kod,
                    geomtype = x.OT_BUBD_A.x_rodzajReprGeom
                });
        }
    }
    public class Parser_BUBD_A : BDOT10kParser<BDOT10k_AL, BUBD_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_BUBD_A.x_kod,
                    geomtype = x.OT_BUBD_A.x_rodzajReprGeom,
                    xyline = x.OT_BUBD_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== BUCM_A =====================================
    public class Parser_BUCM_A_types : BDOT10kParser<BDOT10k, BUCM_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_BUCM_A.x_kod,
                    geomtype = x.OT_BUCM_A.x_rodzajReprGeom
                });
        }
    }
    public class Parser_BUCM_A : BDOT10kParser<BDOT10k_AL, BUCM_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_BUCM_A.x_kod,
                    geomtype = x.OT_BUCM_A.x_rodzajReprGeom,
                    xyline = x.OT_BUCM_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== BUHD_A =====================================
    //NIEPEŁNY MODEL

    //public class Parser_BUHD_A_types : BDOT10kParser<BDOT10k, BUHD_A.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k
    //            {
    //                xkod = x.OT_BUHD_A.x_kod,
    //                geomtype = x.OT_BUHD_A.x_rodzajReprGeom
    //            });
    //    }
    //}
    //public class Parser_BUHD_A : BDOT10kParser<BDOT10k_AL, BUHD_A.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k_AL
    //            {
    //                xkod = x.OT_BUHD_A.x_kod,
    //                geomtype = x.OT_BUHD_A.x_rodzajReprGeom,
    //                xyline = x.OT_BUHD_A.geometria.Polygon.exterior.LinearRing.posList
    //            });
    //    }
    //}

    //===================================== BUHD_L =====================================
    public class Parser_BUHD_L_types : BDOT10kParser<BDOT10k, BUHD_L.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_BUHD_L.x_kod,
                    geomtype = x.OT_BUHD_L.x_rodzajReprGeom
                });
        }
    }
    public class Parser_BUHD_L : BDOT10kParser<BDOT10k_AL, BUHD_L.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_BUHD_L.x_kod,
                    geomtype = x.OT_BUHD_L.x_rodzajReprGeom,
                    xyline = x.OT_BUHD_L.geometria.Curve.segments.LineStringSegment.posList
                });
        }
    }

    //===================================== BUHD_P =====================================
    //NIEPEŁNY MODEL

    //public class Parser_BUHD_P_types : BDOT10kParser<BDOT10k, BUHD_P.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k
    //            {
    //                xkod = x.OT_BUHD_P.x_kod,
    //                geomtype = x.OT_BUHD_P.x_rodzajReprGeom
    //            });
    //    }
    //}
    //public class Parser_BUHD_P : BDOT10kParser<BDOT10k_P, BUHD_P.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k_P> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k_P
    //            {
    //                xkod = x.OT_BUHD_P.x_kod,
    //                geomtype = x.OT_BUHD_P.x_rodzajReprGeom,
    //                xypoint = x.OT_BUHD_P.geometria.Point.pos
    //            });
    //    }
    //}

    //===================================== BUIB_A =====================================
    public class Parser_BUIB_A_types : BDOT10kParser<BDOT10k, BUIB_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_BUIB_A.x_kod,
                    geomtype = x.OT_BUIB_A.x_rodzajReprGeom
                });
        }
    }
    public class Parser_BUIB_A : BDOT10kParser<BDOT10k_AL, BUIB_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_BUIB_A.x_kod,
                    geomtype = x.OT_BUIB_A.x_rodzajReprGeom,
                    xyline = x.OT_BUIB_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== BUIB_L =====================================
    public class Parser_BUIB_L_types : BDOT10kParser<BDOT10k, BUIB_L.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_BUIB_L.x_kod,
                    geomtype = x.OT_BUIB_L.x_rodzajReprGeom
                });
        }
    }
    public class Parser_BUIB_L : BDOT10kParser<BDOT10k_AL, BUIB_L.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_BUIB_L.x_kod,
                    geomtype = x.OT_BUIB_L.x_rodzajReprGeom,
                    xyline = x.OT_BUIB_L.geometria.Curve.segments.LineStringSegment.posList
                });
        }
    }

    //===================================== BUIN_L =====================================
    public class Parser_BUIN_L_types : BDOT10kParser<BDOT10k, BUIN_L.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_BUIN_L.x_kod,
                    geomtype = x.OT_BUIN_L.x_rodzajReprGeom
                });
        }
    }
    public class Parser_BUIN_L : BDOT10kParser<BDOT10k_AL, BUIN_L.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_BUIN_L.x_kod,
                    geomtype = x.OT_BUIN_L.x_rodzajReprGeom,
                    xyline = x.OT_BUIN_L.geometria.Curve.segments.LineStringSegment.posList
                });
        }
    }

    //===================================== BUIT_A =====================================
    public class Parser_BUIT_A_types : BDOT10kParser<BDOT10k, BUIT_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_BUIT_A.x_kod,
                    geomtype = x.OT_BUIT_A.x_rodzajReprGeom
                });
        }
    }
    public class Parser_BUIT_A : BDOT10kParser<BDOT10k_AL, BUIT_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_BUIT_A.x_kod,
                    geomtype = x.OT_BUIT_A.x_rodzajReprGeom,
                    xyline = x.OT_BUIT_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== BUIT_P =====================================
    public class Parser_BUIT_P_types : BDOT10kParser<BDOT10k, BUIT_P.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_BUIT_P.x_kod,
                    geomtype = x.OT_BUIT_P.x_rodzajReprGeom
                });
        }
    }
    public class Parser_BUIT_P : BDOT10kParser<BDOT10k_P, BUIT_P.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_P> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_P
                {
                    xkod = x.OT_BUIT_P.x_kod,
                    geomtype = x.OT_BUIT_P.x_rodzajReprGeom,
                    xypoint = x.OT_BUIT_P.geometria.Point.pos
                });
        }
    }

    //===================================== BUSP_A =====================================
    public class Parser_BUSP_A_types : BDOT10kParser<BDOT10k, BUSP_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_BUSP_A.x_kod,
                    geomtype = x.OT_BUSP_A.x_rodzajReprGeom
                });
        }
    }
    public class Parser_BUSP_A : BDOT10kParser<BDOT10k_AL, BUSP_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_BUSP_A.x_kod,
                    geomtype = x.OT_BUSP_A.x_rodzajReprGeom,
                    xyline = x.OT_BUSP_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== BUSP_L =====================================
    //NIEPEŁNY MODEL

    //public class Parser_BUSP_L_types : BDOT10kParser<BDOT10k, BUSP_L.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k
    //            {
    //                xkod = x.OT_BUSP_L.x_kod,
    //                geomtype = x.OT_BUSP_L.x_rodzajReprGeom
    //            });
    //    }
    //}
    //public class Parser_BUSP_L : BDOT10kParser<BDOT10k_AL, BUSP_L.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k_AL
    //            {
    //                xkod = x.OT_BUSP_L.x_kod,
    //                geomtype = x.OT_BUSP_L.x_rodzajReprGeom,
    //                xyline = x.OT_BUSP_L.geometria.Curve.segments.LineStringSegment.posList
    //            });
    //    }
    //}

    //===================================== BUTR_L =====================================
    public class Parser_BUTR_L_types : BDOT10kParser<BDOT10k, BUTR_L.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_BUTR_L.x_kod,
                    geomtype = x.OT_BUTR_L.x_rodzajReprGeom
                });
        }
    }
    public class Parser_BUTR_L : BDOT10kParser<BDOT10k_AL, BUTR_L.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_BUTR_L.x_kod,
                    geomtype = x.OT_BUTR_L.x_rodzajReprGeom,
                    xyline = x.OT_BUTR_L.geometria.Curve.segments.LineStringSegment.posList
                });
        }
    }

    //===================================== BUTR_P =====================================
    //NIEPEŁNY MODEL

    //public class Parser_BUTR_P_types : BDOT10kParser<BDOT10k, BUTR_P.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k
    //            {
    //                xkod = x.OT_BUTR_P.x_kod,
    //                geomtype = x.OT_BUTR_P.x_rodzajReprGeom
    //            });
    //    }
    //}
    //public class Parser_BUTR_P : BDOT10kParser<BDOT10k_P, BUTR_P.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k_P> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k_P
    //            {
    //                xkod = x.OT_BUTR_P.x_kod,
    //                geomtype = x.OT_BUTR_P.x_rodzajReprGeom,
    //                xypoint = x.OT_BUTR_P.geometria.geometria.Point.pos
    //            });
    //    }
    //}

    //===================================== BUUO_L =====================================
    public class Parser_BUUO_L_types : BDOT10kParser<BDOT10k, BUUO_L.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_BUUO_L.x_kod,
                    geomtype = x.OT_BUUO_L.x_rodzajReprGeom
                });
        }
    }
    public class Parser_BUUO_L : BDOT10kParser<BDOT10k_AL, BUUO_L.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_BUUO_L.x_kod,
                    geomtype = x.OT_BUUO_L.x_rodzajReprGeom,
                    xyline = x.OT_BUUO_L.geometria.Curve.segments.LineStringSegment.posList
                });
        }
    }

    //===================================== BUWT_A =====================================
    //NIEPEŁNY MODEL

    //public class Parser_BUWT_A_types : BDOT10kParser<BDOT10k, BUWT_A.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k
    //            {
    //                xkod = x.OT_BUWT_A.x_kod,
    //                geomtype = x.OT_BUWT_A.x_rodzajReprGeom
    //            });
    //    }
    //}
    //public class Parser_BUWT_A : BDOT10kParser<BDOT10k_AL, BUWT_A.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k_AL
    //            {
    //                xkod = x.OT_BUWT_A.x_kod,
    //                geomtype = x.OT_BUWT_A.x_rodzajReprGeom,
    //                xyline = x.OT_BUWT_A.geometria.Polygon.exterior.LinearRing.posList
    //            });
    //    }
    //},

    //===================================== BUWT_P =====================================
    public class Parser_BUWT_P_types : BDOT10kParser<BDOT10k, BUWT_P.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_BUWT_P.x_kod,
                    geomtype = x.OT_BUWT_P.x_rodzajReprGeom
                });
        }
    }
    public class Parser_BUWT_P : BDOT10kParser<BDOT10k_P, BUWT_P.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_P> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_P
                {
                    xkod = x.OT_BUWT_P.x_kod,
                    geomtype = x.OT_BUWT_P.x_rodzajReprGeom,
                    xypoint = x.OT_BUWT_P.geometria.Point.pos
                });
        }
    }

    //===================================== BUZM_L =====================================
    public class Parser_BUZM_L_types : BDOT10kParser<BDOT10k, BUZM_L.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_BUZM_L.x_kod,
                    geomtype = x.OT_BUZM_L.x_rodzajReprGeom
                });
        }
    }
    public class Parser_BUZM_L : BDOT10kParser<BDOT10k_AL, BUZM_L.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_BUZM_L.x_kod,
                    geomtype = x.OT_BUZM_L.x_rodzajReprGeom,
                    xyline = x.OT_BUZM_L.geometria.Curve.segments.LineStringSegment.posList
                });
        }
    }

    //===================================== BUZT_A =====================================
    public class Parser_BUZT_A_types : BDOT10kParser<BDOT10k, BUZT_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_BUZT_A.x_kod,
                    geomtype = x.OT_BUZT_A.x_rodzajReprGeom
                });
        }
    }
    public class Parser_BUZT_A : BDOT10kParser<BDOT10k_AL, BUZT_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_BUZT_A.x_kod,
                    geomtype = x.OT_BUZT_A.x_rodzajReprGeom,
                    xyline = x.OT_BUZT_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== BUZT_P =====================================
    public class Parser_BUZT_P_types : BDOT10kParser<BDOT10k, BUZT_P.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_BUZT_P.x_kod,
                    geomtype = x.OT_BUZT_P.x_rodzajReprGeom
                });
        }
    }
    public class Parser_BUZT_P : BDOT10kParser<BDOT10k_P, BUZT_P.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_P> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_P
                {
                    xkod = x.OT_BUZT_P.x_kod,
                    geomtype = x.OT_BUZT_P.x_rodzajReprGeom,
                    xypoint = x.OT_BUZT_P.geometria.Point.pos
                });
        }
    }

    //===================================== KUHO_A =====================================
    //COŚ NIE TAK Z OBIEKTAMI WEWNĄTRZ LUB Z ODWOŁANIEM DO NICH!!!

    //public class Parser_KUHO_A_types : BDOT10kParser<BDOT10k, KUHO_A.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k
    //            {
    //                xkod = x.OT_KUHO_A.x_kod,
    //                geomtype = x.OT_KUHO_A.x_rodzajReprGeom
    //            });
    //    }
    //}
    //public class Parser_KUHO_A : BDOT10kParser<BDOT10k_AL, KUHO_A.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k_AL
    //            {
    //                xkod = x.OT_KUHO_A.x_kod,
    //                geomtype = x.OT_KUHO_A.x_rodzajReprGeom,
    //                xyline = x.OT_KUHO_A.geometria.Polygon.exterior.LinearRing.posList
    //            });
    //    }
    //}

    //===================================== KUHU_A =====================================
    public class Parser_KUHU_A_types : BDOT10kParser<BDOT10k, KUHU_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_KUHU_A.x_kod,
                    geomtype = x.OT_KUHU_A.x_rodzajReprGeom
                });
        }
    }
    public class Parser_KUHU_A : BDOT10kParser<BDOT10k_AL, KUHU_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_KUHU_A.x_kod,
                    geomtype = x.OT_KUHU_A.x_rodzajReprGeom,
                    xyline = x.OT_KUHU_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== KUHU_P =====================================
    public class Parser_KUHU_P_types : BDOT10kParser<BDOT10k, KUHU_P.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_KUHU_P.x_kod,
                    geomtype = x.OT_KUHU_P.x_rodzajReprGeom
                });
        }
    }
    public class Parser_KUHU_P : BDOT10kParser<BDOT10k_P, KUHU_P.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_P> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_P
                {
                    xkod = x.OT_KUHU_P.x_kod,
                    geomtype = x.OT_KUHU_P.x_rodzajReprGeom,
                    xypoint = x.OT_KUHU_P.geometria.Point.pos
                });
        }
    }

    //===================================== KUIK_A =====================================
    //NIEPEŁNY MODEL

    //public class Parser_KUIK_A_types : BDOT10kParser<BDOT10k, KUIK_A.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k
    //            {
    //                xkod = x.OT_KUIK_A.x_kod,
    //                geomtype = x.OT_KUIK_A.x_rodzajReprGeom
    //            });
    //    }
    //}
    //public class Parser_KUIK_A : BDOT10kParser<BDOT10k_AL, KUIK_A.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k_AL
    //            {
    //                xkod = x.OT_KUIK_A.x_kod,
    //                geomtype = x.OT_KUIK_A.x_rodzajReprGeom,
    //                xyline = x.OT_KUIK_A.geometria.Polygon.exterior.LinearRing.posList
    //            });
    //    }
    //}

    //===================================== KUKO_A =====================================
    public class Parser_KUKO_A_types : BDOT10kParser<BDOT10k, KUKO_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_KUKO_A.x_kod,
                    geomtype = x.OT_KUKO_A.x_rodzajReprGeom
                });
        }
    }
    public class Parser_KUKO_A : BDOT10kParser<BDOT10k_AL, KUKO_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_KUKO_A.x_kod,
                    geomtype = x.OT_KUKO_A.x_rodzajReprGeom,
                    xyline = x.OT_KUKO_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== KUKO_P =====================================
    //COŚ NIE TAK Z OBIEKTAMI WEWNĄTRZ LUB Z ODWOŁANIEM DO NICH!!!

    //public class Parser_KUKO_P_types : BDOT10kParser<BDOT10k, KUKO_P.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k
    //            {
    //                xkod = x.OT_KUKO_P.x_kod,
    //                geomtype = x.OT_KUKO_P.x_rodzajReprGeom
    //            });
    //    }
    //}
    //public class Parser_KUKO_P : BDOT10kParser<BDOT10k_P, KUKO_P.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k_P> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k_P
    //            {
    //                xkod = x.OT_KUKO_P.x_kod,
    //                geomtype = x.OT_KUKO_P.x_rodzajReprGeom,
    //                xypoint = x.OT_KUKO_P.geometria.Point.pos
    //            });
    //    }
    //}

    //===================================== KUMN_A =====================================
    public class Parser_KUMN_A_types : BDOT10kParser<BDOT10k, KUMN_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_KUMN_A.x_kod,
                    geomtype = x.OT_KUMN_A.x_rodzajReprGeom
                });
        }
    }
    public class Parser_KUMN_A : BDOT10kParser<BDOT10k_AL, KUMN_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_KUMN_A.x_kod,
                    geomtype = x.OT_KUMN_A.x_rodzajReprGeom,
                    xyline = x.OT_KUMN_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== KUOS_A =====================================
    public class Parser_KUOS_A_types : BDOT10kParser<BDOT10k, KUOS_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_KUOS_A.x_kod,
                    geomtype = x.OT_KUOS_A.x_rodzajReprGeom
                });
        }
    }
    public class Parser_KUOS_A : BDOT10kParser<BDOT10k_AL, KUOS_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_KUOS_A.x_kod,
                    geomtype = x.OT_KUOS_A.x_rodzajReprGeom,
                    xyline = x.OT_KUOS_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== KUOZ_A =====================================
    public class Parser_KUOZ_A_types : BDOT10kParser<BDOT10k, KUOZ_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_KUOZ_A.x_kod,
                    geomtype = x.OT_KUOZ_A.x_rodzajReprGeom
                });
        }
    }
    public class Parser_KUOZ_A : BDOT10kParser<BDOT10k_AL, KUOZ_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_KUOZ_A.x_kod,
                    geomtype = x.OT_KUOZ_A.x_rodzajReprGeom,
                    xyline = x.OT_KUOZ_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== KUPG_A =====================================
    public class Parser_KUPG_A_types : BDOT10kParser<BDOT10k, KUPG_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_KUPG_A.x_kod,
                    geomtype = x.OT_KUPG_A.x_rodzajReprGeom
                });
        }
    }
    public class Parser_KUPG_A : BDOT10kParser<BDOT10k_AL, KUPG_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_KUPG_A.x_kod,
                    geomtype = x.OT_KUPG_A.x_rodzajReprGeom,
                    xyline = x.OT_KUPG_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== KUPG_P =====================================
    //COŚ NIE TAK Z OBIEKTAMI WEWNĄTRZ LUB Z ODWOŁANIEM DO NICH!!!

    //public class Parser_KUPG_P_types : BDOT10kParser<BDOT10k, KUPG_P.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k
    //            {
    //                xkod = x.OT_KUPG_P.x_kod,
    //                geomtype = x.OT_KUPG_P.x_rodzajReprGeom
    //            });
    //    }
    //}
    //public class Parser_KUPG_P : BDOT10kParser<BDOT10k_P, KUPG_P.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k_P> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k_P
    //            {
    //                xkod = x.OT_KUPG_P.x_kod,
    //                geomtype = x.OT_KUPG_P.x_rodzajReprGeom,
    //                xypoint = x.OT_KUPG_P.geometria.Point.pos
    //            });
    //    }
    //}

    //===================================== KUSC_A =====================================
    public class Parser_KUSC_A_types : BDOT10kParser<BDOT10k, KUSC_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_KUSC_A.x_kod,
                    geomtype = x.OT_KUSC_A.x_rodzajReprGeom
                });
        }
    }
    public class Parser_KUSC_A : BDOT10kParser<BDOT10k_AL, KUSC_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_KUSC_A.x_kod,
                    geomtype = x.OT_KUSC_A.x_rodzajReprGeom,
                    xyline = x.OT_KUSC_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== KUSK_A =====================================
    public class Parser_KUSK_A_types : BDOT10kParser<BDOT10k, KUSK_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_KUSK_A.x_kod,
                    geomtype = x.OT_KUSK_A.x_rodzajReprGeom
                });
        }
    }
    public class Parser_KUSK_A : BDOT10kParser<BDOT10k_AL, KUSK_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_KUSK_A.x_kod,
                    geomtype = x.OT_KUSK_A.x_rodzajReprGeom,
                    xyline = x.OT_KUSK_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== KUZA_A =====================================
    //COŚ NIE TAK Z OBIEKTAMI WEWNĄTRZ LUB Z ODWOŁANIEM DO NICH!!!

    //public class Parser_KUZA_A_types : BDOT10kParser<BDOT10k, KUZA_A.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k
    //            {
    //                xkod = x.OT_KUZA_A.x_kod,
    //                geomtype = x.OT_KUZA_A.x_rodzajReprGeom
    //            });
    //    }
    //}
    //public class Parser_KUZA_A : BDOT10kParser<BDOT10k_AL, KUZA_A.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k_AL
    //            {
    //                xkod = x.OT_KUZA_A.x_kod,
    //                geomtype = x.OT_KUZA_A.x_rodzajReprGeom,
    //                xyline = x.OT_KUZA_A.geometria.Polygon.exterior.LinearRing.posList
    //            });
    //    }
    //}

    //===================================== OIKM_A =====================================
    //NIEPEŁNY MODEL

    //public class Parser_OIKM_A_types : BDOT10kParser<BDOT10k, OIKM_A.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k
    //            {
    //                xkod = x.OT_OIKM_A.x_kod,
    //                geomtype = x.OT_OIKM_A.x_rodzajReprGeom
    //            });
    //    }
    //}
    //public class Parser_OIKM_A : BDOT10kParser<BDOT10k_AL, OIKM_A.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k_AL
    //            {
    //                xkod = x.OT_OIKM_A.x_kod,
    //                geomtype = x.OT_OIKM_A.x_rodzajReprGeom,
    //                xyline = x.OT_OIKM_A.geometria.Polygon.exterior.LinearRing.posList
    //            });
    //    }
    //}

    //===================================== OIKM_L =====================================
    //NIEPEŁNY MODEL

    //public class Parser_OIKM_L_types : BDOT10kParser<BDOT10k, OIKM_L.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k
    //            {
    //                xkod = x.OT_OIKM_L.x_kod,
    //                geomtype = x.OT_OIKM_L.x_rodzajReprGeom
    //            });
    //    }
    //}
    //public class Parser_OIKM_L : BDOT10kParser<BDOT10k_AL, OIKM_L.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k_AL
    //            {
    //                xkod = x.OT_OIKM_L.x_kod,
    //                geomtype = x.OT_OIKM_L.x_rodzajReprGeom,
    //                xyline = x.OT_OIKM_L.geometria.Curve.segments.LineStringSegment.posList
    //            });
    //    }
    //}

    //===================================== OIKM_P =====================================
    public class Parser_OIKM_P_types : BDOT10kParser<BDOT10k, OIKM_P.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_OIKM_P.x_kod,
                    geomtype = x.OT_OIKM_P.x_rodzajReprGeom
                });
        }
    }
    public class Parser_OIKM_P : BDOT10kParser<BDOT10k_P, OIKM_P.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_P> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_P
                {
                    xkod = x.OT_OIKM_P.x_kod,
                    geomtype = x.OT_OIKM_P.x_rodzajReprGeom,
                    xypoint = x.OT_OIKM_P.geometria.Point.pos
                });
        }
    }

    //===================================== OIMK_A =====================================
    public class Parser_OIMK_A_types : BDOT10kParser<BDOT10k, OIMK_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_OIMK_A.x_kod,
                    geomtype = x.OT_OIMK_A.x_rodzajReprGeom
                });
        }
    }
    public class Parser_OIMK_A : BDOT10kParser<BDOT10k_AL, OIMK_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_OIMK_A.x_kod,
                    geomtype = x.OT_OIMK_A.x_rodzajReprGeom,
                    xyline = x.OT_OIMK_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== OIOR_A =====================================
    public class Parser_OIOR_A_types : BDOT10kParser<BDOT10k, OIOR_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_OIOR_A.x_kod,
                    geomtype = x.OT_OIOR_A.x_rodzajReprGeom
                });
        }
    }
    public class Parser_OIOR_A : BDOT10kParser<BDOT10k_AL, OIOR_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_OIOR_A.x_kod,
                    geomtype = x.OT_OIOR_A.x_rodzajReprGeom,
                    xyline = x.OT_OIOR_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== OIOR_L =====================================
    public class Parser_OIOR_L_types : BDOT10kParser<BDOT10k, OIOR_L.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_OIOR_L.x_kod,
                    geomtype = x.OT_OIOR_L.x_rodzajReprGeom
                });
        }
    }
    public class Parser_OIOR_L : BDOT10kParser<BDOT10k_AL, OIOR_L.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_OIOR_L.x_kod,
                    geomtype = x.OT_OIOR_L.x_rodzajReprGeom,
                    xyline = x.OT_OIOR_L.geometria.Curve.segments.LineStringSegment.posList
                });
        }
    }

    //===================================== OIOR_P =====================================
    public class Parser_OIOR_P_types : BDOT10kParser<BDOT10k, OIOR_P.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_OIOR_P.x_kod,
                    geomtype = x.OT_OIOR_P.x_rodzajReprGeom
                });
        }
    }
    public class Parser_OIOR_P : BDOT10kParser<BDOT10k_P, OIOR_P.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_P> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_P
                {
                    xkod = x.OT_OIOR_P.x_kod,
                    geomtype = x.OT_OIOR_P.x_rodzajReprGeom,
                    xypoint = x.OT_OIOR_P.geometria.Point.pos
                });
        }
    }

    //===================================== OIPR_L =====================================
    public class Parser_OIPR_L_types : BDOT10kParser<BDOT10k, OIPR_L.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_OIPR_L.x_kod,
                    geomtype = x.OT_OIPR_L.x_rodzajReprGeom
                });
        }
    }
    public class Parser_OIPR_L : BDOT10kParser<BDOT10k_AL, OIPR_L.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_OIPR_L.x_kod,
                    geomtype = x.OT_OIPR_L.x_rodzajReprGeom,
                    xyline = x.OT_OIPR_L.geometria.Curve.segments.LineStringSegment.posList
                });
        }
    }

    //===================================== OIPR_P =====================================
    public class Parser_OIPR_P_types : BDOT10kParser<BDOT10k, OIPR_P.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_OIPR_P.x_kod,
                    geomtype = x.OT_OIPR_P.x_rodzajReprGeom
                });
        }
    }
    public class Parser_OIPR_P : BDOT10kParser<BDOT10k_P, OIPR_P.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_P> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_P
                {
                    xkod = x.OT_OIPR_P.x_kod,
                    geomtype = x.OT_OIPR_P.x_rodzajReprGeom,
                    xypoint = x.OT_OIPR_P.geometria.Point.pos
                });
        }
    }

    //===================================== OISZ_A =====================================
    public class Parser_OISZ_A_types : BDOT10kParser<BDOT10k, OISZ_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_OISZ_A.x_kod,
                    geomtype = x.OT_OISZ_A.x_rodzajReprGeom
                });
        }
    }
    public class Parser_OISZ_A : BDOT10kParser<BDOT10k_AL, OISZ_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_OISZ_A.x_kod,
                    geomtype = x.OT_OISZ_A.x_rodzajReprGeom,
                    xyline = x.OT_OISZ_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== PTGN_A =====================================
    public class Parser_PTGN_A_types : BDOT10kParser<BDOT10k, PTGN_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_PTGN_A.x_kod,
                    geomtype = x.OT_PTGN_A.x_rodzajReprGeom
                });
        }
    }
    public class Parser_PTGN_A : BDOT10kParser<BDOT10k_AL, PTGN_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_PTGN_A.x_kod,
                    geomtype = x.OT_PTGN_A.x_rodzajReprGeom,
                    xyline = x.OT_PTGN_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== PTKM_A =====================================
    public class Parser_PTKM_A_types : BDOT10kParser<BDOT10k, PTKM_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_PTKM_A.x_kod,
                    geomtype = x.OT_PTKM_A.x_rodzajReprGeom
                });
        }
    }
    public class Parser_PTKM_A : BDOT10kParser<BDOT10k_AL, PTKM_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_PTKM_A.x_kod,
                    geomtype = x.OT_PTKM_A.x_rodzajReprGeom,
                    xyline = x.OT_PTKM_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== PTLZ_A =====================================
    public class Parser_PTLZ_A_types : BDOT10kParser<BDOT10k, PTLZ_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_PTLZ_A.x_kod,
                    geomtype = x.OT_PTLZ_A.x_rodzajReprGeom
                });
        }
    }
    public class Parser_PTLZ_A : BDOT10kParser<BDOT10k_AL, PTLZ_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_PTLZ_A.x_kod,
                    geomtype = x.OT_PTLZ_A.x_rodzajReprGeom,
                    xyline = x.OT_PTLZ_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== PTNZ_A =====================================
    public class Parser_PTNZ_A_types : BDOT10kParser<BDOT10k, PTNZ_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_PTNZ_A.x_kod,
                    geomtype = x.OT_PTNZ_A.x_rodzajReprGeom
                });
        }
    }
    public class Parser_PTNZ_A : BDOT10kParser<BDOT10k_AL, PTNZ_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_PTNZ_A.x_kod,
                    geomtype = x.OT_PTNZ_A.x_rodzajReprGeom,
                    xyline = x.OT_PTNZ_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== PTPL_A =====================================
    public class Parser_PTPL_A_types : BDOT10kParser<BDOT10k, PTPL_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_PTPL_A.x_kod,
                    geomtype = x.OT_PTPL_A.x_rodzajReprGeom
                });
        }
    }
    public class Parser_PTPL_A : BDOT10kParser<BDOT10k_AL, PTPL_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_PTPL_A.x_kod,
                    geomtype = x.OT_PTPL_A.x_rodzajReprGeom,
                    xyline = x.OT_PTPL_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== PTRK_A =====================================
    public class Parser_PTRK_A_types : BDOT10kParser<BDOT10k, PTRK_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_PTRK_A.x_kod,
                    geomtype = x.OT_PTRK_A.x_rodzajReprGeom
                });
        }
    }
    public class Parser_PTRK_A : BDOT10kParser<BDOT10k_AL, PTRK_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_PTRK_A.x_kod,
                    geomtype = x.OT_PTRK_A.x_rodzajReprGeom,
                    xyline = x.OT_PTRK_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== PTSO_A =====================================
    public class Parser_PTSO_A_types : BDOT10kParser<BDOT10k, PTSO_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_PTSO_A.x_kod,
                    geomtype = x.OT_PTSO_A.x_rodzajReprGeom
                });
        }
    }
    public class Parser_PTSO_A : BDOT10kParser<BDOT10k_AL, PTSO_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_PTSO_A.x_kod,
                    geomtype = x.OT_PTSO_A.x_rodzajReprGeom,
                    xyline = x.OT_PTSO_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== PTTR_A =====================================
    public class Parser_PTTR_A_types : BDOT10kParser<BDOT10k, PTTR_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_PTTR_A.x_kod,
                    geomtype = x.OT_PTTR_A.x_rodzajReprGeom
                });
        }
    }
    public class Parser_PTTR_A : BDOT10kParser<BDOT10k_AL, PTTR_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_PTTR_A.x_kod,
                    geomtype = x.OT_PTTR_A.x_rodzajReprGeom,
                    xyline = x.OT_PTTR_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== PTUT_A =====================================
    public class Parser_PTUT_A_types : BDOT10kParser<BDOT10k, PTUT_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_PTUT_A.x_kod,
                    geomtype = x.OT_PTUT_A.x_rodzajReprGeom
                });
        }
    }
    public class Parser_PTUT_A : BDOT10kParser<BDOT10k_AL, PTUT_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_PTUT_A.x_kod,
                    geomtype = x.OT_PTUT_A.x_rodzajReprGeom,
                    xyline = x.OT_PTUT_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== PTWP_A =====================================
    public class Parser_PTWP_A_types : BDOT10kParser<BDOT10k, PTWP_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_PTWP_A.x_kod,
                    geomtype = x.OT_PTWP_A.x_rodzajReprGeom
                });
        }
    }
    public class Parser_PTWP_A : BDOT10kParser<BDOT10k_AL, PTWP_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_PTWP_A.x_kod,
                    geomtype = x.OT_PTWP_A.x_rodzajReprGeom,
                    xyline = x.OT_PTWP_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== PTWZ_A =====================================
    public class Parser_PTWZ_A_types : BDOT10kParser<BDOT10k, PTWZ_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_PTWZ_A.x_kod,
                    geomtype = x.OT_PTWZ_A.x_rodzajReprGeom
                });
        }
    }
    public class Parser_PTWZ_A : BDOT10kParser<BDOT10k_AL, PTWZ_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_PTWZ_A.x_kod,
                    geomtype = x.OT_PTWZ_A.x_rodzajReprGeom,
                    xyline = x.OT_PTWZ_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== PTZB_A =====================================
    public class Parser_PTZB_A_types : BDOT10kParser<BDOT10k, PTZB_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_PTZB_A.x_kod,
                    geomtype = x.OT_PTZB_A.x_rodzajReprGeom
                });
        }
    }
    public class Parser_PTZB_A : BDOT10kParser<BDOT10k_AL, PTZB_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_PTZB_A.x_kod,
                    geomtype = x.OT_PTZB_A.x_rodzajReprGeom,
                    xyline = x.OT_PTZB_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== SKDR_L =====================================
    public class Parser_SKDR_L_types : BDOT10kParser<BDOT10k, SKDR_L.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_SKDR_L.x_kod,
                    geomtype = x.OT_SKDR_L.x_rodzajReprGeom
                });
        }
    }
    public class Parser_SKDR_L : BDOT10kParser<BDOT10k_AL, SKDR_L.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_SKDR_L.x_kod,
                    geomtype = x.OT_SKDR_L.x_rodzajReprGeom,
                    xyline = x.OT_SKDR_L.geometria.Curve.segments.LineStringSegment.posList
                });
        }
    }

    //===================================== SKJZ_L =====================================
    public class Parser_SKJZ_L_types : BDOT10kParser<BDOT10k, SKJZ_L.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_SKJZ_L.x_kod,
                    geomtype = x.OT_SKJZ_L.x_rodzajReprGeom
                });
        }
    }
    public class Parser_SKJZ_L : BDOT10kParser<BDOT10k_AL, SKJZ_L.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_SKJZ_L.x_kod,
                    geomtype = x.OT_SKJZ_L.x_rodzajReprGeom,
                    xyline = x.OT_SKJZ_L.geometria.Curve.segments.LineStringSegment.posList
                });
        }
    }

    //===================================== SKPP_L =====================================
    //NIEKOMPLETNY MODEL

    //public class Parser_SKPP_L_types : BDOT10kParser<BDOT10k, SKPP_L.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k
    //            {
    //                xkod = x.OT_SKPP_L.x_kod,
    //                geomtype = x.OT_SKPP_L.x_rodzajReprGeom
    //            });
    //    }
    //}
    //public class Parser_SKPP_L : BDOT10kParser<BDOT10k_AL, SKPP_L.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k_AL
    //            {
    //                xkod = x.OT_SKPP_L.x_kod,
    //                geomtype = x.OT_SKPP_L.x_rodzajReprGeom,
    //                xyline = x.OT_SKPP_L.geometria.Curve.segments.LineStringSegment.posList
    //            });
    //    }
    //}

    //===================================== SKRP_L =====================================
    public class Parser_SKRP_L_types : BDOT10kParser<BDOT10k, SKRP_L.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_SKRP_L.x_kod,
                    geomtype = x.OT_SKRP_L.x_rodzajReprGeom
                });
        }
    }
    public class Parser_SKRP_L : BDOT10kParser<BDOT10k_AL, SKRP_L.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_SKRP_L.x_kod,
                    geomtype = x.OT_SKRP_L.x_rodzajReprGeom,
                    xyline = x.OT_SKRP_L.geometria.Curve.segments.LineStringSegment.posList
                });
        }
    }

    //===================================== SKRW_P =====================================
    public class Parser_SKRW_P_types : BDOT10kParser<BDOT10k, SKRW_P.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_SKRW_P.x_kod,
                    geomtype = x.OT_SKRW_P.x_rodzajReprGeom
                });
        }
    }
    public class Parser_SKRW_P : BDOT10kParser<BDOT10k_P, SKRW_P.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_P> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_P
                {
                    xkod = x.OT_SKRW_P.x_kod,
                    geomtype = x.OT_SKRW_P.x_rodzajReprGeom,
                    xypoint = x.OT_SKRW_P.geometria.Point.pos
                });
        }
    }

    //===================================== SKTR_L =====================================
    //NIEKOMPLETNY MODEL

    //public class Parser_SKTR_L_types : BDOT10kParser<BDOT10k, SKTR_L.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k
    //            {
    //                xkod = x.OT_SKTR_L.x_kod,
    //                geomtype = x.OT_SKTR_L.x_rodzajReprGeom
    //            });
    //    }
    //}
    //public class Parser_SKTR_L : BDOT10kParser<BDOT10k_AL, SKTR_L.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k_AL
    //            {
    //                xkod = x.OT_SKTR_L.x_kod,
    //                geomtype = x.OT_SKTR_L.x_rodzajReprGeom,
    //                xyline = x.OT_SKTR_L.geometria.Curve.segments.LineStringSegment.posList
    //            });
    //    }
    //}

    //===================================== SULN_L =====================================
    public class Parser_SULN_L_types : BDOT10kParser<BDOT10k, SULN_L.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_SULN_L.x_kod,
                    geomtype = x.OT_SULN_L.x_rodzajReprGeom
                });
        }
    }
    public class Parser_SULN_L : BDOT10kParser<BDOT10k_AL, SULN_L.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_SULN_L.x_kod,
                    geomtype = x.OT_SULN_L.x_rodzajReprGeom,
                    xyline = x.OT_SULN_L.geometria.Curve.segments.LineStringSegment.posList
                });
        }
    }

    //===================================== SUPR_L =====================================
    //NIEKOMPLETNY MODEL

    //public class Parser_SUPR_L_types : BDOT10kParser<BDOT10k, SUPR_L.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k
    //            {
    //                xkod = x.OT_SUPR_L.x_kod,
    //                geomtype = x.OT_SUPR_L.x_rodzajReprGeom
    //            });
    //    }
    //}
    //public class Parser_SUPR_L : BDOT10kParser<BDOT10k_AL, SUPR_L.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k_AL
    //            {
    //                xkod = x.OT_SUPR_L.x_kod,
    //                geomtype = x.OT_SUPR_L.x_rodzajReprGeom,
    //                xyline = x.OT_SUPR_L.geometria.Curve.segments.LineStringSegment.posList
    //            });
    //    }
    //}

    //===================================== SWKN_L =====================================
    //NIEKOMPLETNY MODEL

    //public class Parser_SWKN_L_types : BDOT10kParser<BDOT10k, SWKN_L.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k
    //            {
    //                xkod = x.OT_SWKN_L.x_kod,
    //                geomtype = x.OT_SWKN_L.x_rodzajReprGeom
    //            });
    //    }
    //}
    //public class Parser_SWKN_L : BDOT10kParser<BDOT10k_AL, SWKN_L.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k_AL
    //            {
    //                xkod = x.OT_SWKN_L.x_kod,
    //                geomtype = x.OT_SWKN_L.x_rodzajReprGeom,
    //                xyline = x.OT_SWKN_L.geometria.Curve.segments.LineStringSegment.posList
    //            });
    //    }
    //}

    //===================================== SWRM_L =====================================
    public class Parser_SWRM_L_types : BDOT10kParser<BDOT10k, SWRM_L.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_SWRM_L.x_kod,
                    geomtype = x.OT_SWRM_L.x_rodzajReprGeom
                });
        }
    }
    public class Parser_SWRM_L : BDOT10kParser<BDOT10k_AL, SWRM_L.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_SWRM_L.x_kod,
                    geomtype = x.OT_SWRM_L.x_rodzajReprGeom,
                    xyline = x.OT_SWRM_L.geometria.Curve.segments.LineStringSegment.posList
                });
        }
    }

    //===================================== SWRS_L =====================================
    public class Parser_SWRS_L_types : BDOT10kParser<BDOT10k, SWRS_L.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_SWRS_L.x_kod,
                    geomtype = x.OT_SWRS_L.x_rodzajReprGeom
                });
        }
    }
    public class Parser_SWRS_L : BDOT10kParser<BDOT10k_AL, SWRS_L.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_SWRS_L.x_kod,
                    geomtype = x.OT_SWRS_L.x_rodzajReprGeom,
                    xyline = x.OT_SWRS_L.geometria.Curve.segments.LineStringSegment.posList
                });
        }
    }

    //===================================== TCON_A =====================================
    public class Parser_TCON_A_types : BDOT10kParser<BDOT10k, TCON_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k
                {
                    xkod = x.OT_TCON_A.x_kod,
                    geomtype = x.OT_TCON_A.x_rodzajReprGeom
                });
        }
    }
    public class Parser_TCON_A : BDOT10kParser<BDOT10k_AL, TCON_A.FeatureCollection>
    {
        public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
        {
            return Document.featureMember.Select(x =>
                new BDOT10k_AL
                {
                    xkod = x.OT_TCON_A.x_kod,
                    geomtype = x.OT_TCON_A.x_rodzajReprGeom,
                    xyline = x.OT_TCON_A.geometria.Polygon.exterior.LinearRing.posList
                });
        }
    }

    //===================================== TCPK_A =====================================
    //COŚ NIE TAK Z OBIEKTAMI WEWNĄTRZ LUB Z ODWOŁANIEM DO NICH!!!

    //public class Parser_TCPK_A_types : BDOT10kParser<BDOT10k, TCPK_A.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k
    //            {
    //                xkod = x.OT_TCPK_A.x_kod,
    //                geomtype = x.OT_TCPK_A.x_rodzajReprGeom
    //            });
    //    }
    //}
    //public class Parser_TCPK_A : BDOT10kParser<BDOT10k_AL, TCPK_A.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k_AL
    //            {
    //                xkod = x.OT_TCPK_A.x_kod,
    //                geomtype = x.OT_TCPK_A.x_rodzajReprGeom,
    //                xyline = x.OT_TCPK_A.geometria.Polygon.exterior.LinearRing.posList
    //            });
    //    }
    //}

    //===================================== TCPN_A =====================================
    //NIEKOMPLETNY MODEL

    //public class Parser_TCPN_A_types : BDOT10kParser<BDOT10k, TCPN_A.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k
    //            {
    //                xkod = x.OT_TCPN_A.x_kod,
    //                geomtype = x.OT_TCPN_A.x_rodzajReprGeom
    //            });
    //    }
    //}
    //public class Parser_TCPN_A : BDOT10kParser<BDOT10k_AL, TCPN_A.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k_AL
    //            {
    //                xkod = x.OT_TCPN_A.x_kod,
    //                geomtype = x.OT_TCPN_A.x_rodzajReprGeom,
    //                xyline = x.OT_TCPN_A.geometria.Polygon.exterior.LinearRing.posList
    //            });
    //    }
    //}

    //===================================== TCRZ_A =====================================
    //COŚ NIE TAK Z OBIEKTAMI WEWNĄTRZ LUB Z ODWOŁANIEM DO NICH!!!

    //public class Parser_TCRZ_A_types : BDOT10kParser<BDOT10k, TCRZ_A.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k
    //            {
    //                xkod = x.OT_TCRZ_A.x_kod,
    //                geomtype = x.OT_TCRZ_A.x_rodzajReprGeom
    //            });
    //    }
    //}
    //public class Parser_TCRZ_A : BDOT10kParser<BDOT10k_AL, TCRZ_A.FeatureCollection>
    //{
    //    public override IEnumerable<BDOT10k_AL> GetBDOT10Ks()
    //    {
    //        return Document.featureMember.Select(x =>
    //            new BDOT10k_AL
    //            {
    //                xkod = x.OT_TCRZ_A.x_kod,
    //                geomtype = x.OT_TCRZ_A.x_rodzajReprGeom,
    //                xyline = x.OT_TCRZ_A.geometria.Polygon.exterior.LinearRing.posList
    //            });
    //    }
    //}
}