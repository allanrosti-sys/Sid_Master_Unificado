using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace SID.Standard.Models.XML
{
    [XmlRoot(ElementName = "Description")]
    public class Description
    {
        [XmlElement(ElementName = "LocalizedDescription")]
        public LocalizedDescription LocalizedDescription { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "LocalizedDescription")]
    public class LocalizedDescription
    {


        [XmlAttribute(AttributeName = "Lang")]
        public string Lang { get; set; }

        [XmlIgnore]
        public string text { get; set; }

        [XmlText]
        public XmlNode[] Text
        {
            get
            {
                var dummy = new XmlDocument();
                return new XmlNode[] {dummy.CreateCDataSection(text)};
            }
            set
            {
                if (value == null)
                {
                    text = null;
                    return;
                }

                if (value.Length != 1)
                {
                    throw new InvalidOperationException(
                        String.Format(
                            "Invalid array length {0}", value.Length));
                }

                text = value[0].Value;
            }
        }
    }
}