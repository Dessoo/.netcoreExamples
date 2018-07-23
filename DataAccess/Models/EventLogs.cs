using System;

namespace DataAccess.Models
{
    public partial class EventLog
    {
        public long Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int? EventId { get; set; }
        public string LogLevel { get; set; }
        public string Logger { get; set; }
        public string Message { get; set; }
        public string Host { get; set; }
        public string Server { get; set; }
    }
}
