﻿using System;

namespace endo.io
{
    internal class UserProfile
    {
        public readonly string UserName;
        public readonly string FirstName;
        public double[] BasalRates { get; set; }
        public int TargetBg { get; set; }
        public int HighBg { get; set; }
        public int LowBg { get; set; }

        public UserProfile(string userName, String firstName, double[] basalRates,
            int targetBg, int highBg, int lowBg)
        {
            UserName = userName;
            FirstName = firstName;
            BasalRates = basalRates;
            TargetBg = targetBg;
            HighBg = highBg;
            LowBg = lowBg;
        }
    }
}