using Newtonsoft.Json;

namespace CleanArchitecture.Domain.DTOs.Calendly
{
    public record CalendlyEventType(
        [property: JsonProperty("uri")] string Uri,
        [property: JsonProperty("name")] string Name
    );
}
