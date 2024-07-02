using Newtonsoft.Json;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.DTOs.Calendly
{
    public record CalendlyEventResponse(
        [property: JsonProperty("collection")] List<CalendlyEvent> Collection,
        [property: JsonProperty("pagination")] CalendlyPagination Pagination
    );
}
