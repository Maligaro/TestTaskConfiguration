using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskConfiguration.Model.Entites;

namespace TestTaskConfiguration.Model.ConfigReaders
{
    internal interface IConfigReader
    {
        public static string Extention { get; }

        public Configuration ReadFromFile(string filepath);
    }
}
