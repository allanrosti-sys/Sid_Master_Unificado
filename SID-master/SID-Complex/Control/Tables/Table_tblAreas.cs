using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SID.Complex.Control;
using System.Data;
using SID.Complex.Control.Model;


namespace SID.Complex.Control.Tables
{
    public class Table_tblAreas : Table
    {
        public Table_tblAreas()
        {
            tableName = "tblAreas";
            name = "Areas";
        }

        public override object MapModel(DataTable dataTable)
        {
            tblAreas returnedObject = Function.MapToClass<tblAreas>(dataTable); ;
            return returnedObject;
        }

        public override DataTable Select()
        {
            string sqlStringViewer = $"Select iId,sName_1,sName_2 FROM {tableName} order by iId;";
            return complex.Connection.Select(sqlStringViewer);
        }

        public override DataTable SelectRow(int id)
        {
            string sqlSelectRow = $"SELECT * FROM {tableName} WHERE iID = {id};";
            return complex.Connection.SelectRow(sqlSelectRow);
        }

        public override DataTable SelectView()
        {
            string sqlStringViewer = $"Select iId,sName_1 FROM {tableName} order by iId;";
            return complex.Connection.Select(sqlStringViewer);
        }

        public override int Update(object input)
        {
            tblAreas model = (tblAreas)input;
            string sqlString = Function.Update_String<tblAreas>(model, tableName, $"iID = {model.iID}");
            return complex.Connection.Update(sqlString);
        }

        public override int Insert(object input)
        {
            tblAreas model = (tblAreas)input;
            model.iID = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, "iID", ""))) + 1;
            string sqlString = Function.Insert_String<tblAreas>((tblAreas)input, tableName);
            return complex.Connection.Insert(sqlString);
        }

        public override int DeleteRow(int id)
        {
            string sqlString = $"DELETE FROM {tableName} WHERE iID = {id};";
            return complex.Connection.Delete(sqlString);
        }

        public override object NewModel()
        {
            tblAreas model = new tblAreas();
            model.iID = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, "iID", ""))) + 1;
            return model;
        }

        public override object DuplicateModel(int id)
        {
            DataTable dt = SelectRow(id);
            tblAreas model = (tblAreas)MapModel(dt);
            model.iID = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, "iID", ""))) + 1;
            return model;
        }

    }
}
