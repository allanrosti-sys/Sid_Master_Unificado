using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SID.Designer.Layout;
using SID.Designer.Control;
using System.Windows.Controls;

namespace SID.Designer.Control
{
    public class DesignerControl
    {
        public Header Header { get; set; }

        public Manager Manager { get; set; }

        public Main Main { get; set; }

        public void Load()
        {
            Header.DesignerControl = this;
            Main.DesignerControl = this;
        }


        public void SetViewer(View_Control view_Control)
        {
            Main.SetViewer(view_Control);
        }



    }
}
