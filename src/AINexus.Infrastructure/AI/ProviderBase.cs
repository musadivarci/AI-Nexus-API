using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using AINexus.Application.Abstractions;
using AINexus.Domain.Enums;

namespace AINexus.Infrastructure.AI;

public abstract class ProviderBase(HttpClient client) : IAiProvider
{
    protected HttpClient Client { get; } = client;
    public abstract AiProvider Provider { get; }

    protected abstract HttpRequestMessage BuildRequest(AiRequest request);
    protected abstract Task<string> ParseAsync(HttpResponseMessage response, CancellationToken cancellationToken);

    public async Task<AiResult> GenerateAsync(AiRequest request, CancellationToken cancellationToken = default)
    {
        using var message = BuildRequest(request);
        using var response = await Client.SendAsync(message, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        var content = await ParseAsync(response, cancellationToken);
        return new AiResult(content, Provider, "configured", Guid.NewGuid().ToString("N"));
    }

    protected static StringContent Json(object value) => new(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
    protected static void Bearer(HttpRequestMessage request, string token) => request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
    protected static async Task<string> ReadSuccess(HttpResponseMessage response, CancellationToken ct)
    {
        var body = await response.Content.ReadAsStringAsync(ct);
        if (!response.IsSuccessStatusCode) throw new HttpRequestException($"AI provider returned {(int)response.StatusCode}: {body}");
        return body;
    }
}
