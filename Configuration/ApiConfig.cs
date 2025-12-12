using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessMonitor.App.Configuration
{
    public static class ApiConfig
    {
        public static string GetBaseUrl()
        {
            var env = Environment.GetEnvironmentVariable("RUN_ENV")?.ToLower();

            return env switch
            {
                "docker" => "http://localhost:8080/v1/processmonitor",
                _ => "https://localhost:7023/v1/processmonitor"
            };
        }
    }
}
