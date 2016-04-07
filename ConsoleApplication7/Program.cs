using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using Microsoft.SqlServer.Management.Smo;

namespace ConsoleApplication7
{
    class Program
    {
        static void Main(string[] args)
        {
            var opts = new CommandLineOptions();
            var result = CommandLine.Parser.Default.ParseArguments(args, opts);

            var dbName = opts.Database;
            var srv = new Server(new ServerConnection(opts.Server));

            var db = srv.Databases[dbName];

            Scripter script = new Scripter(srv);
            Scripter roleScripter = new Scripter(srv);

            script.Options.ScriptDrops = false;            
            script.Options.Indexes = true;              // To include indexes
            script.Options.DriAllConstraints = true;   // to include referential constraints in the script
            script.Options.Permissions = true;
            script.Options.TargetServerVersion = SqlServerVersion.Version105;
            script.Options.Triggers = true;
            
            
            foreach (DatabaseRole role in db.Roles)
            {
                if(!role.IsFixedRole) { 
                Console.WriteLine("-- Scripting for role " + role.Name);

                // Generating script for table tb
                System.Collections.Specialized.StringCollection sc = roleScripter.Script(new Urn[] { role.Urn });
                foreach (string st in sc)
                {
                    Console.WriteLine(st);
                }
                Console.WriteLine("--");
                    Console.WriteLine("GO");
                }
            }

            foreach (Table tb in db.Tables)
            {
                if (tb.IsSystemObject == false)
                {
                    Console.WriteLine("-- Scripting for table " + tb.Name);

                    // Generating script for table tb
                    System.Collections.Specialized.StringCollection sc = script.Script(new Urn[] { tb.Urn });
                    foreach (string st in sc)
                    {
                        Console.WriteLine(st);
                    }
                    Console.WriteLine("--");
                    Console.WriteLine("GO");
                }
            }

            foreach (View tb in db.Views)
            {
                if (tb.IsSystemObject == false)
                {
                    Console.WriteLine("-- Scripting for View " + tb.Name);

                    // Generating script for table tb
                    System.Collections.Specialized.StringCollection sc = script.Script(new Urn[] { tb.Urn });
                    foreach (string st in sc)
                    {
                        Console.WriteLine(st);
                    }
                    Console.WriteLine("--");
                }
            }

            foreach (StoredProcedure tb in db.StoredProcedures)
            {
                if (tb.IsSystemObject == false)
                {
                    Console.WriteLine("-- Scripting for Stored Procedure " + tb.Name);

                    // Generating script for table tb
                    System.Collections.Specialized.StringCollection sc = script.Script(new Urn[] { tb.Urn });
                    foreach (string st in sc)
                    {
                        Console.WriteLine(st);
                    }
                    Console.WriteLine("--");
                    Console.WriteLine("GO");
                }
            }

            foreach (UserDefinedFunction udf in db.UserDefinedFunctions)
            {
                if (udf.IsSystemObject == false)
                {
                    Console.WriteLine("-- Scripting for Stored Procedure " + udf.Name);

                    // Generating script for table tb
                    System.Collections.Specialized.StringCollection sc = script.Script(new Urn[] { udf.Urn });
                    foreach (string st in sc)
                    {
                        Console.WriteLine(st);
                    }
                    Console.WriteLine("--");
                    Console.WriteLine("GO");
                }
            }

            
        }
    }
}
