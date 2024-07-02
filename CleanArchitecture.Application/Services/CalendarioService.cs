using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Queries.Calendarios.GetAll;
using CleanArchitecture.Application.Queries.Calendarios.GetCalendarioById;
using CleanArchitecture.Application.Queries.Calendarios.GetAccessToken;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Calendarios;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Commands.Calendarios.CreateCalendario;
using CleanArchitecture.Domain.Commands.Calendarios.DeleteCalendario;
using CleanArchitecture.Domain.Commands.Calendarios.UpdateCalendario;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Extensions;
using CleanArchitecture.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using CleanArchitecture.Domain.Constants;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using Polly;
using CleanArchitecture.Application.ViewModels.Citas;
using Microsoft.IdentityModel.Tokens;
using CleanArchitecture.Domain.DTOs.Calendly;
using System.Linq;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.Services
{
    public sealed class CalendarioService : ICalendarioService
    {
        private readonly IMediatorHandler _bus;
        private readonly IDistributedCache _distributedCache;
        private readonly IUserService _userService;
        private readonly ICitaService _citaService;
        private readonly ICalendly _calendly;

        // Constructor to initialize dependencies
        public CalendarioService(IMediatorHandler bus, IDistributedCache distributedCache, IUserService userService, ICitaService citaService, ICalendly calendly)
        {
            _bus = bus;
            _distributedCache = distributedCache;
            _userService = userService;
            _citaService = citaService;
            _calendly = calendly;
        }

        // Method to create a new calendar
        public async Task<Guid> CreateCalendarioAsync(CreateCalendarioViewModel calendario)
        {
            var calendarioId = Guid.NewGuid(); // Generate new GUID for calendario
            await _bus.SendCommandAsync(new CreateCalendarioCommand(
                calendarioId,
                calendario.AccessToken,
                calendario.AccessTokenExpiration,
                calendario.RefreshToken,
                calendario.RefreshTokenExpiration));

            return calendarioId; // Return the new calendario ID
        }

        // Method to update an existing calendar
        public async Task UpdateCalendarioAsync(UpdateCalendarioViewModel calendario)
        {
            await _bus.SendCommandAsync(new UpdateCalendarioCommand(
                calendario.Id,
                calendario.AccessToken,
                calendario.AccessTokenExpiration,
                calendario.RefreshToken,
                calendario.RefreshTokenExpiration,
                calendario.UserUri,
                calendario.EventType,
                calendario.EventsPageToken));
        }

        // Method to delete a calendar by ID
        public async Task DeleteCalendarioAsync(Guid calendarioId)
        {
            await _bus.SendCommandAsync(new DeleteCalendarioCommand(calendarioId));
        }

        // Method to synchronize calendar events from the Calendly API
        public async Task SincronizarCalendarioAsync(Guid calendarioId)
        {
            var calendario = await GetCalendarioByIdAsync(calendarioId)
                ?? throw new InvalidOperationException("Calendario could not be found.");

            using (var httpClient = _calendly.GetHttpClient(calendario.AccessToken))
            {
                var apiUri = $"https://api.calendly.com/scheduled_events?user={calendario.UserUri}";

                var pageToken = calendario.EventsPageToken;
                if (pageToken != null)
                    apiUri += $"&page_token={pageToken}";

                do
                {
                    var eventsResponse = await _calendly.GetDataAsync<CalendlyEventResponse>(apiUri, httpClient);

                    foreach (CalendlyEvent calendlyEvent in eventsResponse.Collection)
                    {
                        if (calendlyEvent.EventType == calendario.EventType)
                        {
                            var inviteesResponse = await _calendly.GetDataAsync<CalendlyInviteeResponse>(calendlyEvent.Uri + "/invitees", httpClient);
                            CalendlyInvitee invitee = inviteesResponse.Collection.First();

                            CitaEstado citaEstado = CitaEstado.Programado;
                            if (calendlyEvent.Status == "canceled")
                            {
                                citaEstado = CitaEstado.Cancelado;
                            }
                            else if(DateTime.UtcNow > DateTime.Parse(calendlyEvent.EndTime))
                            {
                                citaEstado = CitaEstado.Completado;
                            }

                            try
                            {
                            await _citaService.CreateOrUpdateCitaAsync(new CreateCitaViewModel(
                                calendlyEvent.Uri,
                                calendario.UserId,
                                invitee.Email,
                                citaEstado));
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($"Failed to create cita for event {calendlyEvent.Uri}: {ex.Message}");
                            }

                        }
                    }

                    if (!string.IsNullOrEmpty(eventsResponse.Pagination.NextPage))
                    {
                        pageToken = eventsResponse.Pagination.NextPageToken;
                        apiUri = eventsResponse.Pagination.NextPage;
                    }
                    else
                    {
                        break;
                    }
                } while (true);

                if (pageToken != null)
                {
                    await UpdateCalendarioAsync(new UpdateCalendarioViewModel(
                        calendarioId,
                        calendario.AccessToken,
                        calendario.AccessTokenExpiration,
                        calendario.RefreshToken,
                        calendario.RefreshTokenExpiration,
                        calendario.UserUri,
                        calendario.EventType,
                        pageToken));
                }
            }
        }

        // Method to link a calendar to an event type on Calendly
        public async Task VincularCalendarioAsync(Guid calendarioId, string calendarioNombre)
        {
            string calendarioEventType = string.Empty;
            var calendario = await GetCalendarioByIdAsync(calendarioId)
                ?? throw new InvalidOperationException("Calendario could not be found.");

            CalendlyEventTypeResponse eventTypeResponse = await _calendly.GetDataAsync<CalendlyEventTypeResponse>(
                $"https://api.calendly.com/event_types?user={calendario.UserUri}", 
                calendario.AccessToken);
            foreach (CalendlyEventType eventType in eventTypeResponse.Collection)
            {
                if (eventType.Name == calendarioNombre)
                {
                    calendarioEventType = eventType.Uri;
                    break;
                }
            }

            // Ensure the event type was found
            if (string.IsNullOrEmpty(calendarioEventType))
            {
                throw new InvalidOperationException($"Event type '{calendarioNombre}' could not be found.");
            }

            await UpdateCalendarioAsync(new UpdateCalendarioViewModel(
                calendarioId,
                calendario.AccessToken,
                calendario.AccessTokenExpiration,
                calendario.RefreshToken,
                calendario.RefreshTokenExpiration,
                calendario.UserUri,
                calendarioEventType,
                null));
        }

        // Method to get all calendars with pagination, sorting, and optional search term
        public async Task<PagedResult<CalendarioViewModel>> GetAllCalendariosAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = "",
            SortQuery? sortQuery = null)
        {
            return await _bus.QueryAsync(new GetAllCalendariosQuery(query, includeDeleted, searchTerm, sortQuery));
        }

        // Method to get a specific calendar by its ID, with caching
        public async Task<CalendarioViewModel?> GetCalendarioByIdAsync(Guid calendarioId)
        {
            var cachedCalendario = await _distributedCache.GetOrCreateJsonAsync(
                CacheKeyGenerator.GetEntityCacheKey<Calendario>(calendarioId),
                async () => await _bus.QueryAsync(new GetCalendarioByIdQuery(calendarioId)),
                new DistributedCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromDays(3),
                    AbsoluteExpiration = DateTimeOffset.Now.AddDays(30)
                });

            return cachedCalendario;
        }

        // Method to get the access token for a specific user, with caching
        public async Task<string?> GetAccessTokenAsync(Guid userId)
        {
            var cachedCalendario = await _distributedCache.GetOrCreateJsonAsync(
                CacheKeyGenerator.GetEntityCacheKey<Calendario>(userId),
                async () => await _bus.QueryAsync(new GetAccessTokenQuery(userId)),
                new DistributedCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromDays(3),
                    AbsoluteExpiration = DateTimeOffset.Now.AddDays(30)
                });

            if (cachedCalendario != null)
            {
                return cachedCalendario.AccessToken;
            }
            return null;
        }
    }
}