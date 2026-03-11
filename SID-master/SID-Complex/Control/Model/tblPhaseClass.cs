using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SID.Complex.Control.Model
{
    public class tblPhaseClass
    {
        [Mapping(ColumnName = "iID", Type = MappingType.PrimaryKey)]
        public int iID { get; set; }

        [Mapping(ColumnName = "sName_1", Type = MappingType.Text, NotNull = true)]
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

        [Mapping(ColumnName = "iControllerID", Type = MappingType.ForeignKey, ForeignTable = ForeignTable.Controller)]
        public int iControllerID { get; set; }

        [Mapping(ColumnName = "iNumberAnalogParam", Type = MappingType.Integer)]
        public int iNumberAnalogParam { get; set; }

        [Mapping(ColumnName = "iNumberSVParam", Type = MappingType.Integer)]
        public int iNumberSVParam { get; set; }

        [Mapping(ColumnName = "iNumberAlarms", Type = MappingType.Integer)]
        public int iNumberAlarms { get; set; }

        [Mapping(ColumnName = "iNumberWarnings", Type = MappingType.Integer)]
        public int iNumberWarnings { get; set; }

        [Mapping(ColumnName = "iNumberInformations", Type = MappingType.Integer)]
        public int iNumberInformations { get; set; }

        [Mapping(ColumnName = "iNumberQueues", Type = MappingType.Integer)]
        public int iNumberQueues { get; set; }

        [Mapping(ColumnName = "iNumberSteps", Type = MappingType.Integer)]
        public int iNumberSteps { get; set; }

        [Mapping(ColumnName = "iNumberSoftkeys", Type = MappingType.Integer)]
        public int iNumberSoftkeys { get; set; }

    }
}
