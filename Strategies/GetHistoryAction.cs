using ProcessMonitor.App.Http;
using ProcessMonitor.App.Interfaces;
using ProcessMonitor.App.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ProcessMonitor.App.Strategies
{
    public class GetHistoryAction : IConsoleAction
    {
        public async Task ExecuteAsync(ApiClient client)
        {
            int page = 1;
            int pageSize = 2;

            while (true)
            {
                var response = await client.GetAsync($"history?Page={page}&PageSize={pageSize}");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<PaginatedResponse<JsonElement>>(json);

                if (data?.Items == null || data.Items.Count == 0)
                {
                    Console.WriteLine("\nNo more items.");
                    return;
                }

                Console.WriteLine($"\n=== Page {data.Page} of {data.TotalPages} ===");
                foreach (var item in data.Items)
                {
                    Console.WriteLine(JsonSerializer.Serialize(item, new JsonSerializerOptions { WriteIndented = true }));
                }

                if (data.Page >= data.TotalPages) return;

                Console.Write("Press Enter to continue, 'stop' to exit: ");
                if (Console.ReadLine()?.Trim().ToLower() == "stop") return;
                page++;
            }
        }
    }
}