using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace SID.Standard.Models.XML
{
    [XmlRoot(ElementName="LocalTags")]
	public class LocalTags 
    {
		[XmlElement(ElementName="LocalTag")]
		public List<LocalTag> LocalTag { get; set; }
	}

    [XmlRoot(ElementName="LocalTag")]
	public class LocalTag 
    {
		[XmlElement(ElementName="Description")]
		public Description Description { get; set; }
		[XmlElement(ElementName="DefaultData")]
		public List<DefaultData> DefaultData { get; set; }
		[XmlAttribute(AttributeName="Name")]
		public string Name { get; set; }
		[XmlAttribute(AttributeName="DataType")]
		public string DataType { get; set; }
		[XmlAttribute(AttributeName="ExternalAccess")]
		public string ExternalAccess { get; set; }
		[XmlAttribute(AttributeName="Radix")]
		public string Radix { get; set; }
		[XmlAttribute(AttributeName="Dimensions")]
		public string Dimensions { get; set; }
	}
}