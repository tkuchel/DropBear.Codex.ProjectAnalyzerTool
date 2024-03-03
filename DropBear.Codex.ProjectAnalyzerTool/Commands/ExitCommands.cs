namespace DropBear.Codex.ProjectAnalyzerTool.Commands;

public class ExitCommands : ConsoleAppBase
{
    [Command("exit")]
    public static int ExitCode()
    {
        return 1;
    }
    
    // [Command("exit-with-task")]
    // public async Task<int> ExitCodeWithTask()
    // {
    //     return 1;
    // }
}
