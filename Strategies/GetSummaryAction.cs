using ProcessMonitor.App.Http;
using ProcessMonitor.App.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessMonitor.App.Strategies
{
    public class GetSummaryAction : IConsoleAction
    {
        public async Task ExecuteAsync(ApiClient client)
        {
            var response = await client.GetAsync("summary");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine("\n=== Summary ===");
            Console.WriteLine(json);
        }
    }
}
