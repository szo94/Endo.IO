using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using System.Linq;
using System.Threading;
using CsvHelper.TypeConversion;

namespace endo.io
{
    internal class Program
    {
        public const int DEF_TARGET_BG = 100;
        public const int DEF_LOW_BG = 70;
        public const int DEF_HIGH_BG = 180;

        static void Main(string[] args)
        {
            BasalProfile profile1 = new BasalProfile("Profile 1",
                new double[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 });
            
            List<ClarityEvent> events = ReadCleanedClarityExport("C:\\Users\\shlom\\Downloads\\SampleClarityExport_Cleaned.csv");

            int numOfReadings = events.Count;
            double averageBG = events.Average(e => e.GlucoseValue);
            double timeInRange = events.Count(e => e.GlucoseValue >= (profile1.LowBG ?? DEF_LOW_BG) &&
                                                   e.GlucoseValue <= (profile1.HighBG ?? DEF_HIGH_BG)) / (double) numOfReadings;

            Console.WriteLine($"Number of Readings: {numOfReadings}");
            Console.WriteLine("Average BG: {0:F0}", averageBG);
            Console.WriteLine("Time In Range: {0:P1}", timeInRange);

            Console.WriteLine("Press any key to continue");
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
    }
}