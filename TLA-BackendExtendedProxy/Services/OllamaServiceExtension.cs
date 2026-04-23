using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OllamaSharp;
using System.Net.Http.Headers;

public static class OllamaServiceExtensions
{
    public static IServiceCollection AddOllamaClient(this IServiceCollection services, IConfiguration configuration)
    {
        var ollamaUrl = new Uri("https://ollama.com");
        var apiKey = configuration["OllamaApiKey"] ?? throw new Exception("Ollama API key is not configured.");

        services.AddScoped<IOllamaApiClient>(sp =>
        {
            var httpClient = new HttpClient { BaseAddress = ollamaUrl };
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            return new OllamaApiClient(httpClient);
        });

        return services;
    }
}