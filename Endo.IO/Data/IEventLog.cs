using System.Collections.Generic;

namespace Endo.IO.Data
{
    internal interface IEventLog
    {
        public List<Event> Events { get; }
    }
}
