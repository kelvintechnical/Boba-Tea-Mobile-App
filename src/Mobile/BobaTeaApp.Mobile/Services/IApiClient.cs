using System.Net.Http.Json;
using System.Text.Json;

namespace BobaTeaApp.Mobile.Services;

public interface IApiClient
{
    Task<T?> GetAsync<T>(string uri, CancellationToken cancellationToken = default);
    Task<TResponse?> PostAsync<TRequest, TResponse>(string uri, TRequest payload, CancellationToken cancellationToken = default);
    Task<HttpResponseMessage> DeleteAsync(string uri, CancellationToken cancellationToken = default);
}

public sealed class ApiClient : IApiClient
{
    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web);
    private readonly HttpClient _client;
    private readonly SecureStorageService _secureStorage;

    public ApiClient(HttpClient client, SecureStorageService secureStorage)
    {
        _client = client;
        _secureStorage = secureStorage;
    }

    public async Task<T?> GetAsync<T>(string uri, CancellationToken cancellationToken = default)
    {
        await ApplyAuthHeaderAsync();
        return await _client.GetFromJsonAsync<T>(uri, SerializerOptions, cancellationToken);
    }

    public async Task<TResponse?> PostAsync<TRequest, TResponse>(string uri, TRequest payload, CancellationToken cancellationToken = default)
    {
        await ApplyAuthHeaderAsync();
        var response = await _client.PostAsJsonAsync(uri, payload, SerializerOptions, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TResponse>(SerializerOptions, cancellationToken);
    }

    public async Task<HttpResponseMessage> DeleteAsync(string uri, CancellationToken cancellationToken = default)
    {
        await ApplyAuthHeaderAsync();
        return await _client.DeleteAsync(uri, cancellationToken);
    }

    private async Task ApplyAuthHeaderAsync()
    {
        var token = await _secureStorage.GetAccessTokenAsync();
        if (!string.IsNullOrWhiteSpace(token))
        {
            _client.DefaultRequestHeaders.Authorization = new("Bearer", token);
        }
    }
}
