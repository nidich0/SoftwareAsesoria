using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Domain.Settings;
using CleanArchitecture.Shared.Events.Solicitud;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;

namespace CleanArchitecture.Domain.Commands.Solicitudes.CreateSolicitud;

public sealed class CreateSolicitudCommandHandler : CommandHandlerBase,
    IRequestHandler<CreateSolicitudCommand>
{
    private readonly ISolicitudRepository _solicitudRepository;
    private readonly IOptionsSnapshot<AsesoriaSettings> _asesoriaSettings;

    public CreateSolicitudCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        ISolicitudRepository solicitudRepository,
        IOptionsSnapshot<AsesoriaSettings> asesoriaSettings) : base(bus, unitOfWork, notifications)
    {
        _solicitudRepository = solicitudRepository;
        _asesoriaSettings = asesoriaSettings;
    }

    public async Task Handle(CreateSolicitudCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        /*
        if (_user.GetUserRole() != UserRole.Admin)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"No permission to create solicitud {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }
        */

        if (await _solicitudRepository.ExistsAsync(request.AggregateId))
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is already a solicitud with Id {request.AggregateId}",
                    DomainErrorCodes.Solicitud.AlreadyExists));

            return;
        }

        var existingSolicitudes = _solicitudRepository.GetByFechaCreacionAsync(DateTime.Today, DateTime.Today.AddDays(1));
        if (existingSolicitudes.Count() >= _asesoriaSettings.Value.SolicitudesMaximasAlDiaPorAsesor) {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"The maximum number of Solicitudes per day for this Asesor has been reached.",
                    ErrorCodes.UnexpectedError));
            return;
        }

        var solicitud = new Solicitud(
            request.SolicitudId,
            request.AsesoradoUserId,
            request.AsesorUserId,
            request.NumeroTesis,
            request.Afinidad,
            request.Mensaje,
            SolicitudStatus.Pendiente);

        _solicitudRepository.Add(solicitud);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new SolicitudCreatedEvent(
                solicitud.Id,
                solicitud.AsesoradoUserId,
                solicitud.AsesorUserId,
                solicitud.NumeroTesis,
                solicitud.Afinidad));
        }
    }
}