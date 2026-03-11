using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SID.Complex.Control.Model
{
    public class tblCmVSD
    {
        [Mapping(ColumnName = "iID", Type = MappingType.PrimaryKey)]
        public int iID { get; set; }

        [Mapping(ColumnName = "iIndexNo", Type = MappingType.Integer,NotNull =true)]
        public int iIndexNo { get; set; }

        [Mapping(ColumnName = "sName", Type = MappingType.Text,NotNull = true)]
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

        [Mapping(ColumnName = "iTypeID", Type = MappingType.ForeignKey, ForeignTable = ForeignTable.VSDType)]
        public int iTypeID { get; set; }

        [Mapping(ColumnName = "iVendorID", Type = MappingType.ForeignKey, ForeignTable = ForeignTable.VSDVendor)]
        public int iVendorID { get; set; }

        [Mapping(ColumnName = "iVSDProtocolID", Type = MappingType.Integer)]
        public int iVSDProtocolID { get; set; }

        [Mapping(ColumnName = "rRawMinimum", Type = MappingType.Real)]
        public float rRawMinimum { get; set; }

        [Mapping(ColumnName = "rRawMaximum", Type = MappingType.Real)]
        public float rRawMaximum { get; set; }

        [Mapping(ColumnName = "rScaleMinimum", Type = MappingType.Real)]
        public float rScaleMinimum { get; set; }

        [Mapping(ColumnName = "rScaleMaximum", Type = MappingType.Real)]
        public float rScaleMaximum { get; set; }

        [Mapping(ColumnName = "sDeviceAdrOutput", Type = MappingType.Text)]
        public string sDeviceAdrOutput { get; set; }

        [Mapping(ColumnName = "sDeviceAdrInput", Type = MappingType.Text)]
        public string sDeviceAdrInput { get; set; }

        [Mapping(ColumnName = "sDeviceAdrOutputExt", Type = MappingType.Text)]
        public string sDeviceAdrOutputExt { get; set; }

        [Mapping(ColumnName = "sDeviceAdrInputExt", Type = MappingType.Text)]
        public string sDeviceAdrInputExt { get; set; }

        [Mapping(ColumnName = "sDeviceAdrReverseOutput", Type = MappingType.Text)]
        public string sDeviceAdrReverseOutput { get; set; }

        [Mapping(ColumnName = "sDeviceAdrVsdSpeed", Type = MappingType.Text)]
        public string sDeviceAdrVsdSpeed { get; set; }

        [Mapping(ColumnName = "bReverse", Type = MappingType.Boolean)]
        public bool bReverse { get; set; }

        [Mapping(ColumnName = "bUseSafetyIO", Type = MappingType.Boolean)]
        public bool bUseSafetyIO { get; set; }

        [Mapping(ColumnName = "bSimulationInOfficemodeOnly", Type = MappingType.Boolean)]
        public bool bSimulationInOfficemodeOnly { get; set; }

        [Mapping(ColumnName = "bEnableInterlock", Type = MappingType.Boolean)]
        public bool bEnableInterlock { get; set; }

        [Mapping(ColumnName = "bEnableDirectionChange", Type = MappingType.Boolean)]
        public bool bEnableDirectionChange { get; set; }

        [Mapping(ColumnName = "iTmrRunningError", Type = MappingType.Integer)]
        public int iTmrRunningError { get; set; }

        [Mapping(ColumnName = "iTmrDirectionDelay", Type = MappingType.Integer)]
        public int iTmrDirectionDelay { get; set; }

        [Mapping(ColumnName = "iTmrOnDelay", Type = MappingType.Integer)]
        public int iTmrOnDelay { get; set; }

        [Mapping(ColumnName = "iTmrOffDelay", Type = MappingType.Integer)]
        public int iTmrOffDelay { get; set; }

        [Mapping(ColumnName = "iEngineeringUnitID", Type = MappingType.ForeignKey, ForeignTable = ForeignTable.EngineeringUnit)]
        public int iEngineeringUnitID { get; set; }

        [Mapping(ColumnName = "iPrecision", Type = MappingType.Integer)]
        public int iPrecision { get; set; }

        [Mapping(ColumnName = "sSymbol", Type = MappingType.Text)]
        public string sSymbol { get; set; }

    }
}
