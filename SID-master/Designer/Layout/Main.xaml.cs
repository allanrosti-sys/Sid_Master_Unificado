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
using SID.Designer.Control;

namespace SID.Designer.Layout
{
    /// <summary>
    /// Interação lógica para Main.xam
    /// </summary>
    public partial class Main : UserControl
    {
        public DesignerControl DesignerControl { get; set; }

        public Main()
        {
            InitializeComponent();
        }

        public void SetViewer(View_Control view_Control)
        {
            MainViewer.Children.Clear();
            MainViewer.Children.Add(view_Control.View);
            Header.Content = view_Control.Name;
        }
    }
}
