using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SID.Complex.Control.Model
{
    public class tblPhaseAlarmWarningInfos
    {
        [Mapping(ColumnName = "iClassID", Type = MappingType.ForeignKey, ForeignTable = ForeignTable.PhaseClass)]
        public int iClassID { get; set; }

        [Mapping(ColumnName = "iIndexNo", Type = MappingType.Integer)]
        public int iIndexNo { get; set; }

        [Mapping(ColumnName = "iTextID", Type = MappingType.ForeignKey, ForeignTable = ForeignTable.PhaseAlarmWarningInfosText)]
        public int iTextID { get; set; }

        [Mapping(ColumnName = "iPriority", Type = MappingType.Integer)]
        public int iPriority { get; set; }

        [Mapping(ColumnName = "iType", Type = MappingType.Integer)]
        public int iType { get; set; }

        [Mapping(ColumnName = "sSymbol", Type = MappingType.Text)]
        public string sSymbol { get; set; }

    }
}
