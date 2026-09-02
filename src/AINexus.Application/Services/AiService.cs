using AINexus.Application.Abstractions;
using AINexus.Domain.Enums;

namespace AINexus.Application.Services;

public sealed class AiService(IEnumerable<IAiProvider> providers)
{
    public Task<AiResult> ChatAsync(AiProvider provider, AiRequest request, CancellationToken cancellationToken = default)
    {
        var selected = providers.FirstOrDefault(x => x.Provider == provider)
            ?? throw new InvalidOperationException($"AI provider '{provider}' is not configured.");
        return selected.GenerateAsync(request, cancellationToken);
    }
}
