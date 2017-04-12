using CocoScout.Data;
using CocoScout.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
            UserNameTextbox.DataContext = StaticDataViewModel.DataList.Settings;
            UpdateRegional();
        }

        private void RegionalSelected(object sender, SelectionChangedEventArgs e)
        {
            StaticDataViewModel.DataList.Settings.Event = ((ComboBoxItem)RegionalComboBox.SelectedItem).Content.ToString();
        }

        public void UpdateRegional()
        {
            RegionalComboBox.SelectedItem = StaticDataViewModel.DataList.Settings.Event.ToString();
        }

        private void LoadCloudEvent(object sender, RoutedEventArgs e)
        {
            DataHandler.LoadDataCloud();
        }

        private async void SaveCloudEvent(object sender, RoutedEventArgs e)
        {
            var window = Application.Current.MainWindow as MetroWindow;
            var result = await window.ShowInputAsync("Save to Cloud", "Please enter the password.");
            if (result == "@CocoNuts")
                DataHandler.SaveDataCloud();
            else if (String.IsNullOrEmpty(result)) ;
            else
                await window.ShowMessageAsync("Save to Cloud", "Incorrect password.");
        }

        private void SaveLocallyEvent(object sender, RoutedEventArgs e)
        {
            DataHandler.SaveLocal();
        }

        private void LoadLocallyClick(object sender, RoutedEventArgs e)
        {
            DataHandler.LoadLocal();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            foreach(ComboBoxItem item in RegionalComboBox.Items)
            {
                if(item.Content.ToString() == StaticDataViewModel.DataList.Settings.Event)
                {
                    RegionalComboBox.SelectedValue = item;
                    break;
                }
            }
        }
    }
}
