using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Serialization;
using System.Xml;
using SID.Standard.Models.XML;
using SID.TPM_ROCKWELL.Models.FTVAlarms;
using System.Windows.Shapes;
using System.Data;
using System.Windows.Documents;
using System.Xml.Linq;

namespace SID.TPM_ROCKWELL.Controls
{


    public class Objects
    {
        const string file_TaskContent = @"Plugin/DataBase/Components/Base_Program.xml";

        string pathOut;
        string fileOut;

        RSLogix5000Content rSLogix5000Content;
        Program program;

        List<Phase> phases = new List<Phase>();
        List<Act> acts = new List<Act>();

        public Objects(string path, string filePhases, string fileSteps, string fileActs)
        {
            pathOut = path + @"Out/Objects/";
            fileOut = pathOut + "NewObject.L5X";
            if (!Directory.Exists(pathOut))
            {
                Directory.CreateDirectory(pathOut);
            }

            LoadFiles(filePhases, fileSteps, fileActs);
            Create_BaseProgram();
            Generate_LocalTag();
            Generate_Main();
            Generate_Unit_Act();
            Generate_Phases();
            Save_Program();
        }

        void LoadFiles(string filePhases, string fileSteps, string fileActs)
        {
            #region Read Files
            StreamReader docPhases;
            if (File.Exists(filePhases))
            {
                docPhases = new StreamReader(filePhases);
            }
            else
            {
                return;
            }

            StreamReader docSteps;
            if (File.Exists(filePhases))
            {
                docSteps = new StreamReader(fileSteps);
            }
            else
            {
                return;
            }

            StreamReader docActs;
            if (File.Exists(filePhases))
            {
                docActs = new StreamReader(fileActs);
            }
            else
            {
                return;
            }

            #endregion

            program = new Program()
            {
                Name = "NewObject",
                TestEdits = "false",
                MainRoutineName = "Main",
                Disabled = "false",
                UseAsFolder = "false",
                Use = "Target",

            };

            #region Load Phases
            docPhases.ReadLine();
            while (!docPhases.EndOfStream)
            {
                string line = docPhases.ReadLine();
                string[] split = line.Split(',');
                if (split.Length > 1)
                {
                    Phase phase = new Phase()
                    {
                        Name = split[0],
                        Description = split[1]
                    };
                    phases.Add(phase);
                }
            }
            #endregion

            #region Load Steps
            docSteps.ReadLine();
            while (!docSteps.EndOfStream)
            {
                string line = docSteps.ReadLine();
                string[] split = line.Split(',');
                if (split.Length > 1)
                {
                    Steps step = new Steps()
                    {
                        Number = split[1],
                        Description = split[2],
                        Time = split[3],
                        Amount = split[4]
                    };
                    if (step.Time == "")
                    {
                        step.Time = "0";
                    }
                    if (step.Amount == "")
                    {
                        step.Amount = "0";
                    }
                    phases.Find(p => p.Name == split[0]).Steps.Add(step);
                }
            }
            #endregion



            #region Load Acts
            string[] splitPhase = docActs.ReadLine().Split(',');
            string[] splitSteps = docActs.ReadLine().Split(',');
            while (!docActs.EndOfStream)
            {
                string line = docActs.ReadLine();
                string[] split = line.Split(',');
                if (split.Length > 1)
                {
                    Act act = new Act()
                    {
                        Name = split[0],
                        Alias = split[1],
                        Command = split[2]
                    };
                    for (int i = 3; i < split.Length; i++)
                    {
                        if (splitPhase[i] != "")
                        {
                            if (act.Act_Phases.Count > 0)
                            {
                                if (act.Act_Phases[act.Act_Phases.Count - 1].Steps.Count == 0)
                                {
                                    act.Act_Phases.RemoveAt(act.Act_Phases.Count - 1);
                                }
                            }
                            act.Act_Phases.Add(new Act_Phase() { Name = splitPhase[i] });
                        }
                        if (split[i] == "X" || split[i] == "x")
                        {

                            act.Act_Phases[act.Act_Phases.Count - 1].Steps.Add(splitSteps[i]);
                        }
                    }
                    if (act.Act_Phases[act.Act_Phases.Count - 1].Steps.Count == 0)
                    {
                        act.Act_Phases.RemoveAt(act.Act_Phases.Count - 1);
                    }
                    if (act.Act_Phases.Count > 0)
                    {

                        acts.Add(act);
                    }
                }
            }
            #endregion
        }

        void Create_BaseProgram()
        {
            string dataPrograms = File.ReadAllText(file_TaskContent);

            RSLogix5000Content taskContent;
            using (StringReader sr = new StringReader(dataPrograms))
            {
                XmlSerializer ser = new XmlSerializer(typeof(RSLogix5000Content));
                rSLogix5000Content = (RSLogix5000Content)ser.Deserialize(sr);
            }

            rSLogix5000Content.Controller.Programs.Program = new List<Program>();
            rSLogix5000Content.Controller.Programs.Program.Add(program);
            program.Routines = new Routines();
            program.Routines.Routine = new List<Routine>();
            program.Tags = new Tags()
            {
                Use = "Context"
            };
            program.Tags.Tag = new List<Tag>();
        }

        void Save_Program()
        {
            using (var docOut = new StreamWriter(fileOut))
            {
                using (XmlTextWriter writer = new XmlTextWriter(docOut))
                {
                    writer.Formatting = Formatting.Indented;

                    XmlSerializer ser = new XmlSerializer(typeof(RSLogix5000Content));

                    ser.Serialize(writer, rSLogix5000Content);
                }
            }
        }

        void Generate_LocalTag()
        {
            foreach (Act act in acts)
            {
                Tag tag = new Tag()
                {
                    Name = act.Name,
                    TagType = "Alias",
                    AliasFor = act.Alias,
                    ExternalAccess = "Read/Write"
                };

                program.Tags.Tag.Add(tag);
            }
            foreach (Phase phase in phases)
            {
                Tag tag = new Tag()
                {
                    Name = phase.Name,
                    TagType = "Base",
                    DataType = "_TMP_PH_Phase_Gen",
                    Constant = "false",
                    ExternalAccess = "Read/Write",
                    Description = phase.Description
                };

                program.Tags.Tag.Add(tag);
            }
        }

        void Generate_Main()
        {
            Routine routine = new Routine()
            {
                Name = "Main",
                Type = "RLL"
            };
            program.Routines.Routine.Add(routine);
            RLLContent rllContent = new RLLContent();

            routine.RLLContent = rllContent;
            rllContent.Rungs = new List<Rung>();
            int i = 0;
            for (i = 0; i < phases.Count; i++)
            {
                Rung rung = new Rung() { Number = i.ToString(), Type = "N" };
                rung.Comment = phases[i].Name;
                rung.Text = $"JSR({phases[i].Name},0);";
                rllContent.Rungs.Add(rung);
            }

            rllContent.Rungs.Add(new Rung()
            {
                Number = (++i).ToString(),
                Type = "N",
                Comment = "Unit_Act",
                Text = $"JSR(Unit_Act,0);"
            });

        }

        void Generate_Unit_Act()
        {
            Routine routine = new Routine()
            {
                Name = "Unit_Act",
                Type = "RLL"
            };
            program.Routines.Routine.Add(routine);

            RLLContent rllContent = new RLLContent();

            routine.RLLContent = rllContent;
            rllContent.Rungs = new List<Rung>();

            for (int i = 0; i < acts.Count; i++)///ativações
            {
                Rung rung = new Rung() { Number = i.ToString(), Type = "N" };
                rung.Comment = acts[i].Name;

                string ladder = "";
                if (acts[i].Act_Phases.Count > 1)
                {
                    ladder += "[";
                }
                foreach (Act_Phase act_Phase in acts[i].Act_Phases)
                {
                    ladder += $"XIC({act_Phase.Name}.Ctrl.Oper)";
                    if (act_Phase.Steps.Count > 1)
                    {
                        ladder += "[";
                    }
                    foreach (string step in act_Phase.Steps)
                    {
                        ladder += $" XIC({act_Phase.Name}.SeqSteps.Step[{step}]),";
                    }

                    ladder = ladder.Remove(ladder.Length - 1);

                    if (act_Phase.Steps.Count > 1)
                    {
                        ladder += "]";
                    }
                    ladder += ",";
                }
                ladder = ladder.Remove(ladder.Length - 1);
                if (acts[i].Act_Phases.Count > 1)
                {
                    ladder += "]";
                }

                ladder += $"OTL({acts[i].Name}{acts[i].Command});";


                rung.Text = ladder;

                rllContent.Rungs.Add(rung);

            }
        }

        void Generate_Phases()
        {
            foreach (Phase phase in phases)
            {
                Routine routine = new Routine()
                {
                    Name = phase.Name,
                    Type = "RLL"
                };
                program.Routines.Routine.Add(routine);
                RLLContent rllContent = new RLLContent();
                routine.RLLContent = rllContent;
                rllContent.Rungs = new List<Rung>();

                rllContent.Rungs.Add(new Rung()
                {
                    Number = "0",
                    Type = "N",
                    Comment = "Phase Sections:\r\n\r\nInterlocks\r\nRunning Faults\r\nControl Commands\r\nPhase Control\r\nStep Sequence\r\nPhase Specific Logic\r\nEvent Logging\r\nButton Enabling\r\nConversion",
                    Text = $"NOP();"
                });
                int n = +rllContent.Rungs.Count;
                for (int i = 0; i < phase.Steps.Count; i++)
                {
                    Rung rung = new Rung() { Number = (i + n).ToString(), Type = "N" };
                    rung.Comment = $"Step - {phase.Steps[i].Number}\r\n{phase.Steps[i].Description}";
                    if (i == 0)
                    {
                        rung.Text = $"XIC({phase.Name}.Ctrl.Running)AFI()_STD_GF_StepChange(_STD_GF_StepChange, {phase.Name}.Seq, {phase.Steps[i].Number}, {phase.Steps[i].Time}, {phase.Steps[i].Amount});";
                    }
                    else
                    {
                        rung.Text = $"XIC({phase.Name}.Ctrl.Running)XIC({phase.Name}.SeqSteps.Step[{Convert.ToInt32(phase.Steps[i].Number)-1}])_STD_GF_StepChange(_STD_GF_StepChange, {phase.Name}.Seq, {phase.Steps[i].Number}, {phase.Steps[i].Time}, {phase.Steps[i].Amount});";
                    }
                    rllContent.Rungs.Add(rung);
                }
            }
        }
    }

    class Phase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Steps> Steps { get; set; } = new List<Steps>();

    }

    class Steps
    {
        public string Number { get; set; }
        public string Description { get; set; }
        public string Time { get; set; }
        public string Amount { get; set; }
    }

    class Act
    {
        public string Name { get; set; }

        public List<Act_Phase> Act_Phases { get; set; } = new List<Act_Phase>();

        public string Alias { get; set; }

        public string Command { get; set; }
    }

    class Act_Phase
    {
        public string Name { get; set; }
        public List<string> Steps { get; set; } = new List<string>();
    }


}