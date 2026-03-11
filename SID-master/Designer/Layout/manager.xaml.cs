using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SID.Standard.Control;
using SID.Designer.Control;
using SID.Designer.ToolBox;

namespace SID.Designer.Layout
{
    /// <summary>
    /// Interação lógica para manager.xam
    /// </summary>
    public partial class Manager : UserControl
    {
        Project project;
        DesignerControl designerControl;
 
        public void Add(Select_Manager select_Manager)
        {
            main.Items.Add(select_Manager);
        }

        public Manager()
        {
            InitializeComponent();
        }

        public Project Project
        {
            get => project;

            set
            {
                project = value;
                Info.Content = project.Name;
            }
        }

        public DesignerControl DesignerControl { get => designerControl; set => designerControl = value; }
    }
}
