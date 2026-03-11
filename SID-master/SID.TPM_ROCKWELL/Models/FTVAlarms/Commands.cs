using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SID.TPM_ROCKWELL.Models.FTVAlarms
{
    [XmlRoot(ElementName = "Commands", Namespace = "urn://www.factorytalk.net/schema/2003/FTLDDAlarms.xsd")]
    public class Commands
    {

        [XmlElement(ElementName = "FTAeDetectorCommand", Namespace = "urn://www.factorytalk.net/schema/2003/FTLDDAlarms.xsd")]
        public List<FTAeDetectorCommand> FTAeDetectorCommand { get; set; }
    }
}
