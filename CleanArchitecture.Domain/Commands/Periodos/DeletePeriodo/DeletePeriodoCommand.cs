using System;

namespace CleanArchitecture.Domain.Commands.Periodos.DeletePeriodo;

public sealed class DeletePeriodoCommand : CommandBase
{
    private static readonly DeletePeriodoCommandValidation s_validation = new();

    public DeletePeriodoCommand(Guid periodoId) : base(periodoId)
    {
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}