using Serilog;
using Serilog.Events;
using Serilog.Exceptions;


namespace UniversityManagementSystemPortal.Authorization
{
    public static class SerilogConfig
    {
        public static Serilog.Core.Logger CreateLogger( )
        {
            var logLevel = LogEventLevel.Information;
            var outputTemplate = "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz}] [{Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}";

            var loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Is(logLevel)
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .WriteTo.Console(outputTemplate: outputTemplate)
                .WriteTo.File("log.txt", outputTemplate: outputTemplate, rollingInterval: RollingInterval.Day)
                .WriteTo.Logger(lc => lc
                    .MinimumLevel.Is(LogEventLevel.Warning)
                    .WriteTo.File("warnings.txt", outputTemplate: outputTemplate, rollingInterval: RollingInterval.Day))
                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(e => e.Level >= LogEventLevel.Error)
                    .WriteTo.File("errors.txt", outputTemplate: outputTemplate, rollingInterval: RollingInterval.Day))
                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Fatal)
                    .WriteTo.File("fatal.txt", outputTemplate: outputTemplate, rollingInterval: RollingInterval.Day));

            return loggerConfiguration.CreateLogger();
        }
    }


}
