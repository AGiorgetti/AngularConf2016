using System;
using WebApplication.Monitoring.Core;
using WebApplication.Monitoring.Core.Events;

namespace WebApplication.Monitoring.Repository
{
    public class InMemoryLogDataRepository : InMemoryRepository<LogData, Guid>
    {

    }

    public class InMemoryLogEventRepository : InMemoryRepository<LogEvent, Guid>
    {

    }

    public class InMemoryHeartbeatEventRepository : InMemoryRepository<HeartbeatEvent, string>
    {
        
    }
}