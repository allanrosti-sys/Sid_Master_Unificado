using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SID.TPM_ROCKWELL.Models.FTVAlarms
{
    [XmlRoot(ElementName = "HandshakeTags")]
    public class HandshakeTags
    {

        [XmlElement(ElementName = "InAlarmDataItem")]
        public string InAlarmDataItem { get; set; }

        [XmlElement(ElementName = "DisabledDataItem")]
        public object DisabledDataItem { get; set; }

        [XmlElement(ElementName = "AckedDataItem")]
        public string AckedDataItem { get; set; }

        [XmlElement(ElementName = "SuppressedDataItem")]
        public string SuppressedDataItem { get; set; }

        [XmlElement(ElementName = "ShelvedDataItem")]
        public object ShelvedDataItem { get; set; }
    }
}
