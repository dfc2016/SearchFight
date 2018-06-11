using System.Collections.Generic;

namespace SearchFight.Utilities
{
    interface IConsolePrinter
    {
        void PrintResult(IReadOnlyList<string> rowHeaders, IReadOnlyList<string> colHeaders, long[,] values, string formatString = "{0}");
        void PrintWinners(IReadOnlyList<KeyValuePair<string, string>> winners);
        void PrintTotalWinner(string winner);
        void PrintNormalizedWinner(string winner, string normalizedWinner);
    }
}
