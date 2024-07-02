using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Database;

namespace CleanArchitecture.Infrastructure.Repositories;

public sealed class LineaInvestigacionesRepository : BaseRepository<LineaInvestigacion>, ILineaInvestigacionRepository
{
    public LineaInvestigacionesRepository(ApplicationDbContext context) : base(context)
    {
    }
}