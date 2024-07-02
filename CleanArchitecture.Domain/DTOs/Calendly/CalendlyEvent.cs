using Newtonsoft.Json;

namespace CleanArchitecture.Domain.DTOs.Calendly
{
    public record CalendlyEvent(
        [property: JsonProperty("uri")] string Uri,
        [property: JsonProperty("name")] string Name,
        [property: JsonProperty("event_type")] string EventType,
        [property: JsonProperty("status")] string Status,
        [property: JsonProperty("start_time")] string StartTime,
        [property: JsonProperty("end_time")] string EndTime
    );
}
