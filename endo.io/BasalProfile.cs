namespace endo.io
{
    internal class BasalProfile
    {
        private const double MAX_BASAL_RATE  = 5.00;
        private const double DEF_BASAL_RATE  = 1.00;

        public string Name { get; }

        public double[] BasalRates { get; }

        public int? TargetBG { get; }
        public int? HighBG { get; }
        public int? LowBG { get; }

        // FOR TESTING
        public BasalProfile(string name)
        {
            Name        = name;
            BasalRates  = new double[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            TargetBG    = null;
            HighBG      = null;
            LowBG       = null;
        }

        public BasalProfile(string name, double[] basalRates, int? targetBG = null, int? highBG = null, int? lowBG = null) 
        {
            Name        = name;
            BasalRates  = basalRates;
            TargetBG    = targetBG;
            HighBG      = highBG;
            LowBG       = lowBG;
        }
    }
}
