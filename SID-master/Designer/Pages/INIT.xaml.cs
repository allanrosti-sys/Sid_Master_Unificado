using SID.Designer.Control;
using SID.Designer.Pages;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace SID.Designer.Pages
{
    /// <summary>
    /// Interação lógica para INIT.xam
    /// </summary>
    public partial class INIT : UserControl
    {
        public INIT()
        {
            InitializeComponent();
        }
    }

    public class INITViewer : View_Control
    {
        public INITViewer() : base(new INIT(), "-BEM VINDO-")
        {
        }
    }
}
