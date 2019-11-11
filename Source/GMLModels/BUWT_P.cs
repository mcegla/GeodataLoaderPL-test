﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeodataLoader.Source.GMLModels
{
    public abstract class BUWT_P
    {

        // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opengis.net/gml/3.2")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.opengis.net/gml/3.2", IsNullable = false)]
        public partial class FeatureCollection : IGML
        {

            private FeatureCollectionBoundedBy boundedByField;

            private FeatureCollectionFeatureMember[] featureMemberField;

            private string idField;

            /// <remarks/>
            public FeatureCollectionBoundedBy boundedBy
            {
                get
                {
                    return this.boundedByField;
                }
                set
                {
                    this.boundedByField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("featureMember")]
            public FeatureCollectionFeatureMember[] featureMember
            {
                get
                {
                    return this.featureMemberField;
                }
                set
                {
                    this.featureMemberField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
            public string id
            {
                get
                {
                    return this.idField;
                }
                set
                {
                    this.idField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opengis.net/gml/3.2")]
        public partial class FeatureCollectionBoundedBy
        {

            private FeatureCollectionBoundedByEnvelope envelopeField;

            /// <remarks/>
            public FeatureCollectionBoundedByEnvelope Envelope
            {
                get
                {
                    return this.envelopeField;
                }
                set
                {
                    this.envelopeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opengis.net/gml/3.2")]
        public partial class FeatureCollectionBoundedByEnvelope
        {

            private string lowerCornerField;

            private string upperCornerField;

            private string srsNameField;

            /// <remarks/>
            public string lowerCorner
            {
                get
                {
                    return this.lowerCornerField;
                }
                set
                {
                    this.lowerCornerField = value;
                }
            }

            /// <remarks/>
            public string upperCorner
            {
                get
                {
                    return this.upperCornerField;
                }
                set
                {
                    this.upperCornerField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string srsName
            {
                get
                {
                    return this.srsNameField;
                }
                set
                {
                    this.srsNameField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opengis.net/gml/3.2")]
        public partial class FeatureCollectionFeatureMember
        {

            private OT_BUWT_P oT_BUWT_PField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:gugik:specyfikacje:gmlas:bazaDanychObiektowTopograficznych10k:1.0")]
            public OT_BUWT_P OT_BUWT_P
            {
                get
                {
                    return this.oT_BUWT_PField;
                }
                set
                {
                    this.oT_BUWT_PField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:gugik:specyfikacje:gmlas:bazaDanychObiektowTopograficznych10k:1.0")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:gugik:specyfikacje:gmlas:bazaDanychObiektowTopograficznych10k:1.0", IsNullable = false)]
        public partial class OT_BUWT_P
        {

            private OT_BUWT_PIdIIP idIIPField;

            private bool czyObiektBDOOField;

            private string x_kodField;

            private OT_BUWT_PX_skrKarto x_skrKartoField;

            private string x_katDoklGeomField;

            private OT_BUWT_PX_doklGeom x_doklGeomField;

            private string x_zrodloDanychGField;

            private string x_zrodloDanychAField;

            private string x_katIstnieniaField;

            private string x_rodzajReprGeomField;

            private OT_BUWT_PX_uzytkownik x_uzytkownikField;

            private System.DateTime x_aktualnoscGField;

            private System.DateTime x_aktualnoscAField;

            private OT_BUWT_PX_cyklZycia x_cyklZyciaField;

            private System.DateTime x_dataUtworzeniaField;

            private string x_informDodatkowaField;

            private OT_BUWT_PX_kodKarto10k x_kodKarto10kField;

            private OT_BUWT_PX_kodKarto25k x_kodKarto25kField;

            private OT_BUWT_PX_kodKarto50k x_kodKarto50kField;

            private OT_BUWT_PX_kodKarto100k x_kodKarto100kField;

            private OT_BUWT_PX_kodKarto250k x_kodKarto250kField;

            private OT_BUWT_PX_kodKarto500k x_kodKarto500kField;

            private OT_BUWT_PX_kodKarto1000k x_kodKarto1000kField;

            private OT_BUWT_PRodzaj rodzajField;

            private byte wysokoscField;

            private OT_BUWT_PGeometria geometriaField;

            private string idField;

            /// <remarks/>
            public OT_BUWT_PIdIIP idIIP
            {
                get
                {
                    return this.idIIPField;
                }
                set
                {
                    this.idIIPField = value;
                }
            }

            /// <remarks/>
            public bool czyObiektBDOO
            {
                get
                {
                    return this.czyObiektBDOOField;
                }
                set
                {
                    this.czyObiektBDOOField = value;
                }
            }

            /// <remarks/>
            public string x_kod
            {
                get
                {
                    return this.x_kodField;
                }
                set
                {
                    this.x_kodField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public OT_BUWT_PX_skrKarto x_skrKarto
            {
                get
                {
                    return this.x_skrKartoField;
                }
                set
                {
                    this.x_skrKartoField = value;
                }
            }

            /// <remarks/>
            public string x_katDoklGeom
            {
                get
                {
                    return this.x_katDoklGeomField;
                }
                set
                {
                    this.x_katDoklGeomField = value;
                }
            }

            /// <remarks/>
            public OT_BUWT_PX_doklGeom x_doklGeom
            {
                get
                {
                    return this.x_doklGeomField;
                }
                set
                {
                    this.x_doklGeomField = value;
                }
            }

            /// <remarks/>
            public string x_zrodloDanychG
            {
                get
                {
                    return this.x_zrodloDanychGField;
                }
                set
                {
                    this.x_zrodloDanychGField = value;
                }
            }

            /// <remarks/>
            public string x_zrodloDanychA
            {
                get
                {
                    return this.x_zrodloDanychAField;
                }
                set
                {
                    this.x_zrodloDanychAField = value;
                }
            }

            /// <remarks/>
            public string x_katIstnienia
            {
                get
                {
                    return this.x_katIstnieniaField;
                }
                set
                {
                    this.x_katIstnieniaField = value;
                }
            }

            /// <remarks/>
            public string x_rodzajReprGeom
            {
                get
                {
                    return this.x_rodzajReprGeomField;
                }
                set
                {
                    this.x_rodzajReprGeomField = value;
                }
            }

            /// <remarks/>
            public OT_BUWT_PX_uzytkownik x_uzytkownik
            {
                get
                {
                    return this.x_uzytkownikField;
                }
                set
                {
                    this.x_uzytkownikField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
            public System.DateTime x_aktualnoscG
            {
                get
                {
                    return this.x_aktualnoscGField;
                }
                set
                {
                    this.x_aktualnoscGField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
            public System.DateTime x_aktualnoscA
            {
                get
                {
                    return this.x_aktualnoscAField;
                }
                set
                {
                    this.x_aktualnoscAField = value;
                }
            }

            /// <remarks/>
            public OT_BUWT_PX_cyklZycia x_cyklZycia
            {
                get
                {
                    return this.x_cyklZyciaField;
                }
                set
                {
                    this.x_cyklZyciaField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
            public System.DateTime x_dataUtworzenia
            {
                get
                {
                    return this.x_dataUtworzeniaField;
                }
                set
                {
                    this.x_dataUtworzeniaField = value;
                }
            }

            /// <remarks/>
            public string x_informDodatkowa
            {
                get
                {
                    return this.x_informDodatkowaField;
                }
                set
                {
                    this.x_informDodatkowaField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public OT_BUWT_PX_kodKarto10k x_kodKarto10k
            {
                get
                {
                    return this.x_kodKarto10kField;
                }
                set
                {
                    this.x_kodKarto10kField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public OT_BUWT_PX_kodKarto25k x_kodKarto25k
            {
                get
                {
                    return this.x_kodKarto25kField;
                }
                set
                {
                    this.x_kodKarto25kField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public OT_BUWT_PX_kodKarto50k x_kodKarto50k
            {
                get
                {
                    return this.x_kodKarto50kField;
                }
                set
                {
                    this.x_kodKarto50kField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public OT_BUWT_PX_kodKarto100k x_kodKarto100k
            {
                get
                {
                    return this.x_kodKarto100kField;
                }
                set
                {
                    this.x_kodKarto100kField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public OT_BUWT_PX_kodKarto250k x_kodKarto250k
            {
                get
                {
                    return this.x_kodKarto250kField;
                }
                set
                {
                    this.x_kodKarto250kField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public OT_BUWT_PX_kodKarto500k x_kodKarto500k
            {
                get
                {
                    return this.x_kodKarto500kField;
                }
                set
                {
                    this.x_kodKarto500kField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public OT_BUWT_PX_kodKarto1000k x_kodKarto1000k
            {
                get
                {
                    return this.x_kodKarto1000kField;
                }
                set
                {
                    this.x_kodKarto1000kField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public OT_BUWT_PRodzaj rodzaj
            {
                get
                {
                    return this.rodzajField;
                }
                set
                {
                    this.rodzajField = value;
                }
            }

            /// <remarks/>
            public byte wysokosc
            {
                get
                {
                    return this.wysokoscField;
                }
                set
                {
                    this.wysokoscField = value;
                }
            }

            /// <remarks/>
            public OT_BUWT_PGeometria geometria
            {
                get
                {
                    return this.geometriaField;
                }
                set
                {
                    this.geometriaField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://www.opengis.net/gml/3.2")]
            public string id
            {
                get
                {
                    return this.idField;
                }
                set
                {
                    this.idField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:gugik:specyfikacje:gmlas:bazaDanychObiektowTopograficznych10k:1.0")]
        public partial class OT_BUWT_PIdIIP
        {

            private BT_Identyfikator bT_IdentyfikatorField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:gugik:specyfikacje:gmlas:modelPodstawowy:1.0")]
            public BT_Identyfikator BT_Identyfikator
            {
                get
                {
                    return this.bT_IdentyfikatorField;
                }
                set
                {
                    this.bT_IdentyfikatorField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:gugik:specyfikacje:gmlas:modelPodstawowy:1.0")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:gugik:specyfikacje:gmlas:modelPodstawowy:1.0", IsNullable = false)]
        public partial class BT_Identyfikator
        {

            private string lokalnyIdField;

            private string przestrzenNazwField;

            private System.DateTime wersjaIdField;

            /// <remarks/>
            public string lokalnyId
            {
                get
                {
                    return this.lokalnyIdField;
                }
                set
                {
                    this.lokalnyIdField = value;
                }
            }

            /// <remarks/>
            public string przestrzenNazw
            {
                get
                {
                    return this.przestrzenNazwField;
                }
                set
                {
                    this.przestrzenNazwField = value;
                }
            }

            /// <remarks/>
            public System.DateTime wersjaId
            {
                get
                {
                    return this.wersjaIdField;
                }
                set
                {
                    this.wersjaIdField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:gugik:specyfikacje:gmlas:bazaDanychObiektowTopograficznych10k:1.0")]
        public partial class OT_BUWT_PX_skrKarto
        {

            private string nilReasonField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string nilReason
            {
                get
                {
                    return this.nilReasonField;
                }
                set
                {
                    this.nilReasonField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:gugik:specyfikacje:gmlas:bazaDanychObiektowTopograficznych10k:1.0")]
        public partial class OT_BUWT_PX_doklGeom
        {

            private string uomField;

            private byte valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string uom
            {
                get
                {
                    return this.uomField;
                }
                set
                {
                    this.uomField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public byte Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:gugik:specyfikacje:gmlas:bazaDanychObiektowTopograficznych10k:1.0")]
        public partial class OT_BUWT_PX_uzytkownik
        {

            private string hrefField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/1999/xlink")]
            public string href
            {
                get
                {
                    return this.hrefField;
                }
                set
                {
                    this.hrefField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:gugik:specyfikacje:gmlas:bazaDanychObiektowTopograficznych10k:1.0")]
        public partial class OT_BUWT_PX_cyklZycia
        {

            private BT_CyklZyciaInfo bT_CyklZyciaInfoField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:gugik:specyfikacje:gmlas:modelPodstawowy:1.0")]
            public BT_CyklZyciaInfo BT_CyklZyciaInfo
            {
                get
                {
                    return this.bT_CyklZyciaInfoField;
                }
                set
                {
                    this.bT_CyklZyciaInfoField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:gugik:specyfikacje:gmlas:modelPodstawowy:1.0")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:gugik:specyfikacje:gmlas:modelPodstawowy:1.0", IsNullable = false)]
        public partial class BT_CyklZyciaInfo
        {

            private System.DateTime poczatekWersjiObiektuField;

            /// <remarks/>
            public System.DateTime poczatekWersjiObiektu
            {
                get
                {
                    return this.poczatekWersjiObiektuField;
                }
                set
                {
                    this.poczatekWersjiObiektuField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:gugik:specyfikacje:gmlas:bazaDanychObiektowTopograficznych10k:1.0")]
        public partial class OT_BUWT_PX_kodKarto10k
        {

            private string nilReasonField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string nilReason
            {
                get
                {
                    return this.nilReasonField;
                }
                set
                {
                    this.nilReasonField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:gugik:specyfikacje:gmlas:bazaDanychObiektowTopograficznych10k:1.0")]
        public partial class OT_BUWT_PX_kodKarto25k
        {

            private string nilReasonField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string nilReason
            {
                get
                {
                    return this.nilReasonField;
                }
                set
                {
                    this.nilReasonField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:gugik:specyfikacje:gmlas:bazaDanychObiektowTopograficznych10k:1.0")]
        public partial class OT_BUWT_PX_kodKarto50k
        {

            private string nilReasonField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string nilReason
            {
                get
                {
                    return this.nilReasonField;
                }
                set
                {
                    this.nilReasonField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:gugik:specyfikacje:gmlas:bazaDanychObiektowTopograficznych10k:1.0")]
        public partial class OT_BUWT_PX_kodKarto100k
        {

            private string nilReasonField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string nilReason
            {
                get
                {
                    return this.nilReasonField;
                }
                set
                {
                    this.nilReasonField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:gugik:specyfikacje:gmlas:bazaDanychObiektowTopograficznych10k:1.0")]
        public partial class OT_BUWT_PX_kodKarto250k
        {

            private string nilReasonField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string nilReason
            {
                get
                {
                    return this.nilReasonField;
                }
                set
                {
                    this.nilReasonField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:gugik:specyfikacje:gmlas:bazaDanychObiektowTopograficznych10k:1.0")]
        public partial class OT_BUWT_PX_kodKarto500k
        {

            private string nilReasonField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string nilReason
            {
                get
                {
                    return this.nilReasonField;
                }
                set
                {
                    this.nilReasonField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:gugik:specyfikacje:gmlas:bazaDanychObiektowTopograficznych10k:1.0")]
        public partial class OT_BUWT_PX_kodKarto1000k
        {

            private string nilReasonField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string nilReason
            {
                get
                {
                    return this.nilReasonField;
                }
                set
                {
                    this.nilReasonField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:gugik:specyfikacje:gmlas:bazaDanychObiektowTopograficznych10k:1.0")]
        public partial class OT_BUWT_PRodzaj
        {

            private string nilReasonField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string nilReason
            {
                get
                {
                    return this.nilReasonField;
                }
                set
                {
                    this.nilReasonField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:gugik:specyfikacje:gmlas:bazaDanychObiektowTopograficznych10k:1.0")]
        public partial class OT_BUWT_PGeometria
        {

            private Point pointField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.opengis.net/gml/3.2")]
            public Point Point
            {
                get
                {
                    return this.pointField;
                }
                set
                {
                    this.pointField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opengis.net/gml/3.2")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.opengis.net/gml/3.2", IsNullable = false)]
        public partial class Point
        {

            private string posField;

            private string idField;

            private string srsNameField;

            private byte srsDimensionField;

            /// <remarks/>
            public string pos
            {
                get
                {
                    return this.posField;
                }
                set
                {
                    this.posField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
            public string id
            {
                get
                {
                    return this.idField;
                }
                set
                {
                    this.idField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string srsName
            {
                get
                {
                    return this.srsNameField;
                }
                set
                {
                    this.srsNameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte srsDimension
            {
                get
                {
                    return this.srsDimensionField;
                }
                set
                {
                    this.srsDimensionField = value;
                }
            }
        }


    }
}