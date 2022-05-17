using System;

namespace endo.io.Data
{
    internal class Event
    {
        public int Index { get; set; }
        public DateTime Timestamp { get; set; }
        public int GlucoseValue { get; set; }
    }
}
