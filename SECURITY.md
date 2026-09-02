# Security

Do not commit API keys, JWT signing keys, passwords, certificates, or provider credentials.

For local development use .NET User Secrets or environment variables. Production deployments should use a managed secret store and an external identity provider such as Microsoft Entra ID or another OIDC provider.

If you discover a security issue, please do not publish credentials or exploit details in a public issue. Contact the maintainer privately.
