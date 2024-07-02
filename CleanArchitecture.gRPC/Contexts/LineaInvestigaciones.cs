using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.gRPC.Interfaces;
using CleanArchitecture.Proto.LineaInvestigaciones;
using CleanArchitecture.Shared.LineaInvestigacion;

namespace CleanArchitecture.gRPC.Contexts;

public sealed class LineaInvestigacionesContext : ILineaInvestigacionesContext
{
    private readonly LineaInvestigacionesApi.LineaInvestigacionesApiClient _client;

    public LineaInvestigacionesContext(LineaInvestigacionesApi.LineaInvestigacionesApiClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<LineaInvestigacionViewModel>> GetLineaInvestigacionesByIds(IEnumerable<Guid> ids)
    {
        var request = new GetLineaInvestigacionesByIdsRequest();

        request.Ids.AddRange(ids.Select(id => id.ToString()));

        var result = await _client.GetByIdsAsync(request);

        return result.Lineainvestigaciones.Select(lineainvestigacion => new LineaInvestigacionViewModel(
            Guid.Parse(lineainvestigacion.Id),
            lineainvestigacion.Nombre));
    }
}