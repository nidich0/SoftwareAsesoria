﻿using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Database;

namespace CleanArchitecture.Infrastructure.Repositories;

public sealed class PeriodoRepository : BaseRepository<Periodo>, IPeriodoRepository
{
    public PeriodoRepository(ApplicationDbContext context) : base(context)
    {
    }
}