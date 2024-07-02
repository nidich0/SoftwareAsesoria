using CleanArchitecture.Domain.Commands.GrupoInvestigaciones.UpdateGrupoInvestigacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Commands.GrupoInvestigaciones.UpdateGrupoInvestigacion;
public sealed class UpdateGrupoInvestigacionCommand : CommandBase
{
    private static readonly UpdateGrupoInvestigacionCommandValidation s_validation = new();

    public string Nombre { get; }

    public UpdateGrupoInvestigacionCommand(Guid grupoinvestigacionId, string nombre) : base(grupoinvestigacionId)
    {
        Nombre = nombre;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}
