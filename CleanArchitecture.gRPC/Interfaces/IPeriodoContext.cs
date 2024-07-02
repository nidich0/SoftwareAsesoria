using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Shared.Periodos;

namespace CleanArchitecture.gRPC.Interfaces;

public interface IPeriodosContext
{
    Task<IEnumerable<PeriodoViewModel>> GetPeriodosByIds(IEnumerable<Guid> ids);
}