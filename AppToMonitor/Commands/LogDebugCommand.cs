namespace ApplicationToMonitor.Commands
{
    class LogDebugCommand : IConsoleCommand
	{
		public LogDebugCommand()
		{
		}

		public void Execute()
		{
			Logger.Debug("Debug! Debug!");
		}
	}
}
