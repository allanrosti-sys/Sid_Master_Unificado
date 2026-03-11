using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SID.MSQL.Control
{
    [Serializable]
    public class MSQL_Info
    {
        string dataSource;
        string database;
        string user;
        string password;
        
        public string DataSource { get => dataSource; set => dataSource = value; }
        public string Database { get => database; set => database = value; }
        public string User { get => user; set => user = value; }
        public string Password { get => password; set => password = value; }
    }
}
