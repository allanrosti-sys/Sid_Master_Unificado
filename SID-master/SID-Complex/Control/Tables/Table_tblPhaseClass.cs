using System;
using System.Data;
using SID.Complex.Control.Model;
using System.Reflection;

namespace SID.Complex.Control.Tables
{
    public class Table_tblPhaseClass : Table
    {
        public Table_tblPhaseClass()
        {
            tableName = "tblPhaseClass";
            name = "Classes Fases";

            tblPhaseClass model = new tblPhaseClass();
            field01 = GetColumnName(nameof(model.iID));
            field02 = GetColumnName(nameof(model.sName_1));
            field03 = GetColumnName(nameof(model.sName_2));
            //field04 = GetColumnName(nameof(model.iIndexNo));
            field05 = GetColumnName(nameof(model.iControllerID));
            //field06 = GetColumnName(nameof(model.iID));
        }

        string GetColumnName(string name)
        {
            tblPhaseClass model = new tblPhaseClass();
            return ((MappingAttribute)model.GetType().GetProperty(name).GetCustomAttribute(typeof(MappingAttribute))).ColumnName;
        }
        public override object MapModel(DataTable dataTable)
        {
            tblPhaseClass returnedObject = Function.MapToClass<tblPhaseClass>(dataTable);
            return returnedObject;
        }

        public override int Update(object input)
        {
            tblPhaseClass model = (tblPhaseClass)input;
            string sqlString = Function.Update_String<tblPhaseClass>(model, tableName, $"{field01} = {model.iID}");
            return complex.Connection.Update(sqlString);
        }

        public override int Insert(object input)
        {
            tblPhaseClass model = (tblPhaseClass)input;
            model.iID = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field01, ""))) + 1;
            //model.iIndexNo = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field04, $"{field05}={model.iControllerID}"))) + 1;
            string sqlString = Function.Insert_String<tblPhaseClass>((tblPhaseClass)input, tableName);
            return complex.Connection.Insert(sqlString);
        }

        public override object NewModel()
        {
            tblPhaseClass model = new tblPhaseClass();
            model.iID = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field01, ""))) + 1;
            //model.iIndexNo = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field04, $"{field05}={model.iControllerID}"))) + 1;
            return model;
        }

        public override object DuplicateModel(int id)
        {
            DataTable dt = SelectRow(id);
            tblPhaseClass model = (tblPhaseClass)MapModel(dt);
            model.iID = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field01, ""))) + 1;
            //model.iIndexNo = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field04, $"{field05}={model.iControllerID}"))) + 1;
            return model;
        }

    }
}
