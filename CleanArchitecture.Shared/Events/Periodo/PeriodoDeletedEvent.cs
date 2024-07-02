using System;

namespace CleanArchitecture.Shared.Events.Periodo;

public sealed class PeriodoDeletedEvent : DomainEvent
{
    public PeriodoDeletedEvent(Guid periodoId) : base(periodoId)
    {
    }
}