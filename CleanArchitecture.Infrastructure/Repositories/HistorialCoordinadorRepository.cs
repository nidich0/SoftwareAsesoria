using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Database;

namespace CleanArchitecture.Infrastructure.Repositories;

public sealed class HistorialCoordinadorRepository : BaseRepository<HistorialCoordinador>, IHistorialCoordinadorRepository
{
    public HistorialCoordinadorRepository(ApplicationDbContext context) : base(context)
    {
    }
}