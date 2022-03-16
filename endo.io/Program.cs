using System;
using System.Globalization;

namespace endo.io
{
    internal class Program
    {
        private static readonly string[] HOUR =
        {
            "12AM", "1AM", "2AM", "3AM", "4AM", "5AM", "6AM", "7AM", "8AM", "9AM", "10AM", "11AM",
            "12PM", "1PM", "2PM", "3PM", "4PM", "5PM", "6PM", "7PM", "8PM", "9PM", "10PM", "11PM"
        };
        private const int PAD = 7;
        private const string TEST_FILE = "C:\\Users\\shlom\\source\\repos\\szo94\\endo.io\\endo.io\\TestFiles\\SampleClarityExport_Cleaned.csv";

        static void Main()
        {
            PatientProfile profile = new PatientProfile("Profile1");

            LogAnalyzer analyzer = new LogAnalyzer(profile, TEST_FILE);
            if (analyzer.EventLog != null)
            {
                double timeInRange          = analyzer.TimeInRange;
                double[] averageByHour      = analyzer.AverageByHour;
                double[] varianceByHour     = analyzer.VarianceByHour;
                double[] basalSuggestions   = analyzer.BasalSuggestions;

                PrintHeader();
                PrintRow("Average",     analyzer.AverageByHour,     v => $"{v,PAD:F1}");
                PrintRow("Variance",    analyzer.VarianceByHour,    v => $"{v,PAD:F1}");
                PrintRow("Basal Rates", profile.BasalRates,         v => $"{v,PAD:F1}");
                PrintRow("Suggestions", analyzer.BasalSuggestions,  v => $"{v,PAD:+#.#;-#.#;0}");
                Console.WriteLine();
                Console.WriteLine($"Number of Readings:{analyzer.EventLog.Count,8}");
                Console.WriteLine($"Average BG:{analyzer.AverageBG,16:F}");
                Console.WriteLine($"Time In Range:{timeInRange,13:P1}");
            }

            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();        
        }

        private static void PrintHeader()
        {
            Console.Write("             ");
            foreach (string s in HOUR)
                Console.Write($"{s,PAD}");
            Console.Write("\n             ");
            for (int i = 0; i < (24 * PAD); i++)
                Console.Write('-');
            Console.WriteLine();
        }

        private static void PrintRow(string rowTitle, double[] rowData, Func<double, string> format)
        {
            Console.Write($"{rowTitle,-13}");
            foreach (var v in rowData)
                Console.Write(format(v));
            Console.WriteLine();
        }
    }
}