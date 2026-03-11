using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SID.TPM_ROCKWELL.Models.FTVAlarms
{
    [XmlRoot(ElementName = "FTAeAlarmStore", Namespace = "urn://www.factorytalk.net/schema/2003/FTLDDAlarms.xsd")]
    public class FTAeAlarmStore
    {
        [XmlElement(ElementName = "Version", Namespace = "urn://www.factorytalk.net/schema/2003/FTLDDAlarms.xsd")]
        public string Version { get; set; }

        [XmlElement(ElementName = "Commands", Namespace = "urn://www.factorytalk.net/schema/2003/FTLDDAlarms.xsd")]
        public Commands Commands { get; set; }

        [XmlAttribute(AttributeName = "dt", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string dt { get; set; }

        //[XmlAttribute(AttributeName = "xmlns", Namespace = "")]
        //public string xmlns { get; set; }

        //[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string xsi { get; set; }

        [XmlAttribute(AttributeName = "schemaLocation", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
        public string schemaLocation { get; set; }

        //[XmlText]
        //public string text { get; set; }
    }

}
