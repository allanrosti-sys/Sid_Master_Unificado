
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SID.TPM_ROCKWELL.Controls
{
    public class CMModel
    {
        const string pathTags = @"/Tags/";
        const string pathRungs = @"/Rungs/";
        const string pathSupTags = @"/SupTags/";

        string name;
        CMType cMtype;
        string fileTag;
        string fileRung;
        string fileSupTag;
        bool enableExtPar;
        bool enableAlarm;
        string dataType;
        string extParDataType;

        string pathBase;

        public CMModel(string pathBase)
        {
            this.pathBase = pathBase;
        }

        public string Name { get => name; set => name = value; }
        public CMType CMType { get => cMtype; set => cMtype = value; }
        public string FileTag { get => fileTag; set => fileTag = pathBase + pathTags + value; }
        public string FileRung { get => fileRung; set => fileRung = pathBase + pathRungs + value; }
        public string FileSupTag { get => fileSupTag; set => fileSupTag = pathBase + pathSupTags + value; }
        public bool EnableExtPar { get => enableExtPar; set => enableExtPar = value; }
        public bool EnableAlarm { get => enableAlarm; set => enableAlarm = value; }
        public string DataType { get => dataType; set => dataType = value; }
        public string ExtParDataType { get => extParDataType; set => extParDataType = value; }
    }
}
