using System;
using System.Data;
using SID.Complex.Control.Model;
using System.Reflection;

namespace SID.Complex.Control.Tables
{
    public class Table_tblPhaseAlarmWarningInfos : Table
    {
        public Table_tblPhaseAlarmWarningInfos()
        {
            tableName = "tblPhaseAlarmWarningInfos";
            name = "Alarme/Avisos/Infos";

            tblPhaseAlarmWarningInfos model = new tblPhaseAlarmWarningInfos();
            field01 = GetColumnName(nameof(model.iClassID));
            field02 = GetColumnName(nameof(model.iIndexNo));
            field03 = GetColumnName(nameof(model.iTextID));
            field04 = GetColumnName(nameof(model.iType));
            //field05 = GetColumnName(nameof(model.iControllerID));
            //field06 = GetColumnName(nameof(model.iID));
        }

        string GetColumnName(string name)
        {
            tblPhaseAlarmWarningInfos model = new tblPhaseAlarmWarningInfos();
            return ((MappingAttribute)model.GetType().GetProperty(name).GetCustomAttribute(typeof(MappingAttribute))).ColumnName;
        }
        public override object MapModel(DataTable dataTable)
        {
            tblPhaseAlarmWarningInfos returnedObject = Function.MapToClass<tblPhaseAlarmWarningInfos>(dataTable);
            return returnedObject;
        }

        public override int Update(object input)
        {
            tblPhaseAlarmWarningInfos model = (tblPhaseAlarmWarningInfos)input;
            string sqlString = Function.Update_String<tblPhaseAlarmWarningInfos>(model, tableName, $"{field01} = {model.iClassID} and {field02} = {model.iIndexNo} and {field04} = {model.iType}");
            return complex.Connection.Update(sqlString);
        }

        public override int Insert(object input)
        {
            tblPhaseAlarmWarningInfos model = (tblPhaseAlarmWarningInfos)input;
            model.iIndexNo = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field02, $"{field01}={model.iClassID} and {field04} = {model.iType}"))) + 1;
            string sqlString = Function.Insert_String<tblPhaseAlarmWarningInfos>((tblPhaseAlarmWarningInfos)input, tableName);
            return complex.Connection.Insert(sqlString);
        }

        public override object NewModel()
        {
            tblPhaseAlarmWarningInfos model = new tblPhaseAlarmWarningInfos();
            model.iIndexNo = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field02, $"{field01}={model.iClassID} and {field04} = {model.iType}"))) + 1;
            return model;
        }

        public override object DuplicateModel(int id)
        {
            DataTable dt = SelectRow(id);
            tblPhaseAlarmWarningInfos model = (tblPhaseAlarmWarningInfos)MapModel(dt);
            model.iIndexNo = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field02, $"{field01}={model.iClassID} and {field04} = {model.iType}"))) + 1;
            return model;
        }

    }
}
