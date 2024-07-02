using CleanArchitecture.Domain.Commands.GrupoInvestigaciones.DeleteGrupoInvestigacion;
using CleanArchitecture.Domain.Errors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Commands.GrupoInvestigaciones.DeleteGrupoInvestigacion;
public sealed class DeleteGrupoInvestigacionCommandValidation : AbstractValidator<DeleteGrupoInvestigacionCommand>
{
    public DeleteGrupoInvestigacionCommandValidation()
    {
        AddRuleForId();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.GrupoInvestigacion.EmptyId)
            .WithMessage("GrupoInvestigacion id may not be empty");
    }
}
