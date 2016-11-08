using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using WebApplication.Monitoring.Core.Events;
using WebApplication.Monitoring.Core;

namespace WebApplication.Monitoring
{
    /// <summary>
    /// some helper function used to parse incoming data into useful classes
    /// </summary>
    public static class LogMessageParser
    {
        internal static MonitoringEvent Parse(LogData logData)
        {
            switch (logData.Type)
            {
                case LogMessageTypes.Log:
                    return Mapper.Map<LogEvent>(logData);
                case LogMessageTypes.Heartbeat:
                    return Mapper.Map<HeartbeatEvent>(logData);
            }
            return null;
        }
    }
}