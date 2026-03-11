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
using System.Data;
using SID.Designer.Control;

namespace SID.Designer.Pages
{
    /// <summary>
    /// Interação lógica para DataTableViewer.xam
    /// </summary>
    public partial class usc_DataTableViewer : UserControl
    {
        public usc_DataTableViewer(DataTable dataTable)
        {
            InitializeComponent();
            datagrid.DataContext = dataTable;
        }




    }


    public class DataTableViewer : View_Control
    {
        public DataTableViewer(DataTable dataTable, string info) : base(new usc_DataTableViewer(dataTable), "Tabela: " + info)
        {
        }
    }


}
