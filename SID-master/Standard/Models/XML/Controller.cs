using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace SID.Standard.Models.XML
{
    [XmlRoot(ElementName = "Controller")]
    public class Controller
    {
        [XmlElement(ElementName = "DataTypes")]
        public DataTypes DataTypes { get; set; }
        [XmlElement(ElementName = "AddOnInstructionDefinitions")]
        public AddOnInstructionDefinitions AddOnInstructionDefinitions { get; set; }
        [XmlElement(ElementName = "Tags")]
        public Tags Tags { get; set; }
        [XmlElement(ElementName = "Programs")]
        public Programs Programs { get; set; }

        [XmlAttribute(AttributeName = "Use")]
        public string Use { get; set; }
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
    }

}