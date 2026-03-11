using System;
using System.Data;
using SID.Complex.Control.Model;
using System.Reflection;

namespace SID.Complex.Control.Tables
{
    public class Table_tblControllers : Table
    {
        public Table_tblControllers()
        {
            tableName = "tblControllers";
            name = "Controladores";

            tblControllers model = new tblControllers();
            field01 = GetColumnName(nameof(model.iID));
            field02 = GetColumnName(nameof(model.sName));
            field03 = GetColumnName(nameof(model.sDescription_1));
            field04 = GetColumnName(nameof(model.iIndexNo));
            field05 = GetColumnName(nameof(model.iControllerTypeID));
            //field06 = GetColumnName(nameof(model.iID));
        }

        string GetColumnName(string name)
        {
            tblControllers model = new tblControllers();
            return ((MappingAttribute)model.GetType().GetProperty(name).GetCustomAttribute(typeof(MappingAttribute))).ColumnName;
        }
        public override object MapModel(DataTable dataTable)
        {
            tblControllers returnedObject = Function.MapToClass<tblControllers>(dataTable);
            return returnedObject;
        }

        public override int Update(object input)
        {
            tblControllers model = (tblControllers)input;
            string sqlString = Function.Update_String<tblControllers>(model, tableName, $"{field01} = {model.iID}");
            return complex.Connection.Update(sqlString);
        }

        public override int Insert(object input)
        {
            tblControllers model = (tblControllers)input;
            model.iID = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field01, ""))) + 1;
            model.iIndexNo = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field04, $"{field05}={model.iControllerTypeID}"))) + 1;
            string sqlString = Function.Insert_String<tblControllers>((tblControllers)input, tableName);
            return complex.Connection.Insert(sqlString);
        }

        public override object NewModel()
        {
            tblControllers model = new tblControllers();
            model.iID = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field01, ""))) + 1;
            model.iIndexNo = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field04, $"{field05}={model.iControllerTypeID}"))) + 1;
            return model;
        }

        public override object DuplicateModel(int id)
        {
            DataTable dt = SelectRow(id);
            tblControllers model = (tblControllers)MapModel(dt);
            model.iID = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field01, ""))) + 1;
            model.iIndexNo = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field04, $"{field05}={model.iControllerTypeID}"))) + 1;
            return model;
        }

    }
}
