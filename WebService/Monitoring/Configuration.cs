using AutoMapper;
using System;
using WebApplication.Monitoring.Repository;
using WebApplication.Monitoring.Core;
using WebApplication.Monitoring.Core.Events;

namespace WebApplication.Monitoring
{
    /// <summary>
    /// probably an IoC will be better
    /// </summary>
    public static class Configuration
	{
		//public static WindsorContainer Container { get; private set; }

		/// <summary>
		/// does all the configuration job!
		/// </summary>
		public static void Configure()
		{
			ConfigureAutomapper();

			//Container = new WindsorContainer();
			//Container.Kernel.Resolver.AddSubResolver(new CollectionResolver(Container.Kernel, true));

			// todo: use proper inversion of control to configure things
			// todo: implement proper configuration based on app.config settings (like what we have in log4net appenders)

			// make it properly pluggable (start with properly pluggable databases modules)
			LogDataReposiroty = new InMemoryLogDataRepository();
			LogEventReposiroty = new InMemoryLogEventRepository();
			HearbeatEventReposiroty = new InMemoryHeartbeatEventRepository();

			// configure the loggers

			// original loggers (before splitting in multiple events, maybe we will remove these)
			LogDataProcessor.AddHandler(new StoreLogDataHander(LogDataReposiroty));
			//LogDataProcessor.AddHandler(new SignalrLogDataHandler()); // send a notification to the connected clients

			// new message processing, decode and handle multiple messages formats
			var eventHandlers = new MonitoringEventsProcessor();
			eventHandlers.AddHandler(new LogMonitoringEventHandler(LogEventReposiroty));
			eventHandlers.AddHandler(new HeartbeatMonitoringEventHandler(HearbeatEventReposiroty));
			LogDataProcessor.AddHandler(eventHandlers);
		}

		/// <summary>
		/// get an instance of the log data repository
		/// </summary>
		static internal ICollectionRepository<LogData, Guid> LogDataReposiroty { get; private set; }
		static internal ICollectionRepository<LogEvent, Guid> LogEventReposiroty { get; private set; }
		static internal ICollectionRepository<HeartbeatEvent, string> HearbeatEventReposiroty { get; private set; }

        /// <summary>
        /// quick and easy object mapping between classes
        /// </summary>
        private static void ConfigureAutomapper()
		{
            Mapper.Initialize(cfg => {
                cfg.CreateMap<LogData, LogEvent>();
                cfg.CreateMap<LogData, HeartbeatEvent>().ForMember(
                        dest => dest.Id, map => map.MapFrom(src => src.SourceId)
                    );
            });
		}
	}
}