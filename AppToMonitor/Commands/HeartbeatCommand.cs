using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationToMonitor.Commands
{
	public class HeartbeatCommand : IConsoleCommand
	{
		public void Execute()
		{
			Logger.Debug("Heartbeat!");
			Logger.Heartbeat();
		}
	}
}
