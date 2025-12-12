using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ProcessMonitor.App.Models
{
    public class PaginatedResponse<T>
    {
        [JsonPropertyName("page")]
        public int Page { get; set; }
        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }
        [JsonPropertyName("totalPages")]
        public int TotalPages { get; set; }
        [JsonPropertyName("totalItems")]
        public int TotalItems { get; set; }
        [JsonPropertyName("items")]
        public List<T> Items { get; set; }
    }
}
