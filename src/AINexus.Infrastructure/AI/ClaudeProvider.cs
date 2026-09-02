using System.Text.Json;
using AINexus.Application.Abstractions;
using AINexus.Domain.Enums;

namespace AINexus.Infrastructure.AI;

public sealed class ClaudeProvider(HttpClient client, IConfiguration config) : ProviderBase(client)
{
    public override AiProvider Provider => AiProvider.Claude;
    protected override HttpRequestMessage BuildRequest(AiRequest request)
    {
        var message = new HttpRequestMessage(HttpMethod.Post, "messages");
        message.Headers.Add("x-api-key", config["AI:Claude:ApiKey"] ?? throw new InvalidOperationException("Claude API key is not configured."));
        message.Headers.Add("anthropic-version", "2023-06-01");
        message.Content = Json(new { model = config["AI:Claude:DefaultModel"] ?? "claude-3-5-haiku-latest", max_tokens = request.MaxOutputTokens ?? 500, messages = new[] { new { role = "user", content = request.Prompt } } });
        return message;
    }
    protected override async Task<string> ParseAsync(HttpResponseMessage response, CancellationToken ct)
    {
        using var json = JsonDocument.Parse(await ReadSuccess(response, ct));
        return json.RootElement.GetProperty("content")[0].GetProperty("text").GetString() ?? string.Empty;
    }
}
