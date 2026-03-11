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
using Newtonsoft.Json;
using System.IO;

namespace SID.ClickUp
{
    class Install : Plugin_Model
    {
        const string name = "Clickup";

        public override string Name
        {
            get { return name; }
        }

        public override string Author
        {
            get { return "Jonathan Alves Batista"; }
        }

        public override string Caption
        {
            get { return "Interação ClickUp"; }
        }


        //public Select_Manager Select_Manager { get => select_Manager; set => select_Manager = value; }

        public Install() : base(name, 10)
        {
            EnablePlugin = false;


        }

        #region Inicialização
        public override void INIT()
        {
            Load_Select_Manager();
        }


        void Load_Select_Manager()
        {
            //Inicializar as seleções do gerenciador
            Select_Manager = new Select_Manager();
            Select_Manager.Title = Name;

            #region Seleção 01
            TreeViewItem item_01 = new TreeViewItem();
            item_01.Header = "Export";

            #region Seleção 01_01
            TreeViewItem item_01_01 = new TreeViewItem();
            item_01_01.Header = "Task";
            item_01_01.MouseLeftButtonUp += Item_01_01_Selected;

            item_01.Items.Add(item_01_01);

            Select_Manager.Items.Add(item_01);
            #endregion
            #endregion
        }
        #endregion

        #region Atualização
        public override void UPDATE()
        {


        }
        #endregion


        #region Seleção 01 - Entradas Digitais
        private void Item_01_01_Selected(object sender, RoutedEventArgs e)
        {
            SID.ClickUp.Control.API api = new ClickUp.Control.API("pk_49109101_OHUDNFPPQ9WKNJU14ULMYVYX4OB8YGBD");

            StreamWriter doc = new StreamWriter("teste.csv");


            string ListId = "900700310566";
            string reqString = "https://api.clickup.com/api/v2/list/" + ListId + "/task?order_by=due_date&reverse=1";
            string response = api.Get(reqString);

            //List<Model.Task> result = JsonConvert.DeserializeObject<List<Model.Task>>(response);
            Control.Model.Task_List task_List = JsonConvert.DeserializeObject<Control.Model.Task_List>(response);

            foreach (Control.Model.Task item in task_List.Tasks)
            {
                doc.WriteLine($"{item.name};{item.time_estimate/3600/1000}h"); 
            }

            string ListId2 = "900700311687";
            string reqString2 = "https://api.clickup.com/api/v2/list/" + ListId2 + "/task?order_by=due_date&reverse=1";
            string response2 = api.Get(reqString2);

            //List<Model.Task> result = JsonConvert.DeserializeObject<List<Model.Task>>(response);
            Control.Model.Task_List task_List2 = JsonConvert.DeserializeObject<Control.Model.Task_List>(response2);

            foreach (Control.Model.Task item in task_List2.Tasks)
            {
                doc.WriteLine($"{item.name};{item.time_estimate / 3600 / 1000}h");
            }


            doc.Close();
        }

        #endregion


    }
}

