using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SID.Designer.Control;
using SID.Designer.ToolBox;
using System.Windows.Controls;
using SID.Standard.Control;

namespace SID.Plugin.Control
{
    /// <summary>
    /// Modelo de iniciador de plugin
    /// </summary>
    public class Plugin_Model
    {
        Select_Manager select_Manager;
        Project project;

        public virtual string Name { get; set; }

        public virtual string Author { get; set; }

        public virtual string Caption { get; set; }

        public virtual bool EnablePlugin { get; set; }

        public virtual int ExecOrder { get; set; }

        public virtual DesignerControl DesignerControl { get; set; }

        public virtual Select_Manager Select_Manager { get { return select_Manager; } set { select_Manager = value; } }

        public virtual Project Project { get => project; set => project = value; }

        #region Construtor
        /// <summary>
        /// Construtor do plugin
        /// </summary>
        /// <param name="name">Nome do Plugin</param>
        public Plugin_Model(string name, int execOrder)
        {
            select_Manager = new Select_Manager();
            select_Manager.Title = name;
            Name = name;

            ExecOrder = execOrder;
            if (EnablePlugin)
            {
                INIT();
            }


        }
        #endregion


        #region Inicialização
        /// <summary>
        /// Executa inicialização do plugin
        /// </summary>
        public virtual void INIT() { }

        #endregion

        #region Atualização
        /// <summary>
        /// Executa atualização do plugin
        /// </summary>
        public virtual void UPDATE() { }
    }


    #endregion




}

