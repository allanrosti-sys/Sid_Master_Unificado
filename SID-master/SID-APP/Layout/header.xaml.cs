using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SID_Standard.Controls;
using SID_APP.PopUp;
using Microsoft.Win32;
using System.IO;

namespace SID_APP.Layout
{
    /// <summary>
    /// Interação lógica para header.xaml
    /// </summary>
    public partial class header : UserControl
    {
        Project project;
        public event EventHandler<Event_ProjectUpdate> UpdateProject;

        public header()
        {
            InitializeComponent();
        }


        void Load_Project(string path)
        {
            using (Stream stream = File.Open(path, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                project = (Project)binaryFormatter.Deserialize(stream);
                this.UpdateProject?.Invoke(this, new Event_ProjectUpdate() { Project = project });
            }
        }

        public Project Project { get => project; set => project = value; }

        

        private void OpenProject_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Arquivo de projeto (*.sid)|*.sid";
            if (openFileDialog.ShowDialog() == true)
                Load_Project(openFileDialog.FileName);
        }

        private void NewProject_Click(object sender, RoutedEventArgs e)
        {
            PopUp.Project.New popup = new PopUp.Project.New();
            if (popup.ShowDialog() == true)
            {
                project = popup.Project;

                this.UpdateProject?.Invoke(this, new Event_ProjectUpdate() { Project = project });
           
            }


        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
