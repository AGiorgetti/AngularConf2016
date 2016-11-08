using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Monitoring.Core;
using WebApplication.Monitoring.Core.Events;

namespace WebApplication.Monitoring
{
	/// <summary>
	/// process the different kinds of events and do something with them
	/// </summary>
	public class MonitoringEventsProcessor : ILogDataHandler
	{
		private List<IMonitoringEventHandler> _handlers = new List<IMonitoringEventHandler>();

		public void AddHandler(IMonitoringEventHandler handler)
		{
			_handlers.Add(handler);
		}

		public void Handle(LogData logdata)
		{
			var evt = LogMessageParser.Parse(logdata);
			if (evt != null)
				_handlers.ForEach(handler => handler.Handle(evt));
		}
	}

	public class LogMonitoringEventHandler : IMonitoringEventHandler
	{
		private ICollectionRepository<LogEvent, Guid> _repository;

		public LogMonitoringEventHandler(ICollectionRepository<LogEvent, Guid> repository)
		{
			_repository = repository;
		}

		public void Handle(MonitoringEvent evt)
		{
			var logEvent = evt as LogEvent;
			if (logEvent != null)
			{
				_repository.SaveOrUpdate(logEvent);
				// notify the clients (signalr)
				MonitoringHub.Log(logEvent);
			}
		}
	}

	public class HeartbeatMonitoringEventHandler : IMonitoringEventHandler
	{
		private ICollectionRepository<HeartbeatEvent, string> _repository;

		public HeartbeatMonitoringEventHandler(ICollectionRepository<HeartbeatEvent, string> repository)
		{
			_repository = repository;
		}

		public void Handle(MonitoringEvent evt)
		{
			var heartbeatEvent = evt as HeartbeatEvent;
			if (heartbeatEvent != null)
			{
				_repository.SaveOrUpdate(heartbeatEvent);
				// notify the clients (signalr)
				MonitoringHub.Heartbeat(heartbeatEvent);
			}
		}
	}
}