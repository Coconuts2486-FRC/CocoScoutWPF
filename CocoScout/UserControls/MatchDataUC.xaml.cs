using System;
using System.Collections.Generic;
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
using CocoScout.Data;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace CocoScout.UserControls
{
    /// <summary>
    /// Interaction logic for MatchDataUC.xaml
    /// </summary>
    public partial class MatchDataUC : UserControl
    {
        MatchData data;
        public MatchDataUC()
        {
            data = new MatchData();
            InitializeComponent();
            this.DataContext = data;
        }

        public void LoadData(MatchData matchData)
        {
            data = matchData;
            this.DataContext = data;
        }

        private void ViewLoaded(object sender, RoutedEventArgs e)
        {
            AutoGearPlacementBox.ItemsSource = Enum.GetValues(typeof(Data.GearPlacement)).Cast<Data.GearPlacement>();
            AutoFuelHighSpeedBox.ItemsSource = Enum.GetValues(typeof(Data.Speed)).Cast<Speed>();
            TeleOpFuelSpeedBox.ItemsSource = Enum.GetValues(typeof(Data.Speed)).Cast<Speed>();
            ClimbSpeed.ItemsSource = Enum.GetValues(typeof(Data.Speed)).Cast<Speed>();
        }

        private void AddToList(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(data);
            data.User = StaticDataViewModel.DataList.Settings.UserName;
            data.Event = StaticDataViewModel.DataList.Settings.Event;
            //var window = Application.Current.MainWindow as MetroWindow;
            //var result = await window.ShowMessageAsync("Add Data to List", "Please enter the password.");
            //Console.WriteLine(")
            DataHandler.AddDataToList(data);
        }

        private void ClearAllFields(object sender, RoutedEventArgs e)
        {
            data.ResetAllFields();
            TeamNumberBox.Value = 0;
            MatchNumberBox.Value = 0;
            AutoGearPlacementBox.SelectedItem = GearPlacement.None;
            AutoPressureBox.Value = 0;
            TeleOpGearsPlaced.Value = 0;
            GroundPickupSwitch.IsChecked = false;
            TeleOpPressureBox.Value = 0;
            TeleOpFuelSpeedBox.SelectedItem = Speed.None;
            ClimbSpeed.SelectedItem = Speed.None;
        }
    }
}
