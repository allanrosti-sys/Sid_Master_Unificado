using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SID.Designer.Control
{
    public class View_Control
    {
        UserControl view;
        string name;

        public UserControl View { get => view;  }
        public string Name { get => name; }

        public View_Control(UserControl view, string name)
        {
            this.view = view;
            this.name = name;
        }

      
    }
}
