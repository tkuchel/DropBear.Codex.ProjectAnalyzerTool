using DropBear.Codex.ProjectAnalyzerTool.Commands;
using DropBear.Codex.ProjectAnalyzerTool.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ZLogger;
using ZLogger.Providers;

namespace DropBear.Codex.ProjectAnalyzerTool;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = ConsoleApp.CreateBuilder(args);
        builder.ConfigureServices((ctx, services) =>
        {
            // Register appconfig.json to IOption<MyConfig>
            services.Configure<AnalyzerConfiguration>(ctx.Configuration);

            // Using Cysharp/ZLogger for logging to file
            ConfigureZLogger(services);
        });

        var app = builder.Build();

        app.AddCommands<AnalyzeCommands>();
        app.AddCommands<ExitCommands>();
        
        app.Run();
    }

    /// <summary>
    ///     Configures ZLogger logging services.
    /// </summary>
    /// <param name="services">The IServiceCollection to add logging services to.</param>
    private static void ConfigureZLogger(IServiceCollection services) =>
        services.AddLogging(builder =>
        {
            builder.ClearProviders()
                .SetMinimumLevel(LogLevel.Information)
                .AddZLoggerConsole(options =>
                {
                    options.UseJsonFormatter(formatter =>
                    {
                        formatter.IncludeProperties = IncludeProperties.Timestamp | IncludeProperties.Message |
                                                      IncludeProperties.Exception | IncludeProperties.LogLevel |
                                                      IncludeProperties.CategoryName;
                    });
                })
                .AddZLoggerRollingFile(options =>
                {
                    options.FilePathSelector = (timestamp, sequenceNumber) =>
                        $"logs/{timestamp.ToLocalTime():yyyy-MM-dd}_{sequenceNumber:000}.log";
                    options.RollingInterval = RollingInterval.Day;
                    options.RollingSizeKB = 1024; // 1MB
                });
        });
}
