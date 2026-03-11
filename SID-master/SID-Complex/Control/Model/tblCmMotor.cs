using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SID.Complex.Control.Model
{
    public class tblCmMotor
    {
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

        [Mapping(ColumnName = "iTypeID", Type = MappingType.ForeignKey, ForeignTable = ForeignTable.MotorType, NotNull = true)]
        public int iTypeID { get; set; }

        [Mapping(ColumnName = "bUseSafetyIO", Type = MappingType.Boolean)]
        public bool bUseSafetyIO { get; set; }

        [Mapping(ColumnName = "bSimulationInOfficemodeOnly", Type = MappingType.Boolean)]
        public bool bSimulationInOfficemodeOnly { get; set; }

        [Mapping(ColumnName = "bEnableInterlock", Type = MappingType.Boolean)]
        public bool bEnableInterlock { get; set; }

        [Mapping(ColumnName = "sStateName1_1", Type = MappingType.Text)]
        public string sStateName1_1 { get; set; }

        [Mapping(ColumnName = "sStateName1_2", Type = MappingType.Text)]
        public string sStateName1_2 { get; set; }

        [Mapping(ColumnName = "sStateName1_3", Type = MappingType.Text)]
        public string sStateName1_3 { get; set; }

        [Mapping(ColumnName = "sStateName1_4", Type = MappingType.Text)]
        public string sStateName1_4 { get; set; }

        [Mapping(ColumnName = "sStateName1_5", Type = MappingType.Text)]
        public string sStateName1_5 { get; set; }

        [Mapping(ColumnName = "sDeviceAdrOutput1", Type = MappingType.Text)]
        public string sDeviceAdrOutput1 { get; set; }

        [Mapping(ColumnName = "sDeviceAdrInputInAct1", Type = MappingType.Text)]
        public string sDeviceAdrInputInAct1 { get; set; }

        [Mapping(ColumnName = "sDeviceAdrInputAct1", Type = MappingType.Text)]
        public string sDeviceAdrInputAct1 { get; set; }

        [Mapping(ColumnName = "sStateName2_1", Type = MappingType.Text)]
        public string sStateName2_1 { get; set; }

        [Mapping(ColumnName = "sStateName2_2", Type = MappingType.Text)]
        public string sStateName2_2 { get; set; }

        [Mapping(ColumnName = "sStateName2_3", Type = MappingType.Text)]
        public string sStateName2_3 { get; set; }

        [Mapping(ColumnName = "sStateName2_4", Type = MappingType.Text)]
        public string sStateName2_4 { get; set; }

        [Mapping(ColumnName = "sStateName2_5", Type = MappingType.Text)]
        public string sStateName2_5 { get; set; }

        [Mapping(ColumnName = "sDeviceAdrOutput2", Type = MappingType.Text)]
        public string sDeviceAdrOutput2 { get; set; }

        [Mapping(ColumnName = "sDeviceAdrInputInAct2", Type = MappingType.Text)]
        public string sDeviceAdrInputInAct2 { get; set; }

        [Mapping(ColumnName = "sDeviceAdrInputAct2", Type = MappingType.Text)]
        public string sDeviceAdrInputAct2 { get; set; }

        [Mapping(ColumnName = "iTmrRunningError", Type = MappingType.Integer)]
        public int iTmrRunningError { get; set; }

        [Mapping(ColumnName = "iTmrDirectionDelay", Type = MappingType.Integer)]
        public int iTmrDirectionDelay { get; set; }

        [Mapping(ColumnName = "iDelayTimeOn", Type = MappingType.Integer)]
        public int iDelayTimeOn { get; set; }

        [Mapping(ColumnName = "iDelayTimeOff", Type = MappingType.Integer)]
        public int iDelayTimeOff { get; set; }

        [Mapping(ColumnName = "sSymbol", Type = MappingType.Text)]
        public string sSymbol { get; set; }
    }
}
