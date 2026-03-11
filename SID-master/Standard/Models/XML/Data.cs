using SID.Standard.Models.XML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SID.Standard.Models.XML
{
    [XmlRoot(ElementName = "Data")]
    public class Data
    {

        [XmlElement(ElementName = "Structure")]
        public Structure Structure { get; set; }

        [XmlAttribute(AttributeName = "Format")]
        public string Format { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
}
