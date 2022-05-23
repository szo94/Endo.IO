using System.Collections.Generic;
using System.Linq;

namespace Endo.IO.Data
{
    internal class DexcomClarityExport : IEventLog
    {
        private List<DexcomClarityEvent> dexcomClarityEvents;

        internal DexcomClarityExport(List<DexcomClarityEvent> dexcomClarityEvents)
        {
            this.dexcomClarityEvents = dexcomClarityEvents;
        }

        public List<Event> Events
        {
            get
            {
                return dexcomClarityEvents
                    .Select(e => new Event(e.Index, e.Timestamp, e.GlucoseValue))
                    .ToList();
            }
        }
    }
}
