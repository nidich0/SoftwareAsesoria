using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Periodo;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Periodos.UpdatePeriodo;

public sealed class UpdatePeriodoCommandHandler : CommandHandlerBase,
    IRequestHandler<UpdatePeriodoCommand>
{
    private readonly IPeriodoRepository _periodoRepository;

    public UpdatePeriodoCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IPeriodoRepository periodoRepository) : base(bus, unitOfWork, notifications)
    {
        _periodoRepository = periodoRepository;
    }

    public async Task Handle(UpdatePeriodoCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        var periodo = await _periodoRepository.GetByIdAsync(request.AggregateId);

        if (periodo is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no periodo with Id {request.AggregateId}",
                    ErrorCodes.ObjectNotFound));

            return;
        }

        periodo.SetFechaInicio(request.FechaInicio);
        periodo.SetFechaFinal(request.FechaFinal);
        periodo.SetNombre(request.Nombre);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new PeriodoUpdatedEvent(
                periodo.Id,
                periodo.FechaInicio,
                periodo.FechaFinal,
                periodo.Nombre));
        }
    }
}