using CleanArchitecture.Domain.Entities;
using CleanArchitecture.gRPC.Interfaces;

namespace CleanArchitecture.gRPC;

public sealed class CleanArchitecture : ICleanArchitecture
{
    public CleanArchitecture(
        IUsersContext users,
        ITenantsContext tenants,
        IPeriodosContext periodos,
        IFacultadesContext facultades,
        IEscuelasContext escuelas,
        IGrupoInvestigacionesContext grupoinvestigaciones,
        IHistorialCoordinadoresContext historialCoordinadores,
        ISolicitudesContext solicitudes,
        ILineaInvestigacionesContext lineainvestigaciones)
    {
        Users = users;
        Tenants = tenants;
        Periodos = periodos;
        Facultades = facultades;
        Escuelas = escuelas;
        GrupoInvestigaciones = grupoinvestigaciones;
        HistorialCoordinadores = historialCoordinadores;
        Solicitudes = solicitudes;
        LineaInvestigaciones = lineainvestigaciones;
    }

    public IUsersContext Users { get; }

    public ITenantsContext Tenants { get; }

    public IFacultadesContext Facultades { get; } 

    public IEscuelasContext Escuelas { get; }

    public IPeriodosContext Periodos { get; }

    public IFacultadesContext? Facultad { get; }

    public IEscuelasContext? Escuela { get; }

    public IGrupoInvestigacionesContext GrupoInvestigaciones { get; }

    public IHistorialCoordinadoresContext HistorialCoordinadores { get; }

    public ISolicitudesContext Solicitudes { get; }

    public ILineaInvestigacionesContext LineaInvestigaciones { get; }

}