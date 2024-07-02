using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Proto.LineaInvestigaciones;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.gRPC;

public sealed class LineaInvestigacionesApiImplementation : LineaInvestigacionesApi.LineaInvestigacionesApiBase
{
    private readonly ILineaInvestigacionRepository _lineainvestigacionRepository;

    public LineaInvestigacionesApiImplementation(ILineaInvestigacionRepository lineainvestigacionRepository)
    {
        _lineainvestigacionRepository = lineainvestigacionRepository;
    }

    public override async Task<GetLineaInvestigacionesByIdsResult> GetByIds(
        GetLineaInvestigacionesByIdsRequest request,
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

        var lineainvestigaciones = await _lineainvestigacionRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(lineainvestigacion => idsAsGuids.Contains(lineainvestigacion.Id))
            .Select(lineainvestigacion => new LineaInvestigacion
            {
                Id = lineainvestigacion.Id.ToString(),
                Nombre = lineainvestigacion.Nombre,
                IsDeleted = lineainvestigacion.Deleted
            })
            .ToListAsync();

        var result = new GetLineaInvestigacionesByIdsResult();

        result.Lineainvestigaciones.AddRange(lineainvestigaciones);

        return result;
    }
}