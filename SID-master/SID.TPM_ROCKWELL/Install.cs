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
using Microsoft.Win32;
using SID.TPM_ROCKWELL.Designer;
using System.IO;
using System.Windows.Shapes;
using System.Diagnostics;
using SID.TPM_ROCKWELL.Controls;

namespace SID.TPM_ROCKWELL
{
    class Install : Plugin_Model
    {
        const string name = "TPM_ROCKWELL";

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
            get { return "Sistema de insersão para PLC Rockwell Padrão TPM"; }
        }


        //public Select_Manager Select_Manager { get => select_Manager; set => select_Manager = value; }

        public Install() : base(name, 1)
        {

            EnablePlugin = true;

        }

        #region Inicialização
        public override void INIT()
        {
            if (!Directory.Exists(Project.Path + @"/sid.files/TPM_ROCKWELL"))
            {
                Directory.CreateDirectory(System.IO.Path.GetDirectoryName(Project.Path) + @"/sid.files/TPM_ROCKWELL");
            }
            if (!Directory.Exists(Project.Path + @"/sid.files/TPM_ROCKWELL/Database"))
            {
                Directory.CreateDirectory(System.IO.Path.GetDirectoryName(Project.Path) + @"/sid.files/TPM_ROCKWELL/Database");
            }

            DirectoryInfo sourceDir = new DirectoryInfo(@"Plugin/DataBase/Components");
            DirectoryInfo destinationDir = new DirectoryInfo(System.IO.Path.GetDirectoryName(Project.Path) + @"/sid.files/TPM_ROCKWELL/Database");

            CopyDirectory(sourceDir, destinationDir);


            Load_Select_Manager();
        }


        void Load_Select_Manager()
        {
            //Inicializar as seleções do gerenciador
            Select_Manager = new Select_Manager();
            Select_Manager.Title = Name;

            #region Seleção 01
            TreeViewItem item_01 = new TreeViewItem();
            item_01.Header = "Config";

            #region Seleção 01_01
            TreeViewItem item_01_01 = new TreeViewItem();
            item_01_01.Header = "Import";
            item_01_01.MouseLeftButtonUp += Item_01_01_Selected;

            item_01.Items.Add(item_01_01);
            #endregion


            Select_Manager.Items.Add(item_01);
            #endregion

            #region Seleção 02
            TreeViewItem item_02 = new TreeViewItem();
            item_02.Header = "Control Modules";

            #region Seleção 02_01 - Digital Input
            TreeViewItem item_02_01 = new TreeViewItem();
            item_02_01.Header = "Digital Input";
            item_02_01.MouseLeftButtonUp += Item_02_01_Selected;

            item_02.Items.Add(item_02_01);
            #endregion

            #region Seleção 02_02 - Digital Output
            TreeViewItem item_02_02 = new TreeViewItem();
            item_02_02.Header = "Digital Output";
            item_02_02.MouseLeftButtonUp += Item_02_02_Selected;
            item_02.Items.Add(item_02_02);
            #endregion

            #region Seleção 02_03 - Analog Input
            TreeViewItem item_02_03 = new TreeViewItem();
            item_02_03.Header = "Analog Input";
            item_02_03.MouseLeftButtonUp += Item_02_03_Selected;
            item_02.Items.Add(item_02_03);
            #endregion

            #region Seleção 02_04 - Analog Output
            TreeViewItem item_02_04 = new TreeViewItem();
            item_02_04.Header = "Analog Output";
            item_02_04.MouseLeftButtonUp += Item_02_04_Selected;
            item_02.Items.Add(item_02_04);
            #endregion

            #region Seleção 02_05 - Valv Dig 1
            TreeViewItem item_02_05 = new TreeViewItem();
            item_02_05.Header = "ValvDig 1";
            item_02_05.MouseLeftButtonUp += Item_02_05_Selected;
            item_02.Items.Add(item_02_05);
            #endregion

            #region Seleção 02_06 - Valv Dig 2
            TreeViewItem item_02_06 = new TreeViewItem();
            item_02_06.Header = "ValvDig 2";
            item_02_06.MouseLeftButtonUp += Item_02_06_Selected;
            item_02.Items.Add(item_02_06);
            #endregion

            #region Seleção 02_07 - MotDig 1
            TreeViewItem item_02_07 = new TreeViewItem();
            item_02_07.Header = "MotDig1";
            item_02.Items.Add(item_02_07);
            item_02_07.MouseLeftButtonUp += Item_02_07_Selected;
            #endregion

            Select_Manager.Items.Add(item_02);
            #endregion


            #region Seleção 03
            TreeViewItem item_03 = new TreeViewItem();
            item_03.Header = "Object";

            #region Seleção 03_01
            TreeViewItem item_03_01 = new TreeViewItem();
            item_03_01.Header = "Generate";
            item_03_01.MouseLeftButtonUp += Item_03_01_Selected;

            item_03.Items.Add(item_03_01);


            #endregion 
            #endregion


            Select_Manager.Items.Add(item_03);

            //Seleção 04
            TreeViewItem item_04 = new TreeViewItem();
            item_04.Header = "Routes";
            item_04.MouseLeftButtonUp += Item_04_Selected;
            Select_Manager.Items.Add(item_04);


        }


        #endregion

        #region Atualização
        public override void UPDATE()
        {


        }
        #endregion

        #region Seleção 01-01 - Importação
        private void Item_01_01_Selected(object sender, RoutedEventArgs e)
        {

            // Configure open file dialog box
            var dialog = new Microsoft.Win32.OpenFileDialog();
            //dialog.FileName = "Document"; // Default file name
            dialog.DefaultExt = ".L5X"; // Default file extension
            dialog.Filter = "Export RsLogix (.L5X)|*.l5x"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                //string filename = dialog.FileName;
                Import.Execute(System.IO.Path.GetDirectoryName(Project.Path) + @"/sid.files/TPM_ROCKWELL/Database/", dialog.FileName);
            }
        }

        #endregion

        #region Seleção 02-01 - Digital Input
        private void Item_02_01_Selected(object sender, RoutedEventArgs e)
        {
            GenerateCM(CMType.DI);
        }
        #endregion

        #region Seleção 02-02 - Digital Output
        private void Item_02_02_Selected(object sender, RoutedEventArgs e)
        {
            GenerateCM(CMType.DO);
        }
        #endregion

        #region Seleção 02-03 - Analog Input
        private void Item_02_03_Selected(object sender, RoutedEventArgs e)
        {
            GenerateCM(CMType.AI);
        }
        #endregion

        #region Seleção 02-04 - Analog Output
        private void Item_02_04_Selected(object sender, RoutedEventArgs e)
        {
            GenerateCM(CMType.AO);
        }
        #endregion

        #region Seleção 02-05 - ValvDig 1
        private void Item_02_05_Selected(object sender, RoutedEventArgs e)
        {
            GenerateCM(CMType.ValvDig1);
        }
        #endregion

        #region Seleção 02-06 - ValvDig 2
        private void Item_02_06_Selected(object sender, RoutedEventArgs e)
        {
            GenerateCM(CMType.ValvDig2);
        }

        #endregion

        #region Seleção 02-07 - MotDig 1
        private void Item_02_07_Selected(object sender, RoutedEventArgs e)
        {
            GenerateCM(CMType.MotDig1);
        }

        #endregion

        private void Item_02_Selected(object sender, RoutedEventArgs e)
        {


        }

        private void Item_03_01_Selected(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bem vindo a criação de Objetos!", "Gerador de Objetos") == MessageBoxResult.OK)
            {
                GenerateObject();
            }


        }





        #region Seleção 04 - Routes
        private void Item_04_Selected(object sender, RoutedEventArgs e)
        {
            GenerateRouteOut();
        }

        #endregion


        string GetCSV(string title = "sem Titulo")
        {
            // Configure open file dialog box
            var dialog = new Microsoft.Win32.OpenFileDialog();
            //dialog.FileName = "Document"; // Default file name
            dialog.DefaultExt = ".csv"; // Default file extension
            dialog.Filter = "Planilha de dados (.csv)|*.csv"; // Filter files by extension
            dialog.Title = title;

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                return dialog.FileName;
            }
            return null;
        }



        static void CopyDirectory(DirectoryInfo source, DirectoryInfo destination)
        {
            if (!destination.Exists)
            {
                destination.Create();
            }

            // Copy all files.
            FileInfo[] files = source.GetFiles();
            foreach (FileInfo file in files)
            {
                if (!File.Exists(System.IO.Path.Combine(destination.FullName, file.Name)))
                {
                    file.CopyTo(System.IO.Path.Combine(destination.FullName,
                               file.Name));
                }
            }

            // Process subdirectories.
            DirectoryInfo[] dirs = source.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                // Get destination directory.
                string destinationDir = System.IO.Path.Combine(destination.FullName, dir.Name);

                // Call CopyDirectory() recursively.
                CopyDirectory(dir, new DirectoryInfo(destinationDir));
            }
        }

        void GenerateCM(CMType cMType)
        {
            // Configure open file dialog box
            var dialog = new Microsoft.Win32.OpenFileDialog();
            //dialog.FileName = "Document"; // Default file name
            dialog.DefaultExt = ".csv"; // Default file extension
            dialog.Filter = "Planilha de dados (.csv)|*.csv"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dialog.FileName;
                string path = System.IO.Path.GetDirectoryName(Project.Path) + @"/sid.files/TPM_ROCKWELL/";

                new Controls.ControlModules(path, cMType, dialog.FileName);
            }
        }

        void GenerateObject()
        {

            string filePhases = GetCSV("Selecione o arquivo de fases");
            string fileSteps = GetCSV("Selecione o arquivo de passos");
            string fileActs = GetCSV("Selecione o arquivo de ativações");
            string path = System.IO.Path.GetDirectoryName(Project.Path) + @"/sid.files/TPM_ROCKWELL/";

            new Controls.Objects(path, filePhases, fileSteps, fileActs);

        }

        void GenerateRouteOut()
        {
            // Configure open file dialog box
            var dialog = new Microsoft.Win32.OpenFileDialog();
            //dialog.FileName = "Document"; // Default file name
            dialog.DefaultExt = ".csv"; // Default file extension
            dialog.Filter = "Planilha de dados (.csv)|*.csv"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dialog.FileName;
                string path = System.IO.Path.GetDirectoryName(Project.Path) + @"/sid.files/TPM_ROCKWELL/";

                new Controls.Routes(path, filename);
            }
        }
    }
}