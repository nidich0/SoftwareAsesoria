using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.HistorialCoordinadores.UpdateHistorialCoordinador;

public sealed class UpdateHistorialCoordinadorCommandValidation : AbstractValidator<UpdateHistorialCoordinadorCommand>
{
    public UpdateHistorialCoordinadorCommandValidation()
    {
        AddRuleForId();
        AddRuleForUserId();
        AddRuleForGrupoInvestigacionId();
        AddRuleForFechaInicio();
        AddRuleForFechaFin();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.HistorialCoordinadorId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.HistorialCoordinador.EmptyId)
            .WithMessage("HistorialCoordinador id may not be empty");
    }

    private void AddRuleForUserId()
    {
        RuleFor(cmd => cmd.UserId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.HistorialCoordinador.EmptyUserId)
            .WithMessage("Asesor User id may not be empty")
            .WithErrorCode(DomainErrorCodes.HistorialCoordinador.InvalidUserId)
            .WithMessage("Asesor User Id is not a valid User Id");
    }

    private void AddRuleForGrupoInvestigacionId()
    {
        RuleFor(cmd => cmd.GrupoInvestigacionId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.HistorialCoordinador.EmptyGrupoInvestigacionId)
            .WithMessage("Asesorado User id may not be empty")
            .WithErrorCode(DomainErrorCodes.HistorialCoordinador.InvalidGrupoInvestigacionId)
            .WithMessage("Asesorado User Id is not a valid User Id");
    }

    private void AddRuleForFechaInicio()
    {
        RuleFor(cmd => cmd.FechaInicio)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.HistorialCoordinador.EmptyFechaInicio)
            .WithMessage("Fecha may not be empty")
            .WithErrorCode(DomainErrorCodes.HistorialCoordinador.InvalidFechaInicio)
            .WithMessage("Fecha is not a valid Fecha");
    }

    private void AddRuleForFechaFin()
    {
        RuleFor(cmd => cmd.FechaFin)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.HistorialCoordinador.EmptyFechaFin)
            .WithMessage("Fecha may not be empty")
            .WithErrorCode(DomainErrorCodes.HistorialCoordinador.InvalidFechaFin)
            .WithMessage("Fecha is not a valid Fecha");
    }
}

 