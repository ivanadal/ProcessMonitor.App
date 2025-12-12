using ProcessMonitor.App.Http;


namespace ProcessMonitor.App.Interfaces
{
    public interface IConsoleAction
    {
        Task ExecuteAsync(ApiClient client);
    }
}
