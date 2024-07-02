namespace CleanArchitecture.Domain.Settings;

public sealed class CalendlySettings
{
    public string ClientId { get; set; } = null!;
    public string ClientSecret { get; set; } = null!;
    public string WebhookSigningKey { get; set; } = null!;
}
