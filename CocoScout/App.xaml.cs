using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CocoScout
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void ExitHandler(object sender, ExitEventArgs e)
        {
            DataHandler.SaveSettings();
            DataHandler.SaveLocal(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\CocoScout\\backup.scout");
        }
    }
}
