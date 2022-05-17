using System.Collections.Generic;
using System.Linq;
using endo.io.Data;

namespace endo.io
{
    internal class LogAnalyzer
    {
        private const int ADJ_OFFSET_HRS = 1;

        private readonly UserProfile userProfile;
        public List<ClarityEvent> EventLog { get; }
        public double AverageBG { get; private set; }
        public double TimeInRange { get; private set; }
        public double[] AverageByHour { get; private set; }
        public double[] VarianceByHour { get; private set; }
        public double[] BasalSuggestions { get; private set; }

        public LogAnalyzer(UserProfile profile, List<ClarityEvent> eventLog)
        {
            userProfile = profile;
            EventLog = eventLog;
            Analyze();
        }

        private void Analyze()
        {
            AverageBG           = EventLog.Average(e => e.GlucoseValue);
            TimeInRange         = EventLog.Count(e => e.GlucoseValue >= userProfile.LowBg &&
                                    e.GlucoseValue <= userProfile.HighBg) / (double) EventLog.Count;
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
            double[] varianceByHour = averageByHour.Select(a => a - userProfile.TargetBg).ToArray();
            return varianceByHour;
        }

        private double[] GetBasalSuggestions(double[] varianceByHour)
        {
            double[] basalSuggestions = new double[24];
            for (int i = 0; i < 23; i++)
            {
                int targetHour = (i - ADJ_OFFSET_HRS + 24) % 24;
                basalSuggestions[targetHour] = varianceByHour[i] / userProfile.TargetBg * (double) userProfile.BasalRates[i];
            }

            return basalSuggestions;
        }
    }
}