using SID.Designer.Control;
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

namespace SID.TPM_ROCKWELL.Designer
{
    /// <summary>
    /// Interação lógica para ConfigView.xam
    /// </summary>
    public partial class usc_ConfigView : UserControl
    {
        public usc_ConfigView()
        {
            InitializeComponent();
        }
    }

    public class ConfigView : View_Control
    {
        public ConfigView() : base(new usc_ConfigView(),"Configuração")
        {
        }
    }
}
