using System;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Domain.Commands.Solicitudes.UpdateSolicitud;

public sealed class UpdateSolicitudCommand : CommandBase
{
    private static readonly UpdateSolicitudCommandValidation s_validation = new();

    public string NumeroTesis { get; }
    public string Afinidad { get; }
    public SolicitudStatus Estado { get; }

    public UpdateSolicitudCommand(Guid solicitudId, string numeroTesis,
        string afinidad, SolicitudStatus estado) : base(solicitudId)
    {
        NumeroTesis = numeroTesis;
        Afinidad = afinidad;
        Estado = estado;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}