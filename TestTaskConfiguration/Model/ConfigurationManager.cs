using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskConfiguration.Model.ConfigReaders;
using TestTaskConfiguration.Model.Entites;

namespace TestTaskConfiguration.Model
{
    internal class ConfigurationManager
    {
        private Dictionary<string, IConfigReader> Readers { get; set; }
        public List<Configuration> Configurations { get; set; } 

        public ConfigurationManager() 
        {
            Configurations = new List<Configuration>(); 

            Readers = new Dictionary<string, IConfigReader>();
            Readers.Add(XmlConfigurationReader.Extention, new XmlConfigurationReader());
            Readers.Add(CsvConfigurationReader.Extention, new CsvConfigurationReader());
        }

        public void ReadFromFile(string filePath)
        {
            var extention = Path.GetExtension(filePath);
            if (Readers.ContainsKey(extention)) //check that we can read file of this format
            {
                Console.WriteLine("Reading new configuration from file: " + filePath + "\n");
                Configurations.Add(Readers[extention].ReadFromFile(filePath));
                WriteToConsole();
            }
            else
                Console.WriteLine("File extention is not supported, file: " + filePath + "\n");
        }

        public void ReadFromFolder(string path)
        {
            var files = Directory.GetFiles(path);
            foreach (var file in files)
                ReadFromFile(file);
        }

        public void WriteToConsole()
        {
            for (int i = 0; i < Configurations.Count; i++)
            {
                Console.WriteLine("Configuration " + i);
                typeof(Configuration)
                .GetProperties()
                .ToList()
                .ForEach(property => Console.WriteLine("\t" + property.Name + " : " + property.GetValue(Configurations[i])));
            }
        }
    }
}
