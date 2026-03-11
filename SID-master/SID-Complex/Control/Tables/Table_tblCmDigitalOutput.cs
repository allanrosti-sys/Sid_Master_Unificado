using System;
using System.Data;
using SID.Complex.Control.Model;
using System.Reflection;

namespace SID.Complex.Control.Tables
{
    public class Table_tblCmDigitalOutput : Table
    {
        public Table_tblCmDigitalOutput()
        {
            tableName = "tblCmDigitalOutput";
            name = "Saidas Digitais";

            tblCmDigitalOutput model = new tblCmDigitalOutput();
            field01 = GetColumnName(nameof(model.iID));
            field02 = GetColumnName(nameof(model.sName));
            field03 = GetColumnName(nameof(model.sDescription_1));
            field04 = GetColumnName(nameof(model.iIndexNo));
            field05 = GetColumnName(nameof(model.iControllerID));
            //field06 = GetColumnName(nameof(model.iID));
        }

        string GetColumnName(string name)
        {
            tblCmDigitalOutput model = new tblCmDigitalOutput();
            return ((MappingAttribute)model.GetType().GetProperty(name).GetCustomAttribute(typeof(MappingAttribute))).ColumnName;
        }
        public override object MapModel(DataTable dataTable)
        {
            tblCmDigitalOutput returnedObject = Function.MapToClass<tblCmDigitalOutput>(dataTable);
            return returnedObject;
        }

        public override int Update(object input)
        {
            tblCmDigitalOutput model = (tblCmDigitalOutput)input;
            string sqlString = Function.Update_String<tblCmDigitalOutput>(model, tableName, $"{field01} = {model.iID}");
            return complex.Connection.Update(sqlString);
        }

        public override int Insert(object input)
        {
            tblCmDigitalOutput model = (tblCmDigitalOutput)input;
            model.iID = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field01, ""))) + 1;
            model.iIndexNo = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field04, $"{field05}={model.iControllerID}"))) + 1;
            string sqlString = Function.Insert_String<tblCmDigitalOutput>((tblCmDigitalOutput)input, tableName);
            return complex.Connection.Insert(sqlString);
        }

        public override object NewModel()
        {
            tblCmDigitalOutput model = new tblCmDigitalOutput();
            model.iID = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field01, ""))) + 1;
            model.iIndexNo = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field04, $"{field05}={model.iControllerID}"))) + 1;
            return model;
        }

        public override object DuplicateModel(int id)
        {
            DataTable dt = SelectRow(id);
            tblCmDigitalOutput model = (tblCmDigitalOutput)MapModel(dt);
            model.iID = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field01, ""))) + 1;
            model.iIndexNo = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field04, $"{field05}={model.iControllerID}"))) + 1;
            return model;
        }

    }
}
