using Microsoft.Win32;
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
using System.Windows.Shapes;
using SID_Standard.Controls;

namespace SID_APP.PopUp.Project
{
    /// <summary>
    /// Lógica interna para New.xaml
    /// </summary>
    public partial class New : Window
    {

        SID_Standard.Controls.Project project;

        public New()
        {
            InitializeComponent();
        }

        public SID_Standard.Controls.Project Project { get => project;  }

        private void PathFile_Click(object sender, MouseButtonEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Arquivo de projeto (*.sid)|*.sid";
            if (saveFileDialog.ShowDialog() == true)
                tb_Path.Text = saveFileDialog.FileName;
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Name.Text == "" || tb_Path.Text == "")
            {
                if(tb_Name.Text == "")
                {
                    tb_Name.Background = (Brush)new BrushConverter().ConvertFrom("#D33E43");
                }
                if (tb_Path.Text == "")
                {
                    tb_Path.Background = (Brush)new BrushConverter().ConvertFrom("#D33E43");
                }
            }
            else
            {
                project = new SID_Standard.Controls.Project(tb_Name.Text, tb_Description.Text, tb_Path.Text, DateTime.Now, DateTime.Now);
                project.Save();
                this.DialogResult = true;
            }
        }

        
    }
}
