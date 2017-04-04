using CocoScout.Data;
using CocoScout.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace CocoScout.UserControls
{
    /// <summary>
    /// Interaction logic for SettingsUC.xaml
    /// </summary>
    public partial class SettingsUC : UserControl
    {
        public SettingsUC()
        {
            InitializeComponent();
            UserNameTextbox.DataContext = StaticData.Settings;
            UpdateRegional();
        }

        private void RegionalSelected(object sender, SelectionChangedEventArgs e)
        {
            StaticData.Settings.Event = ((ComboBoxItem)RegionalComboBox.SelectedItem).Content.ToString();
        }

        public void UpdateRegional()
        {
            RegionalComboBox.SelectedItem = StaticData.Settings.Event.ToString();
        }

        private void LoadCloudEvent(object sender, RoutedEventArgs e)
        {
            DataHandler.LoadDataCloud();
        }

        private void SaveCloudEvent(object sender, RoutedEventArgs e)
        {
            PasswordDialog dg = new PasswordDialog();
            dg.ShowDialog();
            if(PasswordDialog.IsAllowed == true)
            {
                Console.WriteLine("Password was correct.");
                DataHandler.SaveDataCloud();
            }
        }
    }
}
