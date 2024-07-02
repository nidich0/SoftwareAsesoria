using System;
using System.Xml.Linq;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Domain.Entities;

public class Cita : Entity
{
    public string EventoId { get; set; }
    public CitaEstado Estado { get; set; }
    public string DesarrolloAsesor { get; set; }
    public string DesarrolloAsesorado { get; set; }

    public Guid AsesorUserId { get; set; }
    public virtual User AsesorUser { get; set; } = null!;
    public Guid AsesoradoUserId { get; set; }
    public virtual User AsesoradoUser { get; set; } = null!;

    public Cita(
        Guid id, 
        string eventoId, 
        Guid asesorUserId, 
        Guid asesoradoUserId, 
        CitaEstado estado = CitaEstado.Inasistido,
        string desarrolloAsesor = "",
        string desarrolloAsesorado = "") : base(id)
    {
        EventoId = eventoId;
        AsesorUserId = asesorUserId;
        AsesoradoUserId = asesoradoUserId;
        Estado = estado;
        DesarrolloAsesor = desarrolloAsesor;
        DesarrolloAsesorado = desarrolloAsesorado;
    }
}