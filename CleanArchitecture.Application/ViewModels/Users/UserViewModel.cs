﻿using System;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.ViewModels.Users;

public sealed class UserViewModel
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string Codigo { get; set; } = string.Empty;
    public string Foto { get; set; } = string.Empty;
    public string Preferencias {  get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public UserStatus Status { get; set; }
    public Guid? EscuelaId { get; set; }

    public static UserViewModel FromUser(User user)
    {
        return new UserViewModel
        {
            Id = user.Id,
            TenantId = user.TenantId,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Telefono = user.Telefono,
            Codigo = user.Codigo,
            Foto = user.Foto,
            Preferencias = user.Preferencias,
            Role = user.Role,
            Status = user.Status,
            EscuelaId = user.EscuelaId
        };
    }
}