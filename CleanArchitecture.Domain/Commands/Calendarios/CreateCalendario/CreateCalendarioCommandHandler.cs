using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.DTOs.Calendly;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Calendario;
using CleanArchitecture.Shared.Events.User;
using MediatR;
using Newtonsoft.Json;
using Polly;

namespace CleanArchitecture.Domain.Commands.Calendarios.CreateCalendario;

public sealed class CreateCalendarioCommandHandler : CommandHandlerBase,
    IRequestHandler<CreateCalendarioCommand>
{
    private readonly ICalendly _calendly;
    private readonly ICalendarioRepository _calendarioRepository;

    public CreateCalendarioCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        ICalendly calendly,
        ICalendarioRepository calendarioRepository
        ) : base(bus, unitOfWork, notifications)
    {
        _calendly = calendly;
        _calendarioRepository = calendarioRepository;

    }

    public async Task Handle(CreateCalendarioCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }
        /*
        if (_user.GetUserRole() != UserRole.Asesor)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"No permission to create calendario {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }*/

        if (await _calendarioRepository.ExistsAsync(request.AggregateId))
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is already a calendario with Id {request.AggregateId}",
                    DomainErrorCodes.Calendario.AlreadyExists));

            return;
        }

        // Determinar la Id del Usuario asociado al calendario
        //DEBUG var userId = _user.GetUserId();
        var userId = Ids.Seed.UserId;
        var existingCalendario = await _calendarioRepository.GetByUserIdAsync(userId);
        if (existingCalendario is not null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is already a calendario with userId {userId}",
                    DomainErrorCodes.Calendario.AlreadyExists));
            return;
        }

        try
        {
            CalendlyUserResponse userResponse = await _calendly.GetDataAsync<CalendlyUserResponse>("https://api.calendly.com/users/me", request.AccessToken);

            var calendario = new Calendario(
                request.AggregateId,
                userId,
                request.AccessToken,
                request.AccessTokenExpiration,
                request.RefreshToken,
                request.RefreshTokenExpiration,
                userResponse.Resource.Uri);

            _calendarioRepository.Add(calendario);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new CalendarioCreatedEvent(
                    calendario.UserId,
                    calendario.Id,
                    calendario.AccessToken,
                    calendario.RefreshToken,
                    calendario.UserUri));
            }
        }
        catch (HttpRequestException httpEx)
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                "There was an error connecting to the Calendly service. Please try again later.",
                ErrorCodes.HttpRequestFailed,
                httpEx));
        }
        catch (InvalidOperationException invalidOpEx)
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                invalidOpEx.Message,
                ErrorCodes.InvalidOperation,
                invalidOpEx));
        }
        catch (Exception ex)
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                "An unexpected error occurred. Please try again later.",
                ErrorCodes.UnexpectedError,
                ex));
        }
    }
}