using System;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Domain.Commands.Users.UpdateUser;

public sealed class UpdateUserCommand(
    Guid userId,
    string email,
    string firstName,
    string lastName,
    string telefono,
    string foto,
    UserRole role, Guid tenantId) : CommandBase(userId)
{
    private static readonly UpdateUserCommandValidation s_validation = new();

    public Guid UserId { get; } = userId;
    public Guid TenantId { get; } = tenantId;
    public string Email { get; } = email;
    public string FirstName { get; } = firstName;
    public string LastName { get; } = lastName;
    public string Telefono { get; } = telefono;
    public string Foto { get; } = foto;
    public UserRole Role { get; } = role;

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}