using CleanArchitecture.Domain.Commands.GrupoInvestigaciones.UpdateGrupoInvestigacion;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.GrupoInvestigacion;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Commands.GrupoInvestigaciones.UpdateGrupoInvestigacion;
public sealed class UpdateGrupoInvestigacionCommandHandler : CommandHandlerBase,
    IRequestHandler<UpdateGrupoInvestigacionCommand>
{
    private readonly IGrupoInvestigacionRepository _grupoinvestigacionRepository;
    private readonly IUser _user;

    public UpdateGrupoInvestigacionCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IGrupoInvestigacionRepository grupoinvestigacionRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _grupoinvestigacionRepository = grupoinvestigacionRepository;
        _user = user;
    }

    public async Task Handle(UpdateGrupoInvestigacionCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        var grupoinvestigacion = await _grupoinvestigacionRepository.GetByIdAsync(request.AggregateId);

        if (grupoinvestigacion is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no grupoinvestigacion with Id {request.AggregateId}",
                    ErrorCodes.ObjectNotFound));

            return;
        }

        grupoinvestigacion.SetName(request.Nombre);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new GrupoInvestigacionUpdatedEvent(
                grupoinvestigacion.Id,
                grupoinvestigacion.Nombre));
        }
    }
}
