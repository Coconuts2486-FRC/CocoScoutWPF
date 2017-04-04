using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Runtime.CredentialManagement;
using CocoScout.Data;
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

            if(File.Exists(rootDirectory + "\\regionals.txt"))
            {
                Console.WriteLine("List of regionals:");
                Data.StaticData.EventList = LoadDataFile(rootDirectory + "\\regionals.txt");
                foreach (string s in Data.StaticData.EventList)
                    Console.WriteLine(" - " + s);
            }
            else
            {
                Console.WriteLine("Could not find regional data.");
            }
        }

        public static string[] LoadDataFile(string directory)
        {
            return File.ReadAllLines(directory);
        }

        public static void SaveSettings()
        {
            Serializer.Serialize(StaticData.Settings, "settings.txt");
        }

        public static void LoadSettings()
        {
            StaticData.Settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\CocoScout\\settings.txt")) ?? new Settings();
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
                // DO NOT DEPLOY TO A VCS WITH THIS.
                var options = new CredentialProfileOptions
                {
                    AccessKey = "",
                    SecretKey = ""
                };

                var profile = new CredentialProfile("scouting_db", options);
                profile.Region = RegionEndpoint.USWest2;
                var netSDKFile = new NetSDKCredentialsFile();
                netSDKFile.RegisterProfile(profile);

                AWSCredentials.Config = new AmazonDynamoDBConfig()
                {
                    ServiceURL = "s3.amazonaws.com",
                    RegionEndpoint = RegionEndpoint.USWest2
                };
                AWSCredentials.Client = new AmazonDynamoDBClient(AWSCredentials.Config);

                AWSCredentials.Table = Table.LoadTable(AWSCredentials.Client, "ScoutingDataBase");
            }
            catch(Exception) { Errors._AWSsuccess = false; }
        }

        public static void SaveDataCloud()
        {
            try
            {
                foreach (TeamData td in StaticData.TeamDataList)
                {
                    Document data = new Document();
                    data["TeamNumber"] = td.TeamNumber;
                    data["MatchNumber"] = -1;
                    data["Notes"] = td.Notes;
                    AWSCredentials.Table.PutItem(data);
                }
                foreach (MatchData md in StaticData.MatchDataList)
                {
                    Document data = new Document();
                    #region Uploading Data
                    data["TeamNumber"] = md.TeamNumber;
                    data["MatchNumber"] = md.MatchNumber;
                    data["User"] = md.User;
                    data["Event"] = md.Event;


                    data["AutoFuelHighSpeed"] = md.autoData.FuelHighSpeed.ToString();
                    data["AutoPressure"] = md.autoData.Pressure;
                    data["AutoGearSpot"] = md.autoData.GearSpot.ToString();
                    data["AutoPlacedGear"] = md.autoData.PlacedGear;

                    data["TeleOpClimbed"] = md.teleOpData.Climbed;
                    data["TeleOpClimbSpeed"] = md.teleOpData.ClimbSpeed.ToString();
                    data["TeleOpFuelSpeed"] = md.teleOpData.FuelSpeed.ToString();
                    data["TeleOpPressure"] = md.teleOpData.Pressure;
                    data["TeleOpGearsPlaced"] = md.teleOpData.GearsPlaced;
                    data["TeleOpGearPickupGround"] = md.teleOpData.GearsPickupGround;
                    #endregion
                    AWSCredentials.Table.PutItem(data);
                }
            }
            catch (Exception ex) { MessageBox.Show("There was an issue uploading to the database.\nMessage: " + ex.Message); }
        }

        public static void LoadDataCloud()
        {

        }
    }

    class AWSCredentials
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
