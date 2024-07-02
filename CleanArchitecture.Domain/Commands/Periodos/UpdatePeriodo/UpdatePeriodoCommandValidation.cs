using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.Periodos.UpdatePeriodo;

public sealed class UpdatePeriodoCommandValidation : AbstractValidator<UpdatePeriodoCommand>
{
    public UpdatePeriodoCommandValidation()
    {
        AddRuleForId();
        AddRuleForFechaInicio();
        AddRuleForFechaFinal();
        AddRuleForNombre();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.PeriodoId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Periodo.EmptyId)
            .WithMessage("Periodo id may not be empty");
    }

    private void AddRuleForFechaInicio()
    {
        RuleFor(cmd => cmd.FechaInicio)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Periodo.EmptyFechaInicio)
            .WithMessage("Fecha Inicio may not be empty")
            .WithErrorCode(DomainErrorCodes.Periodo.InvalidFechaInicio)
            .WithMessage("Fecha Inicio is not a valid fecha");
    }

    private void AddRuleForFechaFinal()
    {
        RuleFor(cmd => cmd.FechaFinal)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Periodo.EmptyFechaFinal)
            .WithMessage("Fecha Final may not be empty")
            .WithErrorCode(DomainErrorCodes.Periodo.InvalidFechaFinal)
            .WithMessage("Fecha Final is not a valid fecha");
    }

    private void AddRuleForNombre()
    {
        RuleFor(cmd => cmd.Nombre)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Periodo.EmptyNombre)
            .WithMessage("Nombre may not be empty")
            .MaximumLength(MaxLengths.Periodo.Nombre)
            .WithErrorCode(DomainErrorCodes.Periodo.NombreExceedsMaxLength)
            .WithMessage($"Nombre may not be longer than {MaxLengths.Periodo.Nombre} characters");
    }
}