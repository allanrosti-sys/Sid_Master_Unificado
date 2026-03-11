using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace SID.Standard.Models.XML
{
    [XmlRoot(ElementName = "Tags")]
    public class Tags
    {
        [XmlElement(ElementName = "Tag")]
        public List<Tag> Tag { get; set; }
        [XmlAttribute(AttributeName = "Use")]
        public string Use { get; set; }
    }

    [XmlRoot(ElementName = "Tag")]
    public class Tag
    {
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "TagType")]
        public string TagType { get; set; }
        [XmlAttribute(AttributeName = "AliasFor")]
        public string AliasFor { get; set; }
        [XmlAttribute(AttributeName = "DataType")]
        public string DataType { get; set; }
        [XmlAttribute(AttributeName = "Dimensions")]
        public string Dimensions { get; set; }
        [XmlAttribute(AttributeName = "Constant")]
        public string Constant { get; set; }
        [XmlAttribute(AttributeName = "ExternalAccess")]
        public string ExternalAccess { get; set; }
        [XmlElement(ElementName = "Description")]
        public string Description { get; set; }
        [XmlElement(ElementName = "Data")]
        public Data Data { get; set; }

    }
}