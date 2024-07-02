using CleanArchitecture.gRPC.Interfaces;

namespace CleanArchitecture.gRPC;

public interface ICleanArchitecture
{
    IUsersContext Users { get; }
    ITenantsContext Tenants { get; }
    IPeriodosContext Periodos { get; }
    IFacultadesContext Facultades { get; }
    IEscuelasContext Escuelas { get; }
    IGrupoInvestigacionesContext GrupoInvestigaciones { get; }
    IHistorialCoordinadoresContext HistorialCoordinadores { get; }
    ISolicitudesContext Solicitudes { get; }
    ILineaInvestigacionesContext LineaInvestigaciones { get; }
}