using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SID.Complex.Control.Model
{
    public class tblCmAnalogOutput
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

        [Mapping(ColumnName = "iTypeID", Type = MappingType.ForeignKey, ForeignTable = ForeignTable.AnalogOutputType)]
        public int iTypeID { get; set; }

        [Mapping(ColumnName = "iEngineeringUnitID", Type = MappingType.ForeignKey, ForeignTable = ForeignTable.EngineeringUnit)]
        public int iEngineeringUnitID { get; set; }

        [Mapping(ColumnName = "rScaleMinimum", Type = MappingType.Real)]
        public float rScaleMinimum { get; set; }

        [Mapping(ColumnName = "rScaleMaximum", Type = MappingType.Real)]
        public float rScaleMaximum { get; set; }

        [Mapping(ColumnName = "rRawMinimum", Type = MappingType.Real)]
        public float rRawMinimum { get; set; }

        [Mapping(ColumnName = "rRawMaximum", Type = MappingType.Real)]
        public float rRawMaximum { get; set; }

        [Mapping(ColumnName = "rInterlocksSetValue", Type = MappingType.Real)]
        public float rInterlocksSetValue { get; set; }

        [Mapping(ColumnName = "iPrecision", Type = MappingType.Integer)]
        public int iPrecision { get; set; }

        [Mapping(ColumnName = "sDeviceAdr", Type = MappingType.Text)]
        public string sDeviceAdr { get; set; }

        [Mapping(ColumnName = "sSymbol", Type = MappingType.Text)]
        public string sSymbol { get; set; }

        [Mapping(ColumnName = "bSimulationInOfficemodeOnly", Type = MappingType.Boolean)]
        public bool bSimulationInOfficemodeOnly { get; set; }

        [Mapping(ColumnName = "bEnableInterlock", Type = MappingType.Boolean)]
        public bool bEnableInterlock { get; set; }

        [Mapping(ColumnName = "bReverse", Type = MappingType.Boolean)]
        public bool bReverse { get; set; }
    }
}
