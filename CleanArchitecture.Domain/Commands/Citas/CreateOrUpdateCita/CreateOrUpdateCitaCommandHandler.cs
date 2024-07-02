using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Cita;
using Google.Apis.Calendar.v3.Data;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Citas.CreateCita;

public sealed class CreateOrUpdateCitaCommandHandler : CommandHandlerBase,
    IRequestHandler<CreateOrUpdateCitaCommand>
{
    private readonly ICitaRepository _citaRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUser _user;

    public CreateOrUpdateCitaCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        ICitaRepository citaRepository,
        IUserRepository userRepository,
        IUser user
        ) : base(bus, unitOfWork, notifications)
    {
        _citaRepository = citaRepository;
        _userRepository = userRepository;
        _user = user;
    }

    private async Task Create(CreateOrUpdateCitaCommand request, CancellationToken cancellationToken)
    {
        if (await _citaRepository.ExistsAsync(request.AggregateId))
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is already a cita with Id {request.AggregateId}",
                    DomainErrorCodes.Cita.AlreadyExists));
            return;
        }

        var asesorUser = await _userRepository.GetByIdAsync(request.AsesorUserId);
        if (asesorUser is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no user with Id {request.AsesorUserId}",
                    ErrorCodes.ObjectNotFound));
            return;
        }
        if (asesorUser.Role > UserRole.Asesor)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"The user with Id {request.AsesorUserId} does not have the required role of 'Asesor'",
                    DomainErrorCodes.Cita.UserMissingAsesorRole));
            return;
        }

        var asesoradoUser = await _userRepository.GetByEmailAsync(request.AsesoradoEmail);
        if (asesoradoUser is null)
        {
            /*
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no user with Email {request.AsesoradoEmail}",
                    ErrorCodes.ObjectNotFound));
            */
            throw new InvalidOperationException($"There is no user with Email {request.AsesoradoEmail}");
        }
        if (asesoradoUser.Role > UserRole.Asesorado)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"The user with Id {asesoradoUser.Id} does not have the required role of 'Asesorado'",
                    DomainErrorCodes.Cita.UserMissingAsesoradoRole));
            return;
        }

        var cita = new Cita(
            request.AggregateId,
            request.EventoId,
            request.AsesorUserId,
            asesoradoUser.Id,
            //Ids.Seed.UserId,
            request.Estado);

        _citaRepository.Add(cita);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new CitaCreatedEvent(
                cita.Id,
                cita.EventoId,
                cita.AsesorUserId,
                cita.AsesoradoUserId));
        }
    }

    private async Task Update(CreateOrUpdateCitaCommand request, CancellationToken cancellationToken)
    {
        var cita = await _citaRepository.GetByIdAsync(request.AggregateId);
        if (cita is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no cita with Id {request.AggregateId}",
                    ErrorCodes.ObjectNotFound));

            return;
        }

        cita.Estado = request.Estado;

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new CitaUpdatedEvent(
                cita.Id,
                cita.EventoId,
                cita.AsesorUserId,
                cita.AsesoradoUserId));
        }
    }
    public async Task Handle(CreateOrUpdateCitaCommand request, CancellationToken cancellationToken)
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
                    $"No permission to create cita {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }
        */

        var existingCita = await _citaRepository.GetByEventoIdAsync(request.EventoId);
        if (existingCita is null)
        {
            await Create(request, cancellationToken);
        }
        else
        {
            await Update(request, cancellationToken);
        }
    }
}