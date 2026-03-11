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
using System.Text.RegularExpressions;
using SID.Complex.Control;
using System.Reflection;

namespace SID.Complex.Designer.ToolBox
{
    /// <summary>
    /// Interação lógica para usc_Field_Integer.xam
    /// </summary>
    public partial class usc_Field_Boolean : UserControl
    {
        bool value;
        PropertyInfo propertyInfo;

        public event EventHandler<Event_UpdateValue> UpdateValue;

        public usc_Field_Boolean(PropertyInfo propertyInfo, bool value)
        {
            InitializeComponent();
            this.value = value;
            this.propertyInfo = propertyInfo;
            INIT();
        }

        void INIT()
        {
            this.DataContext = this;
            lb_Name.Content = propertyInfo.Name;
            cbx_Value.IsChecked = value;

        }

        private void Cbx_Value_Checked(object sender, RoutedEventArgs e)
        {
            value = (bool)cbx_Value.IsChecked;
            this.UpdateValue?.Invoke(this, new Event_UpdateValue() { PropertyInfo = propertyInfo, Value = value });
        }
    }
}

