namespace StudentGroupsManager.Logging
{
    public class CustomLogger : ILogger
    {
        private readonly string _loggerName;
        private readonly CustomLoggerProviderConfiguration _loggerConfig;

        public CustomLogger(string name,
                            CustomLoggerProviderConfiguration loggerConfig)
        {
            _loggerName = name;
            _loggerConfig = loggerConfig;
        }

        public IDisposable BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var msg = string.Format($"{logLevel}: {eventId}" +
                $" - {formatter(state, exception)}");

            WriteTextToFile(msg);
        }

        private void WriteTextToFile(string msg)
        {
            var filePath = Path.Combine(Environment.CurrentDirectory,"bin","Logs", $@"LOG-{DateTime.Now:yyyy-MM-dd}.txt");

            if (!File.Exists(filePath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                File.Create(filePath).Dispose();
            }

            using StreamWriter streamWriter = new StreamWriter(filePath, true);
            streamWriter.WriteLine(msg);
            streamWriter.Close();
        }
    }
}
