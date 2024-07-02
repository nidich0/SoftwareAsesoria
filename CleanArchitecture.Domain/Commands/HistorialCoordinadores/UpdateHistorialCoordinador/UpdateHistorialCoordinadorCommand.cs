using System;

namespace CleanArchitecture.Domain.Commands.HistorialCoordinadores.UpdateHistorialCoordinador;

public sealed class UpdateHistorialCoordinadorCommand : CommandBase
{
    private static readonly UpdateHistorialCoordinadorCommandValidation s_validation = new();

    public Guid HistorialCoordinadorId { get; }
    public Guid UserId { get; }
    public Guid GrupoInvestigacionId { get; }
    public DateTime FechaInicio { get; }
    public DateTime FechaFin { get; }




    public UpdateHistorialCoordinadorCommand(
        Guid historialcoordinadorId,
        Guid userId,
        Guid grupoInvestigacionId,
        DateTime fechainicio,
        DateTime fechafin) : base(historialcoordinadorId)
    {
        HistorialCoordinadorId = historialcoordinadorId;
        UserId = userId;
        GrupoInvestigacionId = grupoInvestigacionId;
        FechaInicio = fechainicio;
        FechaFin = fechafin;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}