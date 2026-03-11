using System;
using System.Data;
using SID.Complex.Control.Model;
using System.Reflection;

namespace SID.Complex.Control.Tables
{
    public class Table_tblCmTotalizer : Table
    {
        public Table_tblCmTotalizer()
        {
            tableName = "tblCmTotalizer";
            name = "Totalizadores";

            tblCmTotalizer model = new tblCmTotalizer();
            field01 = GetColumnName(nameof(model.iID));
            field02 = GetColumnName(nameof(model.sName));
            field03 = GetColumnName(nameof(model.sDescription_1));
            field04 = GetColumnName(nameof(model.iIndexNo));
            field05 = GetColumnName(nameof(model.iControllerID));
            //field06 = GetColumnName(nameof(model.iID));
        }

        string GetColumnName(string name)
        {
            tblCmTotalizer model = new tblCmTotalizer();
            return ((MappingAttribute)model.GetType().GetProperty(name).GetCustomAttribute(typeof(MappingAttribute))).ColumnName;
        }
        public override object MapModel(DataTable dataTable)
        {
            tblCmTotalizer returnedObject = Function.MapToClass<tblCmTotalizer>(dataTable);
            return returnedObject;
        }

        public override int Update(object input)
        {
            tblCmTotalizer model = (tblCmTotalizer)input;
            string sqlString = Function.Update_String<tblCmTotalizer>(model, tableName, $"{field01} = {model.iID}");
            return complex.Connection.Update(sqlString);
        }

        public override int Insert(object input)
        {
            tblCmTotalizer model = (tblCmTotalizer)input;
            model.iID = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field01, ""))) + 1;
            model.iIndexNo = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field04, $"{field05}={model.iControllerID}"))) + 1;
            string sqlString = Function.Insert_String<tblCmTotalizer>((tblCmTotalizer)input, tableName);
            return complex.Connection.Insert(sqlString);
        }

        public override object NewModel()
        {
            tblCmTotalizer model = new tblCmTotalizer();
            model.iID = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field01, ""))) + 1;
            model.iIndexNo = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field04, $"{field05}={model.iControllerID}"))) + 1;
            return model;
        }

        public override object DuplicateModel(int id)
        {
            DataTable dt = SelectRow(id);
            tblCmTotalizer model = (tblCmTotalizer)MapModel(dt);
            model.iID = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field01, ""))) + 1;
            model.iIndexNo = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field04, $"{field05}={model.iControllerID}"))) + 1;
            return model;
        }

    }
}
