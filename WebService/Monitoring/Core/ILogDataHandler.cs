using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Monitoring.Core
{
	/// <summary>
	/// interface that every module that does something with a logdata has to implement
	/// </summary>
	public interface ILogDataHandler
	{
		/// <summary>
		/// Do something with the data
		/// </summary>
		/// <param name="logdata"></param>
		void Handle(LogData logdata);
	}
}
