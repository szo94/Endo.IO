using System.Collections.Generic;
using System.Linq;

namespace endo.io
{
    internal class LogAnalyzer
    {
        private const int DEF_TARGET_BG = 100;
        private const int DEF_LOW_BG = 70;
        private const int DEF_HIGH_BG = 180;
        private const int OFFSET = 1;

        public PatientProfile Profile { get; }
        public List<ClarityEvent> EventLog { get; }
        public double AverageBG { get; private set; }
        public double TimeInRange { get; private set; }
        public double[] AverageByHour { get; private set; }
        public double[] VarianceByHour { get; private set; }
        public double[] BasalSuggestions { get; private set; }

        public LogAnalyzer(PatientProfile profile, List<ClarityEvent> eventLog)
        {
            Profile = profile;
            EventLog = eventLog;
            Analyze();
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