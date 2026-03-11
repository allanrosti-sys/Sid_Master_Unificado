using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using SID.Designer.Control;
using SID.Designer.ToolBox;

namespace SID.Plugin.Control
{
    /// <summary>
    /// Descreve um plugin na sua forma rudimentar.
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Nome do Plugin
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Criador do plugin
        /// </summary>
        string Author { get; }

        /// <summary>
        /// Titulo de apresentação do plugin
        /// </summary>
        string Caption { get; }
        

        DesignerControl DesignerControl { get; }

        Select_Manager Select_Manager { get; }


    }
}
