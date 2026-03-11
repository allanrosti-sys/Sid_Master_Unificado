using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace SID.Standard.Models.XML
{
    [XmlRoot(ElementName = "DataTypes")]
    public class DataTypes
    {
        [XmlElement(ElementName = "DataType")]
        public List<DataType> DataType { get; set; }

        [XmlAttribute(AttributeName = "Use")]
        public string Use { get; set; }
    }

    [XmlRoot(ElementName = "DataType")]
    public class DataType
    {
        [XmlElement(ElementName = "Description")]
        public Description Description { get; set; }

        [XmlElement(ElementName = "Members")]
        public Members Members { get; set; }

        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "Family")]
        public string Family { get; set; }
        [XmlAttribute(AttributeName = "Class")]
        public string Class { get; set; }

        [XmlElement(ElementName = "Dependencies")]
        public Dependencies Dependencies { get; set; }
    }


}