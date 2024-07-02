using CleanArchitecture.gRPC.Contexts;
using CleanArchitecture.gRPC.Interfaces;
using CleanArchitecture.gRPC.Models;
using CleanArchitecture.Proto.Escuelas;
using CleanArchitecture.Proto.Facultades;
using CleanArchitecture.Proto.Tenants;
using CleanArchitecture.Proto.Users;
using CleanArchitecture.Proto.Citas;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CleanArchitecture.Proto.Calendarios;
using CleanArchitecture.Proto.GrupoInvestigaciones;
using CleanArchitecture.Proto.HistorialCoordinadores;
using CleanArchitecture.Proto.LineaInvestigaciones;
using CleanArchitecture.Proto.Periodos;
using CleanArchitecture.Proto.Solicitudes;

namespace CleanArchitecture.gRPC.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGrpcClient(
        this IServiceCollection services,
        IConfiguration configuration,
        string configSectionKey = "gRPC")
    {
        var settings = new GRPCSettings();
        configuration.Bind(configSectionKey, settings);

        return AddGrpcClient(services, settings);
    }

    public static IServiceCollection AddGrpcClient(this IServiceCollection services, GRPCSettings settings)
    {
        if (!string.IsNullOrWhiteSpace(settings.CleanArchitectureUrl))
        {
            services.AddCleanArchitectureGrpcClient(settings.CleanArchitectureUrl);
        }

        services.AddSingleton<ICleanArchitecture, CleanArchitecture>();

        return services;
    }

    public static IServiceCollection AddCleanArchitectureGrpcClient(
        this IServiceCollection services,
        string gRPCUrl)
    {
        if (string.IsNullOrWhiteSpace(gRPCUrl))
        {
            return services;
        }

        var channel = GrpcChannel.ForAddress(gRPCUrl);

        var usersClient = new UsersApi.UsersApiClient(channel);
        services.AddSingleton(usersClient);

        var tenantsClient = new TenantsApi.TenantsApiClient(channel);
        services.AddSingleton(tenantsClient);

        var calendariosClient = new CalendariosApi.CalendariosApiClient(channel);
        services.AddSingleton(calendariosClient);

        var periodosClient = new PeriodosApi.PeriodosApiClient(channel);
        services.AddSingleton(periodosClient);

        var facultadesClient = new FacultadesApi.FacultadesApiClient(channel);
        services.AddSingleton(facultadesClient);

        var escuelasClient = new EscuelasApi.EscuelasApiClient(channel);
        services.AddSingleton(escuelasClient);

        var grupoinvestigacionesClient = new GrupoInvestigacionesApi.GrupoInvestigacionesApiClient(channel);
        services.AddSingleton(grupoinvestigacionesClient);

        var historialcoordinadoresClient = new HistorialCoordinadoresApi.HistorialCoordinadoresApiClient(channel);
        services.AddSingleton(historialcoordinadoresClient);

        var solicitudesClient = new SolicitudesApi.SolicitudesApiClient(channel);
        services.AddSingleton(solicitudesClient);

        var lineainvestigacionesClient = new LineaInvestigacionesApi.LineaInvestigacionesApiClient(channel);
        services.AddSingleton(lineainvestigacionesClient);

        var citasClient = new CitasApi.CitasApiClient(channel);
        services.AddSingleton(citasClient);

        services.AddSingleton<IUsersContext, UsersContext>();
        services.AddSingleton<ITenantsContext, TenantsContext>();
        services.AddSingleton<ICalendariosContext, CalendariosContext>();
        services.AddSingleton<IPeriodosContext, PeriodosContext>();
        services.AddSingleton<IFacultadesContext, FacultadesContext>();
        services.AddSingleton<IEscuelasContext, EscuelasContext>();
        services.AddSingleton<IGrupoInvestigacionesContext, GrupoInvestigacionesContext>();
        services.AddSingleton<IHistorialCoordinadoresContext, HistorialCoordinadoresContext>();
        services.AddSingleton<ISolicitudesContext, SolicitudesContext>();
        services.AddSingleton<ILineaInvestigacionesContext, LineaInvestigacionesContext>();
        services.AddSingleton<ICitasContext, CitasContext>();

        return services;
    }
}