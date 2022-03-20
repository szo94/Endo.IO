﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using CsvHelper;

namespace endo.io
{
    internal class LogAnalyzer
    {
        private const int DEF_TARGET_BG = 100;
        private const int DEF_LOW_BG = 70;
        private const int DEF_HIGH_BG = 180;
        private const int OFFSET = 1;

        public static readonly string[] HOURS =
        {
            "12AM", "1AM", "2AM", "3AM", "4AM", "5AM", "6AM", "7AM", "8AM", "9AM", "10AM", "11AM",
            "12PM", "1PM", "2PM", "3PM", "4PM", "5PM", "6PM", "7PM", "8PM", "9PM", "10PM", "11PM"
        };

        public PatientProfile Profile { get; }
        public List<ClarityEvent> EventLog { get; }
        public double AverageBG { get; private set; }
        public double TimeInRange { get; private set; }
        public double[] AverageByHour { get; private set; }
        public double[] VarianceByHour { get; private set; }
        public double[] BasalSuggestions { get; private set; }

        public LogAnalyzer(PatientProfile profile, string filePath)
        {
            Profile = profile;

            try
            {
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<ClarityEventMap>();
                    EventLog = csv.GetRecords<ClarityEvent>().ToList();
                }
                Analyze();
            }
            catch (IOException)
            {
                Console.WriteLine($"Failed to read file");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to copy events: {ex.GetType()}");
            }
        }

        private void Analyze()
        {
            AverageBG           = EventLog.Average(e => e.GlucoseValue);
            TimeInRange         = EventLog.Count(e => e.GlucoseValue >= (Profile.LowBG ?? DEF_LOW_BG) &&
                                    e.GlucoseValue <= (Profile.HighBG ?? DEF_HIGH_BG)) / (double) EventLog.Count;
            AverageByHour       = GetAverageByHour();
            VarianceByHour      = GetVarianceByHour(AverageByHour);
            BasalSuggestions    = GetBasalSuggestions(VarianceByHour);
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
            for (int i = 0; i < 23; i++)
            {
                int targetHour = (i - OFFSET + 24) % 24; // offset -1 hrs
                basalSuggestions[targetHour] = (varianceByHour[i] / (Profile.TargetBG ?? DEF_TARGET_BG)) * Profile.BasalRates[i];
            }

            return basalSuggestions;
        }
    }
}