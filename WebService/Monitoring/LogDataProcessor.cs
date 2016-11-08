using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Monitoring.Core;

namespace WebApplication.Monitoring
{
	/// <summary>
	/// a class that manages the log information coming in and handle them with different strategies
	/// 
	/// we might want to keep this data processor as a way to do something with the full message information
	/// before parsing it in different classes or similar
	/// </summary>
	public static class LogDataProcessor
	{
		private static List<ILogDataHandler> _handlers = new List<ILogDataHandler>();

		public static void AddHandler(ILogDataHandler handler)
		{
			_handlers.Add(handler);
		}

		public static void Process(LogData logData)
		{
			_handlers.ForEach(handler => handler.Handle(logData));
		}
	}
}