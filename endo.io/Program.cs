using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace endo.io
{
    internal class Program
    {
        private const int COL_WIDTH = 7;

        public static readonly string[] HEADERS =
        {
            "12AM", "1AM", "2AM", "3AM", "4AM", "5AM", "6AM", "7AM", "8AM", "9AM", "10AM", "11AM",
            "12PM", "1PM", "2PM", "3PM", "4PM", "5PM", "6PM", "7PM", "8PM", "9PM", "10PM", "11PM"
        };

        private static void Main()
        {
            string testFilePath = Path.Combine(Assembly.GetExecutingAssembly().Location,
                @"..\..\..\TestFiles\SampleClarityExport_Cleaned.csv");

            PatientProfile profile = new PatientProfile("Profile1");
            LogAnalyzer analyzer = new LogAnalyzer(profile, testFilePath);

            if (analyzer.EventLog != null)
            {
                PrintRow<string>("", HEADERS, v => $"{v,COL_WIDTH}");
                Console.WriteLine(new string('-', 24 * COL_WIDTH + 13));

                PrintRow("Average",     analyzer.AverageByHour,     v => $"{v,COL_WIDTH:F1}");
                PrintRow("Variance",    analyzer.VarianceByHour,    v => $"{v,COL_WIDTH:F1}");
                PrintRow("Basal Rates", profile.BasalRates,         v => $"{v,COL_WIDTH:F1}");
                PrintRow("Suggestions", analyzer.BasalSuggestions,  v => $"{v,COL_WIDTH:+#.#;-#.#;0}");

                Console.WriteLine();
                Console.WriteLine($"Number of Readings:{analyzer.EventLog.Count,8}");
                Console.WriteLine($"Average BG:{analyzer.AverageBG,16:F}");
                Console.WriteLine($"Time In Range:{analyzer.TimeInRange,13:P1}");
            }

            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();        
        }

        private static void PrintRow<T>(string rowTitle, IEnumerable<T> rowData, Func<T, string> format)
        {
            Console.Write($"{rowTitle,-13}");
            foreach (var v in rowData)
                Console.Write(format(v));
            Console.WriteLine();
        }
    }
}