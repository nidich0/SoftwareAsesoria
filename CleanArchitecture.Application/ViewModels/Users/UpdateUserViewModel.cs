using System;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.ViewModels.Users;

public sealed record UpdateUserViewModel(
    Guid Id,
    string Email,
    string FirstName,
    string LastName,
    string Telefono,
    string Foto,
    UserRole Role,
    Guid TenantId);