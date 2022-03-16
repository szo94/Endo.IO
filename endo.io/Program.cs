using System;

namespace endo.io
{
    internal class Program
    {
        private const int CP = 7; // column padding

        static void Main()
        {
            PatientProfile profile = new PatientProfile("Profile1");

            string testFile =
                @"C:\Users\shlom\source\repos\szo94\endo.io\endo.io\TestFiles\SampleClarityExport_Cleaned.csv";

            LogAnalyzer analyzer = new LogAnalyzer(profile, testFile);

            if (analyzer.EventLog != null)
            {
                PrintRow("",            LogAnalyzer.HOURS,          v => $"{v,CP}");
                Console.WriteLine(new string('-', 24 * CP + 13));
                PrintRow("Average",     analyzer.AverageByHour,     v => $"{v,CP:F1}");
                PrintRow("Variance",    analyzer.VarianceByHour,    v => $"{v,CP:F1}");
                PrintRow("Basal Rates", profile.BasalRates,         v => $"{v,CP:F1}");
                PrintRow("Suggestions", analyzer.BasalSuggestions,  v => $"{v,CP:+#.#;-#.#;0}");
                Console.WriteLine();
                Console.WriteLine($"Number of Readings:{analyzer.EventLog.Count,8}");
                Console.WriteLine($"Average BG:{analyzer.AverageBG,16:F}");
                Console.WriteLine($"Time In Range:{analyzer.TimeInRange,13:P1}");
            }

            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();        
        }

        private static void PrintRow<T>(string rowTitle, T[] rowData, Func<T, string> format)
        {
            Console.Write($"{rowTitle,-13}");
            foreach (var v in rowData)
                Console.Write(format(v));
            Console.WriteLine();
        }
    }
}