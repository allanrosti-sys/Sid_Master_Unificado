using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace SID.Standard.Models.XML
{
    [XmlRoot(ElementName = "Routines")]
    public class Routines
    {
        [XmlAttribute(AttributeName = "Use")]
        public string Use { get; set; }
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "Routine")]
        public List<Routine> Routine { get; set; }
    }

    [XmlRoot(ElementName = "Routine")]
    public class Routine
    {
        [XmlElement(ElementName = "RLLContent")]
        public RLLContent RLLContent { get; set; }
        [XmlAttribute(AttributeName = "Use")]
        public string Use { get; set; }
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "Type")]
        public string Type { get; set; }

    }

    [XmlRoot(ElementName = "RLLContent")]
    public class RLLContent
    {
        [XmlAttribute(AttributeName = "Use")]
        public string Use { get; set; }
        [XmlElement(ElementName = "Rung")]
        public List<Rung> Rungs { get; set; }
    }

    //[XmlRoot(ElementName = "Rung")]
    //public class Rung
    //{
    //    [XmlElement(ElementName = "Comment")]
    //    public Comment Comment { get; set; }

    //    [XmlIgnore]
    //    public string text { get; set; }

    //    [XmlElement(ElementName = "Text")]
    //    public XmlCDataSection Text
    //    {
    //        get
    //        {
    //            XmlDocument doc = new XmlDocument();
    //            return doc.CreateCDataSection(text);
    //        }
    //        set
    //        {
    //            text = value.Value;
    //        }
    //    }
    //    [XmlAttribute(AttributeName = "Use")]
    //    public string Use { get; set; }
    //    [XmlAttribute(AttributeName = "Number")]
    //    public string Number { get; set; }
    //    [XmlAttribute(AttributeName = "Type")]
    //    public string Type { get; set; }
    //}
    [XmlRoot(ElementName = "Rung")]
    public class Rung
    {
        [XmlIgnore]
        public string Comment { get; set; }

        [XmlIgnore]
        public string Text { get; set; }

        [XmlElement(ElementName = "Comment")]
        public System.Xml.XmlCDataSection CommentCDATA {
            get
            {
                return new System.Xml.XmlDocument().CreateCDataSection(Comment);
            }
            set
            {
                Comment = value.Value;
            }
        }

        [XmlElement(ElementName = "Text")]
        public System.Xml.XmlCDataSection TextCDATA
        {
            get
            {
                return new System.Xml.XmlDocument().CreateCDataSection(Text);
            }
            set
            {
                Text = value.Value;
            }
        }

        [XmlAttribute(AttributeName = "Number")]
        public string Number { get; set; }

        [XmlAttribute(AttributeName = "Type")]
        public string Type { get; set; }

    }

    [XmlRoot(ElementName = "Comment")]
    public class Comment
    {
        [XmlElement(ElementName = "LocalizedComment")]
        public LocalizedComment LocalizedComment { get; set; }
    }

    [XmlRoot(ElementName = "LocalizedComment")]
    public class LocalizedComment
    {
        [XmlAttribute(AttributeName = "Lang")]
        public string Lang { get; set; }
        [XmlIgnore]
        public string text { get; set; }

        [XmlText]
        public XmlNode[] Text
        {
            get
            {
                var dummy = new XmlDocument();
                return new XmlNode[] { dummy.CreateCDataSection(text) };
            }
            set
            {
                if (value == null)
                {
                    text = null;
                    return;
                }

                if (value.Length != 1)
                {
                    throw new InvalidOperationException(
                        String.Format(
                            "Invalid array length {0}", value.Length));
                }

                text = value[0].Value;
            }
        }
    }

}