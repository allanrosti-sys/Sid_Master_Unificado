using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SID.TPM_ROCKWELL.Models.FTVAlarms
{
    [XmlRoot(ElementName = "RemoteShelveAllDataItem")]
    public class RemoteShelveAllDataItem
    {

        [XmlAttribute(AttributeName = "AutoReset")]
        public bool AutoReset { get; set; }
    }

}
