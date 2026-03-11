using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SID.Complex.Control.Model
{
    public class tblCmPID
    {
        [Mapping(ColumnName = "iID", Type = MappingType.PrimaryKey)]
        public int iID { get; set; }

        [Mapping(ColumnName = "iIndexNo", Type = MappingType.Integer, NotNull = true)]
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

        [Mapping(ColumnName = "iCmAnalogInID", Type = MappingType.ForeignKey, ForeignTable = ForeignTable.CmAnalogIn)]
        public int iCmAnalogInID { get; set; }

        [Mapping(ColumnName = "iCmOutputTypeID", Type = MappingType.ForeignKey, ForeignTable = ForeignTable.AnalogOutputType)]
        public int iCmOutputTypeID { get; set; }

        [Mapping(ColumnName = "iCmOutputID", Type = MappingType.ForeignKey, ForeignTable = ForeignTable.CmAnalogOut)]
        public int iCmOutputID { get; set; }

        [Mapping(ColumnName = "rGain", Type = MappingType.Real)]
        public float rGain { get; set; }

        [Mapping(ColumnName = "rIntegral", Type = MappingType.Real)]
        public float rIntegral { get; set; }

        [Mapping(ColumnName = "rDerivate", Type = MappingType.Real)]
        public float rDerivate { get; set; }

        [Mapping(ColumnName = "rDeadband", Type = MappingType.Real)]
        public float rDeadband { get; set; }

        [Mapping(ColumnName = "iSampleTime", Type = MappingType.Integer)]
        public int iSampleTime { get; set; }

        [Mapping(ColumnName = "iTimeSlice", Type = MappingType.Integer)]
        public int iTimeSlice { get; set; }

        [Mapping(ColumnName = "rOutHiLimit", Type = MappingType.Real)]
        public float rOutHiLimit { get; set; }

        [Mapping(ColumnName = "rOutLoLimit", Type = MappingType.Real)]
        public float rOutLoLimit { get; set; }

        [Mapping(ColumnName = "rInterlockOutput", Type = MappingType.Real)]
        public float rInterlockOutput { get; set; }

        [Mapping(ColumnName = "rDevHiAlmSP", Type = MappingType.Real)]
        public float rDevHiAlmSP { get; set; }

        [Mapping(ColumnName = "rDevLoAlmSP", Type = MappingType.Real)]
        public float rDevLoAlmSP { get; set; }

        [Mapping(ColumnName = "bPIDeqType", Type = MappingType.Boolean)]
        public bool bPIDeqType { get; set; }

        [Mapping(ColumnName = "bReverse", Type = MappingType.Boolean)]
        public bool bReverse { get; set; }

        [Mapping(ColumnName = "bPVTrack", Type = MappingType.Boolean)]
        public bool bPVTrack { get; set; }

        [Mapping(ColumnName = "bDevAlmType", Type = MappingType.Boolean)]
        public bool bDevAlmType { get; set; }

        [Mapping(ColumnName = "bEnableDevAlm", Type = MappingType.Boolean)]
        public bool bEnableDevAlm { get; set; }

        [Mapping(ColumnName = "bLoopOffFreeze", Type = MappingType.Boolean)]
        public bool bLoopOffFreeze { get; set; }

        [Mapping(ColumnName = "bEnableInterlock", Type = MappingType.Boolean)]
        public bool bEnableInterlock { get; set; }

        [Mapping(ColumnName = "sSymbol", Type = MappingType.Text)]
        public string sSymbol { get; set; }

        [Mapping(ColumnName = "iDevSPchange", Type = MappingType.Integer)]
        public int iDevSPchange { get; set; }

        [Mapping(ColumnName = "iDevMANtoAUTO", Type = MappingType.Integer)]
        public int iDevMANtoAUTO { get; set; }

        [Mapping(ColumnName = "iAlarmDelay", Type = MappingType.Integer)]
        public int iAlarmDelay { get; set; }

        [Mapping(ColumnName = "iPIDstable", Type = MappingType.Integer)]
        public int iPIDstable { get; set; }

        [Mapping(ColumnName = "iDelayTimeOn", Type = MappingType.Integer)]
        public int iDelayTimeOn { get; set; }

        [Mapping(ColumnName = "iDelayTimeOff", Type = MappingType.Integer)]
        public int iDelayTimeOff { get; set; }

    }
}
