using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using CsvHelper;
using CsvHelper.Configuration;
using Endo.IO.Data;

namespace Endo.IO
{
    internal class DexcomClarityExportHandler : ILogHandler
    {
        private readonly string filePath;
        
        public DexcomClarityExportHandler(string filePath)
        {
            this.filePath = filePath;
        }

        public DexcomClarityExportHandler()
        {
            // if file path not passed in, get prompt user to select in file explorer
            filePath = GetFilePath();
        }

        IEventLog ILogHandler.GetLog()
        {
            // read file
            List<DexcomClarityEvent> events = ReadFile(filePath);

            // return event log
            return new DexcomClarityExport(events);
        }

        // open file explore for user to select input file, return file path
        private static string GetFilePath()
        {
            string filePath = "";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open Clarity Export";
            ofd.InitialDirectory =
                Path.Combine(Assembly.GetExecutingAssembly().Location, @"..\..\..\Resources");
            ofd.Filter = "CSV Files |*.csv";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filePath = ofd.FileName;
            }
            return filePath;
        }

        // read file using CsvHelper
        public List<DexcomClarityEvent> ReadFile(string filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<ClarityEventMap>();
            return csv.GetRecords<DexcomClarityEvent>().ToList();
        }
    }

    internal class ClarityEventMap : ClassMap<DexcomClarityEvent>
    {
        public ClarityEventMap()
        {
            Map(m => m.Index);
            Map(m => m.Timestamp).Name("Timestamp (YYYY-MM-DDThh:mm:ss)");
            Map(m => m.EventType).Name("Event Type");
            Map(m => m.EventSubtype).Name("Event Subtype");
            Map(m => m.PatientInfo).Name("Patient Info");
            Map(m => m.DeviceInfo).Name("Device Info");
            Map(m => m.SourceDeviceId).Name("Source Device ID");
            Map(m => m.GlucoseValue).Name("Glucose Value (mg/dL)");
            Map(m => m.InsulinValue).Name("Insulin Value (u)");
            Map(m => m.CarbValue).Name("Carb Value (grams)");
            Map(m => m.Duration).Name("Duration (hh:mm:ss)");
            Map(m => m.GlucoseRateOfChange).Name("Glucose Rate of Change (mg/dL/min)");
            Map(m => m.TransmitterTime).Name("Transmitter Time (Long Integer)");
            Map(m => m.TransmitterId).Name("Transmitter ID");
        }
    }
}
