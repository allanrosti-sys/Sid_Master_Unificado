using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using SID.Complex.Control.Model;
using SID.Complex.Control;
using System.Reflection;
using SID.Complex.Designer.ToolBox;


namespace SID.Complex.Designer.PopUp
{
    /// <summary>
    /// Lógica interna para win_CM.xaml
    /// </summary>
    public partial class win_EditReg : Window
    {
        object input;
        Ctrl_Complex complex;
        

        public object Input { get => input; set => input = value; }

        public win_EditReg(Ctrl_Complex complex, object input)
        {
            InitializeComponent();
            this.complex = complex;
            this.input = input;
            INIT();
        }

       

        void INIT()
        {
            List<PropertyInfo> modelProperties = input.GetType().GetProperties().OrderBy(p => p.MetadataToken).ToList();
            foreach (PropertyInfo propertyInfo in modelProperties)
            {
                string propertyName = propertyInfo.Name;

                object[] attribute = propertyInfo.GetCustomAttributes(typeof(MappingAttribute), true);
                // Just in case you have a property without this annotation
                if (attribute.Length > 0)
                {
                    MappingAttribute myAttribute = (MappingAttribute)attribute[0];
                    InsertObject(myAttribute.Type, propertyInfo, myAttribute.Constant);

                }
            }

        }

        void InsertObject(MappingType mp, PropertyInfo property, bool constant)
        {
            switch (mp)
            {
                case MappingType.Null:
                    {

                        break;
                    }
                case MappingType.PrimaryKey:
                    {
                        usc_Field_PrimaryKey usc_Field = new usc_Field_PrimaryKey(property, Convert.ToInt32(property.GetValue(input)));
                        usc_Field.UpdateValue += Field_UpdateValue;
                        main.Children.Add(usc_Field);
                        break;
                    }
                case MappingType.Integer:
                    {
                        usc_Field_Integer usc_Field = new usc_Field_Integer(property, Convert.ToInt32(property.GetValue(input)), constant);
                        usc_Field.UpdateValue += Field_UpdateValue;
                        main.Children.Add(usc_Field);
                        break;
                    }
                case MappingType.Real:
                    {
                        usc_Field_Real usc_Field = new usc_Field_Real(property, float.Parse(property.GetValue(input).ToString()));
                        usc_Field.UpdateValue += Field_UpdateValue;
                        main.Children.Add(usc_Field);
                        break;
                    }
                case MappingType.Text:
                    {
                        if (property.GetValue(input) == null)
                            property.SetValue(input, "");
                        usc_Field_Text usc_Field = new usc_Field_Text(property, property.GetValue(input).ToString());
                        usc_Field.UpdateValue += Field_UpdateValue;
                        main.Children.Add(usc_Field);
                        break;
                    }
                case MappingType.Boolean:
                    {
                        usc_Field_Boolean usc_Field = new usc_Field_Boolean(property, (bool)property.GetValue(input));
                        usc_Field.UpdateValue += Field_UpdateValue;
                        main.Children.Add(usc_Field);
                        break;
                    }
                case MappingType.ForeignKey:
                    {
                        usc_Field_ForeignKey usc_Field = new usc_Field_ForeignKey(complex, property, Convert.ToInt32(property.GetValue(input)));
                        usc_Field.UpdateValue += Field_UpdateValue;
                        main.Children.Add(usc_Field);
                        break;
                    }
                default:
                    {
                        Label lb = new Label();
                        //lb.Content = name;
                        main.Children.Add(lb);
                        break;
                    }
            }
        }


        private void Field_UpdateValue(object sender, Event_UpdateValue e)
        {
            e.PropertyInfo.SetValue(input, e.Value);
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja atualizar o registro?", "Atenção", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
            {
                string retField = FieldsCheck();
                if (retField == "")
                {
                    this.DialogResult = true;
                }
                else
                {
                    MessageBox.Show($"Campos {retField} está vazio ou não selecionado!");
                }
            }
        }

        string FieldsCheck()
        {
            foreach (PropertyInfo propertyInfo in input.GetType().GetProperties().OrderBy(p => p.MetadataToken).ToList())
            {
                object[] attribute = propertyInfo.GetCustomAttributes(typeof(MappingAttribute), true);
                // Just in case you have a property without this annotation
                if (attribute.Length > 0)
                {
                    MappingAttribute myAttribute = (MappingAttribute)attribute[0];
                    switch (myAttribute.Type)
                    {
                        case MappingType.Null:
                            break;
                        case MappingType.PrimaryKey:
                            //if (Convert.ToInt32(propertyInfo.GetValue(input)) == 0)
                            //{
                            //    return false;
                            //}
                            break;
                        case MappingType.Integer:
                            break;
                        case MappingType.Real:
                            break;
                        case MappingType.Text:
                            if (myAttribute.NotNull)
                            {
                                if (propertyInfo.GetValue(input).ToString() == "")
                                {
                                    return myAttribute.ColumnName;
                                }
                            }
                            break;
                        case MappingType.Boolean:
                            break;
                        case MappingType.ForeignKey:
                            if (myAttribute.NotNull)
                            {
                                if (Convert.ToInt32(propertyInfo.GetValue(input)) == 0)
                                {
                                    return myAttribute.ColumnName;
                                }
                            }
                            break;
                        default:
                            break;
                    }

                }
            }
            return "";
        }
    }
}
