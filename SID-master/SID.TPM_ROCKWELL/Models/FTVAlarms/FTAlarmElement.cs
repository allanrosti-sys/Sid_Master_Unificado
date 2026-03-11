using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SID.TPM_ROCKWELL.Models.FTVAlarms
{
    [XmlRoot(ElementName = "FTAlarmElement")]
    public class FTAlarmElement
    {

        [XmlElement(ElementName = "DiscreteElement")]
        public DiscreteElement DiscreteElement { get; set; }

        [XmlAttribute(AttributeName = "name", Namespace = "")]
        public string name { get; set; }

        [XmlAttribute(AttributeName = "latched", Namespace = "")]
        public bool latched { get; set; }

        [XmlAttribute(AttributeName = "ackRequired", Namespace = "")]
        public bool ackRequired { get; set; }

        [XmlAttribute(AttributeName = "style", Namespace = "")]
        public string style { get; set; }

        [XmlText]
        public string text { get; set; }
    }

}
