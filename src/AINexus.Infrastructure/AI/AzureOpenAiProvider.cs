using System.Text.Json;
using AINexus.Application.Abstractions;
using AINexus.Domain.Enums;

namespace AINexus.Infrastructure.AI;

public sealed class AzureOpenAiProvider(HttpClient client, IConfiguration config) : ProviderBase(client)
{
    public override AiProvider Provider => AiProvider.AzureOpenAI;
    protected override HttpRequestMessage BuildRequest(AiRequest request)
    {
        var deployment = config["AI:AzureOpenAI:Deployment"] ?? throw new InvalidOperationException("Azure OpenAI deployment is not configured.");
        var version = config["AI:AzureOpenAI:ApiVersion"] ?? "2025-04-01-preview";
        var message = new HttpRequestMessage(HttpMethod.Post, $"openai/deployments/{deployment}/chat/completions?api-version={Uri.EscapeDataString(version)}");
        message.Headers.Add("api-key", config["AI:AzureOpenAI:ApiKey"] ?? throw new InvalidOperationException("Azure OpenAI API key is not configured."));
        message.Content = Json(new { messages = new[] { new { role = "user", content = request.Prompt } }, temperature = request.Temperature, max_tokens = request.MaxOutputTokens });
        return message;
    }
    protected override async Task<string> ParseAsync(HttpResponseMessage response, CancellationToken ct)
    {
        using var json = JsonDocument.Parse(await ReadSuccess(response, ct));
        return json.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString() ?? string.Empty;
    }
}
