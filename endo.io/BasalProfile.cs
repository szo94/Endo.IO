using System;

namespace endo.io
{
    internal class BasalProfile
    {
        private const int       DEF_TARGET_BG   = 100;
        private const double    MAX_BASAL_RATE  = 5.00;
        private const double    DEF_BASAL_RATE  = 1.00;

        public string Name { get; set; }

        private double[] basalRates;
        public double[] BasalRates
        {
            get { return basalRates; }
            set
            {
                for (int i = 0; i < 24; i++)
                {
                    basalRates[i] = (value[i] >= 0 && value[i] <= 5) ? Math.Round(value[i], 1) : DEF_BASAL_RATE;
                }
            }
        }

        public int TargetBG { get; set; }

        public int? HighBG { get; set; }

        public int? LowBG { get; set; }

        public BasalProfile(string name, double[] basalRates,
            int targetBG = DEF_TARGET_BG, int? highBG = null, int? lowBG = null) 
        {
            Name        = name;
            BasalRates  = basalRates;
            TargetBG    = targetBG;
            HighBG      = highBG;
            LowBG       = lowBG;
        }
    }
}
