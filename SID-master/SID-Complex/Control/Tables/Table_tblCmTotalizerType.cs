using System;
using System.Data;
using SID.Complex.Control.Model;
using System.Reflection;

namespace SID.Complex.Control.Tables
{
    public class Table_tblCmTotalizerType : Table
    {
        public Table_tblCmTotalizerType()
        {
            tableName = "tblCmTotalizerType";
            name = "Tipo de Totalizadores";

            tblCmTotalizerType model = new tblCmTotalizerType();
            field01 = GetColumnName(nameof(model.iID));
            field02 = GetColumnName(nameof(model.sName_1));
            field03 = GetColumnName(nameof(model.sName_2));
            //field04 = GetColumnName(nameof(model.iIndexNo));
            //field05 = GetColumnName(nameof(model.iControllerID));
            //field06 = GetColumnName(nameof(model.iID));
        }

        string GetColumnName(string name)
        {
            tblCmTotalizerType model = new tblCmTotalizerType();
            return ((MappingAttribute)model.GetType().GetProperty(name).GetCustomAttribute(typeof(MappingAttribute))).ColumnName;
        }
        public override object MapModel(DataTable dataTable)
        {
            tblCmTotalizerType returnedObject = Function.MapToClass<tblCmTotalizerType>(dataTable);
            return returnedObject;
        }

        public override int Update(object input)
        {
            tblCmTotalizerType model = (tblCmTotalizerType)input;
            string sqlString = Function.Update_String<tblCmTotalizerType>(model, tableName, $"{field01} = {model.iID}");
            return complex.Connection.Update(sqlString);
        }

        public override int Insert(object input)
        {
            tblCmTotalizerType model = (tblCmTotalizerType)input;
            model.iID = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field01, ""))) + 1;
            //model.iIndexNo = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field04, $"{field05}={model.iControllerID}"))) + 1;
            string sqlString = Function.Insert_String<tblCmTotalizerType>((tblCmTotalizerType)input, tableName);
            return complex.Connection.Insert(sqlString);
        }

        public override object NewModel()
        {
            tblCmTotalizerType model = new tblCmTotalizerType();
            model.iID = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field01, ""))) + 1;
            //model.iIndexNo = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field04, $"{field05}={model.iControllerID}"))) + 1;
            return model;
        }

        public override object DuplicateModel(int id)
        {
            DataTable dt = SelectRow(id);
            tblCmTotalizerType model = (tblCmTotalizerType)MapModel(dt);
            model.iID = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field01, ""))) + 1;
            //model.iIndexNo = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field04, $"{field05}={model.iControllerID}"))) + 1;
            return model;
        }

    }
}
