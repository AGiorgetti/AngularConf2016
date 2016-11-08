using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Monitoring.Core
{
	/// <summary>
	/// very bad: keep in sync with LogMessageType in SID.Monitoring.Logger
	/// </summary>
	public static class LogMessageTypes
	{
		public const string Log = "log";
		public const string Heartbeat = "hb";
	}
}
