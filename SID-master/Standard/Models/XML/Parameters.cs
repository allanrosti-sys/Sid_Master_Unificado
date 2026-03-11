using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace SID.Standard.Models.XML
{
    [XmlRoot(ElementName="Parameters")]
	public class Parameters 
    {
		[XmlElement(ElementName="Parameter")]
		public List<Parameter> Parameter { get; set; }
	}
}