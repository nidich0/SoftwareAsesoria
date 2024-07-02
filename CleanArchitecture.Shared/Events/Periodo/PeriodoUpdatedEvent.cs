using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CleanArchitecture.Shared.Events.Periodo;

public sealed class PeriodoUpdatedEvent : DomainEvent
{
    public DateOnly FechaInicio { get; set; }
    public DateOnly FechaFinal { get; set; }
    public string Nombre { get; set; }

    public PeriodoUpdatedEvent(Guid periodoId, DateOnly fechaInicio, DateOnly fechaFinal, string nombre) : base(periodoId)
    {
        FechaInicio = fechaInicio;
        FechaFinal = fechaFinal;
        Nombre = nombre;
    }
}