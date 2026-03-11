using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SID.Complex.Control.Model
{
    public class tblCmTotalizerType
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

        [Mapping(ColumnName = "iLengthSiemens", Type = MappingType.Integer)]
        public int iLengthSiemens { get; set; }
    }
}
