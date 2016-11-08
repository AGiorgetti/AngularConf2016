using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Monitoring.Core.Events
{
	/// <summary>
	/// informations that travels across the wire are transformed into monitoring events
	/// you can then react to these kind of events
	/// 
	/// a raw <see cref="LogData"/> message is converted to a <see cref="MonitoringEvent"/> derived class you can handle
	/// </summary>
	public abstract class MonitoringEvent
	{ }

	/// <summary>
	/// A logging message that will be displayed on a widget (can be of any type: error, debug, info, etc...)
	/// </summary>
	public class LogEvent : MonitoringEvent, IRepositoryObject<Guid>
	{
		public Guid Id { get; set; }

		public string SourceId { get; set; }

		public string Exception { get; set; }

		public string Level { get; set; }

		public string Msg { get; set; }

		public DateTime Timestamp { get; set; }

		/// <summary>
		/// eventually support extended informations or custom informations
		/// 
		/// first implementation: make it easy for mongodb
		/// </summary>
		public IDictionary<string, object> ExtraElements { get; set; }
	}

	/// <summary>
	/// An hearth beat event received form a remote source
	/// </summary>
	public class HeartbeatEvent : MonitoringEvent, IRepositoryObject<string>
	{
		//public Guid Id { get; set; }

		/// <summary>
		/// mapped to the sourceId
		/// </summary>
		public string Id { get; set; }

		public string Msg { get; set; }

		public DateTime Timestamp { get; set; }

		/// <summary>
		/// eventually support extended informations or custom informations
		/// 
		/// first implementation: make it easy for mongodb
		/// </summary>
		public IDictionary<string, object> ExtraElements { get; set; }
	}
}
