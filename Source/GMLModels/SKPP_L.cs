using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeodataLoader.Source.GMLModels
{
    public abstract class SKPP_L
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

            private object lowerCornerField;

            private object upperCornerField;

            private string srsNameField;

            /// <remarks/>
            public object lowerCorner
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
            public object upperCorner
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


    }
}
