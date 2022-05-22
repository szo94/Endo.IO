using System;

namespace Endo.IO.Data
{
    internal class Event
    {
        public int Index { get; set; }
        public DateTime Timestamp { get; set; }
        public int GlucoseValue { get; set; }
    }
}
