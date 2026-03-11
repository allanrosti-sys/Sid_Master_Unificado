using System;
using System.Data;
using SID.Complex.Control.Model;
using System.Reflection;

namespace SID.Complex.Control.Tables
{
    public class Table_tblCabinets : Table
    {
        public Table_tblCabinets()
        {
            tableName = "tblCabinets";
            name = "Painéis";

            tblCabinets model = new tblCabinets();
            field01 = GetColumnName(nameof(model.iID));
            field02 = GetColumnName(nameof(model.sName));
            //field03 = GetColumnName(nameof(model.sName_2));
            //field04 = GetColumnName(nameof(model.iIndexNo));
            //field05 = GetColumnName(nameof(model.iControllerID));
            //field06 = GetColumnName(nameof(model.iID));
        }

        string GetColumnName(string name)
        {
            tblCabinets model = new tblCabinets();
            return ((MappingAttribute)model.GetType().GetProperty(name).GetCustomAttribute(typeof(MappingAttribute))).ColumnName;
        }
        public override object MapModel(DataTable dataTable)
        {
            tblCabinets returnedObject = Function.MapToClass<tblCabinets>(dataTable);
            return returnedObject;
        }

        public override int Update(object input)
        {
            tblCabinets model = (tblCabinets)input;
            string sqlString = Function.Update_String<tblCabinets>(model, tableName, $"{field01} = {model.iID}");
            return complex.Connection.Update(sqlString);
        }

        public override int Insert(object input)
        {
            tblCabinets model = (tblCabinets)input;
            model.iID = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field01, ""))) + 1;
            //model.iIndexNo = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field04, $"{field05}={model.iControllerID}"))) + 1;
            string sqlString = Function.Insert_String<tblCabinets>((tblCabinets)input, tableName);
            return complex.Connection.Insert(sqlString);
        }

        public override object NewModel()
        {
            tblCabinets model = new tblCabinets();
            model.iID = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field01, ""))) + 1;
            //model.iIndexNo = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field04, $"{field05}={model.iControllerID}"))) + 1;
            return model;
        }

        public override object DuplicateModel(int id)
        {
            DataTable dt = SelectRow(id);
            tblCabinets model = (tblCabinets)MapModel(dt);
            model.iID = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field01, ""))) + 1;
            //model.iIndexNo = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field04, $"{field05}={model.iControllerID}"))) + 1;
            return model;
        }

    }
}
