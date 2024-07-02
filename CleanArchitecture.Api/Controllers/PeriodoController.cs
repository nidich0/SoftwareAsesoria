using System;
using System.Threading.Tasks;
using CleanArchitecture.Api.Models;
using CleanArchitecture.Api.Swagger;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.SortProviders;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Periodos;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CleanArchitecture.Api.Controllers;

[ApiController]
[Authorize]
[Route("/api/v1/[controller]")]
public sealed class PeriodoController : ApiController
{
    private readonly IPeriodoService _periodoService;

    public PeriodoController(
        INotificationHandler<DomainNotification> notifications,
        IPeriodoService periodoService) : base(notifications)
    {
        _periodoService = periodoService;
    }

    [HttpGet]
    [AllowAnonymous]
    [SwaggerOperation("Get a list of all periodos")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<PeriodoViewModel>>))]
    public async Task<IActionResult> GetAllPeriodosAsync(
        [FromQuery] PageQuery query,
        [FromQuery] string searchTerm = "",
        [FromQuery] bool includeDeleted = false,
        [FromQuery] [SortableFieldsAttribute<PeriodoViewModelSortProvider, PeriodoViewModel, Periodo>]
        SortQuery? sortQuery = null)
    {
        var periodos = await _periodoService.GetAllPeriodosAsync(
            query,
            includeDeleted,
            searchTerm,
            sortQuery);
        return Response(periodos);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    [SwaggerOperation("Get a periodo by id")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PeriodoViewModel>))]
    public async Task<IActionResult> GetPeriodoByIdAsync([FromRoute] Guid id)
    {
        var periodo = await _periodoService.GetPeriodoByIdAsync(id);
        return Response(periodo);
    }

    [HttpPost]
    [AllowAnonymous]
    [SwaggerOperation("Create a new periodo")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> CreatePeriodoAsync([FromBody] CreatePeriodoViewModel periodo)
    {
        var periodoId = await _periodoService.CreatePeriodoAsync(periodo);
        return Response(periodoId);
    }

    [HttpPut]
    [AllowAnonymous]
    [SwaggerOperation("Update an existing periodo")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdatePeriodoViewModel>))]
    public async Task<IActionResult> UpdatePeriodoAsync([FromBody] UpdatePeriodoViewModel periodo)
    {
        await _periodoService.UpdatePeriodoAsync(periodo);
        return Response(periodo);
    }

    [HttpDelete("{id}")]
    [AllowAnonymous]
    [SwaggerOperation("Delete an existing periodo")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> DeletePeriodoAsync([FromRoute] Guid id)
    {
        await _periodoService.DeletePeriodoAsync(id);
        return Response(id);
    }
}