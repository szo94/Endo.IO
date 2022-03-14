using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.TypeConversion;

namespace endo.io
{
    internal class LogAnalyzer
    {
        private const int DEF_TARGET_BG = 100;
        private const int DEF_LOW_BG = 70;
        private const int DEF_HIGH_BG = 180;
        private const int DEF_OFFSET = 1;

        public PatientProfile Profile { get; private set; }
        public List<Event> EventLog { get; private set; }
        public double AverageBG { get; private set; }
        public double TimeInRange { get; private set; }
        public double[] AverageByHour { get; private set; }
        public double[] VarianceByHour { get; private set; }
        public double[] BasalSuggestions { get; private set; }

        public LogAnalyzer(PatientProfile profile, string filepath)
        {
            Profile = profile;
            EventLog = ReadCleanedClarityExport(filepath);
            if (EventLog != null)
                Analyze();
        }

        private List<Event> ReadCleanedClarityExport(string filePath)
        {
            List<Event> eventLog = null;

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<ClarityEventMap>();
                try
                {
                    eventLog = csv.GetRecords<Event>().ToList();
                }
                catch (TypeConverterException ex) { }
            }

            Console.WriteLine(eventLog is {Count: > 0} ? $"Copied {eventLog.Count} events\n" : "Failed to copy events\n");

            return eventLog;
        }

        private void Analyze()
        {
            // get overall average BG
            AverageBG = EventLog.Average(e => e.GlucoseValue);

            // get overall time in range
            TimeInRange = EventLog.Count(e => e.GlucoseValue >= (Profile.LowBG ?? DEF_LOW_BG) &&
                                              e.GlucoseValue <= (Profile.HighBG ?? DEF_HIGH_BG));

            // get average BG by hour
            AverageByHour = GetAverageByHour();

            // get variance by hour
            VarianceByHour = GetVarianceByHour(AverageByHour);

            // get basal suggestions
            BasalSuggestions = GetBasalSuggestions(VarianceByHour);
        }

        private double[] GetAverageByHour()
        {
            double[] averageByHour = EventLog
                .GroupBy(e => e.Timestamp.Hour)
                .Select(grp => grp.Average(e => e.GlucoseValue))
                .ToArray();
            return averageByHour;
        }

        private double[] GetVarianceByHour(double[] averageByHour)
        {
            double[] varianceByHour = averageByHour.Select(a => a - DEF_TARGET_BG).ToArray();
            return varianceByHour;
        }

        private double[] GetBasalSuggestions(double[] varianceByHour)
        {
            double[] basalSuggestions = new double[24];
            for (int i = 0; i < 24; i++)
                basalSuggestions[i] = (varianceByHour[i] / (Profile.TargetBG ?? DEF_TARGET_BG)) * (Profile.BasalRates[i]);
            return basalSuggestions;
        }
    }
}