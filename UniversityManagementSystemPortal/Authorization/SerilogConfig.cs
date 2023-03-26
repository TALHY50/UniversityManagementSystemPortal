using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Sinks.SystemConsole.Themes;

namespace UniversityManagementSystemPortal.Authorization
{
    public static class SerilogConfig
    {
         private static readonly string LogDirectory = Path.Combine("wwwroot", "log");

        public static Serilog.Core.Logger CreateLogger()
        {
            var logLevel = LogEventLevel.Information;
            var outputTemplate = "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz}] [{Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}";

            var loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Is(logLevel)
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .WriteTo.Console(outputTemplate: outputTemplate, theme: AnsiConsoleTheme.Literate)
                .WriteTo.File(Path.Combine(LogDirectory, "log.txt"), outputTemplate: outputTemplate, rollingInterval: RollingInterval.Day)
                .WriteTo.Logger(lc => lc
                    .MinimumLevel.Is(LogEventLevel.Warning)
                    .WriteTo.File(Path.Combine(LogDirectory, "warning", "warning.txt"), outputTemplate: outputTemplate, rollingInterval: RollingInterval.Day))
                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(e => e.Level >= LogEventLevel.Error)
                    .WriteTo.File(Path.Combine(LogDirectory, "error", "errors.txt"), outputTemplate: outputTemplate, rollingInterval: RollingInterval.Day))
                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Fatal)
                    .WriteTo.File(Path.Combine(LogDirectory, "fatal", "fatal.txt"), outputTemplate: outputTemplate, rollingInterval: RollingInterval.Day));

            return loggerConfiguration.CreateLogger();
        }
    }


}
