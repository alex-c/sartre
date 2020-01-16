namespace SartreServer.Services.Exceptions
{
    /// <summary>
    /// Indicates that the given exception is of the "resource not found" class or exceptions.
    /// </summary>
    public interface INotFoundException
    {
        string Message { get; }
    }
}
