using System;

namespace CleanArchitecture.Domain.Commands.Periodos.UpdatePeriodo;

public sealed class UpdatePeriodoCommand : CommandBase
{
    private static readonly UpdatePeriodoCommandValidation s_validation = new();

    public Guid PeriodoId { get; }
    public DateOnly FechaInicio { get; }
    public DateOnly FechaFinal { get; }
    public string Nombre { get; }

    public UpdatePeriodoCommand(
        Guid periodoId,
        DateOnly fechaInicio,
        DateOnly fechaFinal,
        string nombre) : base(periodoId)
    {
        PeriodoId = periodoId;
        FechaInicio = fechaInicio;
        FechaFinal = fechaFinal;
        Nombre = nombre;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}