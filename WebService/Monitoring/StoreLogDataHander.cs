using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Monitoring.Core;

namespace WebApplication.Monitoring
{
	/// <summary>
	/// stores the logs in a mongodb database
	/// </summary>
	public class StoreLogDataHander : ILogDataHandler
	{
		private ICollectionRepository<LogData, Guid> _repository;

		public StoreLogDataHander(ICollectionRepository<LogData, Guid> repository)
		{
			_repository = repository;
		}

		public void Handle(LogData logdata)
		{
			_repository.SaveOrUpdate(logdata);
		}
	}
}