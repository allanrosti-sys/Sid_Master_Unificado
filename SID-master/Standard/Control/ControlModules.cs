using System;
using System.Collections.Generic;
using System.Linq;
using SID.Standard.Models.XML;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace SID.Standard.Controls
{

    public class ControlModules
    {
        #region Local dos arquivos

        const string file_BaseRungs = @"DataBase/Components/Base_RUNGS.xml";
        const string pathBase = @"DataBase/Components/ControlModules";
        const string pathTags = pathBase + @"/Tags";
        const string FileTagMotDig1 = pathTags + @"/_STD_CM_MotDig1.xml";
        const string FileTagMotDig1ExtPar = pathTags + @"/_STD_CM_MotDig1_ExtPar.xml";
        const string FileTagValvDig1 = pathTags + @"/_STD_CM_ValvDig1.xml";
        const string FileTagValvDig1_ExtPar = pathTags + @"/_STD_CM_ValvDig1_ExtPar.xml";
        const string FileBaseTag = pathTags + @"/Base_TAG.xml";
        const string FileTagOutAnlg = pathTags + @"/_STD_CM_OutAnlg.xml";

        const string path_DataTypes = @"DataBase/DataTypes/";
        const string path_AddOns = @"DataBase/AddOns/";

        const string pathRungs = pathBase + @"/Rungs";
        const string FileRungValvDig1 = pathRungs + @"/ValvDig1.xml";
        const string FileRungMotDig1 = pathRungs + @"/MotDig1.xml";
        const string FileRungOutAnlg = pathRungs + @"/OutAnlg.xml";
        const string FileRungENET_PF753 = pathRungs + @"/ENET_PF753.xml";

        const string pathOut = @"Out/";

        #endregion





        public ControlModules(string type, string file)
        {
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

            RSLogix5000Content rSLogix5000Content = LoadBase();

            //SaveRung(rSLogix5000Content);

            switch (type)
            {
                case "di":
                    {
                        while (!doc.EndOfStream)
                        {
                            Generate_DigInput(rSLogix5000Content, doc.ReadLine());
                        }
                        SaveRung(rSLogix5000Content, "digInput");
                        break;
                        
                    }

                case "v1":
                    {
                        while (!doc.EndOfStream)
                        {
                            Generate_ValvDig1(rSLogix5000Content, doc.ReadLine());
                        }
                        SaveRung(rSLogix5000Content, "valvdig1");
                        break;
                    }
                case "m1":
                    {
                        while (!doc.EndOfStream)
                        {
                            Generate_MotDig1(rSLogix5000Content, doc.ReadLine());
                        }
                        SaveRung(rSLogix5000Content, "Motdig1");
                        break;
                    }
                case "ao":
                    {
                        while (!doc.EndOfStream)
                        {
                            Generate_OutAnlg(rSLogix5000Content, doc.ReadLine());
                        }
                        SaveRung(rSLogix5000Content, "OutAnlg");
                        break;
                    }
                case "enet":
                    {
                        while (!doc.EndOfStream)
                        {
                            Generate_ENET_PF753(rSLogix5000Content, doc.ReadLine());
                        }
                        SaveRung(rSLogix5000Content, "enet");
                        break;
                    }
                    

                default: break;
            }




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

        void LoadDataType(List<DataType> dataTypes, string name)
        {
            foreach (DataType item in dataTypes)
            {
                if (item.Name == name)
                    return;
            }

            string xmlInputData = File.ReadAllText(path_DataTypes + name + ".xml");

            using (StringReader sr = new StringReader(xmlInputData))
            {
                XmlSerializer ser = new XmlSerializer(typeof(DataType));
                dataTypes.Add((DataType)ser.Deserialize(sr));
            }
        }

        void LoadAddOns(List<AddOnInstructionDefinition> addons, string name)
        {
            foreach (AddOnInstructionDefinition item in addons)
            {
                if (item.Name == name)
                    return;
            }

            string xmlInputData = File.ReadAllText(path_AddOns + name + ".xml");

            using (StringReader sr = new StringReader(xmlInputData))
            {
                XmlSerializer ser = new XmlSerializer(typeof(AddOnInstructionDefinition));
                addons.Add((AddOnInstructionDefinition)ser.Deserialize(sr));
            }
        }

        void SaveRung(RSLogix5000Content rSLogix5000Content, string file)
        {
            using (var doc = new StreamWriter(pathOut + file + ".L5X"))
            {
                using (XmlTextWriter writer = new XmlTextWriter(doc))
                {
                    writer.Formatting = Formatting.Indented;
                    XmlSerializer ser = new XmlSerializer(typeof(RSLogix5000Content));
                    ser.Serialize(writer, rSLogix5000Content);
                }
            }
        }


        /// <summary>
        /// Gera Tag e rung para ValvDig1
        /// </summary>
        /// <param name="rSLogix5000Content"></param>
        /// <param name="commands"></param>
        void Generate_DigInput(RSLogix5000Content rSLogix5000Content, string commands)
        {
            string[] command = commands.Split(';');

            #region Criacao de Tag DigInput
            string dataTAG = File.ReadAllText(FileTagValvDig1);
            Console.Write(command[0]);
            dataTAG = dataTAG.Replace("@TAG", command[0]);
            Tag tag = LoadTag(dataTAG);
            rSLogix5000Content.Controller.Tags.Tag.Add(tag);
            #endregion

            #region Criacao de Tag ValvDig1_ExPar
            string dataTAGExPar = File.ReadAllText(FileTagValvDig1_ExtPar);
            dataTAGExPar = dataTAGExPar.Replace("@TAG", command[0]);
            Tag tagExPar = LoadTag(dataTAGExPar);
            rSLogix5000Content.Controller.Tags.Tag.Add(tagExPar);
            #endregion

            #region Criacao da rung
            string dataRung = File.ReadAllText(FileRungValvDig1);
            if (command.Length == 4)
            {
                dataRung = dataRung.Replace("@TAG.In_MainActFbIO", command[1]);
                dataRung = dataRung.Replace("@TAG.In_MainDeactFbIO", command[2]);
                dataRung = dataRung.Replace("@TAG.Out_Main1IO", command[3]);
            }
            dataRung = dataRung.Replace("@TAG", command[0]);
            Rung rung = LoadRung(dataRung);
            rung.Number = rSLogix5000Content.Controller.Programs.Program[0].Routines.Routine[0].RLLContent.Rungs.Count.ToString();
            rSLogix5000Content.Controller.Programs.Program[0].Routines.Routine[0].RLLContent.Rungs.Add(rung);
            #endregion

            #region Importação dos AddOns
            LoadAddOns(rSLogix5000Content.Controller.AddOnInstructionDefinitions.AddOnInstructionDefinition, "_STD_AH_AlmStat");
            LoadAddOns(rSLogix5000Content.Controller.AddOnInstructionDefinitions.AddOnInstructionDefinition, "_STD_AH_GenAlmStat");
            LoadAddOns(rSLogix5000Content.Controller.AddOnInstructionDefinitions.AddOnInstructionDefinition, "_STD_CM_GenModeStat");
            LoadAddOns(rSLogix5000Content.Controller.AddOnInstructionDefinitions.AddOnInstructionDefinition, "_STD_CM_ValvDig1_ExtPar");
            LoadAddOns(rSLogix5000Content.Controller.AddOnInstructionDefinitions.AddOnInstructionDefinition, "_STD_CM_ValvDig1");
            #endregion
        }

        /// <summary>
        /// Gera Tag e rung para ValvDig1
        /// </summary>
        /// <param name="rSLogix5000Content"></param>
        /// <param name="commands"></param>
        void Generate_ValvDig1(RSLogix5000Content rSLogix5000Content, string commands)
        {
            string[] command = commands.Split(';');

            #region Criacao de Tag ValvDig1
            string dataTAG = File.ReadAllText(FileTagValvDig1);
            Console.Write(command[0]);
            dataTAG = dataTAG.Replace("@TAG", command[0]);
            Tag tag = LoadTag(dataTAG);
            rSLogix5000Content.Controller.Tags.Tag.Add(tag);
            #endregion

            #region Criacao de Tag ValvDig1_ExPar
            string dataTAGExPar = File.ReadAllText(FileTagValvDig1_ExtPar);
            dataTAGExPar = dataTAGExPar.Replace("@TAG", command[0]);
            Tag tagExPar = LoadTag(dataTAGExPar);
            rSLogix5000Content.Controller.Tags.Tag.Add(tagExPar);
            #endregion

            #region Criacao da rung
            string dataRung = File.ReadAllText(FileRungValvDig1);
            if (command.Length == 4)
            {
                dataRung = dataRung.Replace("@TAG.In_MainActFbIO", command[1]);
                dataRung = dataRung.Replace("@TAG.In_MainDeactFbIO", command[2]);
                dataRung = dataRung.Replace("@TAG.Out_Main1IO", command[3]);
            }
            dataRung = dataRung.Replace("@TAG", command[0]);
            Rung rung = LoadRung(dataRung);
            rung.Number = rSLogix5000Content.Controller.Programs.Program[0].Routines.Routine[0].RLLContent.Rungs.Count.ToString();
            rSLogix5000Content.Controller.Programs.Program[0].Routines.Routine[0].RLLContent.Rungs.Add(rung);
            #endregion

            #region Importação dos AddOns
            LoadAddOns(rSLogix5000Content.Controller.AddOnInstructionDefinitions.AddOnInstructionDefinition, "_STD_AH_AlmStat");
            LoadAddOns(rSLogix5000Content.Controller.AddOnInstructionDefinitions.AddOnInstructionDefinition, "_STD_AH_GenAlmStat");
            LoadAddOns(rSLogix5000Content.Controller.AddOnInstructionDefinitions.AddOnInstructionDefinition, "_STD_CM_GenModeStat");
            LoadAddOns(rSLogix5000Content.Controller.AddOnInstructionDefinitions.AddOnInstructionDefinition, "_STD_CM_ValvDig1_ExtPar");
            LoadAddOns(rSLogix5000Content.Controller.AddOnInstructionDefinitions.AddOnInstructionDefinition, "_STD_CM_ValvDig1");
            #endregion
        }

        /// <summary>
        /// Gera Tag e rung para motdig1
        /// </summary>
        /// <param name="rSLogix5000Content">base do programa</param>
        /// <param name="commands"></param>
        void Generate_MotDig1(RSLogix5000Content rSLogix5000Content, string commands)
        {
            string[] command = commands.Split(';');

            #region Criacao de Tag MotDig1
            string dataTAG = File.ReadAllText(FileBaseTag);
            dataTAG = dataTAG.Replace("@TAG", command[0]);
            dataTAG = dataTAG.Replace("@DataType", "_STD_CM_MotDig1");
            Console.WriteLine(command[0]);
            Tag tag = LoadTag(dataTAG);
            rSLogix5000Content.Controller.Tags.Tag.Add(tag);
            #endregion

            #region Criacao de Tag MotDig1_ExPar
            string dataTAGExPar = File.ReadAllText(FileBaseTag);
            dataTAGExPar = dataTAGExPar.Replace("@TAG", command[0]+ "_ExtPar");
            dataTAGExPar = dataTAGExPar.Replace("@DataType", "_STD_CM_MotDig1_ExtPar");
            Tag tagExPar = LoadTag(dataTAGExPar);
            rSLogix5000Content.Controller.Tags.Tag.Add(tagExPar);
            #endregion

            #region Criacao da rung
            string dataRung = File.ReadAllText(FileRungMotDig1);
            if (command.Length == 3)
            {
                dataRung = dataRung.Replace("@TAG.In_FwdFbIO", command[1]);
                dataRung = dataRung.Replace("@TAG.Out_FwdIO", command[2]);
            }
            dataRung = dataRung.Replace("@TAG", command[0]);
            Rung rung = LoadRung(dataRung);
            rung.Number = rSLogix5000Content.Controller.Programs.Program[0].Routines.Routine[0].RLLContent.Rungs.Count.ToString();
            rSLogix5000Content.Controller.Programs.Program[0].Routines.Routine[0].RLLContent.Rungs.Add(rung);
            #endregion

            #region Importação dos AddOns
            LoadAddOns(rSLogix5000Content.Controller.AddOnInstructionDefinitions.AddOnInstructionDefinition, "_STD_AH_AlmStat");
            LoadAddOns(rSLogix5000Content.Controller.AddOnInstructionDefinitions.AddOnInstructionDefinition, "_STD_AH_GenAlmStat");
            LoadAddOns(rSLogix5000Content.Controller.AddOnInstructionDefinitions.AddOnInstructionDefinition, "_STD_CM_GenModeStat");
            LoadAddOns(rSLogix5000Content.Controller.AddOnInstructionDefinitions.AddOnInstructionDefinition, "_STD_CM_MotDig1_ExtPar");
            LoadAddOns(rSLogix5000Content.Controller.AddOnInstructionDefinitions.AddOnInstructionDefinition, "_STD_CM_MotDig1");
            #endregion
        }

        /// <summary>
        /// Gera Tag e rung para saida analogica
        /// </summary>
        /// <param name="rSLogix5000Content"></param>
        /// <param name="commands"></param>
        void Generate_OutAnlg(RSLogix5000Content rSLogix5000Content, string commands)
        {
            string[] command = commands.Split(';');

            #region Criacao de Tag MotDig1
            string dataTAG = File.ReadAllText(FileBaseTag);
            dataTAG = dataTAG.Replace("@TAG", command[0]);
            dataTAG = dataTAG.Replace("@DataType", "_STD_CM_OutAnlg");
            Console.WriteLine(command[0]);
            Tag tag = LoadTag(dataTAG);
            rSLogix5000Content.Controller.Tags.Tag.Add(tag);
            #endregion

            #region Criacao da rung
            string dataRung = File.ReadAllText(FileRungOutAnlg);
            dataRung = dataRung.Replace("@TAG", command[0]);
            Rung rung = LoadRung(dataRung);
            rung.Number = rSLogix5000Content.Controller.Programs.Program[0].Routines.Routine[0].RLLContent.Rungs.Count.ToString();
            rSLogix5000Content.Controller.Programs.Program[0].Routines.Routine[0].RLLContent.Rungs.Add(rung);
            #endregion

            #region Importação dos AddOns
            LoadAddOns(rSLogix5000Content.Controller.AddOnInstructionDefinitions.AddOnInstructionDefinition, "_STD_AH_AlmStat");
            LoadAddOns(rSLogix5000Content.Controller.AddOnInstructionDefinitions.AddOnInstructionDefinition, "_STD_AH_GenAlmStat");
            LoadAddOns(rSLogix5000Content.Controller.AddOnInstructionDefinitions.AddOnInstructionDefinition, "_STD_CM_GenModeStat");
            LoadAddOns(rSLogix5000Content.Controller.AddOnInstructionDefinitions.AddOnInstructionDefinition, "_STD_CM_OutAnlg");
            #endregion
        }

        void Generate_ENET_PF753(RSLogix5000Content rSLogix5000Content, string commands)
        {
            string[] command = commands.Split(';');

            #region Criacao de Tag MotDig1
            string dataTAG = File.ReadAllText(FileBaseTag);
            dataTAG = dataTAG.Replace("@TAG", command[0]);
            dataTAG = dataTAG.Replace("@DataType", "_USR_ENET_PF753");


            Console.WriteLine(command[0]);
            Tag tag = LoadTag(dataTAG);
            rSLogix5000Content.Controller.Tags.Tag.Add(tag);
            #endregion

            #region Criacao da rung
            string dataRung = File.ReadAllText(FileRungENET_PF753);
            dataRung = dataRung.Replace("@TAG", command[0]);
            dataRung = dataRung.Replace("@Inv", command[1]);
            dataRung = dataRung.Replace("@MOT", command[2]);
            dataRung = dataRung.Replace("@SC", command[3]);
            Rung rung = LoadRung(dataRung);
            rung.Number = rSLogix5000Content.Controller.Programs.Program[0].Routines.Routine[0].RLLContent.Rungs.Count.ToString();
            rSLogix5000Content.Controller.Programs.Program[0].Routines.Routine[0].RLLContent.Rungs.Add(rung);
            #endregion

            #region Importação dos AddOns
            LoadAddOns(rSLogix5000Content.Controller.AddOnInstructionDefinitions.AddOnInstructionDefinition, "_STD_AH_AlmStat");
            LoadAddOns(rSLogix5000Content.Controller.AddOnInstructionDefinitions.AddOnInstructionDefinition, "_STD_AH_GenAlmStat");
            LoadAddOns(rSLogix5000Content.Controller.AddOnInstructionDefinitions.AddOnInstructionDefinition, "_STD_CM_GenModeStat");
            LoadAddOns(rSLogix5000Content.Controller.AddOnInstructionDefinitions.AddOnInstructionDefinition, "_USR_ENET_PF753");

            #endregion
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



    }
}
