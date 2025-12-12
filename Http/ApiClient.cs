using System.Text;
using System.Text.Json;

namespace ProcessMonitor.App.Http;

public class ApiClient
{
    private readonly HttpClient _client;
    private readonly string _baseUrl;

    public ApiClient(HttpClient client, string baseUrl, string apiKey)
    {
        _client = client;
        _baseUrl = baseUrl;

        _client.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
    }

    public Task<HttpResponseMessage> GetAsync(string path)
        => _client.GetAsync($"{_baseUrl}/{path}");

    public Task<HttpResponseMessage> PostAsync(string path, object body)
    {
        var content = new StringContent(
            JsonSerializer.Serialize(body),
            Encoding.UTF8,
            "application/json");

        return _client.PostAsync($"{_baseUrl}/{path}", content);
    }
}
