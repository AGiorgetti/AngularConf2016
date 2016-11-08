using System;
using System.Collections.Generic;

namespace ApplicationToMonitor
{
    public class LogData
    {
        public LogData()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// unique identifier for this log entry
        /// </summary>
        public Guid Id { get; set; }

        public string SourceId { get; set; }

        public string Exception { get; set; }

        public string Level { get; set; }

        public string Msg { get; set; }

        public DateTime Timestamp { get; set; }

        /// <summary>
        /// implement a proper serialization / deserialization mechanism
        /// 
        /// eventually support extended informations or custom informations
        /// 
        /// first implementation: make it easy for mongodb
        /// </summary>
        public IDictionary<string, object> ExtraElements { get; set; }

        /// <summary>
        /// maybe handle incoming messages in different ways, each one processes by its very own handler
        /// </summary>
        public string Type { get; set; }
    }

    static class LogMessageTypes
	{
		public const string Log = "log";
		public const string Heartbeat = "hb";
	}
}