using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskConfiguration.Model.Entites;

namespace TestTaskConfiguration.Model.ConfigReaders
{
    public class CsvConfigurationReader : IConfigReader
    {
        public static string Extention { get; } = ".csv";
        public string Separator { get; set; } = ";";
        public Configuration ReadFromFile(string filepath)
        {
            if (!Path.GetExtension(filepath).Equals(Extention))
                throw new Exception($"Expected file extention {Extention}, got {Path.GetExtension(filepath)}, {filepath}");

            var lines = File.ReadLines(filepath).ToList();
            if (lines.Count != 2) throw new Exception("Configuration file is invalid " + filepath);
            var keys = lines[0].Split(Separator);
            var values = lines[1].Split(Separator);

            var properties = typeof(Configuration)
                .GetProperties()
                .ToList();

            var config = new Configuration();
            for (int i = 0; i < keys.Length; i++)
            {
                var key = keys[i].Trim();
                foreach (var property in properties)
                    if (property.Name.Trim().Equals(key, StringComparison.OrdinalIgnoreCase))
                    {
                        property.SetValue(config, values[i]);
                        break;
                    }
            }

            return config;
        }
    }
}
