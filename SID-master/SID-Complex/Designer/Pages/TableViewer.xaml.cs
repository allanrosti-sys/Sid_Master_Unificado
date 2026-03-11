using System;
using System.Windows;
using System.Windows.Controls;
using SID.Designer.Control;
using System.Data;
using SID.Complex.Designer.PopUp;
using SID.Standard.Control;
using SID.Complex.Control;
using SID.Complex.Control.Model;

namespace SID.Complex.Designer.Pages
{
    /// <summary>
    /// Interação lógica para ControlModulesViewer.xam
    /// </summary>
    public partial class usc_TableViewer : UserControl
    {
        Table table;

        public usc_TableViewer(Table table)
        {
            InitializeComponent();
            this.table = table;

            INIT();
        }

        void INIT()
        {
            btn_Edit.IsEnabled = false;
            btn_Duplicate.IsEnabled = false;
            btn_Delete.IsEnabled = false;
            DataTable dt = table.Select();
            datagrid.DataContext = dt;
        }


        private void Btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            DataRowView drv = (DataRowView)datagrid.SelectedItem;
            int id = Convert.ToInt32(drv.Row[0]);
            DataTable dataTable = table.SelectRow(id);
            win_EditReg win_CM = new win_EditReg(table.Complex, table.MapModel(dataTable));
            win_CM.Title = table.Name;
            if ((bool)win_CM.ShowDialog())
            {
                int rowCount = table.Update(win_CM.Input);
                MessageBox.Show($"Foram afetadas {rowCount} linha`s");
                if (rowCount > 0)
                {
                    INIT();
                }
            }

        }

        private void Datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((DataRowView)datagrid.SelectedItem != null)
            {
                btn_Edit.IsEnabled = true;
                btn_Duplicate.IsEnabled = true;
                btn_Delete.IsEnabled = true;
            }
        }

        private void Btn_New_Click(object sender, RoutedEventArgs e)
        {
            win_EditReg win_CM = new win_EditReg(table.Complex, table.NewModel());
            win_CM.Title = table.Name;
            if ((bool)win_CM.ShowDialog())
            {
                object input = win_CM.Input;
                int rowCount = table.Insert(input);
                MessageBox.Show($"Foram afetadas {rowCount} linha`s");
                if (rowCount > 0)
                {
                    INIT();
                }
            }
        }

        private void Btn_Duplicate_Click(object sender, RoutedEventArgs e)
        {
            DataRowView drv = (DataRowView)datagrid.SelectedItem;
            int id = Convert.ToInt32(drv.Row[0]);
            win_EditReg win_CM = new win_EditReg(table.Complex, table.DuplicateModel(id));
            win_CM.Title = table.Name;

            if ((bool)win_CM.ShowDialog())
            {
                object input = win_CM.Input;
                int rowCount = table.Insert(input);
                MessageBox.Show($"Foram afetadas {rowCount} linha`s");
                if (rowCount > 0)
                {
                    INIT();
                }
            }
        }

        private void Btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja DELETAR o registro?", "Atenção", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
            {
                DataRowView drv = (DataRowView)datagrid.SelectedItem;
                int id = Convert.ToInt32(drv.Row[0]);
                int rowCount = table.DeleteRow(id);
                MessageBox.Show($"Foram deletado(s) {rowCount} registro(s)");
                if (rowCount > 0)
                {
                    INIT();
                }
            }
        }
    }

    public class TableViewer : View_Control
    {
        public TableViewer(Table table) : base(new usc_TableViewer(table), table.Name)
        {
        }
    }
}
