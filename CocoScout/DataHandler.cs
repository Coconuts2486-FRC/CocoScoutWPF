using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Runtime.CredentialManagement;
using CocoScout.Data;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CocoScout
{
    class DataHandler
    {
        public static void Run()
        {
            LoadRegionals();
            LoadSettings();
            SetAWSCredentials();
        }

        public static void LoadRegionals()
        {
            // Gets the directory of %AppData%
            string rootDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\CocoScout";
            Console.WriteLine("Loading regional data from " + rootDirectory);

            // If the directory does not exist, create it.
            if (!Directory.Exists(rootDirectory))
            {
                Directory.CreateDirectory(rootDirectory);
                Console.WriteLine("Created root directory at " + rootDirectory);
            }

            if (File.Exists(rootDirectory + "\\regionals.txt"))
            {
                Console.WriteLine("List of regionals:");
                StaticDataViewModel.DataList.EventList = LoadDataFile(rootDirectory + "\\regionals.txt");
                foreach (string s in StaticDataViewModel.DataList.EventList)
                    Console.WriteLine(" - " + s);
            }
            else
            {
                File.WriteAllText(rootDirectory + "\\regionals.txt", "Arizona North\nLos Angeles\nIdaho\nWorld Champs\nState Champs");
                Console.WriteLine("Could not find regional data.");
            }
        }

        public static string[] LoadDataFile(string directory)
        {
            return File.ReadAllLines(directory);
        }

        public static void SaveSettings()
        {
            Serializer.Serialize(StaticDataViewModel.DataList.Settings, "settings.txt");
        }

        public static void LoadSettings()
        {
            try
            {
                StaticDataViewModel.DataList.Settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\CocoScout\\settings.txt")) ?? new Settings();
            }
            catch (FileNotFoundException)
            {
                StaticDataViewModel.DataList.Settings = new Settings();
            }
        }

        public static void OnExitHandler()
        {
            SaveSettings();
        }

        public static void SetAWSCredentials()
        {
            try
            {
                // This is where the secret key is stored.
                var options = new CredentialProfileOptions
                {
                    AccessKey = "AKIAI7RKR52M4BUPUREQ",
                    SecretKey = "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"
                };

                var profile = new CredentialProfile("scouting_db", options);
                profile.Region = RegionEndpoint.USWest2;
                var netSDKFile = new NetSDKCredentialsFile();
                netSDKFile.RegisterProfile(profile);

                AWSConfig.Config = new AmazonDynamoDBConfig()
                {
                    ServiceURL = "s3.amazonaws.com",
                    RegionEndpoint = RegionEndpoint.USWest2
                };
                AWSConfig.Client = new AmazonDynamoDBClient(AWSConfig.Config);

                AWSConfig.Table = Table.LoadTable(AWSConfig.Client, "ScoutingDataBase");
            }
            catch (Exception) { Errors._AWSsuccess = false; }
        }

        public static void SaveDataCloud()
        {
            try
            {
                foreach (TeamData td in StaticDataViewModel.DataList.TeamDataList)
                {
                    Document data = new Document();
                    data["TeamNumber"] = td.TeamNumber;
                    data["MatchNumber"] = -1;
                    data["Event"] = StaticDataViewModel.DataList.Settings.Event;
                    data["Notes"] = td.Notes;
                    data["User"] = td.User;
                    AWSConfig.Table.PutItem(data);
                }
                foreach (MatchData md in StaticDataViewModel.DataList.MatchDataList)
                {
                    Document data = new Document();
                    #region Uploading Data
                    data["TeamNumber"] = md.TeamNumber;
                    data["MatchNumber"] = md.MatchNumber;
                    data["User"] = md.User;
                    data["Event"] = md.Event;


                    data["AutoFuelHighSpeed"] = md.AutoFuelHighSpeed.ToString();
                    data["AutoPressure"] = md.AutoPressure;
                    data["AutoGearSpot"] = md.AutoGearSpot.ToString();
                    data["AutoPlacedGear"] = md.AutoPlacedGear;

                    data["TeleOpClimbed"] = md.Climbed;
                    data["TeleOpClimbSpeed"] = md.ClimbSpeed.ToString();
                    data["TeleOpFuelSpeed"] = md.TeleOpFuelSpeed.ToString();
                    data["TeleOpPressure"] = md.TeleOpPressure;
                    data["TeleOpGearsPlaced"] = md.TeleOpGearsPlaced;
                    data["TeleOpGearPickupGround"] = md.TeleOpGearsPickupGround;
                    #endregion
                    AWSConfig.Table.PutItem(data);
                }
            }
            catch (Exception ex) { MessageBox.Show("There was an issue uploading to the database.\nMessage: " + ex.Message); }
        }

        public static void LoadDataCloud()
        {
            try
            {
                ScanFilter scanFilter = new ScanFilter();
                scanFilter.AddCondition("Event", ScanOperator.Equal, StaticDataViewModel.DataList.Settings.Event);
                scanFilter.AddCondition("Notes", ScanOperator.IsNull);
                Search MatchSearch = AWSConfig.Table.Scan(scanFilter);

                List<Document> documentList = new List<Document>();
                do
                {
                    documentList = MatchSearch.GetNextSet();
                    foreach (Document data in documentList)
                    {
                        try
                        {
                            MatchData matchData = new MatchData()
                            {
                                MatchNumber = (byte)data["MatchNumber"],
                                TeamNumber = (uint)data["TeamNumber"],
                                Event = (string)data["Event"],
                                User = data["User"],

                                AutoGearSpot = (GearPlacement)Enum.Parse(typeof(GearPlacement), data["AutoGearSpot"]),
                                AutoFuelHighSpeed = (Speed)Enum.Parse(typeof(Speed), data["AutoFuelHighSpeed"]),
                                AutoPressure = (byte)data["AutoPressure"],

                                TeleOpGearsPlaced = (byte)data["TeleOpGearsPlaced"],
                                TeleOpGearsPickupGround = (bool)data["TeleOpGearPickupGround"],
                                TeleOpFuelSpeed = (Speed)Enum.Parse(typeof(Speed), data["TeleOpFuelSpeed"]),
                                TeleOpPressure = (byte)data["TeleOpPressure"],
                                ClimbSpeed = (Speed)Enum.Parse(typeof(Speed), data["TeleOpClimbSpeed"])
                            };
                            AddDataToList(matchData);
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                    }
                }
                while (!MatchSearch.IsDone);

                scanFilter.RemoveCondition("Notes");
                scanFilter.AddCondition("Notes", ScanOperator.IsNotNull);
                Search TeamsSearch = AWSConfig.Table.Scan(scanFilter);

                do
                {
                    documentList = TeamsSearch.GetNextSet();
                    foreach (Document data in documentList)
                    {
                        try
                        {
                            TeamData teamData = new TeamData()
                            {
                                TeamNumber = (uint)data["TeamNumber"],
                                Notes = data["Notes"],
                                Event = data["Event"],
                                User = data["User"]
                            };
                            AddDataToList(teamData);
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                    }
                }
                while (!TeamsSearch.IsDone);
            }
            catch (Exception ex) { MessageBox.Show("There was an issue loading from the database.\nMessage: " + ex.Message); }
        }

        public static void AddDataToList(MatchData matchData)
        {
            StaticDataViewModel.DataList.MatchDataList.Remove(StaticDataViewModel.DataList.MatchDataList.SingleOrDefault(s => s.TeamNumber == matchData.TeamNumber
                                                                         && s.MatchNumber == matchData.MatchNumber));
            StaticDataViewModel.DataList.MatchDataList.Add(matchData);
        }

        public static void AddDataToList(TeamData teamData)
        {
            StaticDataViewModel.DataList.TeamDataList.Remove(StaticDataViewModel.DataList.TeamDataList.SingleOrDefault(s => s.TeamNumber == teamData.TeamNumber));
            StaticDataViewModel.DataList.TeamDataList.Add(teamData);
        }

        public static void SaveLocal()
        {
            SaveFileDialog dlg = new SaveFileDialog();

            dlg.FileName = StaticDataViewModel.DataList.Settings.Event + " - " + StaticDataViewModel.DataList.Settings.UserName;
            dlg.DefaultExt = ".scout";
            dlg.Filter = "Scouting Files (.scout)|*.scout";

            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                try
                {
                    string path = dlg.FileName;
                    Console.WriteLine("Saving data to " + path);
                    string data = JsonConvert.SerializeObject(StaticDataViewModel.DataList);
#if DEBUG
                    Console.WriteLine(data);
#endif
                    File.WriteAllText(path, data);
                }
                catch (InvalidOperationException) { ShowErrorAsync("Saving Error", "Storage is either full or file exists and is read only."); }
                catch (PathTooLongException) { ShowErrorAsync("Saving Error", "Path is too long."); }
            }
        }

        public static void SaveLocal(string directory)
        {
                try
                {
                    Console.WriteLine("Saving data to " + directory);
                    string data = JsonConvert.SerializeObject(StaticDataViewModel.DataList);
#if DEBUG
                    Console.WriteLine(data);
#endif
                    File.WriteAllText(directory, data);
                }
                catch (InvalidOperationException) { ShowErrorAsync("Saving Error", "Storage is either full or file exists and is read only."); }
                catch (PathTooLongException) { ShowErrorAsync("Saving Error", "Path is too long."); }
        }

        public static void LoadLocal()
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.DefaultExt = ".scout";
            dlg.Filter = "Scouting Files (.scout)|*.scout";

            bool? result = dlg.ShowDialog();
            if(result == true)
            {
                string path = dlg.FileName;
                string data = File.ReadAllText(path);
                StaticDataViewModel.DataList = JsonConvert.DeserializeObject<StaticData>(data);
                // LoadSettings();
            }
        }

        public static void LoadAppendLocal()
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.DefaultExt = ".scout";
            dlg.Filter = "Scouting Files (.scout)|*.scout";

            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                string path = dlg.FileName;
                string data = File.ReadAllText(path);
                StaticData list = JsonConvert.DeserializeObject<StaticData>(data);
                foreach(TeamData s in list.TeamDataList)
                {
                    StaticDataViewModel.DataList.TeamDataList.Add(s);
                }
                foreach(MatchData s in list.MatchDataList)
                {
                    StaticDataViewModel.DataList.MatchDataList.Add(s);
                }
            }
        }

        public static async void ShowErrorAsync(string title, string message)
        {
            var window = Application.Current.MainWindow as MetroWindow;
            await window.ShowMessageAsync(title, message, MessageDialogStyle.Affirmative);
        }
    }

    class AWSConfig
    {
        public static AmazonDynamoDBClient Client;
        public static AmazonDynamoDBConfig Config;
        public static Table Table;
    }

    class Errors
    {
        public static bool _AWSsuccess { get; set; } = true;
    }
}
