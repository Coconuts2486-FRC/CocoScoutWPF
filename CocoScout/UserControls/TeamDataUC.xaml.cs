﻿using CocoScout.Data;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
    /// Interaction logic for TeamDataUC.xaml
    /// </summary>
    public partial class TeamDataUC : UserControl
    {
        TeamData data;
        public TeamDataUC()
        {
            data = new TeamData();
            InitializeComponent();
            this.DataContext = data;
        }

        public void LoadData(TeamData teamData)
        {
            data = teamData;
            this.DataContext = data;
        }

        private async void AddToList(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(data.Notes))
            {
                var window = Application.Current.MainWindow as MetroWindow;
                await window.ShowMessageAsync("Add Data to List", "Notes field is empty.", MessageDialogStyle.Affirmative);
            }
            else DataHandler.AddDataToList(data);
        }

        private void ClearAllFields(object sender, RoutedEventArgs e)
        {
            data.ResetAllFields();
            NotesBox.Text = "";
            TeamNumberBox.Value = 0;
        }
    }
}
