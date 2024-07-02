using System;

namespace CleanArchitecture.Shared.Events.Solicitud;

public sealed class SolicitudDeletedEvent : DomainEvent
{
    public Guid AsesoradoUserId { get; set; }
    public Guid AsesorUserId { get; set; }
    public string NumeroTesis { get; set; }
    public string Afinidad { get; set; }

    public SolicitudDeletedEvent(Guid solicitudId, Guid asesoradoUserId,
        Guid asesorUserId,
        string numeroTesis, string afinidad) : base(solicitudId)
    {
        AsesoradoUserId = asesoradoUserId;
        AsesorUserId = asesorUserId;
        NumeroTesis = numeroTesis;
        Afinidad = afinidad;
    }
}