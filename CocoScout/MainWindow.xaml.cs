using CocoScout.Data;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CocoScout
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            DataHandler.Run();
#if DEBUG
            Data.Settings settings = new Data.Settings() { Event = "Arizona North Regional", UserName = "Zach" };
            Serializer.Serialize(Data.StaticData.Settings, "settings.txt");
#endif
            InitializeComponent();
        }

        public void OnExit(object sender, EventArgs e)
        {
            DataHandler.OnExitHandler();
        }
    }
}
