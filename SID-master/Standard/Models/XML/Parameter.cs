using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace SID.Standard.Models.XML
{
    [XmlRoot(ElementName = "Parameter")]
    public class Parameter
    {
        [XmlElement(ElementName = "Description")]
        public Description Description { get; set; }
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "TagType")]
        public string TagType { get; set; }
        [XmlAttribute(AttributeName = "DataType")]
        public string DataType { get; set; }
        [XmlAttribute(AttributeName = "Usage")]
        public string Usage { get; set; }
        [XmlAttribute(AttributeName = "Radix")]
        public string Radix { get; set; }
        [XmlAttribute(AttributeName = "Required")]
        public string Required { get; set; }
        [XmlAttribute(AttributeName = "Visible")]
        public string Visible { get; set; }
        [XmlAttribute(AttributeName = "ExternalAccess")]
        public string ExternalAccess { get; set; }
        [XmlElement(ElementName = "DefaultData")]
        public List<DefaultData> DefaultData { get; set; }
        [XmlAttribute(AttributeName = "Constant")]
        public string Constant { get; set; }
        [XmlAttribute(AttributeName = "Dimensions")]
        public string Dimensions { get; set; }
    }

    [XmlRoot(ElementName = "DefaultData")]
    public class DefaultData
    {
        [XmlAttribute(AttributeName = "Format")]
        public string Format { get; set; }
        [XmlText]
        public string Text { get; set; }
        [XmlElement(ElementName = "DataValue")]
        public DataValue DataValue { get; set; }
        [XmlElement(ElementName = "Structure")]
        public Structure Structure { get; set; }
        [XmlElement(ElementName = "Array")]
        public Array Array { get; set; }
    }

    [XmlRoot(ElementName = "DataValue")]
    public class DataValue
    {
        [XmlAttribute(AttributeName = "DataType")]
        public string DataType { get; set; }
        [XmlAttribute(AttributeName = "Radix")]
        public string Radix { get; set; }
        [XmlAttribute(AttributeName = "Value")]
        public string Value { get; set; }
    }

    [XmlRoot(ElementName = "Structure")]
    public class Structure
    {
        [XmlElement(ElementName = "DataValueMember")]
        public List<DataValueMember> DataValueMember { get; set; }
        [XmlAttribute(AttributeName = "DataType")]
        public string DataType { get; set; }
        [XmlElement(ElementName = "StructureMember")]
        public List<StructureMember> StructureMember { get; set; }
    }

    [XmlRoot(ElementName = "Array")]
    public class Array
    {
        [XmlElement(ElementName = "Element")]
        public List<Element> Element { get; set; }
        [XmlAttribute(AttributeName = "DataType")]
        public string DataType { get; set; }
        [XmlAttribute(AttributeName = "Dimensions")]
        public string Dimensions { get; set; }
        [XmlAttribute(AttributeName = "Radix")]
        public string Radix { get; set; }
    }

    [XmlRoot(ElementName = "DataValueMember")]
    public class DataValueMember
    {
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "DataType")]
        public string DataType { get; set; }
        [XmlAttribute(AttributeName = "Radix")]
        public string Radix { get; set; }
        [XmlAttribute(AttributeName = "Value")]
        public string Value { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "StructureMember")]
    public class StructureMember
    {
        [XmlElement(ElementName = "DataValueMember")]
        public List<DataValueMember> DataValueMember { get; set; }
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "DataType")]
        public string DataType { get; set; }
        [XmlElement(ElementName = "StructureMember")]
        public List<StructureMember> StructureMember_ { get; set; }
        [XmlElement(ElementName = "ArrayMember")]
        public List<ArrayMember> ArrayMember { get; set; }
    }

    [XmlRoot(ElementName = "ArrayMember")]
    public class ArrayMember
    {
        [XmlElement(ElementName = "Element")]
        public List<Element> Element { get; set; }
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "DataType")]
        public string DataType { get; set; }
        [XmlAttribute(AttributeName = "Dimensions")]
        public string Dimensions { get; set; }
        [XmlAttribute(AttributeName = "Radix")]
        public string Radix { get; set; }
    }

    [XmlRoot(ElementName = "Element")]
    public class Element
    {
        [XmlAttribute(AttributeName = "Index")]
        public string Index { get; set; }
        [XmlAttribute(AttributeName = "Value")]
        public string Value { get; set; }
        [XmlElement(ElementName = "Structure")]
        public Structure Structure { get; set; }
    }


}