using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace SID.Standard.Models.XML
{
    [XmlRoot(ElementName="Programs")]
	public class Programs 
    {
		[XmlElement(ElementName="Program")]
		public List<Program> Program { get; set; }
		[XmlAttribute(AttributeName="Use")]
		public string Use { get; set; }

	}

    [XmlRoot(ElementName="Program")]
	public class Program {
		[XmlElement(ElementName="Tags")]
		public Tags Tags { get; set; }
		[XmlElement(ElementName="Routines")]
		public Routines Routines { get; set; }
		[XmlAttribute(AttributeName="Use")]
		public string Use { get; set; }
		[XmlAttribute(AttributeName="Name")]
		public string Name { get; set; }
		[XmlAttribute(AttributeName="TestEdits")]
		public string TestEdits { get; set; }
		[XmlAttribute(AttributeName="MainRoutineName")]
		public string MainRoutineName { get; set; }
		[XmlAttribute(AttributeName="Disabled")]
		public string Disabled { get; set; }
		[XmlAttribute(AttributeName="UseAsFolder")]
		public string UseAsFolder { get; set; }
	}

}