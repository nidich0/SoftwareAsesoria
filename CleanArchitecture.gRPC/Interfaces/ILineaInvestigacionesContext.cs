using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Shared.LineaInvestigacion;

namespace CleanArchitecture.gRPC.Interfaces;

public interface ILineaInvestigacionesContext
{
    Task<IEnumerable<LineaInvestigacionViewModel>> GetLineaInvestigacionesByIds(IEnumerable<Guid> ids);
}