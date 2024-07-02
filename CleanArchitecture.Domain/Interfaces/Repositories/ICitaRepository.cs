using CleanArchitecture.Domain.Entities;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Interfaces.Repositories;

public interface ICitaRepository : IRepository<Cita>
{
    Task<Cita?> GetByEventoIdAsync(string eventoId);
}