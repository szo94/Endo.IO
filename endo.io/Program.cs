using System;

namespace endo.io
{
    internal class Program
    {
        private const int PAD = 7;

        private static readonly string[] HOUR =
        {
            "12AM", "1AM", "2AM", "3AM", "4AM", "5AM", "6AM", "7AM", "8AM", "9AM", "10AM", "11AM",
            "12PM", "1PM", "2PM", "3PM", "4PM", "5PM", "6PM", "7PM", "8PM", "9PM", "10PM", "11PM"
        };

        static void Main()
        {
            PatientProfile profile = new PatientProfile("Profile1");

            string filePath = "C:\\Users\\shlom\\source\\repos\\szo94\\endo.io\\endo.io\\TestFiles\\SampleClarityExport_Cleaned.csv";

            LogAnalyzer analyzer = new LogAnalyzer(profile, filePath);
            if (analyzer.EventLog != null)
            {
                double timeInRange          = analyzer.TimeInRange;
                double[] averageByHour      = analyzer.AverageByHour;
                double[] varianceByHour     = analyzer.VarianceByHour;
                double[] basalSuggestions   = analyzer.BasalSuggestions;

                PrintHeader();
                PrintAverageByHour(averageByHour);
                PrintVarianceByHour(varianceByHour);
                PrintBasalRates(profile.BasalRates);
                PrintBasalSuggestions(basalSuggestions);
                Console.WriteLine();
                Console.WriteLine($"Number of Readings:{analyzer.EventLog.Count,8}");
                Console.WriteLine($"Average BG:{analyzer.AverageBG,16:F}");
                Console.WriteLine($"Time In Range:{timeInRange,13:P1}");
            }

            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();        
        }

        static void PrintHeader()
        {
            Console.Write("             ");
            for (int i = 0; i < 24; i++)
                Console.Write($"{HOUR[i],PAD}");
            Console.Write("\n             ");
            for (int i = 0; i < (24 * PAD); i++)
                Console.Write('-');
            Console.WriteLine();
        }

        static void PrintAverageByHour(double[] averageByHour)
        {
            Console.Write("Average      ");
            for (int i = 0; i < 24; i++)
                Console.Write($"{averageByHour[i],PAD:F1}");
            Console.WriteLine();
        }

        static void PrintVarianceByHour(double[] varianceByHour)
        {
            Console.Write("Variance     ");
            for (int i = 0; i < 24; i++)
                Console.Write($"{varianceByHour[i],PAD:+#.#;-#.#;0}");
            Console.WriteLine();
        }

        static void PrintBasalRates(double[] basalRates)
        {
            Console.Write("Basal Rates  ");
            for (int i = 0; i < 24; i++)
                Console.Write($"{basalRates[i],PAD:F1}");
            Console.WriteLine();
        }

        static void PrintBasalSuggestions(double[] basalSuggestions)
        {
            Console.Write("Suggestions  ");
            for (int i = 0; i < 24; i++)
                Console.Write($"{basalSuggestions[i],PAD:+#.#;-#.#;0}");
            Console.WriteLine();
        }
    }
}