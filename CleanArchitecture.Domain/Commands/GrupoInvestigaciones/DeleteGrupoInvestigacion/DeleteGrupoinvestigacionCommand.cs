using CleanArchitecture.Domain.Commands.GrupoInvestigaciones.DeleteGrupoInvestigacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Commands.GrupoInvestigaciones.DeleteGrupoInvestigacion;
public sealed class DeleteGrupoInvestigacionCommand : CommandBase
{
    private static readonly DeleteGrupoInvestigacionCommandValidation s_validation = new();

    public DeleteGrupoInvestigacionCommand(Guid grupoinvestigacionId) : base(grupoinvestigacionId)
    {
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}
