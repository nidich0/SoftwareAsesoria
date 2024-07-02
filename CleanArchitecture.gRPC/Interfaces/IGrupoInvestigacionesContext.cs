using CleanArchitecture.Shared.GrupoInvestigacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.gRPC.Interfaces;
public interface IGrupoInvestigacionesContext
{
    Task<IEnumerable<GrupoInvestigacionViewModel>> GetGrupoInvestigacionesByIds(IEnumerable<Guid> ids);
}
