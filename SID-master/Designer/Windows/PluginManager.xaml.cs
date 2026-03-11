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

namespace SID.Designer.Windows
{
    /// <summary>
    /// Interação lógica para PluginManager.xam
    /// </summary>
    public partial class usc_PluginManager : UserControl
    {
        public usc_PluginManager()
        {
            InitializeComponent();
        }
    }

    public class PluginManager : View_Control
    {
        
        public PluginManager() : base(new usc_PluginManager(), "Gerenciador de extensões")
        {
        }
    }

}
