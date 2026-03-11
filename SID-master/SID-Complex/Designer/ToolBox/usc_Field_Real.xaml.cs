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
    public partial class usc_Field_Real : UserControl
    {
        float value;
        PropertyInfo propertyInfo;

        public event EventHandler<Event_UpdateValue> UpdateValue;

        public usc_Field_Real(PropertyInfo propertyInfo, float value)
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
            tb_Value.Text = value.ToString();

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[0-9]([.,][0-9]{1,3})?$");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Tb_Value_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tb_Value.Text != "")
            {
                value = float.Parse(tb_Value.Text);
                this.UpdateValue?.Invoke(this, new Event_UpdateValue() { PropertyInfo = propertyInfo, Value = value });

            }
        }
    }
}

