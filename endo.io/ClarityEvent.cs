﻿using System;

namespace endo.io
{
    internal class ClarityEvent
    {
        public int Index { get; set; }
        public DateTime Timestamp { get; set; }
        public string EventType { get; set; }
        public string EventSubtype { get; set; }
        public string PatientInfo { get; set; }
        public string DeviceInfo { get; set; }
        public string SourceDeviceId { get; set; }
        public int GlucoseValue { get; set; }
        public string InsulinValue { get; set; }
        public string CarbValue { get; set; }
        public string Duration { get; set; }
        public string GlucoseRateOfChange { get; set; }
        public long TransmitterTime { get; set; }
        public string TransmitterId { get; set; }
    }
}