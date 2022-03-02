using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using CsvHelper.TypeConversion;

namespace endo.io
{
    internal class Program
    {
        const int DEF_TARGET_BG = 100;
        const int DEF_LOW_BG = 70;
        const int DEF_HIGH_BG = 180;
        
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

            Console.WriteLine($"Number of Readings: {events.Count}");
            Console.WriteLine($"Average BG: {averageBG:F}");
            Console.WriteLine($"Time In Range: {timeInRange:P1}");
            Console.WriteLine("Average By Hour:");
            PrintAverageByHour(averageByHour);

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
                    Console.WriteLine($"Copied {events.Count} events");
                }
                catch (TypeConverterException ex) { Console.WriteLine("Failed to copy events"); }
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
            double[] AverageByHour = events
                .GroupBy(e => e.Timestamp.Hour)
                .Select(grp => grp.Average(e => e.GlucoseValue))
                .ToArray();
            return AverageByHour;
        }

        static void PrintAverageByHour(double[] hourlyAverages)
        {
            for (int i = 0; i < 24; i++)
                Console.Write($"{hour[i],-6}");
            Console.WriteLine();
            for (int i = 0; i < (24 * 6); i++) { Console.Write("-"); }
            Console.WriteLine();
            for (int i = 0; i < 24; i++)
                Console.Write($"{hourlyAverages[i],-6:F1}");
        }
    }
}