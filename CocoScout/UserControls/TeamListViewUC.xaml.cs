using CocoScout.Data;
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

namespace CocoScout.UserControls
{
    /// <summary>
    /// Interaction logic for TeamListViewUC.xaml
    /// </summary>
    public partial class TeamListViewUC : UserControl
    {
        public TeamListViewUC()
        {
            InitializeComponent();
        }

        private void FilterUpdated(object sender, TextChangedEventArgs e)
        {

        }

        private void EditMenuClick(object sender, RoutedEventArgs e)
        {
            try
            {
                TeamData SelectedItem = (TeamData)((DataGrid)((ContextMenu)((MenuItem)sender).Parent).PlacementTarget).SelectedCells[0].Item;
                var window = Application.Current.MainWindow as MainWindow;
                window.TeamDataEntryUC.LoadData(SelectedItem);
                window.TabControlSource.SelectedIndex = 3;
            }
            catch (Exception ex) { }
        }

        private void DeleteMenuClick(object sender, RoutedEventArgs e)
        {
            try
            {
                TeamData teamData = (TeamData)((DataGrid)((ContextMenu)((MenuItem)sender).Parent).PlacementTarget).SelectedCells[0].Item;
                StaticData.TeamDataList.Remove(StaticData.TeamDataList.SingleOrDefault(s => s.TeamNumber == teamData.TeamNumber));
            }
            catch (Exception ex) { }
        }

        private void GridLoaded(object sender, RoutedEventArgs e)
        {
            var grid = sender as DataGrid;
            grid.ItemsSource = StaticData.TeamDataList;
        }
    }
}
