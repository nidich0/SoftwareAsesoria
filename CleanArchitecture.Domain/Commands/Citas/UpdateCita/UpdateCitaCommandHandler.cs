using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Cita;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Citas.UpdateCita;

public sealed class UpdateCitaCommandHandler : CommandHandlerBase,
    IRequestHandler<UpdateCitaCommand>
{
    private readonly ICitaRepository _citaRepository;
    private readonly IUser _user;

    public UpdateCitaCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        ICitaRepository citaRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _citaRepository = citaRepository;
        _user = user;

    }

    public async Task Handle(UpdateCitaCommand request, CancellationToken cancellationToken)
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
                    $"No permission to update cita {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }
        */

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
        cita.DesarrolloAsesor = request.DesarrolloAsesor;
        cita.DesarrolloAsesorado = request.DesarrolloAsesorado;

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new CitaUpdatedEvent(
                cita.Id,
                cita.EventoId,
                cita.AsesorUserId,
                cita.AsesoradoUserId));
        }
    }
}