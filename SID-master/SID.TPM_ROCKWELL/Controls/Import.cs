using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using SID.Standard.Models.XML;
using System.Runtime.CompilerServices;

namespace SID.TPM_ROCKWELL.Controls
{
    public class Import

    {
        public static void Execute(string path, string file)
        {
            string pathAddOns = path + @"AddOns/";
            string pathDataTypes = path + @"DataTypes/";


            if (!Directory.Exists(pathAddOns))
            {
                Directory.CreateDirectory(pathAddOns);
            }

            if (!Directory.Exists(pathDataTypes))
            {
                Directory.CreateDirectory(pathDataTypes);
            }

            RSLogix5000Content rSLogix5000Content = LoadFile(file);

            XMLSave(pathDataTypes, rSLogix5000Content.Controller.DataTypes.DataType);
            XMLSave(pathAddOns, rSLogix5000Content.Controller.AddOnInstructionDefinitions.AddOnInstructionDefinition);

            rSLogix5000Content = null;
        }

       static RSLogix5000Content LoadFile(string file)
        {
            RSLogix5000Content rSLogix5000Content;
            string path = file;
            string xmlInputData = File.ReadAllText(path);

            using (StringReader sr = new StringReader(xmlInputData))
            {
                XmlSerializer ser = new XmlSerializer(typeof(RSLogix5000Content));
                rSLogix5000Content = (RSLogix5000Content)ser.Deserialize(sr);
            }
            return rSLogix5000Content;
        }


       static void XMLSave(string pathDataTypes, List<DataType> dataType)
        {
            foreach (DataType item in dataType)
            {
                string fileDataType = pathDataTypes + item.Name + ".xml";
                if (File.Exists(fileDataType))
                {
                    Console.WriteLine(item.Name + " - Já existe!");
                }
                else
                {
                    using (var doc = new StreamWriter(fileDataType))
                    {
                        using (XmlTextWriter writer = new XmlTextWriter(doc))
                        {
                            writer.Formatting = Formatting.Indented;
                            XmlSerializer ser = new XmlSerializer(typeof(DataType));
                            ser.Serialize(writer, item);
                        }
                    }
                    Console.WriteLine(item.Name + " - Criado!");
                }
            }
        }

       static void XMLSave(string pathAddOns, List<AddOnInstructionDefinition> AddOnInstructionDefinition)
        {
            foreach (AddOnInstructionDefinition item in AddOnInstructionDefinition)
            {
                string fileAddOn = pathAddOns + item.Name + ".xml";
                if (File.Exists(fileAddOn))
                {
                    Console.WriteLine(item.Name + " - Já existe!");
                }
                else
                {
                    using (var doc = new StreamWriter(fileAddOn))
                    {
                        using (XmlTextWriter writer = new XmlTextWriter(doc))
                        {
                            writer.Formatting = Formatting.Indented;
                            XmlSerializer ser = new XmlSerializer(typeof(AddOnInstructionDefinition));
                            ser.Serialize(writer, item);
                        }
                    }
                    Console.WriteLine(item.Name + " - Criado!");
                }
            }
        }



    }
}