using System;
using System.Data;
using SID.Complex.Control.Model;
using System.Reflection;

namespace SID.Complex.Control.Tables
{
    public class Table_tblPhaseParameter : Table
    {
            public Table_tblPhaseParameter()
        {
                tableName = "tblPhaseParameter";
                name = "Parâmetro de Fases";

              tblPhaseParameter model = new tblPhaseParameter();
                field01 = GetColumnName(nameof(model.iClassID));
                field02 = GetColumnName(nameof(model.iIndexNo));
                field03 = GetColumnName(nameof(model.sName_1));
                field04 = GetColumnName(nameof(model.sName_2));
                //field05 = GetColumnName(nameof(model.iControllerID));
                //field06 = GetColumnName(nameof(model.iID));
        }

                string GetColumnName(string name)
                {
                    tblPhaseParameter model = new tblPhaseParameter();
                    return ((MappingAttribute)model.GetType().GetProperty(name).GetCustomAttribute(typeof(MappingAttribute))).ColumnName;
            } 
        public override object MapModel(DataTable dataTable)
        {
            tblPhaseParameter returnedObject = Function.MapToClass<tblPhaseParameter>(dataTable);
            return returnedObject;
        }

        public override int Update(object input)
        {
            tblPhaseParameter model = (tblPhaseParameter)input;
            string sqlString = Function.Update_String<tblPhaseParameter>(model, tableName, $"{field01} = {model.iClassID} and {field02}={model.iIndexNo}");
            return complex.Connection.Update(sqlString);
        }

        public override int Insert(object input)
        {
            tblPhaseParameter model = (tblPhaseParameter)input;
            //model.iID = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field01, ""))) + 1;
            model.iIndexNo = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field02, $"{field01}={model.iClassID}"))) + 1;
            string sqlString = Function.Insert_String<tblPhaseParameter>((tblPhaseParameter)input, tableName);
            return complex.Connection.Insert(sqlString);
        }

        public override object NewModel()
        {
            tblPhaseParameter model = new tblPhaseParameter();
            //model.iID = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field01, ""))) + 1;
            model.iIndexNo = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field02, $"{field01}={model.iClassID}"))) + 1;
            return model;
        }

        public override object DuplicateModel(int id)
        {
            DataTable dt = SelectRow(id);
            tblPhaseParameter model = (tblPhaseParameter)MapModel(dt);
            //model.iID = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field01, ""))) + 1;
            model.iIndexNo = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field02, $"{field01}={model.iClassID}"))) + 1;
            return model;
        }

    }
}
