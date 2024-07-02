using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Proto.Periodos;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.gRPC;

public sealed class PeriodosApiImplementation : PeriodosApi.PeriodosApiBase
{
    private readonly IPeriodoRepository _periodoRepository;

    public PeriodosApiImplementation(IPeriodoRepository periodoRepository)
    {
        _periodoRepository = periodoRepository;
    }

    public override async Task<GetPeriodosByIdsResult> GetByIds(
        GetPeriodosByIdsRequest request,
        ServerCallContext context)
    {
        var idsAsGuids = new List<Guid>(request.Ids.Count);

        foreach (var id in request.Ids)
        {
            if (Guid.TryParse(id, out var parsed))
            {
                idsAsGuids.Add(parsed);
            }
        }

        var periodos = await _periodoRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(periodo => idsAsGuids.Contains(periodo.Id))
            .Select(periodo => new Periodo
            {
                Id = periodo.Id.ToString(),
                FechaInicio = periodo.FechaInicio.ToString(),
                FechaFinal = periodo.FechaFinal.ToString(),
                Nombre = periodo.Nombre
            })
            .ToListAsync();

        var result = new GetPeriodosByIdsResult();

        result.Periodos.AddRange(periodos);

        return result;
    }
}