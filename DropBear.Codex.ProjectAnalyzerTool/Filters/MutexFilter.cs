using DropBear.Codex.ProjectAnalyzerTool.Exceptions;

namespace DropBear.Codex.ProjectAnalyzerTool.Filters;

public class MutexFilter : ConsoleAppFilter
{
    public override async ValueTask Invoke(ConsoleAppContext context, Func<ConsoleAppContext, ValueTask> next)
    {
        var name = context.MethodInfo.DeclaringType?.Name + "." + context.MethodInfo.Name;
        using var mutex = new Mutex(initiallyOwned: true, name, out var createdNew);
        if (!createdNew) throw new ApplicationAlreadyRunningException($"already running {name} in another process.");

        await next(context).ConfigureAwait(false);
    }
}
