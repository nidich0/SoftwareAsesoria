using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Periodo;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Periodos.CreatePeriodo;

public sealed class CreatePeriodoCommandHandler : CommandHandlerBase,
    IRequestHandler<CreatePeriodoCommand>
{
    private readonly IPeriodoRepository _periodoRepository;

    public CreatePeriodoCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IPeriodoRepository periodoRepository) : base(bus, unitOfWork, notifications)
    {
        _periodoRepository = periodoRepository;
    }

    public async Task Handle(CreatePeriodoCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        if (await _periodoRepository.ExistsAsync(request.AggregateId))
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is already a periodo with Id {request.AggregateId}",
                    DomainErrorCodes.Periodo.AlreadyExists));

            return;
        }

        var periodo = new Periodo(
            request.PeriodoId,
            request.FechaInicio,
            request.FechaFinal,
            request.Nombre);

        _periodoRepository.Add(periodo);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new PeriodoCreatedEvent(
                periodo.Id,
                periodo.FechaInicio,
                periodo.FechaFinal,
                periodo.Nombre));
        }
    }
}