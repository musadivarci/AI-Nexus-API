# AI Nexus API

**Production-oriented multi-provider AI integration platform built with ASP.NET Core.**

AI Nexus is a reference architecture for exposing stable AI use cases while isolating provider-specific integrations behind a clean application boundary.

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

### Layers

- **Domain** — provider-independent concepts.
- **Application** — use cases and abstractions.
- **Infrastructure** — external AI provider adapters and resilient HTTP clients.
- **API** — REST endpoints, JWT authentication, rate limiting and OpenAPI.

## Features

- ASP.NET Core 10 REST API
- N-Layer architecture and dependency inversion
- OpenAI, Google Gemini, Anthropic Claude and Azure OpenAI adapters
- Chat / text generation
- Text summarization
- Sentiment analysis
- JWT authentication with role claims
- Fixed-window IP rate limiting (60 requests/minute)
- `IHttpClientFactory` with resilience handlers
- Centralized ProblemDetails error endpoint
- Swagger / OpenAPI
- Health endpoint
- Docker and Docker Compose
- Unit tests
- No secrets committed to source control

## API

| Method | Endpoint | Purpose |
|---|---|---|
| POST | `/api/v1/auth/token` | Issue a demo JWT |
| POST | `/api/v1/ai/chat` | General AI interaction |
| POST | `/api/v1/ai/summarize` | Summarize text |
| POST | `/api/v1/ai/sentiment` | Analyze sentiment |
| GET | `/health` | Liveness check |

All `/api/v1/ai/*` endpoints require a bearer token.

## Configuration

Never commit API keys or signing keys. Use .NET User Secrets, environment variables or a managed secret store.

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

## Run locally

```bash
dotnet restore
dotnet run --project src/AINexus.Api
```

Swagger is available at `/swagger` in Development.

## Docker

```bash
docker compose up --build
```

The container listens on port `8080`.

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
- Speech-to-text / text-to-speech / text-to-image endpoints
- OpenTelemetry tracing and metrics
- Redis-backed distributed rate limiting
- OIDC / Microsoft Entra ID
- Integration tests with provider fakes
- Background jobs for long-running AI workloads

## License

MIT
