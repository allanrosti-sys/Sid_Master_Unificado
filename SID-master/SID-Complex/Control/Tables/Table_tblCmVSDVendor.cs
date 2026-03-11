using System;
using System.Data;
using SID.Complex.Control.Model;
using System.Reflection;

namespace SID.Complex.Control.Tables
{
    public class Table_tblCmVSDVendor : Table
    {
        public Table_tblCmVSDVendor()
        {
            tableName = "tblCmVSDVendor";
            name = "Fornecedores VSD";

            tblCmVSDVendor model = new tblCmVSDVendor();
            field01 = GetColumnName(nameof(model.iID));
            field02 = GetColumnName(nameof(model.sName));
            field03 = GetColumnName(nameof(model.sPlcCtrlName));
            //field04 = GetColumnName(nameof(model.iIndexNo));
            //field05 = GetColumnName(nameof(model.iControllerID));
            //field06 = GetColumnName(nameof(model.iID));
        }

        string GetColumnName(string name)
        {
            tblCmVSDVendor model = new tblCmVSDVendor();
            return ((MappingAttribute)model.GetType().GetProperty(name).GetCustomAttribute(typeof(MappingAttribute))).ColumnName;
        }
        public override object MapModel(DataTable dataTable)
        {
            tblCmVSDVendor returnedObject = Function.MapToClass<tblCmVSDVendor>(dataTable);
            return returnedObject;
        }

        public override int Update(object input)
        {
            tblCmVSDVendor model = (tblCmVSDVendor)input;
            string sqlString = Function.Update_String<tblCmVSDVendor>(model, tableName, $"{field01} = {model.iID}");
            return complex.Connection.Update(sqlString);
        }

        public override int Insert(object input)
        {
            tblCmVSDVendor model = (tblCmVSDVendor)input;
            model.iID = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field01, ""))) + 1;
            //model.iIndexNo = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field04, $"{field05}={model.iControllerID}"))) + 1;
            string sqlString = Function.Insert_String<tblCmVSDVendor>((tblCmVSDVendor)input, tableName);
            return complex.Connection.Insert(sqlString);
        }

        public override object NewModel()
        {
            tblCmVSDVendor model = new tblCmVSDVendor();
            model.iID = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field01, ""))) + 1;
            //model.iIndexNo = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field04, $"{field05}={model.iControllerID}"))) + 1;
            return model;
        }

        public override object DuplicateModel(int id)
        {
            DataTable dt = SelectRow(id);
            tblCmVSDVendor model = (tblCmVSDVendor)MapModel(dt);
            model.iID = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field01, ""))) + 1;
            //model.iIndexNo = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field04, $"{field05}={model.iControllerID}"))) + 1;
            return model;
        }

    }
}
