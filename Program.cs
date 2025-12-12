using ProcessMonitor.App;
using System.Text;
using System.Text.Json;
class Program
{
    static async Task Main()
    {
        var apiKey = Environment.GetEnvironmentVariable("ApiKey");

        using var httpClient = new HttpClient();

        httpClient.DefaultRequestHeaders.Add("X-Api-Key", apiKey);

        //Docker
        string baseUrl = "http://localhost:8080/v1/processmonitor";
        //Traditional deployment
        // string baseUrl = "https://localhost:7023/v1/processmonitor";

        string analyzeUrl = $"{baseUrl}/analyze";
        string historyUrl = $"{baseUrl}/history";
        string summaryUrl = $"{baseUrl}/summary";      

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
        int page = 1;
        int pageSize = 2;

        while (true)
        {
            string urlWithParams = $"{apiUrl}?Page={page}&PageSize={pageSize}";

            try
            {
                var response = await httpClient.GetAsync(urlWithParams);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                var paged = JsonSerializer.Deserialize<PaginatedResponse<JsonElement>>(json);

                if (paged == null || paged.Items == null || paged.Items.Count == 0)
                {
                    Console.WriteLine("\nNo more history items.");
                    break;
                }

                Console.WriteLine($"\n=== History Page {paged.Page} of {paged.TotalPages} ===");

                foreach (var item in paged.Items)
                {
                    string pretty = JsonSerializer.Serialize(item, new JsonSerializerOptions { WriteIndented = true });
                    Console.WriteLine(pretty);
                    Console.WriteLine();
                }

                // Ask user if they want next page
                if (paged.Page >= paged.TotalPages)
                {
                    Console.WriteLine("End of history.");
                    break;
                }

                Console.Write("Press Enter for next page, or type 'stop' to return to menu: ");
                string input = Console.ReadLine()?.Trim();
                if (string.Equals(input, "stop", StringComparison.OrdinalIgnoreCase))
                    break;

                page++; // next page
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                break;
            }
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
