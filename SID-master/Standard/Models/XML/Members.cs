using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace SID.Standard.Models.XML
{
    [XmlRoot(ElementName = "Members")]
    public class Members
    {
        [XmlElement(ElementName = "Member")]
        public List<Member> Member { get; set; }
    }


    [XmlRoot(ElementName = "Member")]
    public class Member
    {
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "DataType")]
        public string DataType { get; set; }
        [XmlAttribute(AttributeName = "Dimension")]
        public string Dimension { get; set; }
        [XmlAttribute(AttributeName = "Radix")]
        public string Radix { get; set; }
        [XmlAttribute(AttributeName = "Hidden")]
        public string Hidden { get; set; }
        [XmlAttribute(AttributeName = "ExternalAccess")]
        public string ExternalAccess { get; set; }
        [XmlElement(ElementName = "Description")]
        public Description Description { get; set; }
        [XmlAttribute(AttributeName = "Target")]
        public string Target { get; set; }
        [XmlAttribute(AttributeName = "BitNumber")]
        public string BitNumber { get; set; }
    }
}