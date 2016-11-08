using System;
using System.Diagnostics;
using ApplicationToMonitor.Commands;

namespace ApplicationToMonitor
{
    public class Program
    {
        internal static string ApplicationName;

        static void Main(string[] args)
		{
            var rnd = new Random();
            Program.ApplicationName = "App " + rnd.Next();

			var heartbeat = new Heartbeat();
			heartbeat.Start(); // read the settings from the App.Config every 1 minutes

			Run();

			heartbeat.Stop();
		}

		private static void Run()
		{
			DisplayMenu();
			while (true)
			{
				Console.Write(@"Please enter a command: ");
				var cmd = Console.ReadLine().Trim();
				switch (cmd)
				{
					case "error":
						ExecuteCommand(new LogErrorCommand());
						break;
					case "debug":
						ExecuteCommand(new LogDebugCommand());
						break;
					case "heartbeat":
						ExecuteCommand(new HeartbeatCommand());
						break;
					case "q":
						return;
					case "":
						DisplayMenu();
						break;
					default:
						Console.WriteLine(@"Unknown command");
						break;
				}
			}
		}

		private static void ExecuteCommand(IConsoleCommand cmd)
		{
			var startDate = DateTime.UtcNow;
			var sw = new Stopwatch();
			sw.Start();
			Console.WriteLine("Command started at: " + DateTime.UtcNow);
			cmd.Execute();
			sw.Stop();
			var endDate = DateTime.UtcNow;
			Console.WriteLine("Command ended at: {0} - Elapsed SW: {1} - Elapsed DT: {2}",
				endDate, new TimeSpan(sw.ElapsedTicks), endDate - startDate);
		}

		private static void DisplayMenu()
		{
			Console.Clear();

			Console.WriteLine("==================================================");
			Console.WriteLine(" Application To Monitor: " + ApplicationName);
			Console.WriteLine("==================================================");
			Console.WriteLine();
			Console.WriteLine(" error          -> Log an Error message");
			Console.WriteLine(" debug          -> Log a Debug message");
			Console.WriteLine();
			Console.WriteLine(" heartbeat      -> send a heartbeat");
			Console.WriteLine();
			Console.WriteLine(" q              -> Quit");
			Console.WriteLine(" <enter>        -> Clear screen");
			Console.WriteLine();
			Console.WriteLine("==================================================");
		}
    }
}
