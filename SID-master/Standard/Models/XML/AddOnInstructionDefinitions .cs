using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace SID.Standard.Models.XML
{
    [XmlRoot(ElementName = "AddOnInstructionDefinitions")]
    public class AddOnInstructionDefinitions
    {
        [XmlElement(ElementName = "AddOnInstructionDefinition")]
        public List<AddOnInstructionDefinition> AddOnInstructionDefinition { get; set; }
        [XmlAttribute(AttributeName = "Use")]
        public string Use { get; set; }

    }

    [XmlRoot(ElementName = "AddOnInstructionDefinition")]
    public class AddOnInstructionDefinition
    {
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "Revision")]
        public string Revision { get; set; }
        [XmlAttribute(AttributeName = "RevisionExtension")]
        public string RevisionExtension { get; set; }
        [XmlAttribute(AttributeName = "Vendor")]
        public string Vendor { get; set; }
        [XmlAttribute(AttributeName = "ExecutePrescan")]
        public string ExecutePrescan { get; set; }
        [XmlAttribute(AttributeName = "ExecutePostscan")]
        public string ExecutePostscan { get; set; }
        [XmlAttribute(AttributeName = "ExecuteEnableInFalse")]
        public string ExecuteEnableInFalse { get; set; }
        [XmlAttribute(AttributeName = "CreatedDate")]
        public string CreatedDate { get; set; }
        [XmlAttribute(AttributeName = "CreatedBy")]
        public string CreatedBy { get; set; }
        [XmlAttribute(AttributeName = "EditedDate")]
        public string EditedDate { get; set; }
        [XmlAttribute(AttributeName = "EditedBy")]
        public string EditedBy { get; set; }
        [XmlAttribute(AttributeName = "SoftwareRevision")]
        public string SoftwareRevision { get; set; }

        [XmlElement(ElementName = "Description")]
        public Description Description { get; set; }
        [XmlElement(ElementName = "RevisionNote")]
        public RevisionNote RevisionNote { get; set; }
        [XmlElement(ElementName = "AdditionalHelpText")]
        public AdditionalHelpText AdditionalHelpText { get; set; }
        [XmlElement(ElementName = "Parameters")]
        public Parameters Parameters { get; set; }
        [XmlElement(ElementName = "LocalTags")]
        public LocalTags LocalTags { get; set; }
        [XmlElement(ElementName = "Routines")]
        public Routines Routines { get; set; }
        // [XmlElement(ElementName = "Dependencies")]
        // public Dependencies Dependencies { get; set; }
    }


}
