using System;
using System.Data;
using SID.Complex.Control.Model;
using System.Reflection;

namespace SID.Complex.Control.Tables
{
    public class Table_tblPhaseAlarmWarningInfosText : Table
    {
        public Table_tblPhaseAlarmWarningInfosText()
        {
            tableName = "tblPhaseAlarmWarningInfosText";
            name = "Alarme/Avisos/Infos Textos";

            tblPhaseAlarmWarningInfosText model = new tblPhaseAlarmWarningInfosText();
            field01 = GetColumnName(nameof(model.iID));
            field02 = GetColumnName(nameof(model.sName_1));
            field03 = GetColumnName(nameof(model.sName_2));
            //field04 = GetColumnName(nameof(model.iIndexNo));
            //field05 = GetColumnName(nameof(model.iControllerID));
            //field06 = GetColumnName(nameof(model.iID));
        }

        string GetColumnName(string name)
        {
            tblPhaseAlarmWarningInfosText model = new tblPhaseAlarmWarningInfosText();
            return ((MappingAttribute)model.GetType().GetProperty(name).GetCustomAttribute(typeof(MappingAttribute))).ColumnName;
        }
        public override object MapModel(DataTable dataTable)
        {
            tblPhaseAlarmWarningInfosText returnedObject = Function.MapToClass<tblPhaseAlarmWarningInfosText>(dataTable);
            return returnedObject;
        }

        public override int Update(object input)
        {
            tblPhaseAlarmWarningInfosText model = (tblPhaseAlarmWarningInfosText)input;
            string sqlString = Function.Update_String<tblPhaseAlarmWarningInfosText>(model, tableName, $"{field01} = {model.iID}");
            return complex.Connection.Update(sqlString);
        }

        public override int Insert(object input)
        {
            tblPhaseAlarmWarningInfosText model = (tblPhaseAlarmWarningInfosText)input;
            model.iID = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field01, ""))) + 1;
            //model.iIndexNo = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field04, $"{field05}={model.iControllerID}"))) + 1;
            string sqlString = Function.Insert_String<tblPhaseAlarmWarningInfosText>((tblPhaseAlarmWarningInfosText)input, tableName);
            return complex.Connection.Insert(sqlString);
        }

        public override object NewModel()
        {
            tblPhaseAlarmWarningInfosText model = new tblPhaseAlarmWarningInfosText();
            model.iID = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field01, ""))) + 1;
            //model.iIndexNo = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field04, $"{field05}={model.iControllerID}"))) + 1;
            return model;
        }

        public override object DuplicateModel(int id)
        {
            DataTable dt = SelectRow(id);
            tblPhaseAlarmWarningInfosText model = (tblPhaseAlarmWarningInfosText)MapModel(dt);
            model.iID = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field01, ""))) + 1;
            //model.iIndexNo = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field04, $"{field05}={model.iControllerID}"))) + 1;
            return model;
        }

    }
}
