using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Shared.Events.Periodo;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace CleanArchitecture.Domain.EventHandler;

public sealed class PeriodoEventHandler :
    INotificationHandler<PeriodoCreatedEvent>,
    INotificationHandler<PeriodoDeletedEvent>,
    INotificationHandler<PeriodoUpdatedEvent>
{
    private readonly IDistributedCache _distributedCache;

    public PeriodoEventHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public Task Handle(PeriodoCreatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task Handle(PeriodoDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Periodo>(notification.AggregateId),
            cancellationToken);
    }

    public async Task Handle(PeriodoUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Periodo>(notification.AggregateId),
            cancellationToken);
    }
}