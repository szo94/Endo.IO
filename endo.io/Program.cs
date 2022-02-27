using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using System.Linq;
using CsvHelper.TypeConversion;

namespace endo.io
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<ClarityEvent> events = ReadCleanedClarityExport("C:\\Users\\shlom\\Downloads\\SampleClarityExport_Cleaned.csv");

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