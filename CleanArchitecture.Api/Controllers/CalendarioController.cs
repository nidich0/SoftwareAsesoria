using System;
using System.Threading.Tasks;
using CleanArchitecture.Api.Models;
using CleanArchitecture.Api.Swagger;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.SortProviders;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Calendarios;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using CleanArchitecture.Application.Services;
using System.Net.Http;

namespace CleanArchitecture.Api.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public sealed class CalendarioController : ApiController
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ICalendarioService _calendarioService;

    public CalendarioController(
        INotificationHandler<DomainNotification> notifications,
        ICalendarioService calendarioService,
        IHttpClientFactory httpClientFactory) : base(notifications)
    {
        _calendarioService = calendarioService;
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet("link/{calendarioId}")]
    public async Task<IActionResult> VincularCalendarioAsync([FromRoute] Guid calendarioId, [FromQuery] string calendarName)
    {
        await _calendarioService.VincularCalendarioAsync(calendarioId, calendarName);
        return Response(calendarioId);
    }

    [HttpGet("auth")]
    [Authorize(AuthenticationSchemes = "Calendly")]
    public IActionResult AutorizarCalendarioAsync()
    {
        return Ok("Calendly AUTENTICADO");
    }

    [HttpGet("sync/{calendarioId}")]
    public async Task<IActionResult> SincronizarCalendarioAsync([FromRoute] Guid calendarioId)
    {
        await _calendarioService.SincronizarCalendarioAsync(calendarioId);
        return Response(calendarioId);
    }

    [HttpGet]
    [SwaggerOperation("Get a list of all calendarios")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<CalendarioViewModel>>))]
    public async Task<IActionResult> GetAllCalendariosAsync(
        [FromQuery] PageQuery query,
        [FromQuery] string searchTerm = "",
        [FromQuery] bool includeDeleted = false,
        [FromQuery] [SortableFieldsAttribute<CalendarioViewModelSortProvider, CalendarioViewModel, Calendario>]
        SortQuery? sortQuery = null)
    {
        var calendarios = await _calendarioService.GetAllCalendariosAsync(
            query,
            includeDeleted,
            searchTerm,
            sortQuery);
        return Response(calendarios);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get a calendario by id")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<CalendarioViewModel>))]
    public async Task<IActionResult> GetCalendarioByIdAsync([FromRoute] Guid id)
    {
        var calendario = await _calendarioService.GetCalendarioByIdAsync(id);
        return Response(calendario);
    }

    [HttpPost]
    [SwaggerOperation("Create a new calendario")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> CreateCalendarioAsync([FromBody] CreateCalendarioViewModel calendario)
    {
        var calendarioId = await _calendarioService.CreateCalendarioAsync(calendario);
        return Response(calendarioId);
    }

    [HttpPut]
    [SwaggerOperation("Update an existing calendario")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateCalendarioViewModel>))]
    public async Task<IActionResult> UpdateCalendarioAsync([FromBody] UpdateCalendarioViewModel calendario)
    {
        await _calendarioService.UpdateCalendarioAsync(calendario);
        return Response(calendario);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Delete an existing calendario")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> DeleteCalendarioAsync([FromRoute] Guid id)
    {
        await _calendarioService.DeleteCalendarioAsync(id);
        return Response(id);
    }
}