using System.Text;
using System.Text.Json;
class Program
{
    static async Task Main()
    {
        var apiKey = Environment.GetEnvironmentVariable("ApiKey");

        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("X-Api-Key", apiKey);

        string analyzeUrl = "https://localhost:7023/v1/processmonitor/analyze";
        string historyUrl = "https://localhost:7023/v1/processmonitor/history";
        string summaryUrl = "https://localhost:7023/v1/processmonitor/summary";

        Console.WriteLine("=== Process Monitor Console Client ===");

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Select an option:");
            Console.WriteLine("1) Analyze");
            Console.WriteLine("2) Get History");
            Console.WriteLine("3) Get Summary");
            Console.WriteLine("0) Exit");
            Console.Write("Choice: ");

            string choice = Console.ReadLine()?.Trim();

            if (choice == "0" ||
                string.Equals(choice, "exit", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Goodbye!");
                break;
            }

            switch (choice)
            {
                case "1":
                    await AnalyzeAsync(httpClient, analyzeUrl);
                    break;

                case "2":
                    await GetHistoryAsync(httpClient, historyUrl);
                    break;

                case "3":
                    await GetSummaryAsync(httpClient, summaryUrl);
                    break;

                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }

    static async Task AnalyzeAsync(HttpClient httpClient, string apiUrl)
    {
        Console.Write("Enter Action: ");
        string action = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(action))
        {
            Console.WriteLine("Action cannot be empty.");
            return;
        }

        Console.Write("Enter Guideline: ");
        string guideline = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(guideline))
        {
            Console.WriteLine("Guideline cannot be empty.");
            return;
        }

        var requestBody = new { action, guideline };
        var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

        try
        {
            Console.WriteLine("\nSending Analyze request...\n");

            var response = await httpClient.PostAsync(apiUrl, content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine("=== Analyze Result ===");
            Console.WriteLine(json);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    static async Task GetHistoryAsync(HttpClient httpClient, string apiUrl)
    {
        try
        {
            Console.WriteLine("\nRequesting History...\n");

            var response = await httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            try
            {
                var items = JsonSerializer.Deserialize<List<JsonElement>>(json);

                Console.WriteLine("=== History ===");

                foreach (var item in items)
                {
                    string pretty = JsonSerializer.Serialize(
                        item,
                        new JsonSerializerOptions { WriteIndented = true }
                    );

                    Console.WriteLine(pretty);
                    Console.WriteLine();
                }

                return;
            }
            catch
            {
                Console.WriteLine("=== History (raw) ===");
                Console.WriteLine(json);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }


    static async Task GetSummaryAsync(HttpClient httpClient, string apiUrl)
    {
        try
        {
            Console.WriteLine("\nRequesting Summary...\n");

            var response = await httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine("=== Summary ===");
            Console.WriteLine(json);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

}
