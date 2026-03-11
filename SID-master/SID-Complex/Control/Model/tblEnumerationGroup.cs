using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SID.Complex.Control.Model
{
    public class tblEnumerationGroup
    {
        [Mapping(ColumnName = "iID", Type = MappingType.PrimaryKey)]
        public int iID { get; set; }

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

        [Mapping(ColumnName = "bIsBoolean", Type = MappingType.Boolean)]
        public bool bIsBoolean { get; set; }

        [Mapping(ColumnName = "sSymbol", Type = MappingType.Text)]
        public string sSymbol { get; set; }

    }
}
