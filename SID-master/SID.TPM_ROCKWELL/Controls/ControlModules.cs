using System;
using System.Collections.Generic;
using System.Linq;
using SID.Standard.Models.XML;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Shapes;
using SID.TPM_ROCKWELL.Models.FTVAlarms;
using SID.Designer.Layout;
using SID.Designer.Pages;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Text;

namespace SID.TPM_ROCKWELL.Controls
{

    public class ControlModules
    {
        #region Local dos arquivos

        #region Base
        const string file_BaseRungs = @"Plugin/DataBase/Components/Base_RUNGS.xml";
        const string file_BaseSupTags = @"Plugin/DataBase/Components/Base_SupTags.txt";
        const string file_BaseAlarms = @"Plugin/DataBase/Components/Base_Alarm.xml";

        const string pathBase = @"Plugin/DataBase/Components/ControlModules";
        const string pathRungs = pathBase + @"/Rungs";
        const string pathTags = pathBase + @"/Tags";
        const string pathSupTags = pathBase + @"/SupTags";
        const string pathAlarms = pathBase + @"/Alarms";

        const string FileBaseTag = pathTags + @"/Base_TAG.xml";
        #endregion

        string path_DataTypes;
        string path_AddOns;
        string pathOut;

        const string FileRungENET_PF753 = pathRungs + @"/ENET_PF753.xml";

        const string TopicName = "Castrolanda";

        #endregion

        List<CMModel> cMModels;
        RSLogix5000Content RSLogix5000Content;
        FTAeAlarmStore FTAeAlarmStore;
        Tags Tags;
        RLLContent RungContent;
        AddOnInstructionDefinitions AddOns;

        public ControlModules(string path, CMType cMType, string file)
        {
            //new FileInfo(file).Name.Replace(" ", "").Split('-')[0].ToString();

            path_DataTypes = path + @"DataBase/DataTypes/";
            path_AddOns = path + @"DataBase/AddOns/";
            pathOut = path + @"Out/CM/" + new FileInfo(file).Name.Replace(" ", "").Split('-')[0].ToString() + @"/";

            if (!Directory.Exists(pathOut))
            {
                Directory.CreateDirectory(pathOut);
            }

            StreamReader doc;
            if (File.Exists(file))
            {
                doc = new StreamReader(file);
            }
            else
            {
                return;
            }

            RSLogix5000Content = LoadBase();
            FTAeAlarmStore = LoadBaseAlarms();
            Tags = RSLogix5000Content.Controller.Tags;
            RungContent = RSLogix5000Content.Controller.Programs.Program[0].Routines.Routine[0].RLLContent;
            AddOns = RSLogix5000Content.Controller.AddOnInstructionDefinitions;

           // LoadAddOns("_STD_AH_AlmStat");
            //LoadAddOns("_STD_AH_GenAlmStat");
            //LoadAddOns("_STD_CM_GenModeStat");
            //SaveRung(rSLogix5000Content);

            //Leitura do cabeçalho
            string cab_par = doc.ReadLine();
            doc.ReadLine();
            doc.ReadLine();
            CMModel cMModel = GetCMModel(cMType);
            if (!Directory.Exists($@"{pathOut}/{cMModel.Name}"))
            {
                Directory.CreateDirectory($@"{pathOut}/{cMModel.Name}");
            }
            StreamWriter docSupTags = Save_SupTags($@"{pathOut}/{cMModel.Name}/Tags.csv");
            
            while (!doc.EndOfStream)
            {
                string line = doc.ReadLine();
                GenerateCM(cMModel, cab_par, line);
                docSupTags.WriteLine(Generate_SupTag(cMModel.FileSupTag, cab_par, line));
                if (cMModel.EnableAlarm)
                {
                    Generate_Alarm(cab_par, line);
                }
            }

            SaveRung(cMModel.Name);
            if (cMModel.EnableAlarm)
            {
                SaveAlarm(cMModel.Name);
            }
            docSupTags.Close();
        }

        void Load_CMModels()
        {
            cMModels = new List<CMModel>();

            #region InDig
            cMModels.Add(new CMModel(pathBase)
            {
                Name = "InDig",
                CMType = CMType.DI,
                FileTag = @"_STD_CM_InDig.xml",
                FileRung = "InDig.xml",
                FileSupTag = "InDig.txt",
                EnableExtPar = true,
                DataType = "_STD_CM_InDig",
                ExtParDataType = "_STD_CM_InDig_ExtPar"

            });
            #endregion

            #region OutDig
            cMModels.Add(new CMModel(pathBase)
            {
                Name = "OutDig",
                CMType = CMType.DO,
                FileTag = @"_STD_CM_OutDig.xml",
                FileRung = "OutDig.xml",
                FileSupTag = "OutDig.txt",
                EnableExtPar = true,
                DataType = "_STD_CM_OutDig",
                ExtParDataType = "_STD_CM_OutDig_ExtPar"

            });
            #endregion

            #region InAnalog
            cMModels.Add(new CMModel(pathBase)
            {
                Name = "InAnalog",
                CMType = CMType.AI,
                FileTag = @"_STD_CM_InAnlg.xml",
                FileRung = "InAnlg.xml",
                FileSupTag = "InAnlg.txt",
                EnableExtPar = false,
                DataType = "_STD_CM_InAnlg",


            });
            #endregion

            #region OutAnlg
            cMModels.Add(new CMModel(pathBase)
            {
                Name = "OutAnalog",
                CMType = CMType.AO,
                FileTag = @"_STD_CM_OutAnlg.xml",
                FileRung = "OutAnlg.xml",
                FileSupTag = "OutAnlg.txt",
                EnableExtPar = false,
                DataType = "_STD_CM_OutAnlg",


            });
            #endregion

            #region Valvdig1
            cMModels.Add(new CMModel(pathBase)
            {
                Name = "ValvDig1",
                CMType = CMType.ValvDig1,
                FileTag = @"_STD_CM_ValvDig1.xml",
                FileRung = "ValvDig1.xml",
                FileSupTag = "ValvDig.txt",
                EnableExtPar = true,
                EnableAlarm = true,
                DataType = "_STD_CM_ValvDig1",
                ExtParDataType = "_STD_CM_ValvDig1_ExtPar"
            });
            #endregion

            #region ValvDig2
            cMModels.Add(new CMModel(pathBase)
            {
                Name = "ValvDig2",
                CMType = CMType.ValvDig2,
                FileTag = @"_STD_CM_ValvDig2.xml",
                FileRung = "ValvDig2.xml",
                FileSupTag = "ValvDig.txt",
                EnableExtPar = true,
                EnableAlarm = true,
                DataType = "_STD_CM_ValvDig2",
                ExtParDataType = "_STD_CM_ValvDig2_ExtPar"
            });
            #endregion

            #region MotDig1
            cMModels.Add(new CMModel(pathBase)
            {
                Name = "MotDig1",
                CMType = CMType.MotDig1,
                FileTag = @"_STD_CM_MotDig1.xml",
                FileRung = "MotDig1.xml",
                FileSupTag = "MotDig.txt",
                EnableExtPar = true,
                DataType = "_STD_CM_MotDig1",
                ExtParDataType = "_STD_CM_MotDig1_ExtPar"

            });
            #endregion

        }

        CMModel GetCMModel(CMType cMType)
        {
            Load_CMModels();
            CMModel cMModel = null;
            foreach (CMModel item in cMModels)
            {
                if (cMType == item.CMType)
                {
                    cMModel = item;
                    break;
                }
            }
            return cMModel;
        }

        void GenerateCM(CMModel cMModel, string cab_Par, string command)
        {
            Tags.Tag.Add(Generate_TAG(cMModel.FileTag, cab_Par, command));
            if (cMModel.EnableExtPar)
            {
                Tags.Tag.Add(Generate_TAGExPar(cab_Par, command, cMModel.ExtParDataType));
            }

            Rung rung = Generate_Rung(cMModel.FileRung, cab_Par, command);
            rung.Number = RungContent.Rungs.Count.ToString();
            RungContent.Rungs.Add(rung);

            #region Importação dos AddOns
            //LoadAddOns(cMModel.DataType);
            if (cMModel.EnableExtPar)
            {
                //LoadAddOns(cMModel.ExtParDataType);
            }
            #endregion

        }

        RSLogix5000Content LoadBase()
        {
            RSLogix5000Content rSLogix5000Content;
            string xmlInputData = File.ReadAllText(file_BaseRungs);

            using (StringReader sr = new StringReader(xmlInputData))
            {
                XmlSerializer ser = new XmlSerializer(typeof(RSLogix5000Content));
                rSLogix5000Content = (RSLogix5000Content)ser.Deserialize(sr);
            }

            return rSLogix5000Content;
        }

        FTAeAlarmStore LoadBaseAlarms()
        {
            FTAeAlarmStore fTAeAlarmStore;
            string xmlInputData = File.ReadAllText(file_BaseAlarms);

            using (StringReader sr = new StringReader(xmlInputData))
            {
                XmlSerializer ser = new XmlSerializer(typeof(FTAeAlarmStore));
                fTAeAlarmStore = (FTAeAlarmStore)ser.Deserialize(sr);
            }
            return fTAeAlarmStore;
        }

        void LoadAddOns(string name)
        {
            foreach (AddOnInstructionDefinition item in AddOns.AddOnInstructionDefinition)
            {
                if (item.Name == name)
                    return;
            }
            string xmlInputData = File.ReadAllText(path_AddOns + name + ".xml");
            using (StringReader sr = new StringReader(xmlInputData))
            {
                XmlSerializer ser = new XmlSerializer(typeof(AddOnInstructionDefinition));
                AddOns.AddOnInstructionDefinition.Add((AddOnInstructionDefinition)ser.Deserialize(sr));
            }
        }

        void SaveRung(string file)
        {
            if (!Directory.Exists(pathOut + file))
            {
                Directory.CreateDirectory(pathOut + file);
            }
            using (var doc = new StreamWriter(pathOut + file + "/Rungs.L5X"))
            {
                using (XmlTextWriter writer = new XmlTextWriter(doc))
                {
                    writer.Formatting = Formatting.Indented;
                    XmlSerializer ser = new XmlSerializer(typeof(RSLogix5000Content));
                    ser.Serialize(writer, RSLogix5000Content);
                }
            }
        }

        void SaveAlarm(string file)
        {
            if (!Directory.Exists(pathOut + file))
            {
                Directory.CreateDirectory(pathOut + file);
            }
            //using (var doc = new StreamWriter(pathOut + file + "/Alarmes.xml"))
            //{
            //    using (XmlTextWriter writer = new XmlTextWriter(doc))
            //    {
            //        writer.Settings = new XmlWriterSettings();
            //        writer.Settings.Encoding = Encoding.Unicode;
            //        //writer.Formatting = Formatting.Indented;
            //        XmlSerializer ser = new XmlSerializer(typeof(FTAeAlarmStore));
            //        ser.Serialize(writer, FTAeAlarmStore);
            //    }
            //}

            //var serializer = new XmlSerializer(typeof(FTAeAlarmStore));
            //var settings = new XmlWriterSettings
            //{
            //    Encoding = Encoding.Unicode,
            //    Indent = false
            //};


            //using (var xmlWriter = XmlWriter.Create(pathOut + file + "/Alarmes.xml", settings))
            //{
            //    serializer.Serialize(xmlWriter, FTAeAlarmStore);
            //}

            
            using (FileStream fs = new FileStream(pathOut + file + "/Alarmes.xml", FileMode.Create))
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Encoding = Encoding.Unicode;
                settings.NewLineChars = Environment.NewLine;
                settings.ConformanceLevel = ConformanceLevel.Document;
                settings.Indent = true;
                using (XmlWriter writer = XmlWriter.Create(fs, settings))
                {
                    XmlSerializer s = new XmlSerializer(typeof(FTAeAlarmStore));
                    s.Serialize(new XmlWriterEE(writer), FTAeAlarmStore);
                }
            }
        }

        static Tag LoadTag(string stringTag)
        {
            Tag tag;
            using (StringReader sr = new StringReader(stringTag))
            {
                XmlSerializer ser = new XmlSerializer(typeof(Tag));
                tag = (Tag)ser.Deserialize(sr);
            }
            return tag;
        }

        static FTAlarmElement LoadAlarm(string stringAlarm)
        {
            FTAlarmElement fTAlarmElement;
            using (StringReader sr = new StringReader(stringAlarm))
            {
                XmlSerializer ser = new XmlSerializer(typeof(FTAlarmElement));
                fTAlarmElement = (FTAlarmElement)ser.Deserialize(sr);
            }
            return fTAlarmElement;
        }

        static Rung LoadRung(string stringRung)
        {
            Rung rung;
            using (StringReader sr = new StringReader(stringRung))
            {
                XmlSerializer ser = new XmlSerializer(typeof(Rung));
                rung = (Rung)ser.Deserialize(sr);
            }
            return rung;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rSLogix5000Content"></param>
        /// <param name="fileRung">Arquivo de gereção da Rung</param>
        /// <param name="cab_Pars"></param>
        /// <param name="commands"></param>
        Tag Generate_TAG(string cab_Pars, string commands)
        {
            return Generate_TAG(FileBaseTag, cab_Pars, commands);

        }

        Tag Generate_TAG(string filetag, string cab_Pars, string commands)
        {
            string[] cab_Par = cab_Pars.Split(',');
            string[] command = commands.Split(',');

            #region Criacao de Tag
            string dataTAG = File.ReadAllText(filetag);

            #region Parametros

            for (int i = 0; i < cab_Par.Length; i++)
            {
                if (cab_Par[i] != "")
                {
                    if (command[i] != "")
                    {
                        dataTAG = dataTAG.Replace(cab_Par[i], command[i]);
                    }
                    else
                    {
                        dataTAG = dataTAG.Replace(cab_Par[i], "0");
                        for (int j = i; j < cab_Par.Length; j++)
                        {
                            if (i != j)
                            {
                                cab_Par[j] = cab_Par[j].Replace(cab_Par[i], command[i]);
                            }
                        }
                    }
                }
            }
            #endregion

            return LoadTag(dataTAG);
            #endregion
        }

        Tag Generate_BaseTAG(string tag, string dataType)
        {
            string dataTAG = File.ReadAllText(FileBaseTag);
            dataTAG = dataTAG.Replace("@TAG", tag);
            dataTAG = dataTAG.Replace("@DataType", dataType);

            return LoadTag(dataTAG);
        }

        Tag Generate_TAGExPar(string cab_Pars, string commands, string type)
        {
            string[] cab_Par = cab_Pars.Split(',');
            string[] command = commands.Split(',');

            string dataTAG = File.ReadAllText(FileBaseTag);

            Tag tag = new Tag();
            for (int i = 0; i < cab_Par.Length; i++)
            {
                if (cab_Par[i] == "@TAG")
                {
                    tag = Generate_BaseTAG($"{command[i]}_ExtPar", type);
                }

            }
            return tag;
        }

        Rung Generate_Rung(string fileRung, string cab_Pars, string commands)
        {
            string[] cab_Par = cab_Pars.Split(',');
            string[] command = commands.Split(',');

            #region Criacao da rung
            string dataRung = File.ReadAllText(fileRung);

            #region Parametros

            for (int i = 0; i < cab_Par.Length; i++)
            {
                if (cab_Par[i] != "")
                {
                    if (command[i] != "")
                    {
                        dataRung = dataRung.Replace(cab_Par[i], command[i]);

                        for (int j = i; j < cab_Par.Length; j++)
                        {
                            if (i != j)
                            {
                                cab_Par[j] = cab_Par[j].Replace(cab_Par[i], command[i]);
                            }
                        }
                    }
                    else if (cab_Par[i].Contains("."))
                    {
                        ///Para indentificar se não existe necessidade de fazer a subtituição
                    }

                    else
                    {
                        dataRung = dataRung.Replace(cab_Par[i], "0");
                    }

                }
            }
            #endregion

            return LoadRung(dataRung);
            #endregion

        }

        StreamWriter Save_SupTags(string path)
        {
            StreamWriter doc = new StreamWriter(path,false, new UTF8Encoding(true));
            doc.WriteLine(File.ReadAllText(file_BaseSupTags));
            return doc;
        }

        string Generate_SupTag(string file, string cab_Pars, string commands)
        {
            string[] cab_Par = cab_Pars.Split(',');
            string[] command = commands.Split(',');

            #region Criacao de Tag
            string dataSupTag = File.ReadAllText(file);

            #region Parametros

            for (int i = 0; i < cab_Par.Length; i++)
            {
                if (cab_Par[i] != "")
                {
                    if (command[i] != "")
                    {
                        dataSupTag = dataSupTag.Replace(cab_Par[i], command[i]);
                    }
                    else
                    {
                        dataSupTag = dataSupTag.Replace(cab_Par[i], "0");
                        for (int j = i; j < cab_Par.Length; j++)
                        {
                            if (i != j)
                            {
                                cab_Par[j] = cab_Par[j].Replace(cab_Par[i], command[i]);
                            }
                        }
                    }
                }
            }
            dataSupTag = dataSupTag.Replace("@TOPICNAME", TopicName);

            #endregion

            return dataSupTag;
            #endregion
        }

        void Generate_Alarm(string cab_Pars, string commands)
        {
            string[] cab_Par = cab_Pars.Split(',');
            string[] command = commands.Split(',');

            #region Criacao de Tag
            string dataAlarm = File.ReadAllText($"{pathAlarms}/_Alarm.xml");

            #region Parametros

            for (int i = 0; i < cab_Par.Length; i++)
            {
                if (cab_Par[i] != "")
                {
                    if (command[i] != "")
                    {
                        dataAlarm = dataAlarm.Replace(cab_Par[i], command[i]);
                    }
                    else
                    {
                        dataAlarm = dataAlarm.Replace(cab_Par[i], "0");
                        for (int j = i; j < cab_Par.Length; j++)
                        {
                            if (i != j)
                            {
                                cab_Par[j] = cab_Par[j].Replace(cab_Par[i], command[i]);
                            }
                        }
                    }
                }
            }
            dataAlarm = dataAlarm.Replace("@TOPICNAME", TopicName);
            #endregion

            FTAeAlarmStore.Commands.FTAeDetectorCommand[0].FTAlarmElements.FTAlarmElement.Add(LoadAlarm(dataAlarm));
            #endregion
        }

    }

    public enum CMType
    {
        /// <summary>
        /// Digital Input
        /// </summary>
        DI,

        /// <summary>
        /// Digital Output
        /// </summary>
        DO,

        /// <summary>
        /// Analog Input
        /// </summary>
        AI,

        /// <summary>
        /// Analog Output
        /// </summary>
        AO,

        /// <summary>
        /// ValvDig 1
        /// </summary>
        ValvDig1,

        /// <summary>
        /// ValvDig 2
        /// </summary>
        ValvDig2,

        /// <summary>
        /// MotDig 1
        /// </summary>
        MotDig1

    }


    public class XmlWriterEE : XmlWriter
    {
        private XmlWriter baseWriter;

        public XmlWriterEE(XmlWriter w)
        {
            baseWriter = w;
        }

        //Force WriteEndElement to use WriteFullEndElement
        public override void WriteEndElement() { baseWriter.WriteFullEndElement(); }

        public override void WriteFullEndElement()
        {
            baseWriter.WriteFullEndElement();
        }

        public override void Close()
        {
            baseWriter.Close();
        }

        public override void Flush()
        {
            baseWriter.Flush();
        }

        public override string LookupPrefix(string ns)
        {
            return (baseWriter.LookupPrefix(ns));
        }

        public override void WriteBase64(byte[] buffer, int index, int count)
        {
            baseWriter.WriteBase64(buffer, index, count);
        }

        public override void WriteCData(string text)
        {
            baseWriter.WriteCData(text);
        }

        public override void WriteCharEntity(char ch)
        {
            baseWriter.WriteCharEntity(ch);
        }

        public override void WriteChars(char[] buffer, int index, int count)
        {
            baseWriter.WriteChars(buffer, index, count);
        }

        public override void WriteComment(string text)
        {
            baseWriter.WriteComment(text);
        }

        public override void WriteDocType(string name, string pubid, string sysid, string subset)
        {
            baseWriter.WriteDocType(name, pubid, sysid, subset);
        }

        public override void WriteEndAttribute()
        {
            baseWriter.WriteEndAttribute();
        }

        public override void WriteEndDocument()
        {
            baseWriter.WriteEndDocument();
        }

        public override void WriteEntityRef(string name)
        {
            baseWriter.WriteEntityRef(name);
        }

        public override void WriteProcessingInstruction(string name, string text)
        {
            baseWriter.WriteProcessingInstruction(name, text);
        }

        public override void WriteRaw(string data)
        {
            baseWriter.WriteRaw(data);
        }

        public override void WriteRaw(char[] buffer, int index, int count)
        {
            baseWriter.WriteRaw(buffer, index, count);
        }

        public override void WriteStartAttribute(string prefix, string localName, string ns)
        {
            baseWriter.WriteStartAttribute(prefix, localName, ns);
        }

        public override void WriteStartDocument(bool standalone)
        {
            baseWriter.WriteStartDocument(standalone);
        }

        public override void WriteStartDocument()
        {
            baseWriter.WriteStartDocument();
        }

        public override void WriteStartElement(string prefix, string localName, string ns)
        {
            baseWriter.WriteStartElement(prefix, localName, ns);
        }

        public override WriteState WriteState
        {
            get { return baseWriter.WriteState; }
        }

        public override void WriteString(string text)
        {
            baseWriter.WriteString(text);
        }

        public override void WriteSurrogateCharEntity(char lowChar, char highChar)
        {
            baseWriter.WriteSurrogateCharEntity(lowChar, highChar);
        }

        public override void WriteWhitespace(string ws)
        {
            baseWriter.WriteWhitespace(ws);
        }
    }
}
