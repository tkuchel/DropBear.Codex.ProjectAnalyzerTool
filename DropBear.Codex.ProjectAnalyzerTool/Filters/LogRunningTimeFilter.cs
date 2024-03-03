using ZLogger;

namespace DropBear.Codex.ProjectAnalyzerTool.Filters;

public class LogRunningTimeFilter : ConsoleAppFilter
{
    public override async ValueTask Invoke(ConsoleAppContext context, Func<ConsoleAppContext, ValueTask> next)
    {
        context.Logger.ZLogDebug($"Call method at {context.Timestamp.ToLocalTime()}");
        try
        {
            await next(context).ConfigureAwait(false);
            context.Logger.ZLogDebug(
                $"Call method Completed successfully, Elapsed:{DateTimeOffset.UtcNow - context.Timestamp}");
        }
        catch
        {
            context.Logger.ZLogDebug(
                $"Call method Completed Failed, Elapsed: {DateTimeOffset.UtcNow - context.Timestamp}");
            throw;
        }
    }
}
