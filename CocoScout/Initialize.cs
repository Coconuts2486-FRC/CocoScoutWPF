using CocoScout.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CocoScout
{
    class Initialize
    {
        public static void Run()
        {
            LoadRegionals();
            LoadSettings();
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
            StaticData.Settings = (Settings) Serializer.Deserialize("settings.txt");
        }
    }
}
