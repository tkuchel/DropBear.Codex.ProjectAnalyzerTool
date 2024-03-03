namespace DropBear.Codex.ProjectAnalyzerTool.Exceptions;

public class ApplicationAlreadyRunningException : Exception
{
    public ApplicationAlreadyRunningException(string message) : base(message)
    {
    }

    public ApplicationAlreadyRunningException()
    {
    }

    public ApplicationAlreadyRunningException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
