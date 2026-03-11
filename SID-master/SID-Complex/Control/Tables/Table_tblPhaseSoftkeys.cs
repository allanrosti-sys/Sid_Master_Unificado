using System;
using System.Data;
using SID.Complex.Control.Model;
using System.Reflection;

namespace SID.Complex.Control.Tables
{
    public class Table_tblPhaseSoftkeys : Table
    {
            public Table_tblPhaseSoftkeys()
        {
                tableName = "tblPhaseSoftkeys";
                name = "SoftKeys das Fases";

              tblPhaseSoftkeys model = new tblPhaseSoftkeys();
                field01 = GetColumnName(nameof(model.iClassID));
                field02 = GetColumnName(nameof(model.iIndexNo));
                field03 = GetColumnName(nameof(model.sName_1));
                field04 = GetColumnName(nameof(model.sName_2));
                //field05 = GetColumnName(nameof(model.iControllerID));
                //field06 = GetColumnName(nameof(model.iID));
        }

                string GetColumnName(string name)
                {
                    tblPhaseSoftkeys model = new tblPhaseSoftkeys();
                    return ((MappingAttribute)model.GetType().GetProperty(name).GetCustomAttribute(typeof(MappingAttribute))).ColumnName;
            } 
        public override object MapModel(DataTable dataTable)
        {
            tblPhaseSoftkeys returnedObject = Function.MapToClass<tblPhaseSoftkeys>(dataTable);
            return returnedObject;
        }

        public override int Update(object input)
        {
            tblPhaseSoftkeys model = (tblPhaseSoftkeys)input;
            string sqlString = Function.Update_String<tblPhaseSoftkeys>(model, tableName, $"{field01} = {model.iClassID} and {field02} = {model.iIndexNo} ");
            return complex.Connection.Update(sqlString);
        }

        public override int Insert(object input)
        {
            tblPhaseSoftkeys model = (tblPhaseSoftkeys)input;
            //model.iID = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field01, ""))) + 1;
            model.iIndexNo = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field02, $"{field01}={model.iClassID}"))) + 1;
            string sqlString = Function.Insert_String<tblPhaseSoftkeys>((tblPhaseSoftkeys)input, tableName);
            return complex.Connection.Insert(sqlString);
        }

        public override object NewModel()
        {
            tblPhaseSoftkeys model = new tblPhaseSoftkeys();
            //model.iID = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field01, ""))) + 1;
            model.iIndexNo = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field02, $"{field01}={model.iClassID}"))) + 1;
            return model;
        }

        public override object DuplicateModel(int id)
        {
            DataTable dt = SelectRow(id);
            tblPhaseSoftkeys model = (tblPhaseSoftkeys)MapModel(dt);
            //model.iID = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field01, ""))) + 1;
            model.iIndexNo = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field02, $"{field01}={model.iClassID}"))) + 1;
            return model;
        }

    }
}
