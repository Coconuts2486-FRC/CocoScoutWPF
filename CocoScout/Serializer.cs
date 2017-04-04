using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace CocoScout
{
    class Serializer
    {
        public static void Serialize(object obj, string file)
        {
            try
            {
                string contents = JsonConvert.SerializeObject(obj);
                Console.WriteLine("Serialized " + file + ":\n" + contents);
                File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\CocoScout\\" + file, contents);
            }
            catch (DirectoryNotFoundException ex) { Console.WriteLine(ex.Message); }
        }
        public static object Deserialize(string file)
        {
            try
            {
                string contents = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\CocoScout\\" + file);
                Console.WriteLine("Deserialized " + file + ":\n" + contents);
                return JsonConvert.DeserializeObject(contents);
            }
            catch(FileNotFoundException ex) { Console.WriteLine(ex.Message); }
            return null;
        }
    }
}
