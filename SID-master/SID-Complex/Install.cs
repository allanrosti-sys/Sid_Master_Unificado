using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SID.Plugin.Control;
using System.Windows.Controls;
using SID.Designer.Control;
using SID.Designer.ToolBox;
using System.Windows;
using SID.Designer.Pages;
using System.Data;
using SID.Complex.Designer.Pages;
using SID.Complex.Control;
using SID.Complex.Control.Tables;
using System.IO;
using SID.Complex.Designer.ToolBox;

namespace SID_Complex
{
    class Install : Plugin_Model
    {
        #region Campos

        const string name = "Complex";
        Ctrl_Complex complex;

        #endregion 

        #region Propriedades

        public override string Name
        {
            get { return "Complex"; }
        }

        public override string Author
        {
            get { return "Jonathan Alves Batista"; }
        }

        public override string Caption
        {
            get { return "Sistema de interação banco de dados e supervisório"; }
        }

        #endregion


        List<Table> List_Select_Classification = new List<Table>();
        List<Table> List_Select_ControlModules = new List<Table>();
        List<Table> List_Select_Phases = new List<Table>();

        public Install() : base(name, 2)
        {
            EnablePlugin = false;
        }

        #region Inicialização
        public override void INIT()
        {
            complex = new Ctrl_Complex();
            Load_Select_Manager();
        }


        void Load_Select_Manager()
        {
            //Inicializar as seleções do gerenciador
            Select_Manager = new Select_Manager();
            Select_Manager.Title = Name;



            #region Classificações

            ///Areas
            List_Select_Classification.Add(new Table_tblAreas());

            ///Controladores
            List_Select_Classification.Add(new Table_tblControllers());

            ///Tipo de Controladores
            List_Select_Classification.Add(new Table_tblControllerTypes());

            ///Painéis
            List_Select_Classification.Add(new Table_tblCabinets());

            ///Unidades de Engenharia
            List_Select_Classification.Add(new Table_tblEngineeringUnits());

            ///Tipo de entradas Analogicas
            List_Select_Classification.Add(new Table_tblCmAnalogInputType());

            ///Tipo de Saidas Analogicas
            List_Select_Classification.Add(new Table_tblCmAnalogOutputType());

            ///Tipo de Motores
            List_Select_Classification.Add(new Table_tblCmMotorType());

            ///Tipo de Totalizadores
            List_Select_Classification.Add(new Table_tblCmTotalizerType());

            ///Tipo de Valvulas
            List_Select_Classification.Add(new Table_tblCmValveType());

            ///Tipo de VSD
            List_Select_Classification.Add(new Table_tblCmVSDType());

            ///Fornecedores de VSD
            List_Select_Classification.Add(new Table_tblCmVSDVendor());

            ///Enumerador de grupo
            List_Select_Classification.Add(new Table_tblEnumerationGroup());


            TreeViewItem tvi_Select_Classification = new TreeViewItem();
            tvi_Select_Classification.Header = "Classificações";
            for (int i = 0; i < List_Select_Classification.Count; i++)
            {
                TreeViewItem tv = new TreeViewItem();
                tv.Header = List_Select_Classification[i].Name;
                tv.Tag = i;
                tv.MouseLeftButtonUp += e_Select_Classification;
                tvi_Select_Classification.Items.Add(tv);
            }
            Select_Manager.Items.Add(tvi_Select_Classification);
            #endregion



            #region Control Modules

            ///Entradas analogicas
            List_Select_ControlModules.Add(new Table_tblCmAnalogInput());

            ///Saidas analogicas
            List_Select_ControlModules.Add(new Table_tblCmAnalogOutput());

            ///Entradas Digitais
            List_Select_ControlModules.Add(new Table_tblCmDigitalInput());

            ///Saidas Digitais
            List_Select_ControlModules.Add(new Table_tblCmDigitalOutput());

            ///Motores
            List_Select_ControlModules.Add(new Table_tblCmMotor());

            ///Totalizadores
            List_Select_ControlModules.Add(new Table_tblCmTotalizer());

            ///Valvulas
            List_Select_ControlModules.Add(new Table_tblCmValve());

            ///VSD
            List_Select_ControlModules.Add(new Table_tblCmVSD());

            ///PID
            List_Select_ControlModules.Add(new Table_tblCmPID());

            TreeViewItem tvi_Select_ControlModules = new TreeViewItem();
            tvi_Select_ControlModules.Header = "Control Modules";
            for (int i = 0; i < List_Select_ControlModules.Count; i++)
            {
                TreeViewItem tv = new TreeViewItem();
                tv.Header = List_Select_ControlModules[i].Name;
                tv.Tag = i;
                tv.MouseLeftButtonUp += e_Select_ControlModules;
                tvi_Select_ControlModules.Items.Add(tv);
            }
            Select_Manager.Items.Add(tvi_Select_ControlModules);

            #endregion


            #region Phases

            ///Classe de fases
            List_Select_Phases.Add(new Table_tblPhaseClass());
            List_Select_Phases.Add(new Table_tblPhaseAlarmWarningInfosText());
            List_Select_Phases.Add(new Table_tblPhaseAlarmWarningInfos());
            List_Select_Phases.Add(new Table_tblPhaseSoftkeys());
            List_Select_Phases.Add(new Table_tblPhaseParameter());

            TreeViewItem tvi_Select_Phases = new TreeViewItem();
            tvi_Select_Phases.Header = "Fases";
            for (int i = 0; i < List_Select_Phases.Count; i++)
            {
                TreeViewItem tv = new TreeViewItem();
                tv.Header = List_Select_Phases[i].Name;
                tv.Tag = i;
                tv.MouseLeftButtonUp += e_Select_Phases;
                tvi_Select_Phases.Items.Add(tv);

            }
            Select_Manager.Items.Add(tvi_Select_Phases);




            #endregion


            TreeViewItem item_03 = new TreeViewItem();
            item_03.Header = "Magic";
            item_03.MouseLeftButtonUp += Item_03_MouseLeftButtonUp;
            // Select_Manager.Items.Add(item_03);
        }

        #endregion




        #region Atualização
        public override void UPDATE()
        {

            complex.Connection = Project.Connections[0];

        }
        #endregion

        private void Item_03_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            UPDATE();
            complex.Magic();
        }

        private void e_Select_Classification(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            UPDATE();
            int id = Convert.ToInt32(((TreeViewItem)sender).Tag);
            List_Select_Classification[id].Complex = complex;
            TableViewer tableViewer = new TableViewer(List_Select_Classification[id]);
            DesignerControl.SetViewer(tableViewer);
        }

        private void e_Select_ControlModules(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            UPDATE();
            int id = Convert.ToInt32(((TreeViewItem)sender).Tag);
            List_Select_ControlModules[id].Complex = complex;
            TableViewer tableViewer = new TableViewer(List_Select_ControlModules[id]);
            DesignerControl.SetViewer(tableViewer);
        }

        private void e_Select_Phases(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            UPDATE();
            int id = Convert.ToInt32(((TreeViewItem)sender).Tag);
            List_Select_Phases[id].Complex = complex;
            TableViewer tableViewer = new TableViewer(List_Select_Phases[id]);
            DesignerControl.SetViewer(tableViewer);
        }


    }
}
