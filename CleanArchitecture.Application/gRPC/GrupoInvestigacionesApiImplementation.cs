using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Proto.GrupoInvestigaciones;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
namespace CleanArchitecture.Application.gRPC;

public sealed class GrupoInvestigacionesApiImplementation : GrupoInvestigacionesApi.GrupoInvestigacionesApiBase
{
    private readonly IGrupoInvestigacionRepository _grupoinvestigacionRepository;

    public GrupoInvestigacionesApiImplementation(IGrupoInvestigacionRepository grupoinvestigacionRepository)
    {
        _grupoinvestigacionRepository = grupoinvestigacionRepository;
    }

    public override async Task<GetGrupoInvestigacionesByIdsResult> GetByIds(
        GetGrupoInvestigacionesByIdsRequest request,
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

        var grupoinvestigaciones = await _grupoinvestigacionRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(grupoinvestigacion => idsAsGuids.Contains(grupoinvestigacion.Id))
            .Select(grupoinvestigacion => new GrupoInvestigacion
            {
                Id = grupoinvestigacion.Id.ToString(),
                Nombre = grupoinvestigacion.Nombre,
                IsDeleted = grupoinvestigacion.Deleted
            })
            .ToListAsync();

        var result = new GetGrupoInvestigacionesByIdsResult();

        result.Grupoinvestigaciones.AddRange(grupoinvestigaciones);

        return result;
    }
}