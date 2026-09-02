using System.Text.Json;
using AINexus.Application.Abstractions;
using AINexus.Domain.Enums;

namespace AINexus.Infrastructure.AI;

public sealed class GeminiProvider(HttpClient client, IConfiguration config) : ProviderBase(client)
{
    public override AiProvider Provider => AiProvider.Gemini;
    protected override HttpRequestMessage BuildRequest(AiRequest request)
    {
        var model = config["AI:Gemini:DefaultModel"] ?? "gemini-2.5-flash";
        var key = config["AI:Gemini:ApiKey"] ?? throw new InvalidOperationException("Gemini API key is not configured.");
        var message = new HttpRequestMessage(HttpMethod.Post, $"models/{model}:generateContent?key={Uri.EscapeDataString(key)}");
        message.Content = Json(new { contents = new[] { new { parts = new[] { new { text = request.Prompt } } } } });
        return message;
    }
    protected override async Task<string> ParseAsync(HttpResponseMessage response, CancellationToken ct)
    {
        using var json = JsonDocument.Parse(await ReadSuccess(response, ct));
        return json.RootElement.GetProperty("candidates")[0].GetProperty("content").GetProperty("parts")[0].GetProperty("text").GetString() ?? string.Empty;
    }
}
