using System;

namespace ApplicationToMonitor
{
    // dump to the console and send to the remote client
    public static class Logger
    {
        public static void Debug(string message)
        {
            Console.WriteLine(message);
        }

        public static void Error(string message, Exception ex)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = color;
            LogData data = CreateLogData("ERROR", message, ex);

            HttpService.PostLogData(data);
        }

        private static LogData CreateLogData(string level, string message, Exception ex)
        {
            var data = new LogData();
            data.Id = Guid.NewGuid();
            data.SourceId = Program.ApplicationName;
            if (ex != null)
            {
                data.Exception = ex.ToString() + ex.StackTrace.ToString();
            }
            data.Level = level;
            data.Msg = message;
            data.Timestamp = DateTime.UtcNow;
            data.Type = LogMessageTypes.Log;
            return data;
        }

        public static void Heartbeat()
        {
            var data = new LogData();
            data.Id = Guid.NewGuid();
            data.SourceId = Program.ApplicationName;
            data.Timestamp = DateTime.UtcNow;
            data.Type = LogMessageTypes.Heartbeat;

            HttpService.PostLogData(data);
        }
    }
}