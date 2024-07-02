using CleanArchitecture.Domain.Commands.GrupoInvestigaciones.CreateGrupoInvestigacion;
using CleanArchitecture.Domain.Entities;
using System;

namespace CleanArchitecture.Domain.Commands.GrupoInvestigaciones.CreateGrupoInvestigacion;
public sealed class CreateGrupoInvestigacionCommand : CommandBase
{
    private static readonly CreateGrupoInvestigacionCommandValidation s_validation = new();

    public Guid TenantId {  get; set; }
    public string Nombre { get; }

    public CreateGrupoInvestigacionCommand(
        Guid grupoinvestigacionId,
        Guid tenantId,
        string nombre) : base(grupoinvestigacionId)
    {
        TenantId = tenantId;
        Nombre = nombre;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}

