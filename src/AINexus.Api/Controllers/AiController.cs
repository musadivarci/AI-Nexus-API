using AINexus.Application.Abstractions;
using AINexus.Application.Services;
using AINexus.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AINexus.Api.Controllers;

[ApiController, Authorize, Route("api/v1/ai")]
public sealed class AiController(AiService service) : ControllerBase
{
    [HttpPost("chat")]
    public Task<AiResult> Chat(ChatRequest request, CancellationToken ct) => service.ChatAsync(request.Provider, new AiRequest(request.Prompt, request.Temperature, request.MaxOutputTokens), ct);

    [HttpPost("summarize")]
    public Task<AiResult> Summarize(SummarizeRequest request, CancellationToken ct) => service.ChatAsync(request.Provider, new AiRequest($"Summarize the following text clearly:\n\n{request.Text}"), ct);

    [HttpPost("sentiment")]
    public Task<AiResult> Sentiment(SummarizeRequest request, CancellationToken ct) => service.ChatAsync(request.Provider, new AiRequest($"Analyze the sentiment of this text and explain briefly:\n\n{request.Text}"), ct);
}

public sealed record ChatRequest(string Prompt, AiProvider Provider = AiProvider.OpenAI, double? Temperature = null, int? MaxOutputTokens = null);
public sealed record SummarizeRequest(string Text, AiProvider Provider = AiProvider.OpenAI);
