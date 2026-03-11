using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using SID.Standard.Models.XML;


namespace SID.Standard.Controls
{
    public class Import

    {
        const string path = @"Database/";
        const string pathAddOns = path +@"AddOns/";
        const string pathDataTypes = path + @"DataTypes/";

        public static void Execute(string file="")
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);    
            }

            if (!Directory.Exists(pathAddOns))
            {
                Directory.CreateDirectory(pathAddOns);
            } 

            if (!Directory.Exists(pathDataTypes))
            {
                Directory.CreateDirectory(pathDataTypes);
            }

            string parameter = "";
            if (file != "")
            {
                parameter = file;
            }
            else
            {
                parameter = Input_Parameter("insira o nome do arquivo: ");
            }

            if (parameter == "")
            {
                return;
            }

            if (!File.Exists(parameter))
            {
                parameter += ".L5X";
                if (!File.Exists(parameter))
                {
                    return;
                }
            }


            RSLogix5000Content rSLogix5000Content = LoadFile(parameter);

            XMLSave(rSLogix5000Content.Controller.DataTypes.DataType);
            XMLSave(rSLogix5000Content.Controller.AddOnInstructionDefinitions.AddOnInstructionDefinition);

            // Console.WriteLine("Deseja importar os DataTypes ? (y/n)");
            // if (((char)Console.Read()) == 'y')
            // {

            // }
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


        static void XMLSave(List<DataType> dataType)
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

        static void XMLSave(List<AddOnInstructionDefinition> AddOnInstructionDefinition)
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

        static string Input_Parameter(string msg)
        {
            Console.Write(msg);
            return Console.ReadLine();
        }

    }
}