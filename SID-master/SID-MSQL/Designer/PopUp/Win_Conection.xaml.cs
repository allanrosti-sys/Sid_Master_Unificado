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
using System.Windows.Shapes;
using System.Data.SqlClient;
using SID.MSQL.Control;

namespace SID.MSQL.Designer.PopUp
{
    /// <summary>
    /// Lógica interna para Win_Conection.xaml
    /// </summary>
    public partial class Win_Connection : Window
    {
        public MSQL_Info mSQL_Connection;


        public Win_Connection(MSQL_Info mSQL_Connection)
        {
            InitializeComponent();
            this.mSQL_Connection = mSQL_Connection;

            tb_DataSource.Text = this.mSQL_Connection.DataSource;
            tb_Database.Text = this.mSQL_Connection.Database;
            tb_User.Text = this.mSQL_Connection.User;
            tb_Password.Password = this.mSQL_Connection.Password;
        }
        public Win_Connection()
        {
            InitializeComponent();

            this.mSQL_Connection = new MSQL_Info();
        }

        private void Btn_Test_Click(object sender, RoutedEventArgs e)
        {
            string string_Conn = $"Data Source={tb_DataSource.Text};Initial Catalog = {tb_Database.Text}; User ID = {tb_User.Text}; Password = {tb_Password.Password}";
            SqlConnection Connection = new SqlConnection(string_Conn);
            try
            {
                Connection.Open();
                MessageBox.Show("Conexão OK!");
            }
            catch (Exception)
            {
                MessageBox.Show("Falha na conexão!");
            }

        }

        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;

        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            mSQL_Connection.DataSource = tb_DataSource.Text;
            mSQL_Connection.Database = tb_Database.Text;
            mSQL_Connection.User = tb_User.Text;
            mSQL_Connection.Password = tb_Password.Password;
            DialogResult = true;
        }
    }
}
