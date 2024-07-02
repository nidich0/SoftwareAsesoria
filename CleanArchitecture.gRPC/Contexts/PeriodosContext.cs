using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.gRPC.Interfaces;
using CleanArchitecture.Proto.Periodos;
using CleanArchitecture.Shared.Periodos;

namespace CleanArchitecture.gRPC.Contexts;

public sealed class PeriodosContext : IPeriodosContext
{
    private readonly PeriodosApi.PeriodosApiClient _client;

    public PeriodosContext(PeriodosApi.PeriodosApiClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<PeriodoViewModel>> GetPeriodosByIds(IEnumerable<Guid> ids)
    {
        var request = new GetPeriodosByIdsRequest();

        request.Ids.AddRange(ids.Select(id => id.ToString()));

        var result = await _client.GetByIdsAsync(request);

        return result.Periodos.Select(periodo => new PeriodoViewModel(
            Guid.Parse(periodo.Id),
            DateOnly.Parse(periodo.FechaInicio),
            DateOnly.Parse(periodo.FechaFinal),
            periodo.Nombre));
    }
}