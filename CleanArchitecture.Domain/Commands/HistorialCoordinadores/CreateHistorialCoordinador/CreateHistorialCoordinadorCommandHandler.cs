﻿using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.HistorialCoordinador;
using MediatR;

namespace CleanArchitecture.Domain.Commands.HistorialCoordinadores.CreateHistorialCoordinador;

public sealed class CreateHistorialCoordinadorCommandHandler : CommandHandlerBase,
    IRequestHandler<CreateHistorialCoordinadorCommand>
{
    private readonly IHistorialCoordinadorRepository _historialcoordinadorRepository;

    public CreateHistorialCoordinadorCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IHistorialCoordinadorRepository historialcoordinadorRepository) : base(bus, unitOfWork, notifications)
    {
        _historialcoordinadorRepository = historialcoordinadorRepository;
    }

    public async Task Handle(CreateHistorialCoordinadorCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        if (await _historialcoordinadorRepository.ExistsAsync(request.AggregateId))
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is already a historialcoordinador with Id {request.AggregateId}",
                    DomainErrorCodes.HistorialCoordinador.AlreadyExists));

            return;
        }

        var historialcoordinador = new HistorialCoordinador(
            request.HistorialCoordinadorId,
            request.UserId,
            request.GrupoInvestigacionId,
            request.FechaInicio,
            request.FechaFin);

        _historialcoordinadorRepository.Add(historialcoordinador);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new HistorialCoordinadorCreatedEvent(
                historialcoordinador.Id,
                historialcoordinador.UserId,
                historialcoordinador.GrupoInvestigacionId,
                request.FechaInicio,
                historialcoordinador.FechaFin));
        }
    }
}
