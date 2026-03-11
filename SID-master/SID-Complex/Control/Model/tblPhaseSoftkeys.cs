using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SID.Complex.Control.Model
{
    public class tblPhaseSoftkeys
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

        [Mapping(ColumnName = "bConfirm", Type = MappingType.Boolean)]
        public bool bConfirm { get; set; }

        [Mapping(ColumnName = "bIsSwitch", Type = MappingType.Boolean)]
        public bool bIsSwitch { get; set; }

        [Mapping(ColumnName = "bAutoResetSwitch", Type = MappingType.Boolean)]
        public bool bAutoResetSwitch { get; set; }

        [Mapping(ColumnName = "iAccessLevel", Type = MappingType.Integer)]
        public int iAccessLevel { get; set; }

    }
}
