using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Reflection;

namespace SID.Complex.Control
{
    /// <summary>
    /// Evento de atualização de valores no campos
    /// </summary>
    public class Event_UpdateValue : EventArgs
    {
        /// <summary>
        /// Propriedade atualizada
        /// </summary>
        public PropertyInfo PropertyInfo;
        /// <summary>
        /// Valor atualizado
        /// </summary>
        public object Value;
    }
}
