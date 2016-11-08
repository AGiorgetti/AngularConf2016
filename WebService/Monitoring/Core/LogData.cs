using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication.Monitoring.Core
{
	/// <summary>
	/// Abstract the logging information received from the external Applications 
	/// and translate it to a data structure that can be used easily by the dashboard
	/// 
	/// todo: handle extraelements in the porper way
	/// todo: first implementation will have all the properties of all the different messages kinds
	///       (it will be like the 'ever growing event streams' of incoming logs)
	/// todo: rename to LogMessage
	/// </summary>
	public class LogData : IRepositoryObject<Guid>
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
}