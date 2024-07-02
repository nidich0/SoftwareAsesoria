using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels.Periodos;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using MediatR;

namespace CleanArchitecture.Application.Queries.Periodos.GetPeriodoById;

public sealed class GetPeriodoByIdQueryHandler :
    IRequestHandler<GetPeriodoByIdQuery, PeriodoViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IPeriodoRepository _periodoRepository;

    public GetPeriodoByIdQueryHandler(IPeriodoRepository periodoRepository, IMediatorHandler bus)
    {
        _periodoRepository = periodoRepository;
        _bus = bus;
    }

    public async Task<PeriodoViewModel?> Handle(GetPeriodoByIdQuery request, CancellationToken cancellationToken)
    {
        var periodo = await _periodoRepository.GetByIdAsync(request.PeriodoId);

        if (periodo is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetPeriodoByIdQuery),
                    $"Periodo with id {request.PeriodoId} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return PeriodoViewModel.FromPeriodo(periodo);
    }
}