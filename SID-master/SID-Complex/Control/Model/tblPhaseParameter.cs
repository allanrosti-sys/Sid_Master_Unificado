using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SID.Complex.Control.Model
{
    public class tblPhaseParameter
    {
        [Mapping(ColumnName = "iClassID", Type = MappingType.ForeignKey, ForeignTable = ForeignTable.PhaseClass)]
        public int iClassID { get; set; }

        [Mapping(ColumnName = "iIndexNo", Type = MappingType.Integer)]
        public int iIndexNo { get; set; }

        [Mapping(ColumnName = "sName_1", Type = MappingType.Text)]
        public string sName_1 { get; set; }

        [Mapping(ColumnName = "sName_2", Type = MappingType.Text)]
        public string sName_2 { get; set; }

        [Mapping(ColumnName = "sName_3", Type = MappingType.Text)]
        public string sName_3 { get; set; }

        [Mapping(ColumnName = "sName_4", Type = MappingType.Text)]
        public string sName_4 { get; set; }

        [Mapping(ColumnName = "sName_5", Type = MappingType.Text)]
        public string sName_5 { get; set; }

        [Mapping(ColumnName = "sSymbol", Type = MappingType.Text)]
        public string sSymbol { get; set; }

        [Mapping(ColumnName = "iParamType", Type = MappingType.Integer)]
        public int iParamType { get; set; }

        [Mapping(ColumnName = "iTypeID", Type = MappingType.Integer)]
        public int iTypeID { get; set; }

        [Mapping(ColumnName = "iEnumGroupID", Type = MappingType.ForeignKey, ForeignTable = ForeignTable.EnumerationGroup)]
        public int iEnumGroupID { get; set; }

        [Mapping(ColumnName = "iEngineeringUnitID", Type = MappingType.ForeignKey, ForeignTable = ForeignTable.EngineeringUnit)]
        public int iEngineeringUnitID { get; set; }

        [Mapping(ColumnName = "rMin", Type = MappingType.Real)]
        public float rMin { get; set; }

        [Mapping(ColumnName = "rMax", Type = MappingType.Real)]
        public float rMax { get; set; }

        [Mapping(ColumnName = "iPrecision", Type = MappingType.Integer)]
        public int iPrecision { get; set; }

        [Mapping(ColumnName = "bLock", Type = MappingType.Boolean)]
        public bool bLock { get; set; }

        [Mapping(ColumnName = "iAccessLevel", Type = MappingType.Integer)]
        public int iAccessLevel { get; set; }

        [Mapping(ColumnName = "iViewPosition", Type = MappingType.Integer)]
        public int iViewPosition { get; set; }

        [Mapping(ColumnName = "iViewmode", Type = MappingType.Integer)]
        public int iViewmode { get; set; }

    }

    public enum PhaseParameter_Type
    {
        REAL=0, 
        DINT=1,
        Enumeration=2, 
        Bool=3, 
        Time=4
    }
}
