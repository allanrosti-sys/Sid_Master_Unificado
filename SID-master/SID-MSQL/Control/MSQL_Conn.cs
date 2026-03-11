using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Controls.Primitives;
using System.Data.Common;
using System.Windows.Controls;
using System.Windows;

using SID.Standard.Control;

namespace SID.MSQL.Control
{
    public class MSQL_Conn : Connection
    {

        public string String_Conn { get; set; }
        SqlConnection Connection { get; set; }

        public MSQL_Conn(string string_Conn)
        {
            String_Conn = string_Conn;
            Connection = new SqlConnection(string_Conn);
        }



        public bool Conn()
        {
            try
            {
                Connection.Open();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override DataTable Select(string string_CMD)
        {
            Connection.Open();

            SqlCommand command = new SqlCommand(string_CMD, Connection);

            DbDataReader reader = command.ExecuteReader();


            DataTable tbEsquema = reader.GetSchemaTable();
            DataTable tbRetorno = new DataTable();

            foreach (DataRow r in tbEsquema.Rows)
            {
                if (!tbRetorno.Columns.Contains(r["ColumnName"].ToString()))
                {
                    DataColumn col = new DataColumn()
                    {
                        ColumnName = r["ColumnName"].ToString(),
                        Unique = Convert.ToBoolean(r["IsUnique"]),
                        AllowDBNull = Convert.ToBoolean(r["AllowDBNull"]),
                        ReadOnly = Convert.ToBoolean(r["IsReadOnly"])
                    };
                    tbRetorno.Columns.Add(col);
                }
            }

            while (reader.Read())
            {
                DataRow novaLinha = tbRetorno.NewRow();
                for (int i = 0; i < tbRetorno.Columns.Count; i++)
                {
                    novaLinha[i] = reader.GetValue(i);
                }
                tbRetorno.Rows.Add(novaLinha);
            }
            Connection.Close();

            return tbRetorno;
        }

        public override object SelectScalar(string string_CMD)
        {
            object ScalarReturn;
            Connection.Open();

            SqlCommand command = new SqlCommand(string_CMD, Connection);

            ScalarReturn = command.ExecuteScalar();
            Connection.Close();

            return ScalarReturn;
        }

        public override DataTable SelectRow(string string_CMD)
        {
            Connection.Open();

            SqlCommand command = new SqlCommand(string_CMD, Connection);

            DbDataReader reader = command.ExecuteReader();


            DataTable tbEsquema = reader.GetSchemaTable();
            DataTable tbRetorno = new DataTable();

            foreach (DataRow r in tbEsquema.Rows)
            {
                if (!tbRetorno.Columns.Contains(r["ColumnName"].ToString()))
                {
                    DataColumn col = new DataColumn()
                    {
                        ColumnName = r["ColumnName"].ToString(),
                        Unique = Convert.ToBoolean(r["IsUnique"]),
                        AllowDBNull = Convert.ToBoolean(r["AllowDBNull"]),
                        ReadOnly = Convert.ToBoolean(r["IsReadOnly"])
                    };
                    tbRetorno.Columns.Add(col);
                }
            }

            reader.Read();
            DataRow novaLinha = tbRetorno.NewRow();
            for (int i = 0; i < tbRetorno.Columns.Count; i++)
            {
                novaLinha[i] = reader.GetValue(i);
            }
            tbRetorno.Rows.Add(novaLinha);

            Connection.Close();

            return tbRetorno;
            
        }

        public override int Insert(string string_CMD)
        {
            Connection.Open();

            SqlCommand command = new SqlCommand(string_CMD, Connection);

            int rowCount = command.ExecuteNonQuery();
            Connection.Close();

            return rowCount;
        }

        public override int Update(string string_CMD)
        {
            Connection.Open();

            SqlCommand command = new SqlCommand(string_CMD, Connection);

            int rowCount = command.ExecuteNonQuery();
            Connection.Close();

            return rowCount;
        }

        public override int Delete(string string_CMD)
        {
            Connection.Open();

            SqlCommand command = new SqlCommand(string_CMD, Connection);

            int rowCount = command.ExecuteNonQuery();
            Connection.Close();

            return rowCount;
        }
    }
}

