using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using System.Linq;
using CsvHelper.TypeConversion;

// NOTE: expand console window to properly view output

namespace endo.io
{
    internal class Program
    {
        private const int DEF_TARGET_BG = 100;
        private const int DEF_LOW_BG    = 70;
        private const int DEF_HIGH_BG   = 180;
        private const int PAD           = 7;
        
        static readonly string[] hour =
        {
            "12AM", "1AM", "2AM", "3AM", "4AM", "5AM", "6AM", "7AM", "8AM", "9AM", "10AM", "11AM",
            "12PM", "1PM", "2PM", "3PM", "4PM", "5PM", "6PM", "7PM", "8PM", "9PM", "10PM", "11PM"
        };

        static void Main(string[] args)
        {
            BasalProfile testProfile = new BasalProfile("Test Profile");
            
            List<ClarityEvent> events = ReadCleanedClarityExport("C:\\Users\\shlom\\Downloads\\SampleClarityExport_Cleaned.csv");

            double      averageBG       = events.Average(e => e.GlucoseValue);
            double      timeInRange     = GetTimeInRange(events, testProfile);
            double[]    averageByHour   = GetAverageByHour(events);

            Console.WriteLine(events.Count > 0 ? $"Copied {events.Count} events\n" : "Failed to copy events\n");
            PrintHeader();
            PrintAverageByHour(averageByHour);
            PrintVarianceByHour(GetVarianceByHour(averageByHour));
            Console.WriteLine();
            Console.WriteLine($"Number of Readings:{events.Count,8}");
            Console.WriteLine($"Average BG:{averageBG,16:F}");
            Console.WriteLine($"Time In Range:{timeInRange,13:P1}");

            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
        }

        static List<ClarityEvent> ReadCleanedClarityExport(string filePath)
        {
            List<ClarityEvent> events = null;

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<ClarityEventMap>();
                try
                {
                    events = csv.GetRecords<ClarityEvent>().ToList();
                }
                catch (TypeConverterException ex) {}
            }

            return events;
        }

        static double GetTimeInRange<T>(List<T> events, BasalProfile basalProfile) where T : Event
        {
            int readingsInRange = events.Count(e => e.GlucoseValue >= (basalProfile.LowBG ?? DEF_LOW_BG) &&
                                                    e.GlucoseValue <= (basalProfile.HighBG ?? DEF_HIGH_BG));
            return (double) readingsInRange / events.Count;
        }

        static double[] GetAverageByHour<T>(List<T> events) where T : Event
        {
            double[] averageByHour = events
                .GroupBy(e => e.Timestamp.Hour)
                .Select(grp => grp.Average(e => e.GlucoseValue))
                .ToArray();
            return averageByHour;
        }

        static double[] GetVarianceByHour(double[] averageByHour)
        {
            return averageByHour.Select(a => a - DEF_TARGET_BG).ToArray();
        }

        static void PrintHeader()
        {
            for (int i = 0; i < 24; i++)
                Console.Write($"{hour[i],-PAD}");
            Console.WriteLine();
            for (int i = 0; i < 24 * PAD; i++)
                Console.Write('-');
            Console.WriteLine();
        }

        static void PrintAverageByHour(double[] averageByHour)
        {
            for (int i = 0; i < 24; i++)
                Console.Write($"{averageByHour[i],-PAD:F1}");
            Console.WriteLine();
        }

        static void PrintVarianceByHour(double[] varianceByHour)
        {
            for (int i = 0; i < 24; i++)
                Console.Write($"{varianceByHour[i],-PAD:+#.#;-#.#;0}");
            Console.WriteLine();
        }
    }
}