using System;

namespace CleanArchitecture.Application.ViewModels.Users;

public sealed record CreateUserViewModel(
    Guid EscuelaId,
    string Email,
    string FirstName,
    string LastName,
    string Password,
    string Telefono,
    string Codigo,
    string Foto);