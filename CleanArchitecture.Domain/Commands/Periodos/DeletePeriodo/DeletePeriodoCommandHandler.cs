using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Periodo;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Periodos.DeletePeriodo;

public sealed class DeletePeriodoCommandHandler : CommandHandlerBase,
    IRequestHandler<DeletePeriodoCommand>
{
    private readonly IPeriodoRepository _periodoRepository;

    public DeletePeriodoCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IPeriodoRepository periodoRepository) : base(bus, unitOfWork, notifications)
    {
        _periodoRepository = periodoRepository;
    }

    public async Task Handle(DeletePeriodoCommand request, CancellationToken cancellationToken)
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

        _periodoRepository.Remove(periodo);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new PeriodoDeletedEvent(periodo.Id));
        }
    }
}