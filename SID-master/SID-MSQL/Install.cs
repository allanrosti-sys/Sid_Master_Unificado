using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SID.Plugin.Control;
using System.Windows.Controls;
using SID.MSQL.Control;
using System.Windows;
using SID.Designer.Control;
using SID.Designer.Windows;
using SID.Designer.ToolBox;
using System.IO;
using SID.Standard.Control;
using System.Data;
using SID.Designer.Pages;

namespace SID.MSQL
{

    class Install : Plugin_Model
    {
        const string name = "MSQL";
        string string_Conn;
        MSQL_Info mSQL_Info;
        MSQL_Conn mSQL_Conn;
        TreeViewItem item2;

        public override int ExecOrder { get; set; }

        //public DesignerControl DesignerControl { get; set; }

        public Install() : base(name, 1)
        {
            EnablePlugin = false;

            #region Seleçãoes para tela de navegação

            TreeViewItem item1 = new TreeViewItem();
            item1.Header = "Conexão";
            item1.MouseLeftButtonUp += Item1_Selected;
            Select_Manager.Items.Add(item1);


            //TreeViewItem item2 = new TreeViewItem();
            item2 = new TreeViewItem();
            item2.Header = "Tabelas";
            //item2.MouseLeftButtonUp += Item2_Selected;
            Select_Manager.Items.Add(item2);

            //INIT();


            #endregion
        }

        #region Sequencia de inicialização do plugin
        public override void INIT()
        {

            string path = "MSQL_Conn.sidconfig";
            if (File.Exists(path))
            {
                using (Stream stream = File.Open(path, FileMode.Open))
                {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                    mSQL_Info = (MSQL_Info)binaryFormatter.Deserialize(stream);
                }

                string_Conn = $"Data Source={mSQL_Info.DataSource};Initial Catalog = {mSQL_Info.Database}; User ID = {mSQL_Info.User}; Password = {mSQL_Info.Password}";
                mSQL_Conn = new MSQL_Conn(string_Conn);
                Project.Connections.Add(mSQL_Conn);

            }
            else
            {
                string_Conn = "NOLOAD";
            }

            Load_Tables();

        }


        void Load_Tables()
        {
            DataTable dt = mSQL_Conn.Select("SELECT table_name, table_schema, table_type FROM information_schema.tables ORDER BY table_name ASC; ");

            foreach (DataRow item in dt.Rows)
            {
                TreeViewItem itemtb = new TreeViewItem();
                itemtb.Header = item[1].ToString() + "." + item[0].ToString();
                itemtb.MouseLeftButtonUp += Itemtb_Selected;
                item2.Items.Add(itemtb);
            }
        }

        #endregion




        #region Seleções para tela de navegação

        private void Item1_Selected(object sender, RoutedEventArgs e)
        {
            Designer.PopUp.Win_Connection win_Conection;
            if (string_Conn != "NOLOAD")
            {
                win_Conection = new Designer.PopUp.Win_Connection(mSQL_Info);
            }
            else
            {
                win_Conection = new Designer.PopUp.Win_Connection();
            }
            if ((bool)win_Conection.ShowDialog())
            {
                string path = "MSQL_Conn.sidconfig";
                mSQL_Info = win_Conection.mSQL_Connection;
                using (Stream stream = File.Open(path, FileMode.Create))
                {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    binaryFormatter.Serialize(stream, win_Conection.mSQL_Connection);
                    stream.Close();
                }
                MessageBox.Show("Configuração de banco de dados salva!");
            }
            else
            {
                MessageBox.Show("Configuração de banco de dados mantidas!");
            }
        }

        private void Item2_Selected(object sender, RoutedEventArgs e)
        {
            DataTable dt = Project.Connections[0].Select("SELECT table_name, table_schema, table_type FROM information_schema.tables ORDER BY table_name ASC; ");

            DataTableViewer tableViewer = new DataTableViewer(dt, "Tabelas");


            DesignerControl.SetViewer(tableViewer);
        }

        private void Itemtb_Selected(object sender, RoutedEventArgs e)
        {
            string name = ((TreeViewItem)sender).Header.ToString();
            DataTable dt = Project.Connections[0].Select($"SELECT * FROM {name}; ");

            DataTableViewer tableViewer = new DataTableViewer(dt, name);
            DesignerControl.SetViewer(tableViewer);
        }
        #endregion

    }
}
