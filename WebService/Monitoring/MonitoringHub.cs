using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Infrastructure;
using WebApplication.Monitoring.Core;
using WebApplication.Monitoring.Core.Events;

namespace WebApplication.Monitoring
{
    [EnableCors("CorsPolicy")]
    public class MonitoringHub : Hub
	{
		private readonly ICollectionRepository<LogData, Guid> _repository;
		private readonly ICollectionRepository<LogEvent, Guid> _logEventRepository;
		private readonly ICollectionRepository<HeartbeatEvent, string> _heartbeatEventRepository;

		public MonitoringHub()
		{
			_repository = Configuration.LogDataReposiroty;
			_logEventRepository = Configuration.LogEventReposiroty;
			_heartbeatEventRepository = Configuration.HearbeatEventReposiroty;
		}

		public void Hello()
		{
			Clients.All.hello();
		}

		public string Echo(string message)
		{
			return message;
		}

		public IList<LogData> GetRawLogs(int limitNumber, string type)
		{
			var q = _repository.Queryable();
            if (!string.IsNullOrEmpty(type))
            {
                if (type != "null")
                    q = q.Where(o => o.Type == type);
                else
                    q = q.Where(o => o.Type == null || o.Type == "");
            }

            return q.OrderByDescending(o => o.Timestamp).Take(limitNumber).ToList();
		}

		public PaginatedData<LogEvent> GetLogs(string filterBy, int pageIndex, int pageSize)
		{
            var q = _logEventRepository.Queryable();

            if (!string.IsNullOrEmpty(filterBy))
            {
                var filterByLowercase = filterBy.ToLowerInvariant();
                q = q.Where(l => l.Msg.ToLowerInvariant().Contains(filterByLowercase) ||
                            l.Exception.ToLowerInvariant().Contains(filterByLowercase));
            }

            var count = q.Count();
            var data = q
                .OrderByDescending(o => o.Timestamp)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PaginatedData<LogEvent>(data, count, pageSize);
        }

        public LogEvent GetLog(Guid id)
        {
            return _logEventRepository.LoadById(id);
        }

		public IList<HeartbeatEvent> GetHeartbeats()
		{
			return _heartbeatEventRepository.Queryable().OrderBy(o => o.Id).ToList();
		}

		private static IHubContext GetHubContext()
		{
			var hubContext = Startup.ConnectionManager.GetHubContext<MonitoringHub>();
			return hubContext;
		}

		public static Task Message(object data)
		{
			var hubContext = GetHubContext();
			return hubContext.Clients.All.message(data);
		}

		/// <summary>
		/// call a 'log' function on all the connected clients
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static Task Log(object data)
		{
			var hubContext = GetHubContext();
			return hubContext.Clients.All.log(data);
		}

		/// <summary>
		/// call a 'heartbeat' function on all the connected clients
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static Task Heartbeat(object data)
		{
			var hubContext = GetHubContext();
			return hubContext.Clients.All.heartbeat(data);
		}
	}
}