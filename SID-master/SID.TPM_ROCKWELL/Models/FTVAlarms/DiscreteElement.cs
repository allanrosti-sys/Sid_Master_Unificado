using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SID.TPM_ROCKWELL.Models.FTVAlarms
{
    [XmlRoot(ElementName = "DiscreteElement")]
    public class DiscreteElement
    {

        [XmlElement(ElementName = "DataItem")]
        public string DataItem { get; set; }

        [XmlElement(ElementName = "Style")]
        public string Style { get; set; }

        [XmlElement(ElementName = "Severity")]
        public int Severity { get; set; }

        [XmlElement(ElementName = "DelayInterval")]
        public int DelayInterval { get; set; }

        [XmlElement(ElementName = "EnableTag")]
        public bool EnableTag { get; set; }

        [XmlElement(ElementName = "UserData")]
        public object UserData { get; set; }

        [XmlElement(ElementName = "RSVCmd"), DefaultValue("")]
        public object RSVCmd { get; set; }

        [XmlElement(ElementName = "AlarmClass")]
        public string AlarmClass { get; set; }

        [XmlElement(ElementName = "GroupID")]
        public int GroupID { get; set; }

        [XmlElement(ElementName = "HandshakeTags")]
        public HandshakeTags HandshakeTags { get; set; }

        [XmlElement(ElementName = "RemoteAckAllDataItem")]
        public RemoteAckAllDataItem RemoteAckAllDataItem { get; set; }

        [XmlElement(ElementName = "RemoteDisableDataItem")]
        public RemoteDisableDataItem RemoteDisableDataItem { get; set; }

        [XmlElement(ElementName = "RemoteEnableDataItem")]
        public RemoteEnableDataItem RemoteEnableDataItem { get; set; }

        [XmlElement(ElementName = "RemoteSuppressDataItem")]
        public RemoteSuppressDataItem RemoteSuppressDataItem { get; set; }

        [XmlElement(ElementName = "RemoteUnSuppressDataItem")]
        public RemoteUnSuppressDataItem RemoteUnSuppressDataItem { get; set; }

        [XmlElement(ElementName = "RemoteShelveAllDataItem")]
        public RemoteShelveAllDataItem RemoteShelveAllDataItem { get; set; }

        [XmlElement(ElementName = "RemoteUnShelveDataItem")]
        public RemoteUnShelveDataItem RemoteUnShelveDataItem { get; set; }

        [XmlElement(ElementName = "RemoteShelveDuration")]
        public object RemoteShelveDuration { get; set; }

        [XmlElement(ElementName = "MessageID")]
        public int MessageID { get; set; }

        [XmlElement(ElementName = "Params")]
        public object Params { get; set; }
    }
}
