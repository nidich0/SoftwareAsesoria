using System;
using System.Xml.Linq;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Domain.Entities;

public class Solicitud : Entity
{
    public Guid AsesoradoUserId { get; set; }
    public virtual User AsesoradoUser { get; set; } = null!;
    public Guid AsesorUserId { get; set; }
    public virtual User AsesorUser { get; set; } = null!;
    public string NumeroTesis { get; set; }
    public string Afinidad { get; set; }
    public string Mensaje { get; set; }
    public SolicitudStatus Estado {  get; private set; }
    public DateTimeOffset? FechaCreacion { get; private set; }
    public DateTimeOffset? FechaRespuesta { get; private set; }

    public Solicitud(Guid id,
        Guid asesoradoUserId,
        Guid asesorUserId,
        string numeroTesis,
        string afinidad,
        string mensaje,
        SolicitudStatus estado,
        DateTimeOffset? fechaCreacion = null,
        DateTimeOffset? fechaRespuesta = null) : base(id)
    {
        AsesoradoUserId = asesoradoUserId;
        AsesorUserId = asesorUserId;
        NumeroTesis = numeroTesis;
        Afinidad = afinidad;
        Mensaje = mensaje;
        Estado = estado;
        FechaCreacion = fechaCreacion ?? DateTime.Now;
        FechaRespuesta = fechaRespuesta;
    }

    public void SetAsesorado(Guid asesoradoId)
    {
        AsesoradoUserId = asesoradoId;
    }

    public void SetAsesor(Guid asesorId)
    {
        AsesorUserId = asesorId;
    }
    public void SetFechaCreacion(DateTimeOffset fechaCreacion)
    {
        FechaCreacion = fechaCreacion;
    }
    public void SetFechaRespuesta(DateTimeOffset fechaRespuesta)
    {
        FechaRespuesta = fechaRespuesta;
    }

    public void SetPendiente()
    {
        Estado = SolicitudStatus.Pendiente;
    }
    public void SetAceptado()
    {
        Estado = SolicitudStatus.Aceptado;
    }
    public void SetRechazado()
    {
        Estado = SolicitudStatus.Rechazado;
    }

    public void SetNumeroTesis(string numeroTesis)
    {
        NumeroTesis = numeroTesis;
    }
    public void SetAfinidad(string afinidad)
    {
        Afinidad = afinidad;
    }

}