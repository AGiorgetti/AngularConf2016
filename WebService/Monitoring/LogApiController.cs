using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Monitoring.Core;
using WebApplication.Monitoring.Core.Events;

namespace WebApplication.Monitoring
{
    // very bad for security
    [EnableCors("CorsPolicy")]
    public class LogApiController : Controller
    {
        private readonly ICollectionRepository<LogData, Guid> _repository;
        private readonly ICollectionRepository<LogEvent, Guid> _logEventRepository;
        private readonly ICollectionRepository<HeartbeatEvent, string> _heartbeatEventRepository;

        public LogApiController()
        {
            _repository = Monitoring.Configuration.LogDataReposiroty;
            _logEventRepository = Monitoring.Configuration.LogEventReposiroty;
            _heartbeatEventRepository = Monitoring.Configuration.HearbeatEventReposiroty;
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

        [HttpPost]
        public void PostLogData([FromBody] LogData data)
        {
            LogDataProcessor.Process(data);
        }
    }
}
