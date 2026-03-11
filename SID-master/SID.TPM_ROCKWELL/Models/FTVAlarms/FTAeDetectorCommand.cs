using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SID.TPM_ROCKWELL.Models.FTVAlarms
{
    [XmlRoot(ElementName = "FTAeDetectorCommand", Namespace = "urn://www.factorytalk.net/schema/2003/FTLDDAlarms.xsd")]
    public class FTAeDetectorCommand
    {

        [XmlElement(ElementName = "Operation", Namespace = "urn://www.factorytalk.net/schema/2003/FTLDDAlarms.xsd")]
        public string Operation { get; set; }

        [XmlElement(ElementName = "FTAlarmElements", Namespace = "urn://www.factorytalk.net/schema/2003/FTLDDAlarms.xsd")]
        public FTAlarmElements FTAlarmElements { get; set; }

        [XmlAttribute(AttributeName = "style", Namespace = "")]
        public string style { get; set; }

        [XmlAttribute(AttributeName = "version", Namespace = "")]
        public string version { get; set; }

        [XmlText]
        public string text { get; set; }
    }
}
