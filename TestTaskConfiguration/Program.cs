using System;
using TestTaskConfiguration.Model;
using TestTaskConfiguration.Model.ConfigReaders;

namespace TestTaskConfiguration
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var configs = new ConfigurationManager();
            var configFolderPath = "C:\\Users\\Maligaro\\source\\repos\\TestTaskConfiguration\\TestTaskConfiguration\\Configurations\\";
            configs.ReadFromFolder(configFolderPath);
        }
    }
}