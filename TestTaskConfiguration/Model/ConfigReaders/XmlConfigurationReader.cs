using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TestTaskConfiguration.Model.Entites;

namespace TestTaskConfiguration.Model.ConfigReaders
{
    public class XmlConfigurationReader : IConfigReader
    {
        public static string Extention { get; } = ".xml";

        public Configuration ReadFromFile(string filepath)
        {
            if (!Path.GetExtension(filepath).Equals(Extention))
                throw new Exception($"Expected file extention {Extention}, got {Path.GetExtension(filepath)}, {filepath}");

            var config = new Configuration();

            var file = new XmlDocument();
            file.Load(filepath);

            var properties = typeof(Configuration).GetProperties();
            foreach (var property in properties)
            {
                var elements = file.GetElementsByTagName(property.Name.ToLower());
                if (elements.Count != 1 || elements.Item(0).InnerText is null)
                    throw new Exception("Configuration file is invalid " + filepath);
                property.SetValue(config, elements.Item(0).InnerText);
            }
            return config;
        }
    }
}
