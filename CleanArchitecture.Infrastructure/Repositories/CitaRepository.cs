using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Repositories;

public sealed class CitaRepository : BaseRepository<Cita>, ICitaRepository
{
    public CitaRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Cita?> GetByEventoIdAsync(string eventoId)
    {
        return await DbSet.SingleOrDefaultAsync(Cita => Cita.EventoId == eventoId);
    }
}