using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        var apiKey = Environment.GetEnvironmentVariable("ApiKey");

        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("X-Api-Key", apiKey);

        string apiUrl = "https://localhost:7023/v1/processmonitor/analyze";

        Console.WriteLine("=== Process Monitor Console Client ===");
        Console.WriteLine("Type 'stop' at any time to exit.");
        Console.WriteLine();

        while (true)
        {
            // --- Read Action ---
            Console.Write("Enter Action: ");
            string action = Console.ReadLine()?.Trim();

            if (string.Equals(action, "stop", StringComparison.OrdinalIgnoreCase))
                break;

            if (string.IsNullOrWhiteSpace(action))
            {
                Console.WriteLine("Action cannot be empty.");
                continue;
            }

            // --- Read Guideline ---
            Console.Write("Enter Guideline: ");
            string guideline = Console.ReadLine()?.Trim();

            if (string.Equals(guideline, "stop", StringComparison.OrdinalIgnoreCase))
                break;

            if (string.IsNullOrWhiteSpace(guideline))
            {
                Console.WriteLine("Guideline cannot be empty.");
                continue;
            }

            // Build Request Body
            var requestBody = new
            {
                action,
                guideline
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                Console.WriteLine("\nSending request...\n");

                var response = await httpClient.PostAsync(apiUrl, content);
                response.EnsureSuccessStatusCode();

                var responseJson = await response.Content.ReadAsStringAsync();

                Console.WriteLine("=== API Response ===");
                Console.WriteLine(responseJson);
                Console.WriteLine("====================\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error calling API: " + ex.Message);
            }
        }

        Console.WriteLine("Program stopped. Goodbye!");
    }

}
