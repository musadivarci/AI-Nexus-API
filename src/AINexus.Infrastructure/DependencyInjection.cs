using AINexus.Application.Abstractions;
using AINexus.Application.Services;
using AINexus.Infrastructure.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;

namespace AINexus.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<OpenAiProvider>(c => c.BaseAddress = new Uri(configuration["AI:OpenAI:BaseUrl"] ?? "https://api.openai.com/v1/")).AddStandardResilienceHandler();
        services.AddHttpClient<GeminiProvider>(c => c.BaseAddress = new Uri(configuration["AI:Gemini:BaseUrl"] ?? "https://generativelanguage.googleapis.com/v1beta/")).AddStandardResilienceHandler();
        services.AddHttpClient<ClaudeProvider>(c => c.BaseAddress = new Uri(configuration["AI:Claude:BaseUrl"] ?? "https://api.anthropic.com/v1/")).AddStandardResilienceHandler();
        services.AddHttpClient<AzureOpenAiProvider>(c => c.BaseAddress = new Uri(configuration["AI:AzureOpenAI:Endpoint"] ?? "https://localhost/")).AddStandardResilienceHandler();
        services.AddTransient<IAiProvider, OpenAiProvider>();
        services.AddTransient<IAiProvider, GeminiProvider>();
        services.AddTransient<IAiProvider, ClaudeProvider>();
        services.AddTransient<IAiProvider, AzureOpenAiProvider>();
        services.AddScoped<AiService>();
        return services;
    }
}
