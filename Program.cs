using ProcessMonitor.App.Configuration;
using ProcessMonitor.App.Http;
using ProcessMonitor.App.Interfaces;
using ProcessMonitor.App.Strategies;

class Program
{
    static async Task Main()
    {
        var apiKey = Environment.GetEnvironmentVariable("ApiKey");
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            Console.WriteLine("Missing ApiKey environment variable.");
            return;
        }

        string baseUrl = ApiConfig.GetBaseUrl();
        var apiClient = new ApiClient(new HttpClient(), baseUrl, apiKey);

        var actions = new Dictionary<string, IConsoleAction>
        {
            ["1"] = new AnalyzeAction(),
            ["2"] = new GetHistoryAction(),
            ["3"] = new GetSummaryAction()
        };

        Console.WriteLine("=== Process Monitor Console Client ===");

        while (true)
        {
            Console.WriteLine("\n1) Analyze\n2) History\n3) Summary\n0) Exit");
            Console.Write("Choice: ");

            var choice = Console.ReadLine()?.Trim();

            if (choice == "0") break;

            if (actions.TryGetValue(choice, out var action))
                await action.ExecuteAsync(apiClient);
            else
                Console.WriteLine("Invalid choice.");
        }
    }
}
