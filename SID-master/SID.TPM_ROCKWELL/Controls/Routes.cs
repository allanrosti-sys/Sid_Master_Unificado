using SID.Standard.Models.XML;
using SID.TPM_ROCKWELL.Models.FTVAlarms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Serialization;

namespace SID.TPM_ROCKWELL.Controls
{

    public class Routes
    {
        const string file_TaskContent = @"Plugin/DataBase/Components/Base_Program.xml";
        const string file_BaseRouteIn = @"Plugin/DataBase/Components/LinesAndRoutes/Route_IN.xml";
        const string file_BaseRouteOut = @"Plugin/DataBase/Components/LinesAndRoutes/Route_OUT.xml";


        string path_DataTypes;
        string path_AddOns;
        string pathOut;


        List<RSLogix5000Content> TaskContents;
        AddOnInstructionDefinitions AddOns;

        public Routes(string path, string file)
        {

            path_DataTypes = path + @"DataBase/DataTypes/";
            path_AddOns = path + @"DataBase/AddOns/";
            pathOut = path + @"Out/Routes/" + @"/";

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

            TaskContents = new List<RSLogix5000Content>();
            AddOns = new AddOnInstructionDefinitions();
            AddOns.AddOnInstructionDefinition = new List<AddOnInstructionDefinition>();

            string cab_par = doc.ReadLine();
            doc.ReadLine();
            while (!doc.EndOfStream)
            {
                string line = doc.ReadLine();
                GenerateRoute(cab_par, line);
            }
            LoadAddOns("_USR_PH_Phase_XferIn");

            foreach (RSLogix5000Content rSLogix5000Content in TaskContents)
            {
                rSLogix5000Content.Controller.AddOnInstructionDefinitions = AddOns;

                string pathFile = pathOut + rSLogix5000Content.Name + ".L5X";
                using (var docOut = new StreamWriter(pathOut + rSLogix5000Content.Name + ".L5X"))
                {
                    using (XmlTextWriter writer = new XmlTextWriter(docOut))
                    {
                        writer.Formatting = Formatting.Indented;
                        XmlSerializer ser = new XmlSerializer(typeof(RSLogix5000Content));
                        ser.Serialize(writer, rSLogix5000Content);
                    }
                }
            }
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

        void GenerateRoute(string cab_Pars, string commands)
        {
            string data_Route = "";

            string[] cab_Par = cab_Pars.Split(',');
            string[] command = commands.Split(',');

            Route_Type route_Type;
            for (int i = 0; i < cab_Par.Length; i++)
            {
                if (cab_Par[i] == "@Type")
                {
                    route_Type = (Route_Type)Enum.Parse(typeof(Route_Type), command[i]);
                    switch (route_Type)
                    {
                        case Route_Type.IN:
                            {
                                data_Route = File.ReadAllText(file_BaseRouteIn);
                                break;
                            }
                        case Route_Type.OUT:
                            {
                                data_Route = File.ReadAllText(file_BaseRouteOut);
                                break;
                            }
                        default:
                            break;
                    }
                }
            }

            for (int i = 0; i < cab_Par.Length; i++)
            {
                if (cab_Par[i] != "")
                {
                    if (command[i] != "")
                    {
                        data_Route = data_Route.Replace(cab_Par[i], command[i]);

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
                        data_Route = data_Route.Replace(cab_Par[i], "0");
                    }

                }
            }
            Program program;
            Stream st;
            using (TextReader reader = new StringReader(data_Route))
            {
                XmlSerializer ser = new XmlSerializer(typeof(Program));
                program = (Program)ser.Deserialize(reader);
            }

            RSLogix5000Content taskContent = new RSLogix5000Content();
            bool taskContent_Exist = false;
            string taskContent_Name = "";


            for (int i = 0; i < cab_Par.Length; i++)
            {
                if (cab_Par[i] == "@TASK")
                {
                    taskContent_Name = command[i];
                }
            }
            foreach (RSLogix5000Content item in TaskContents)
            {
                if (item.Name == taskContent_Name)
                {
                    taskContent_Exist = true;
                    taskContent = item;
                }
            }

            if (!taskContent_Exist)
            {
                taskContent = Generate_TaskContent(taskContent_Name);
                TaskContents.Add(taskContent);
            }

            taskContent.Controller.Programs.Program.Add(program);
        }

        RSLogix5000Content Generate_TaskContent(string name)
        {
            string dataPrograms = File.ReadAllText(file_TaskContent);

            RSLogix5000Content taskContent;
            using (StringReader sr = new StringReader(dataPrograms))
            {
                XmlSerializer ser = new XmlSerializer(typeof(RSLogix5000Content));
                taskContent = (RSLogix5000Content)ser.Deserialize(sr);
            }
            taskContent.Name = name;
            return taskContent;

        }
    }

    public enum Route_Type
    {
        IN,
        OUT,
    }
}

