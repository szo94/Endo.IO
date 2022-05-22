using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using Endo.IO.Data;

namespace Endo.IO
{
    internal class ClarityExportReader
    {
        private readonly string filePath;

        public ClarityExportReader(string filePath)
        {
            this.filePath = filePath;
        }

        public List<ClarityEvent> ReadFile()
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<ClarityEventMap>();
            return csv.GetRecords<ClarityEvent>().ToList();
        }
    }
}
