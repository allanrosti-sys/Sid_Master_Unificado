using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SID.Designer.Control
{
    public class Event_WindowsChange : EventArgs
    {
        public UserControl Windows { get;  }

        public Event_WindowsChange(UserControl Windows)
        {
            this.Windows = Windows;
        }

    }
}
