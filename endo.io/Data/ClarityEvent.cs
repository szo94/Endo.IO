namespace Endo.IO.Data
{
    internal class ClarityEvent : Event
    {
        public  string  EventType { get; set; }
        public  string  EventSubtype { get; set; }
        public  string  PatientInfo { get; set; }
        public  string  DeviceInfo { get; set; }
        public  string  SourceDeviceId { get; set; }
        public  string  InsulinValue { get; set; }
        public  string  CarbValue { get; set; }
        public  string  Duration { get; set; }
        public  string  GlucoseRateOfChange { get; set; }
        public  long    TransmitterTime { get; set; }
        public  string  TransmitterId { get; set; }
    }
}