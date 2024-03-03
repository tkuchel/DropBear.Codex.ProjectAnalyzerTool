using Microsoft.Extensions.Logging;

namespace DropBear.Codex.ProjectAnalyzerTool.Commands;

public class AnalyzeCommands(ILogger<AnalyzeCommands> logger) : ConsoleAppBase, IDisposable
{
    private readonly ILogger<AnalyzeCommands> _logger = logger;

    // If implements IDisposable, called for cleanup
    public void Dispose()
    {
    }

    [Command("analyze", "Analyze the current directory.")]
    public void Analyze() =>
        // Context has any useful information.
        Console.WriteLine(Context.Timestamp);
}
