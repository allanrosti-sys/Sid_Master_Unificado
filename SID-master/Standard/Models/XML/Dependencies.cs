using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace SID.Standard.Models.XML
{
    [XmlRoot(ElementName = "Dependencies")]
    public class Dependencies
    {
        [XmlElement(ElementName = "Dependency")]
        public List<Dependency> Dependency { get; set; }
    }

    [XmlRoot(ElementName = "Dependency")]
    public class Dependency
    {
        [XmlAttribute(AttributeName = "Type")]
        public string Type { get; set; }
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
    }
}