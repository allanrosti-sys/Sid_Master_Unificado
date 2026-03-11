using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SID.Complex.Control.Model
{
    public class tblControllers
    {
        [Mapping(ColumnName = "iID", Type = MappingType.PrimaryKey)]
        public int iID { get; set; }

        [Mapping(ColumnName = "iIndexNo", Type = MappingType.Integer, NotNull = true, Constant = true)]
        public int iIndexNo { get; set; }

        [Mapping(ColumnName = "iItemOffset", Type = MappingType.Integer)]
        public int iItemOffset { get; set; }

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

        [Mapping(ColumnName = "iControllerTypeID", Type = MappingType.ForeignKey, ForeignTable = ForeignTable.ControllerType),]
        public int iControllerTypeID { get; set; }

    }

}

