using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Solicitud;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Solicitudes.UpdateSolicitud;

public sealed class UpdateSolicitudCommandHandler : CommandHandlerBase,
    IRequestHandler<UpdateSolicitudCommand>
{
    private readonly ISolicitudRepository _solicitudRepository;
    private readonly IUser _user;

    public UpdateSolicitudCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        ISolicitudRepository solicitudRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _solicitudRepository = solicitudRepository;
        _user = user;
    }

    public async Task Handle(UpdateSolicitudCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        var solicitud = await _solicitudRepository.GetByIdAsync(request.AggregateId);

        if (solicitud is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no solicitud with Id {request.AggregateId}",
                    ErrorCodes.ObjectNotFound));

            return;
        }

        solicitud.SetNumeroTesis(request.NumeroTesis);
        solicitud.SetAfinidad(request.Afinidad);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new SolicitudUpdatedEvent(
                solicitud.Id,
                solicitud.AsesoradoUserId,
                solicitud.AsesorUserId,
                solicitud.NumeroTesis,
                solicitud.Afinidad));
        }
    }
}