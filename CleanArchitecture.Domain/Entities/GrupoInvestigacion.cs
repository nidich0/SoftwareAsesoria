using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities;

public class GrupoInvestigacion : Entity
{
    public string Nombre { get; private set; }
    public Guid TenantId { get; private set; }
    public virtual Tenant Tenant { get; private set; } = null!;

    public virtual ICollection<HistorialCoordinador> HistorialCoordinadores { get; set; } = new HashSet<HistorialCoordinador>();

    public virtual ICollection<LineaInvestigacion> LineaInvestigaciones { get; set; } = new HashSet<LineaInvestigacion>();

    public GrupoInvestigacion(
        Guid id,
        Guid tenantId,
        string nombre) : base(id)
    {
        TenantId = tenantId;
        Nombre = nombre;
    }

    public void SetName(string nombre)
    {
        Nombre = nombre;
    }

    public void SetTenant(Guid tenantId)
    {
        TenantId = tenantId;
    }
}