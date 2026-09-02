using AINexus.Application.Abstractions;
using AINexus.Application.Services;
using AINexus.Domain.Enums;

namespace AINexus.UnitTests;

public sealed class AiServiceTests
{
    [Fact]
    public async Task Chat_UsesRequestedProvider()
    {
        var service = new AiService([new FakeProvider(AiProvider.Gemini)]);
        var result = await service.ChatAsync(AiProvider.Gemini, new AiRequest("hello"));
        Assert.Equal(AiProvider.Gemini, result.Provider);
        Assert.Equal("fake-response", result.Content);
    }

    private sealed class FakeProvider(AiProvider provider) : IAiProvider
    {
        public AiProvider Provider => provider;
        public Task<AiResult> GenerateAsync(AiRequest request, CancellationToken cancellationToken = default) => Task.FromResult(new AiResult("fake-response", provider, "fake-model", "test"));
    }
}
