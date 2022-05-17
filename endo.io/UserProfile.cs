using System;
using System.Collections.Generic;

namespace Endo.IO
{
    internal class UserProfile
    {
        private const int DEF_TARGET_BG = 100;
        private const int DEF_HIGH_BG = 140;
        private const int DEF_LOW_BG = 70;
        
        public readonly string UserName;
        public readonly string FirstName;
        public List<decimal?> BasalRates { get; private set; }
        public int TargetBg { get; private set; }
        public int HighBg { get; private set; }
        public int LowBg { get; private set; }

        public UserProfile(string userName, String firstName, List<decimal?> basalRates,
            int? targetBg, int? highBg, int? lowBg)
        {
            UserName = userName;
            FirstName = firstName;
            BasalRates = basalRates;
            TargetBg = targetBg ?? DEF_TARGET_BG;
            HighBg = highBg ?? DEF_HIGH_BG;
            LowBg = lowBg ?? DEF_LOW_BG;
        }
    }
}
