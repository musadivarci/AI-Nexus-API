# AI Nexus API

Production-oriented multi-provider AI integration platform built with ASP.NET Core.

AI Nexus demonstrates how a .NET backend can expose stable AI use cases while isolating provider-specific integrations behind application abstractions.

## Architecture

```text
AINexus.Api
    |
AINexus.Application
    |
AINexus.Domain
    ^
AINexus.Infrastructure
    +-- OpenAI
    +-- Google Gemini
    +-- Anthropic Claude
    +-- Azure OpenAI
```

## Features

- ASP.NET Core 10 REST API
- N-Layer architecture
- Provider abstraction for OpenAI, Gemini, Claude and Azure OpenAI
- Chat, summarization, sentiment analysis, Q&A and content generation
- JWT authentication
- IP-based fixed-window rate limiting
- IHttpClientFactory and resilient outbound HTTP
- ProblemDetails exception handling
- Swagger / OpenAPI
- Health endpoint
- Docker support
- Unit tests
- Secret-free configuration

## API

| Method | Endpoint | Purpose |
|---|---|---|
| POST | `/api/v1/auth/token` | Demo JWT token |
| POST | `/api/v1/ai/chat` | General AI interaction |
| POST | `/api/v1/ai/summarize` | Summarize text |
| POST | `/api/v1/ai/sentiment` | Sentiment analysis |
| POST | `/api/v1/ai/question-answer` | Contextual Q&A |
| POST | `/api/v1/ai/content` | Content generation |
| POST | `/api/v1/media/speech-to-text` | Audio transcription |
| POST | `/api/v1/media/text-to-speech` | Speech synthesis |
| POST | `/api/v1/media/text-to-image` | Image generation |
| GET | `/health` | Liveness |

## Configuration

Never commit API keys. Use environment variables or .NET User Secrets.

```text
Authentication__SigningKey
Authentication__Issuer
Authentication__Audience
DemoCredentials__Username
DemoCredentials__Password
AI__OpenAI__ApiKey
AI__OpenAI__DefaultModel
AI__Gemini__ApiKey
AI__Gemini__DefaultModel
AI__Claude__ApiKey
AI__Claude__DefaultModel
AI__AzureOpenAI__ApiKey
AI__AzureOpenAI__Endpoint
AI__AzureOpenAI__Deployment
AI__AzureOpenAI__ApiVersion
```

## Run

```bash
dotnet restore
dotnet run --project src/AINexus.Api
```

Swagger is available in Development at `/swagger`.

## Docker

```bash
docker compose up --build
```

The container listens on port 8080.

## Engineering principles

- Dependency inversion
- Separation of concerns
- Provider-agnostic application services
- Cancellation-token propagation
- Resilient outbound HTTP
- Secure secret handling
- Explicit API versioning
- Operational health checks
- Testable business logic

## Roadmap

- Streaming responses
- Persistent conversation history
- OpenTelemetry
- Redis-backed distributed rate limiting
- OIDC / Microsoft Entra ID
- Integration tests with provider fakes
- Background jobs for long-running AI workloads

## License

MIT
