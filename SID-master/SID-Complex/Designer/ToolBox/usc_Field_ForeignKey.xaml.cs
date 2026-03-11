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
    public partial class usc_Field_ForeignKey : UserControl
    {
        int value;
        PropertyInfo propertyInfo;

        Ctrl_Complex complex;

        public event EventHandler<Event_UpdateValue> UpdateValue;

        public usc_Field_ForeignKey(Ctrl_Complex complex, PropertyInfo propertyInfo, int value)
        {
            InitializeComponent();
            this.value = value;
            this.complex = complex;
            this.propertyInfo = propertyInfo;
            INIT();
        }

        void INIT()
        {
            MappingAttribute myAttribute = (MappingAttribute)(propertyInfo.GetCustomAttributes(typeof(MappingAttribute), true))[0];
            lb_Name.Content = propertyInfo.Name;
            cb_Value.DataContext = complex.GetForeignTable(myAttribute.ForeignTable).DefaultView;
            cb_Value.SelectedValue = value;

            this.DataContext = this;
        }

        private void Cb_Value_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Convert.ToInt32(cb_Value.SelectedValue) != 0)
            {
                value = Convert.ToInt32(cb_Value.SelectedValue);
                this.UpdateValue?.Invoke(this, new Event_UpdateValue() { PropertyInfo = propertyInfo, Value = value });

            }
        }
    }
}

