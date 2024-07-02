using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.Periodos.DeletePeriodo;

public sealed class DeletePeriodoCommandValidation : AbstractValidator<DeletePeriodoCommand>
{
    public DeletePeriodoCommandValidation()
    {
        AddRuleForId();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Periodo.EmptyId)
            .WithMessage("Periodo id may not be empty");
    }
}