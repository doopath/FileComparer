using System;
using NLog;

namespace FileComparer
{
    public static class Program
    {
        private static Logger _consoleLogger = LogManager.GetLogger("FilesComparer.Program.Console");
        private static Logger _fileLogger = LogManager.GetLogger("FilesComparer.Program.File");

        public static void Main(string[] args)
        {
            _fileLogger.Debug("FilesComparer is starting...");

            LogReceivedArguments(args);

            _fileLogger.Debug("FilesComparer has finished working.\n\n");
        }

        private static void LogReceivedArguments(string[] args)
        {
            _fileLogger.Debug(args.Length < 1 ? "No argumetns were received." : $"Received arguments: {string.Join(" ", args)}.");
        }
    }
}
