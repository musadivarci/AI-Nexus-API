using AINexus.Domain.Enums;

namespace AINexus.Application.Abstractions;

public sealed record AiResult(string Content, AiProvider Provider, string Model, string RequestId);
public sealed record AiRequest(string Prompt, double? Temperature = null, int? MaxOutputTokens = null);

public interface IAiProvider
{
    AiProvider Provider { get; }
    Task<AiResult> GenerateAsync(AiRequest request, CancellationToken cancellationToken = default);
}
