using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Monitoring.Core.Events
{
	public interface IMonitoringEventHandler
	{
		/// <summary>
		/// Do something with the data
		/// </summary>
		/// <param name="evt"></param>
		void Handle(MonitoringEvent evt);
	}
}
