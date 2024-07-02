﻿using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Queries.GrupoInvestigaciones.GetAll;
using CleanArchitecture.Application.ViewModels.GrupoInvestigaciones;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Domain.Commands.GrupoInvestigaciones.CreateGrupoInvestigacion;
using CleanArchitecture.Domain.Commands.GrupoInvestigaciones.DeleteGrupoInvestigacion;
using CleanArchitecture.Domain.Commands.GrupoInvestigaciones.UpdateGrupoInvestigacion;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Extensions;
using CleanArchitecture.Application.Queries.GrupoInvestigaciones.GetGrupoInvestigacionById;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Services;

public sealed class GrupoInvestigacionService : IGrupoInvestigacionService
{
    private readonly IMediatorHandler _bus;
    private readonly IDistributedCache _distributedCache;

    public GrupoInvestigacionService(IMediatorHandler bus, IDistributedCache distributedCache)
    {
        _bus = bus;
        _distributedCache = distributedCache;
    }

    public async Task<Guid> CreateGrupoInvestigacionAsync(CreateGrupoInvestigacionViewModel grupoinvestigacion)
    {
        var grupoinvestigacionId = Guid.NewGuid();

        await _bus.SendCommandAsync(new CreateGrupoInvestigacionCommand(
            grupoinvestigacionId,
            grupoinvestigacion.TenantId,
            grupoinvestigacion.Nombre));

        return grupoinvestigacionId;
    }

    public async Task UpdateGrupoInvestigacionAsync(UpdateGrupoInvestigacionViewModel grupoinvestigacion)
    {
        await _bus.SendCommandAsync(new UpdateGrupoInvestigacionCommand(
            grupoinvestigacion.Id,
            grupoinvestigacion.Nombre));
    }

    public async Task DeleteGrupoInvestigacionAsync(Guid grupoinvestigacionId)
    {
        await _bus.SendCommandAsync(new DeleteGrupoInvestigacionCommand(grupoinvestigacionId));
    }

    public async Task<GrupoInvestigacionViewModel?> GetGrupoInvestigacionByIdAsync(Guid grupoinvestigacionId)
    {
        var cachedGrupoInvestigacion = await _distributedCache.GetOrCreateJsonAsync(
            CacheKeyGenerator.GetEntityCacheKey<GrupoInvestigacion>(grupoinvestigacionId),
            async () => await _bus.QueryAsync(new GetGrupoInvestigacionByIdQuery(grupoinvestigacionId)),
            new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromDays(3),
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(30)
            });

        return cachedGrupoInvestigacion;
    }

    public async Task<PagedResult<GrupoInvestigacionViewModel>> GetAllGrupoInvestigacionesAsync(
        PageQuery query,
        bool includeDeleted,
    string searchTerm = "",
        SortQuery? sortQuery = null)
    {
        return await _bus.QueryAsync(new GetAllGrupoInvestigacionesQuery(query, includeDeleted, searchTerm, sortQuery));
    }
}
