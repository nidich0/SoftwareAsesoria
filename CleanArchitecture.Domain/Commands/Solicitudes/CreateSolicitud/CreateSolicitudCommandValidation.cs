using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.Solicitudes.CreateSolicitud;

public sealed class CreateSolicitudCommandValidation : AbstractValidator<CreateSolicitudCommand>
{
    public CreateSolicitudCommandValidation()
    {
        AddRuleForId();
        AddRuleForAsesoradoUserId();
        AddRuleForAsesorUserId();
        AddRuleForNumeroTesis();

        AddRuleForAfinidad();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Solicitud.EmptyId)
            .WithMessage("Solicitud id may not be empty");
    }

    private void AddRuleForAsesoradoUserId()
    {
        RuleFor(cmd => cmd.AsesoradoUserId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.User.EmptyId)
            .WithMessage("Asesorado id may not be empty");
    }
    private void AddRuleForAsesorUserId()
    {
        RuleFor(cmd => cmd.AsesorUserId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.User.EmptyId)
            .WithMessage("Asesor id may not be empty");
    }

    private void AddRuleForNumeroTesis()
    {
        RuleFor(cmd => cmd.NumeroTesis)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Solicitud.EmptyNumeroTesis)
            .WithMessage("NumeroTesis may not be empty")
            .MaximumLength(MaxLengths.Solicitud.NumeroTesis)
            .WithErrorCode(DomainErrorCodes.Solicitud.NumeroTesisExceedsMaxLength)
            .WithMessage($"NumeroTesis may not be longer than {MaxLengths.Solicitud.NumeroTesis} characters");
    }

    private void AddRuleForAfinidad()
    {
        RuleFor(cmd => cmd.Afinidad)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Solicitud.EmptyAfinidad)
            .WithMessage("Afinidad may not be empty")
            .MaximumLength(MaxLengths.Solicitud.Afinidad)
            .WithErrorCode(DomainErrorCodes.Solicitud.AfinidadExceedsMaxLength)
            .WithMessage($"Afinidad may not be longer than {MaxLengths.Solicitud.Afinidad} characters");
    }
}