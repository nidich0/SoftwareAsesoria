using System;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.ViewModels.Periodos;

public sealed class PeriodoViewModel
{
    public Guid Id { get; set; }
    public DateOnly FechaInicio { get; set; }
    public DateOnly FechaFinal { get; set; }
    public string Nombre { get; set; } = string.Empty;

    public static PeriodoViewModel FromPeriodo(Periodo periodo)
    {
        return new PeriodoViewModel
        {
            Id = periodo.Id,
            FechaInicio = periodo.FechaInicio,
            FechaFinal = periodo.FechaFinal,
            Nombre = periodo.Nombre
        };
    }
}