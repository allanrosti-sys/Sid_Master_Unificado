using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SID.Standard.Control
{

    [Serializable]
    public class Connection
    {
        string name;

        public string Name { get => name; }

        public virtual DataTable Select(string string_CMD)
        {
            return null;
        }

        public virtual object SelectScalar(string string_CMD)
        {
            return null;
        }

        public virtual DataTable SelectRow(string string_CMD)
        {
            return null;
        }


        public virtual int Insert(string string_CMD)
        {
            return 0;
        }

        public virtual int Update(string string_CMD)
        {
            return 0;
        }

        public virtual int Delete(string string_CMD)
        {
            return 0;
        }

        public void Update()
        {

        }
    }
}
