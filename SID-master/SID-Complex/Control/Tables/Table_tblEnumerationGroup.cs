using System;
using System.Data;
using SID.Complex.Control.Model;
using System.Reflection;

namespace SID.Complex.Control.Tables
{
    public class Table_tblEnumerationGroup : Table
    {
            public Table_tblEnumerationGroup()
        {
                tableName = "tblEnumerationGroup";
                name = "Grupo Enumerado";

              tblEnumerationGroup model = new tblEnumerationGroup();
                field01 = GetColumnName(nameof(model.iID));
                field02 = GetColumnName(nameof(model.sName_1));
                field03 = GetColumnName(nameof(model.sName_2));
                //field04 = GetColumnName(nameof(model.iIndexNo));
                //field05 = GetColumnName(nameof(model.iControllerID));
                //field06 = GetColumnName(nameof(model.iID));
        }

                string GetColumnName(string name)
                {
                    tblEnumerationGroup model = new tblEnumerationGroup();
                    return ((MappingAttribute)model.GetType().GetProperty(name).GetCustomAttribute(typeof(MappingAttribute))).ColumnName;
            } 
        public override object MapModel(DataTable dataTable)
        {
            tblEnumerationGroup returnedObject = Function.MapToClass<tblEnumerationGroup>(dataTable);
            return returnedObject;
        }

        public override int Update(object input)
        {
            tblEnumerationGroup model = (tblEnumerationGroup)input;
            string sqlString = Function.Update_String<tblEnumerationGroup>(model, tableName, $"{field01} = {model.iID}");
            return complex.Connection.Update(sqlString);
        }

        public override int Insert(object input)
        {
            tblEnumerationGroup model = (tblEnumerationGroup)input;
            model.iID = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field01, ""))) + 1;
            //model.iIndexNo = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field04, $"{field05}={model.iControllerID}"))) + 1;
            string sqlString = Function.Insert_String<tblEnumerationGroup>((tblEnumerationGroup)input, tableName);
            return complex.Connection.Insert(sqlString);
        }

        public override object NewModel()
        {
            tblEnumerationGroup model = new tblEnumerationGroup();
            model.iID = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field01, ""))) + 1;
            //model.iIndexNo = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field04, $"{field05}={model.iControllerID}"))) + 1;
            return model;
        }

        public override object DuplicateModel(int id)
        {
            DataTable dt = SelectRow(id);
            tblEnumerationGroup model = (tblEnumerationGroup)MapModel(dt);
            model.iID = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field01, ""))) + 1;
            //model.iIndexNo = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field04, $"{field05}={model.iControllerID}"))) + 1;
            return model;
        }

    }
}
