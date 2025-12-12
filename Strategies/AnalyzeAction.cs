using ProcessMonitor.App.Http;
using ProcessMonitor.App.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessMonitor.App.Strategies
{
    public class AnalyzeAction : IConsoleAction
    {
        public async Task ExecuteAsync(ApiClient client)
        {
            Console.Write("Enter Action: ");
            var action = Console.ReadLine()?.Trim();

            Console.Write("Enter Guideline: ");
            var guideline = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(action) || string.IsNullOrWhiteSpace(guideline))
            {
                Console.WriteLine("Action and guideline are required.");
                return;
            }

            var response = await client.PostAsync("analyze", new { action, guideline });
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine("\n=== Analyze Result ===");
            Console.WriteLine(result);
        }
    }
}
