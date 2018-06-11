using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SearchFight.SearchRunners;
using SearchFight.Utilities;

namespace SearchFight
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Run(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected exception has occurred: " + Environment.NewLine + ex.ToString());
            }

            Console.WriteLine("Press any key to quit");
            Console.Read();
        }

        private static void Run(string[] args)
        {
            try
            {
                if (args.Length == 0)
                    throw new ConfigurationException("Expected at least one argument!!!");

                var runners = Configuration.LoadConfiguration().SearchRunners.Where(runner => !runner.Disabled).ToList();
                var results = CollectResults(args, runners).Result;                

                IConsolePrinter consolePrinter = new TinyConsolePrinter();
                consolePrinter.PrintResult(results.Languages, results.Runners, results.Counts);
                consolePrinter.PrintWinners(results.Winners);
                consolePrinter.PrintTotalWinner(results.Winner);
                consolePrinter.PrintNormalizedWinner(results.Winner, results.NormalizedWinner);
                
            }
            catch (ConfigurationException ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
            }
            catch (AggregateException ex)
            {
                ex.Handle(e =>
                {
                    var searchException = e as SearchException;

                    if (searchException != null)
                    {
                        Console.WriteLine();
                        Console.WriteLine(string.Format("Runner '{0}' failed. {1}", searchException.Runner, searchException.Message));
                        return true;
                    }
                    else
                        return false;
                });
            }
        }

        private static async Task<Results> CollectResults(IReadOnlyList<string> languages, IReadOnlyList<ISearchRunner> runners)
        {
            using (var reporter = new ConsoleProgressReporter("Running..."))
            {
                return await Results.Collect(languages, runners, reporter);
            }
        }
    }
}
