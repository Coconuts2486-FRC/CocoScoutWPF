using CocoScout.Data;
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
            DataHandler.Run();
#if DEBUG
            Data.Settings settings = new Data.Settings() { Event = "Arizona North Regional", UserName = "Zach" };
            Serializer.Serialize(Data.StaticData.Settings, "settings.txt");
#endif
            InitializeComponent();
#if DEBUG
            StaticData.MatchDataList.Add(new MatchData()
            {
                MatchNumber = 1,
                TeamNumber = 2486,
                autoData = new AutoData()
                {
                    FuelHighSpeed = Speed.Medium,
                    GearSpot = GearPlacement.Center,
                    Pressure = 10
                },
                teleOpData = new TeleOpData()
                {
                    ClimbSpeed = Speed.Fast,
                    FuelSpeed = Speed.Slow,
                    GearsPickupGround = true,
                    GearsPlaced = 5,
                    Pressure = 50
                }
            });
            StaticData.TeamDataList.Add(new TeamData()
            {
                TeamNumber = 10,
                Notes = "Could not climb for two matches. Looks ugly."
            });
#endif
        }

        public void OnExit(object sender, EventArgs e)
        {
            DataHandler.OnExitHandler();
        }
    }
}
