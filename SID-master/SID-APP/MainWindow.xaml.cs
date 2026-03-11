using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SID.Standard.Control;
using SID.Plugin.Control;
using SID.Designer.Control;

namespace SID_APP
{



    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {

        Project project;
        DesignerControl designerControl;
        List<Plugin_Model> plugins;

        public MainWindow()
        {
            InitializeComponent();


            Load_Designer();

           

        }

        void Load_Project()
        {
            this.Title = "SID - " + project.Name; 


            plugins = PluginManager.Current.Plugins.OrderBy(x => x.ExecOrder).ToList();
            foreach (Plugin_Model plugin in plugins)
            {
                if (plugin.ExecOrder != 0)
                {
                    //Main.SetViewer(p.Main);
                    plugin.DesignerControl = designerControl;
                    plugin.Project = project;
                    if (plugin.EnablePlugin)
                    {
                        plugin.INIT();
                        designerControl.Manager.Add(plugin.Select_Manager);

                    }

                }
            }

            designerControl.SetViewer(new SID.Designer.Pages.INITViewer());
        }

        #region Designer

        void Load_Designer()
        {
            designerControl = new DesignerControl();
            designerControl.Header = Header;
            designerControl.Main = Main;
            designerControl.Manager = Manager;
            designerControl.Load();

            //Manager.DesignerControl = designerControl;
            designerControl.SetViewer(new SID.Designer.Pages.INITViewer());
        }

        #endregion



        public void Open_Project()
        {
            MessageBox.Show(project.Path);

        }

        private void Header1_UpdateProject(object sender, Event_ProjectUpdate e)
        {
            project = e.Project;
            //manager.Project = project;
            //Open_Project();
            Load_Project();
        }

        private void Header_e_PluginManager(object sender, Event_WindowsChange e)
        {
            // main.Children.Clear();
            //Main.Children.Add(e.Windows);
        }
    }
}
