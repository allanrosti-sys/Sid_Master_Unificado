using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace SID.Standard.Models.XML
{

    [XmlRoot(ElementName = "RSLogix5000Content")]
    public class RSLogix5000Content
    {
        [XmlElement(ElementName = "Controller")]
        public Controller Controller { get; set; }

        [XmlAttribute(AttributeName = "SchemaRevision")]
        public string SchemaRevision { get; set; }
        [XmlAttribute(AttributeName = "SoftwareRevision")]
        public string SoftwareRevision { get; set; }
        [XmlAttribute(AttributeName = "TargetName")]
        public string TargetName { get; set; }
        [XmlAttribute(AttributeName = "TargetType")]
        public string TargetType { get; set; }
        [XmlAttribute(AttributeName = "CurrentLanguage")]
        public string CurrentLanguage { get; set; }
        [XmlAttribute(AttributeName = "ContainsContext")]
        public string ContainsContext { get; set; }
        [XmlAttribute(AttributeName = "Owner")]
        public string Owner { get; set; }
        [XmlAttribute(AttributeName = "ExportDate")]
        public string ExportDate { get; set; }
        [XmlAttribute(AttributeName = "ExportOptions")]
        public string ExportOptions { get; set; }


        [XmlIgnore]
        public string Name { get; set; }
    }
}