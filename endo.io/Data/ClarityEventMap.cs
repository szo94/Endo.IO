using CsvHelper.Configuration;

namespace endo.io.Data
{
    internal class ClarityEventMap : ClassMap<ClarityEvent>
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