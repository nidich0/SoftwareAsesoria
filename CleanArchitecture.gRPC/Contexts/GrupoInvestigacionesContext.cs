using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.gRPC.Interfaces;
using CleanArchitecture.Proto.GrupoInvestigaciones;
using CleanArchitecture.Shared.GrupoInvestigacion;

namespace CleanArchitecture.gRPC.Contexts;

public sealed class GrupoInvestigacionesContext : IGrupoInvestigacionesContext
{
    private readonly GrupoInvestigacionesApi.GrupoInvestigacionesApiClient _client;

    public GrupoInvestigacionesContext(GrupoInvestigacionesApi.GrupoInvestigacionesApiClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<GrupoInvestigacionViewModel>> GetGrupoInvestigacionesByIds(IEnumerable<Guid> ids)
    {
        var request = new GetGrupoInvestigacionesByIdsRequest();

        request.Ids.AddRange(ids.Select(id => id.ToString()));

        var result = await _client.GetByIdsAsync(request);

        return result.Grupoinvestigaciones.Select(grupoinvestigacion => new GrupoInvestigacionViewModel(
            Guid.Parse(grupoinvestigacion.Id),
            grupoinvestigacion.Nombre));
    }
}
