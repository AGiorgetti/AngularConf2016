using System;

namespace ApplicationToMonitor.Commands
{
    class LogErrorCommand : IConsoleCommand
    {
        public LogErrorCommand()
        {
        }

        public void Execute()
        {
            string errorMessage = "";
            try
            {
                Console.WriteLine("Type the error message");
                errorMessage = Console.ReadLine();

                var rnd = new Random().Next(1, 3);
                switch (rnd)
                {
                    case 1:
                        throw new Exception("Generic Exception");
                    case 2:
                        throw new NotImplementedException("Not Implemented");
                    default:
                        throw new NotSupportedException("Not Supported");
                }
            }
            catch (Exception ex)
            {
                if (String.IsNullOrWhiteSpace(errorMessage))
                {
                    errorMessage = "Error! Error!";
                };
                Logger.Error(errorMessage, ex);
            }
        }
    }
}
