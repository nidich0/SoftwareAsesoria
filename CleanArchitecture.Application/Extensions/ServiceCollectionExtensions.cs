using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Queries.Calendarios.GetAccessToken;
using CleanArchitecture.Application.Queries.Calendarios.GetAll;
using CleanArchitecture.Application.Queries.Calendarios.GetCalendarioById;
using CleanArchitecture.Application.Queries.Citas.GetAll;
using CleanArchitecture.Application.Queries.Citas.GetCitaById;
using CleanArchitecture.Application.Queries.Escuelas.GetAll;
using CleanArchitecture.Application.Queries.Escuelas.GetEscuelasById;
using CleanArchitecture.Application.Queries.Facultades.GetAll;
using CleanArchitecture.Application.Queries.Facultades.GetFacultadesById;
using CleanArchitecture.Application.Queries.Tenants.GetAll;
using CleanArchitecture.Application.Queries.Tenants.GetTenantById;
using CleanArchitecture.Application.Queries.Users.GetAll;
using CleanArchitecture.Application.Queries.Users.GetUserById;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Application.SortProviders;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Calendarios;
using CleanArchitecture.Application.ViewModels.Citas;
using CleanArchitecture.Application.ViewModels.Escuelas;
using CleanArchitecture.Application.ViewModels.Facultades;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Tenants;
using CleanArchitecture.Application.ViewModels.Users;
using CleanArchitecture.Application.ViewModels.Periodos;
using CleanArchitecture.Application.ViewModels.GrupoInvestigaciones;
using CleanArchitecture.Application.ViewModels.HistorialCoordinadores;
using CleanArchitecture.Application.ViewModels.Solicitudes;
using CleanArchitecture.Application.ViewModels.LineaInvestigaciones;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using CleanArchitecture.Application.Queries.GrupoInvestigaciones.GetAll;
using CleanArchitecture.Application.Queries.GrupoInvestigaciones.GetGrupoInvestigacionById;
using CleanArchitecture.Application.Queries.HistorialCoordinadores.GetAll;
using CleanArchitecture.Application.Queries.HistorialCoordinadores.GetHistorialCoordinadorById;
using CleanArchitecture.Application.Queries.LineaInvestigaciones.GetAll;
using CleanArchitecture.Application.Queries.LineaInvestigaciones.GetLineaInvestigacionById;
using CleanArchitecture.Application.Queries.Periodos.GetAll;
using CleanArchitecture.Application.Queries.Periodos.GetPeriodoById;
using CleanArchitecture.Application.Queries.Solicitudes.GetAll;
using CleanArchitecture.Application.Queries.Solicitudes.GetSolicitudById;

namespace CleanArchitecture.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITenantService, TenantService>();
        services.AddScoped<IPeriodoService, PeriodoService>();
        services.AddScoped<IFacultadesService, FacultadesService>();
        services.AddScoped<IEscuelasService, EscuelasService>();
        services.AddScoped<IGrupoInvestigacionService, GrupoInvestigacionService>();
        services.AddScoped<IHistorialCoordinadorService, HistorialCoordinadorService>();
        services.AddScoped<ISolicitudService, SolicitudService>();
        services.AddScoped<ILineaInvestigacionService, LineaInvestigacionService>();
        services.AddScoped<ICitaService, CitaService>();
        services.AddScoped<ICalendarioService, CalendarioService>();

        return services;
    }

    public static IServiceCollection AddQueryHandlers(this IServiceCollection services)
    {
        // User
        services.AddScoped<IRequestHandler<GetUserByIdQuery, UserViewModel?>, GetUserByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllUsersQuery, PagedResult<UserViewModel>>, GetAllUsersQueryHandler>();

        // Tenant
        services.AddScoped<IRequestHandler<GetTenantByIdQuery, TenantViewModel?>, GetTenantByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllTenantsQuery, PagedResult<TenantViewModel>>, GetAllTenantsQueryHandler>();

        // Calendario
        services.AddScoped<IRequestHandler<GetCalendarioByIdQuery, CalendarioViewModel?>, GetCalendarioByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllCalendariosQuery, PagedResult<CalendarioViewModel>>, GetAllCalendariosQueryHandler>();
        services.AddScoped<IRequestHandler<GetAccessTokenQuery, CalendarioViewModel?>, GetAccessTokenQueryHandler>();

        // Periodo
        services.AddScoped<IRequestHandler<GetPeriodoByIdQuery, PeriodoViewModel?>, GetPeriodoByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllPeriodosQuery, PagedResult<PeriodoViewModel>>, GetAllPeriodosQueryHandler>();

        // Facultad
        services.AddScoped<IRequestHandler<GetFacultadesByIdQuery, FacultadViewModel?>, GetFacultadesByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllFacultadesQuery, PagedResult<FacultadViewModel>>, GetAllFacultadesQueryHandler>();

        // Escuela
        services.AddScoped<IRequestHandler<GetEscuelasByIdQuery, EscuelaViewModel?>, GetEscuelasByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllEscuelasQuery, PagedResult<EscuelaViewModel>>, GetAllEscuelasQueryHandler>();

        // GrupoInvestigaciones
        services.AddScoped<IRequestHandler<GetGrupoInvestigacionByIdQuery, GrupoInvestigacionViewModel?>, GetGrupoInvestigacionByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllGrupoInvestigacionesQuery, PagedResult<GrupoInvestigacionViewModel>>, GetAllGrupoInvestigacionesQueryHandler>();

        // HistorialCoordinadores
        services.AddScoped<IRequestHandler<GetHistorialCoordinadorByIdQuery, HistorialCoordinadorViewModel?>, GetHistorialCoordinadorByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllHistorialCoordinadoresQuery, PagedResult<HistorialCoordinadorViewModel>>, GetAllHistorialCoordinadoresQueryHandler>();

        // Solicitud
        services.AddScoped<IRequestHandler<GetSolicitudByIdQuery, SolicitudViewModel?>, GetSolicitudByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllSolicitudesQuery, PagedResult<SolicitudViewModel>>, GetAllSolicitudesQueryHandler>();

        // LineaInvestigaciones
        services.AddScoped<IRequestHandler<GetLineaInvestigacionByIdQuery, LineaInvestigacionViewModel?>, GetLineaInvestigacionByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllLineaInvestigacionesQuery, PagedResult<LineaInvestigacionViewModel>>, GetAllLineaInvestigacionesQueryHandler>();

        // Citas
        services.AddScoped<IRequestHandler<GetCitaByIdQuery, CitaViewModel?>, GetCitaByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllCitasQuery, PagedResult<CitaViewModel>>, GetAllCitasQueryHandler>();

        return services;
    }

    public static IServiceCollection AddSortProviders(this IServiceCollection services)
    {
        services.AddScoped<ISortingExpressionProvider<TenantViewModel, Tenant>, TenantViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<CalendarioViewModel, Calendario>, CalendarioViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<UserViewModel, User>, UserViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<PeriodoViewModel, Periodo>, PeriodoViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<FacultadViewModel, Facultad>, FacultadViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<EscuelaViewModel, Escuela>, EscuelaViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<GrupoInvestigacionViewModel, GrupoInvestigacion>, GrupoInvestigacionViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<HistorialCoordinadorViewModel, HistorialCoordinador>, HistorialCoordinadorViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<SolicitudViewModel, Solicitud>, SolicitudViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<LineaInvestigacionViewModel, LineaInvestigacion>, LineaInvestigacionViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<CitaViewModel, Cita>, CitaViewModelSortProvider>();

        return services;
    }
}