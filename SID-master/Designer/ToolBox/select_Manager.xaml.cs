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

namespace SID.Designer.ToolBox
{
    /// <summary>
    /// Interação lógica para select_Manager.xam
    /// </summary>
    public partial class Select_Manager : TreeViewItem
    {
        string title;

        public Select_Manager()
        {
            InitializeComponent();

        }

        public string Title
        {
            get => title;


            set
            {
                title = value;
                this.Header = title;
            }

        }
    }
}
