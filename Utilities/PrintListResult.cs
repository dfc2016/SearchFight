using System;
using System.Collections.Generic;
using System.Linq;

namespace SearchFight.Utilities
{
    class TinyConsolePrinter : IConsolePrinter
    {
        public void PrintResult(IReadOnlyList<string> rowHeaders, IReadOnlyList<string> colHeaders, long[,] values, string formatString = "{0}")
        {
            if (rowHeaders == null)
                throw new ArgumentNullException("rowHeaders");

            if (colHeaders == null)
                throw new ArgumentNullException("colHeaders");

            if (values == null)
                throw new ArgumentNullException("values");

            if (rowHeaders.Count != values.GetLength(0))
                throw new InvalidOperationException("rowHeaders length must equal values first length.");

            if (colHeaders.Count != values.GetLength(1))
                throw new InvalidOperationException("colHeaders length must equal values second length.");

            Console.WriteLine();

            for (var ri = 0; ri < rowHeaders.Count; ri++)
            {
                Console.WriteLine("\n{0}: {1}", rowHeaders[ri], string.Join(
                    " ",
                    Enumerable.Range(0, colHeaders.Count).Select(ci =>
                        string.Format("{0}: {1}", colHeaders[ci], string.Format(formatString, values[ri, ci]))
                    )
                ));
            }
        }

        public void PrintWinners(IReadOnlyList<KeyValuePair<string, string>> winners)
        {
            Console.WriteLine();

            foreach (var winner in winners)
                Console.WriteLine("{0} winner: {1}", winner.Key, winner.Value);
        }

        public void PrintTotalWinner(string winner)
        {
            Console.WriteLine();

            Console.WriteLine("Total winner: {0}", winner);
        }

        public void PrintNormalizedWinner(string winner, string normalizedWinner)
        {
            Console.WriteLine();

            if (winner != normalizedWinner)
                Console.WriteLine("Normalized winner: {0}", normalizedWinner);
        }
    }
}
