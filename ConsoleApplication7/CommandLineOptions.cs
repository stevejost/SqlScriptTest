using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace ConsoleApplication7
{
    public class CommandLineOptions
    {
        [Option('d',"database", Required = true)]
        public string Database { get; set; }

        [Option('s',"server", Required = true)]
        public string Server { get; set; }
    }
}
