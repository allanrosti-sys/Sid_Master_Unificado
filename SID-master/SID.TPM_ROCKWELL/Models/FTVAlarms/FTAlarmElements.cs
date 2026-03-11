using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SID.TPM_ROCKWELL.Models.FTVAlarms
{
    [XmlRoot(ElementName = "FTAlarmElements", Namespace = "urn://www.factorytalk.net/schema/2003/FTLDDAlarms.xsd")]
    public class FTAlarmElements
    {

        [XmlElement(ElementName = "FTAlarmElement", Namespace = "urn://www.factorytalk.net/schema/2003/FTLDDAlarms.xsd")]
        public List<FTAlarmElement> FTAlarmElement { get; set; }

        [XmlAttribute(AttributeName = "shelveMaxValue", Namespace = "")]
        public int shelveMaxValue { get; set; }

        [XmlText]
        public string text { get; set; }
    }
}
