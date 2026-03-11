using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SID.Complex.Control;
using SID.Standard.Control;

namespace SID.Complex.Control.Model
{
    public class tblCmDigitalInput
    {
        #region Propriedades
        [Mapping(ColumnName = "iID", Type = MappingType.PrimaryKey)]
        public int iID { get; set; }

        [Mapping(ColumnName = "iIndexNo", Type = MappingType.Integer, NotNull = true, Constant = true)]
        public int iIndexNo { get; set; }

        [Mapping(ColumnName = "sName", Type = MappingType.Text, NotNull = true)]
        public string sName { get; set; }

        [Mapping(ColumnName = "sDescription_1", Type = MappingType.Text)]
        public string sDescription_1 { get; set; }

        [Mapping(ColumnName = "sDescription_2", Type = MappingType.Text)]
        public string sDescription_2 { get; set; }

        [Mapping(ColumnName = "sDescription_3", Type = MappingType.Text)]
        public string sDescription_3 { get; set; }

        [Mapping(ColumnName = "sDescription_4", Type = MappingType.Text)]
        public string sDescription_4 { get; set; }

        [Mapping(ColumnName = "sDescription_5", Type = MappingType.Text)]
        public string sDescription_5 { get; set; }

        [Mapping(ColumnName = "iAreaID", Type = MappingType.ForeignKey, ForeignTable = ForeignTable.Area)]
        public int iAreaID { get; set; }

        [Mapping(ColumnName = "iControllerID", Type = MappingType.ForeignKey, ForeignTable = ForeignTable.Controller)]
        public int iControllerID { get; set; }

        [Mapping(ColumnName = "iCabinetID", Type = MappingType.ForeignKey, ForeignTable = ForeignTable.Cabinet)]
        public int iCabinetID { get; set; }

        [Mapping(ColumnName = "sCabinetDesc", Type = MappingType.Text)]
        public string sCabinetDesc { get; set; }

        [Mapping(ColumnName = "sDeviceAdr", Type = MappingType.Text)]
        public string sDeviceAdr { get; set; }

        [Mapping(ColumnName = "iDelayTimeOn", Type = MappingType.Integer)]
        public int iDelayTimeOn { get; set; }

        [Mapping(ColumnName = "iDelayTimeOff", Type = MappingType.Integer)]
        public int iDelayTimeOff { get; set; }

        [Mapping(ColumnName = "sOnStatusMessage_1", Type = MappingType.Text)]
        public string sOnStatusMessage_1 { get; set; }

        [Mapping(ColumnName = "sOnStatusMessage_2", Type = MappingType.Text)]
        public string sOnStatusMessage_2 { get; set; }

        [Mapping(ColumnName = "sOnStatusMessage_3", Type = MappingType.Text)]
        public string sOnStatusMessage_3 { get; set; }

        [Mapping(ColumnName = "sOnStatusMessage_4", Type = MappingType.Text)]
        public string sOnStatusMessage_4 { get; set; }

        [Mapping(ColumnName = "sOnStatusMessage_5", Type = MappingType.Text)]
        public string sOnStatusMessage_5 { get; set; }

        [Mapping(ColumnName = "sOffStatusMessage_1", Type = MappingType.Text)]
        public string sOffStatusMessage_1 { get; set; }

        [Mapping(ColumnName = "sOffStatusMessage_2", Type = MappingType.Text)]
        public string sOffStatusMessage_2 { get; set; }

        [Mapping(ColumnName = "sOffStatusMessage_3", Type = MappingType.Text)]
        public string sOffStatusMessage_3 { get; set; }

        [Mapping(ColumnName = "sOffStatusMessage_4", Type = MappingType.Text)]
        public string sOffStatusMessage_4 { get; set; }

        [Mapping(ColumnName = "sOffStatusMessage_5", Type = MappingType.Text)]
        public string sOffStatusMessage_5 { get; set; }

        [Mapping(ColumnName = "bDeviceValueInvert", Type = MappingType.Boolean)]
        public bool bDeviceValueInvert { get; set; }

        [Mapping(ColumnName = "bSimulationInOfficemodeOnly", Type = MappingType.Boolean)]
        public bool bSimulationInOfficemodeOnly { get; set; }

        [Mapping(ColumnName = "bAlarmEnabled", Type = MappingType.Boolean)]
        public bool bAlarmEnabled { get; set; }

        [Mapping(ColumnName = "bAlarmTypeOn", Type = MappingType.Boolean)]
        public bool bAlarmTypeOn { get; set; }

        [Mapping(ColumnName = "iAlarmDelay", Type = MappingType.Integer)]
        public int iAlarmDelay { get; set; }

        [Mapping(ColumnName = "sAlarmMessage_1", Type = MappingType.Text)]
        public string sAlarmMessage_1 { get; set; }

        [Mapping(ColumnName = "sAlarmMessage_2", Type = MappingType.Text)]
        public string sAlarmMessage_2 { get; set; }

        [Mapping(ColumnName = "sAlarmMessage_3", Type = MappingType.Text)]
        public string sAlarmMessage_3 { get; set; }

        [Mapping(ColumnName = "sAlarmMessage_4", Type = MappingType.Text)]
        public string sAlarmMessage_4 { get; set; }

        [Mapping(ColumnName = "sAlarmMessage_5", Type = MappingType.Text)]
        public string sAlarmMessage_5 { get; set; }

        [Mapping(ColumnName = "sSymbol", Type = MappingType.Text)]
        public string sSymbol { get; set; }

        #endregion





    }
}
