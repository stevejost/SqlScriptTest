using CommandLine;

namespace SqlScripterTest
{
    public class CommandLineOptions
    {
        [Option('d',"database", Required = true)]
        public string Database { get; set; }

        [Option('s',"server", Required = true)]
        public string Server { get; set; }
    }
}
