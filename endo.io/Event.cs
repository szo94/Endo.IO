using System;

namespace endo.io
{
    internal class Event
    {
        public int Index { get; set; }
        public DateTime Timestamp { get; set; }
        public int GlucoseValue { get; set; }
    }
}
