using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace WebApp.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApiService(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
        
        var apiBaseUrl = _configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7017";
        _httpClient.BaseAddress = new Uri(apiBaseUrl);
    }

    public void SetAuthorizationHeader(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public void ClearAuthorizationHeader()
    {
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }

    public async Task<T?> PostAsync<T>(string endpoint, object data)
    {
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await _httpClient.PostAsync(endpoint, content);
        
        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        
        return default;
    }

    public async Task<T?> GetAsync<T>(string endpoint)
    {
        // Get token from session if available
        var token = _httpContextAccessor.HttpContext?.Session.GetString("JWTToken");
        if (!string.IsNullOrEmpty(token))
        {
            SetAuthorizationHeader(token);
        }

        var response = await _httpClient.GetAsync(endpoint);
        
        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        
        return default;
    }

    public async Task<HttpResponseMessage> PostWithTokenAsync(string endpoint, object data, string token)
    {
        SetAuthorizationHeader(token);
        
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        return await _httpClient.PostAsync(endpoint, content);
    }

    public async Task<HttpResponseMessage> PutWithTokenAsync(string endpoint, object data, string token)
    {
        SetAuthorizationHeader(token);
        
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        return await _httpClient.PutAsync(endpoint, content);
    }

    public async Task<HttpResponseMessage> DeleteWithTokenAsync(string endpoint, string token)
    {
        SetAuthorizationHeader(token);
        return await _httpClient.DeleteAsync(endpoint);
    }
}
