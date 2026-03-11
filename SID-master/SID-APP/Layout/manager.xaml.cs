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

namespace SID_APP.Layout
{
    /// <summary>
    /// Interação lógica para manager.xam
    /// </summary>
    public partial class manager : UserControl
    {
        Project project;
        public manager()
        {
            InitializeComponent();
        }

        public Project Project
        {
            get => project;

            set
            {
                project = value;
                Label1.Content = project.Name;
            }
        }
    }
}
