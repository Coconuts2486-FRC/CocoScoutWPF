using CocoScout.Data;
using MahApps.Metro.Controls;
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
    /// Interaction logic for MatchListViewUC.xaml
    /// </summary>
    public partial class MatchListViewUC : UserControl
    {
        public MatchListViewUC()
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
                MatchData SelectedItem = (MatchData)((DataGrid)((ContextMenu)((MenuItem)sender).Parent).PlacementTarget).SelectedCells[0].Item;
                var window = Application.Current.MainWindow as MainWindow;
                window.MatchDataEntryUC.LoadData(SelectedItem);
                window.TabControlSource.SelectedIndex = 2;
            }
            catch(Exception ex) { }
        }

        private void DeleteMenuClick(object sender, RoutedEventArgs e)
        {
            try
            {
                MatchData matchData = (MatchData)((DataGrid)((ContextMenu)((MenuItem)sender).Parent).PlacementTarget).SelectedCells[0].Item;
                StaticData.MatchDataList.Remove(StaticData.MatchDataList.SingleOrDefault(s => s.TeamNumber == matchData.TeamNumber
                                                                             && s.MatchNumber == matchData.MatchNumber));
            }
            catch(Exception ex) { }
        }

        private void GridLoaded(object sender, RoutedEventArgs e)
        {
            var grid = sender as DataGrid;
            grid.ItemsSource = StaticData.MatchDataList;
        }
    }
}
