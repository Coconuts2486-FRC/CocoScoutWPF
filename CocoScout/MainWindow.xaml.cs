using MahApps.Metro.Controls;
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
#if DEBUG
            Data.Settings settings = new Data.Settings() { Event = "Arizona North Regional", UserName = "Zach" };
            Serializer.Serialize(Data.StaticData.Settings, "settings.txt");
#endif
            Initialize.Run();
            InitializeComponent();
        }
    }
}
