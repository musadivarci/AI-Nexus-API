using System.Text.Json;
using AINexus.Application.Abstractions;
using AINexus.Domain.Enums;

namespace AINexus.Infrastructure.AI;

public sealed class OpenAiProvider(HttpClient client, IConfiguration config) : ProviderBase(client)
{
    public override AiProvider Provider => AiProvider.OpenAI;
    protected override HttpRequestMessage BuildRequest(AiRequest request)
    {
        var message = new HttpRequestMessage(HttpMethod.Post, "chat/completions");
        Bearer(message, config["AI:OpenAI:ApiKey"] ?? throw new InvalidOperationException("OpenAI API key is not configured."));
        message.Content = Json(new { model = config["AI:OpenAI:DefaultModel"] ?? "gpt-4.1-mini", messages = new[] { new { role = "user", content = request.Prompt } }, temperature = request.Temperature, max_tokens = request.MaxOutputTokens });
        return message;
    }
    protected override async Task<string> ParseAsync(HttpResponseMessage response, CancellationToken ct)
    {
        using var json = JsonDocument.Parse(await ReadSuccess(response, ct));
        return json.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString() ?? string.Empty;
    }
}
