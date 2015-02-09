// Copyright (c) 2011-2015 iSEC Partners
// Author: Jason Bubolz, Peter Oehlert
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.


using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;
using Microsoft.SqlServer.Management.Common;
using SqlPermissions.Core.Permissions;
using SqlPermissions.Core.Trace;
using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;

namespace SqlPermissions
{
    class Program
    {
        private readonly Options _options;
        private bool _loopComplete;
        private TraceSource _traceSource;

        private class Options
        {
            public enum InputConfiguration
            {
                Invalid,
                File,
                SqlConnection
            };

            [Option('f', "file", HelpText = "SQL Profiler TRC event data input file.", MutuallyExclusiveSet = "File")]
            public string InputFile { get; set; }

            [Option('o', "output", HelpText = "Permission statements output file.")]
            public string OutputFile { get; set; }

            [Option('c', "connection", HelpText = "SQL connection string.", MutuallyExclusiveSet = "Connection")]
            public string SqlConnectionString { get; set; }

            [Option('t', "tdfFile", HelpText = "SQL Profiler TDF configuration file.", MutuallyExclusiveSet = "Connection")]
            public string TdfFile { get; set; }

            [Option('v', "verbose", DefaultValue = false, HelpText = "Print all messages to standard output.")]
            public bool Verbose { get; set; }

            [ParserState]
            public IParserState LastParserState { get; set; }

            [HelpOption]
            public string GetUsage()
            {
                return HelpText.AutoBuild(this,
                    (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
            }

            public Options.InputConfiguration ParseInputOption()
            {
                var fSet = !String.IsNullOrWhiteSpace(this.InputFile);
                var cSet = !String.IsNullOrWhiteSpace(this.SqlConnectionString);
                var tSet = !String.IsNullOrWhiteSpace(this.TdfFile);

                // -f is mutually exclusive with both -c and -t (library doesn't actually enforce this)
                // either the -f or -c argument must be present
                // if -c is present, then -t is also required
                if ((fSet && cSet) || (fSet && tSet) || !(fSet || cSet) || (cSet && !tSet))
                {
                    return InputConfiguration.Invalid;
                }
                else if (fSet)
                {
                    return InputConfiguration.File;
                }

                return InputConfiguration.SqlConnection;
            }
        }

        private void Report(IAccessStatement statement)
        {
            var accessStatement = statement as UnimplementedAccessStatement;
            if (accessStatement != null)
            {
                if (_options.Verbose)
                {
                    Console.WriteLine(statement.BuildSqlCommand());
                }
                else
                {
                    Console.WriteLine("/* Event Unimplemented: Name=[" + accessStatement.EventType + "] */");
                }
            }
            else
            {
                Console.WriteLine(statement.BuildSqlCommand());
            }
        }

        private void OnComplete()
        {
            Console.WriteLine("Processing done.");
            _loopComplete = true;
        }

        static int Main(string[] args)
        {
            try
            {
                var p = new Program(args);
                p.Run().Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1;
            }

            return 0;
        }

        private Program(String[] args)
        {
            _options = new Options();
            if (!CommandLine.Parser.Default.ParseArguments(args, _options))
            {
                throw new Exception("Unexpected command line.");
            }

            var iconfig = _options.ParseInputOption();
            if (iconfig == Options.InputConfiguration.Invalid)
            {
                Console.WriteLine("The input source must be a SQL Profiler capture file or a live SQL connection.");
                Console.WriteLine("\tTo use an input file, specify the TRC file with -f");
                Console.WriteLine(
                    "\tTo use a live connection, specify the SQL connection string with -c and the SQL Profiler configuration with -t");
                Console.WriteLine();
                Console.WriteLine(_options.GetUsage());
                Environment.Exit(-1);
            }

            if (iconfig == Options.InputConfiguration.File)
            {
                // use a file source
                _traceSource = new TraceSource(_options.InputFile);
            }
            else
            {
                // use a SQL connection source
                var csb = new SqlConnectionStringBuilder(_options.SqlConnectionString);
                var conn = csb.IntegratedSecurity
                    ? new SqlConnectionInfo(csb.DataSource)
                    : new SqlConnectionInfo(csb.DataSource, csb.UserID, csb.Password);
                _traceSource = new TraceSource(conn, _options.TdfFile);
            }

        }

        private async Task Run()
        {

            // Values are available here
            if (_options.Verbose)
            {
                var writer = new System.Diagnostics.TextWriterTraceListener(System.Console.Out);
                System.Diagnostics.Debug.Listeners.Add(writer);
            }


            // event loop
            var events = (from evt in _traceSource.GetEvents().ObserveOn(TaskPoolScheduler.Default)
                          // transition to task pool immediately to not block the trace
                          where null != evt
                          let permissions = evt.BuildPermissions()
                          from permission in permissions
                          select permission)
                .SubscribeOn(TaskPoolScheduler.Default);


            var statementsBag = new ConcurrentBag<Task<IAccessStatement>>();

            var subscription = events.Do(s => Console.WriteLine(s.BuildSqlCommand()))
                                     .GroupBy(s => s.BuildSqlCommand())
                                     .Select(grp => grp.FirstAsync().ToTask())
                                     .Subscribe(statementsBag.Add);
            using (subscription)
            {
                while (!_loopComplete)
                {
                    Thread.Sleep(250);
                    if (Console.KeyAvailable)
                    {
                        var key = Console.ReadKey();
                        if (key.Key == ConsoleKey.Escape)
                        {
                            _loopComplete = true;
                        }
                    }
                }

                // closing using block will dispose of subscription
            }


            // open file and write output
            if (!string.IsNullOrEmpty(_options.OutputFile))
            {

                var statements = new List<IAccessStatement>();
                foreach (var task in statementsBag)
                {
                    statements.Add(await task);
                }

                var ordered = statements.Where(s => !(s is UnimplementedAccessStatement))
                                       .OrderBy(s => s.Database)
                                       .ThenBy(s => null != s.Principals ? s.Principals.Select(p => p.Name).FirstOrDefault() : "")
                                       .ThenBy(s => null != s.SecurableObject ? s.SecurableObject.ObjectOwner : "")
                                       .ThenBy(s => null != s.SecurableObject ? s.SecurableObject.ObjectName : "")
                                       .ThenBy(s => null != s.Permissions ? s.Permissions.Select(p => p.Permission).FirstOrDefault() : "");


                var unimplemented = statements.Where(s => s is UnimplementedAccessStatement);

                using (var fstm = File.Open(_options.OutputFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                using (var writeBuff = new StreamWriter(fstm))
                {
                    String lastDb = null;
                    foreach (var accessStatement in ordered)
                    {
                        if (lastDb != accessStatement.Database)
                        {
                            // don't need these at the top
                            if (null != lastDb)
                            {
                                writeBuff.WriteLine("GO");
                                writeBuff.WriteLine();
                            }

                            writeBuff.WriteLine("-- Database " + accessStatement.Database);
                            writeBuff.WriteLine("USE [{0}]", accessStatement.Database);
                            writeBuff.WriteLine("GO");
                            writeBuff.WriteLine();
                            lastDb = accessStatement.Database;
                        }

                        writeBuff.WriteLine(accessStatement.BuildSqlCommand());
                    }

                    writeBuff.WriteLine();
                    writeBuff.WriteLine();

                    foreach (var accessStatement in unimplemented)
                    {
                        writeBuff.WriteLine("These types have not yet been implemented.");
                        // TODO: Need verbose version to dump the original generated statements
                        writeBuff.WriteLine(accessStatement.BuildSqlCommand());
                    }
                }
            }


            Console.WriteLine("Exiting");
        }
    }
}
