using System.Text.Json;
using Microsoft.Extensions.Logging;
using YuGiOh.Application.DTOs;

namespace YuGiOh.Application;

public class YgoprodeckApiService : IYgoprodeckApiService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<YgoprodeckApiService> _logger;
    private const string BaseUrl = "https://db.ygoprodeck.com/api/v7/cardinfo.php";

    public YgoprodeckApiService(HttpClient httpClient, ILogger<YgoprodeckApiService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public async Task<CardData?> GetCardByNameAsync(string cardName)
    {
        try
        {
            var url = $"{BaseUrl}?name={Uri.EscapeDataString(cardName)}";
            var httpResponse = await _httpClient.GetAsync(url);
            
            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            
            // Check for API errors (400 status code)
            if (!httpResponse.IsSuccessStatusCode)
            {
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                {
                    _logger.LogWarning("Rate limit exceeded when fetching card: {CardName}. Please wait before retrying.", cardName);
                    throw new InvalidOperationException("Rate limit exceeded. Please wait before retrying.");
                }
                
                // Try to parse error response
                try
                {
                    var errorResponse = JsonSerializer.Deserialize<YgoprodeckApiErrorResponse>(responseContent, JsonOptions);
                    if (errorResponse?.Error != null)
                    {
                        _logger.LogWarning("API error when fetching card {CardName}: {Error}", cardName, errorResponse.Error);
                        throw new InvalidOperationException($"API Error: {errorResponse.Error}");
                    }
                }
                catch (JsonException)
                {
                    // Not a JSON error response, use HTTP status
                }
                
                _logger.LogError("HTTP error {StatusCode} when fetching card: {CardName}", httpResponse.StatusCode, cardName);
                httpResponse.EnsureSuccessStatusCode();
            }
            
            var apiResponse = JsonSerializer.Deserialize<YgoprodeckApiResponse>(responseContent, JsonOptions);
            
            if (apiResponse?.Data == null || apiResponse.Data.Length == 0)
            {
                _logger.LogWarning("No card data returned for: {CardName}", cardName);
                return null;
            }
            
            return apiResponse.Data.FirstOrDefault();
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP request error when fetching card by name: {CardName}", cardName);
            throw;
        }
        catch (InvalidOperationException)
        {
            throw; // Re-throw our custom exceptions
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error fetching card by name: {CardName}", cardName);
            throw;
        }
    }

    public async Task<CardData?> GetCardByIdAsync(string cardId)
    {
        try
        {
            var url = $"{BaseUrl}?id={Uri.EscapeDataString(cardId)}";
            var httpResponse = await _httpClient.GetAsync(url);
            
            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            
            // Check for API errors (400 status code)
            if (!httpResponse.IsSuccessStatusCode)
            {
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                {
                    _logger.LogWarning("Rate limit exceeded when fetching card ID: {CardId}. Please wait before retrying.", cardId);
                    throw new InvalidOperationException("Rate limit exceeded. Please wait before retrying.");
                }
                
                // Try to parse error response
                try
                {
                    var errorResponse = JsonSerializer.Deserialize<YgoprodeckApiErrorResponse>(responseContent, JsonOptions);
                    if (errorResponse?.Error != null)
                    {
                        _logger.LogWarning("API error when fetching card ID {CardId}: {Error}", cardId, errorResponse.Error);
                        throw new InvalidOperationException($"API Error: {errorResponse.Error}");
                    }
                }
                catch (JsonException)
                {
                    // Not a JSON error response, use HTTP status
                }
                
                _logger.LogError("HTTP error {StatusCode} when fetching card ID: {CardId}", httpResponse.StatusCode, cardId);
                httpResponse.EnsureSuccessStatusCode();
            }
            
            var apiResponse = JsonSerializer.Deserialize<YgoprodeckApiResponse>(responseContent, JsonOptions);
            
            if (apiResponse?.Data == null || apiResponse.Data.Length == 0)
            {
                _logger.LogWarning("No card data returned for ID: {CardId}", cardId);
                return null;
            }
            
            return apiResponse.Data.FirstOrDefault();
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP request error when fetching card by ID: {CardId}", cardId);
            throw;
        }
        catch (InvalidOperationException)
        {
            throw; // Re-throw our custom exceptions
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error fetching card by ID: {CardId}", cardId);
            throw;
        }
    }
}

