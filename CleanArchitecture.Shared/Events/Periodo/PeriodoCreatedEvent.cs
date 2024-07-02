using System;

namespace CleanArchitecture.Shared.Events.Periodo;

public sealed class PeriodoCreatedEvent : DomainEvent
{
    public DateOnly FechaInicio { get; set; }
    public DateOnly FechaFinal { get; set; }
    public string Nombre { get; set; }

    public PeriodoCreatedEvent(Guid periodoId, DateOnly fechaInicio, DateOnly fechaFinal, string nombre) : base(periodoId)
    {
        FechaInicio = fechaInicio;
        FechaFinal = fechaFinal;
        Nombre = nombre;
    }
}