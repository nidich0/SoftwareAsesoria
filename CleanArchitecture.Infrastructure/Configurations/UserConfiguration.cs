using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CleanArchitecture.Infrastructure.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .Property(user => user.Email)
            .IsRequired()
            .HasMaxLength(MaxLengths.User.Email);

        builder
            .Property(user => user.FirstName)
            .IsRequired()
            .HasMaxLength(MaxLengths.User.FirstName);

        builder
            .Property(user => user.LastName)
            .IsRequired()
            .HasMaxLength(MaxLengths.User.LastName);

        builder
            .Property(user => user.Password)
            .IsRequired()
            .HasMaxLength(MaxLengths.User.Password);

        builder
            .Property(user => user.Telefono)
            .IsRequired()
            .HasMaxLength(MaxLengths.User.Telefono);

        builder
            .Property(user => user.Codigo)
            .IsRequired()
            .HasMaxLength(MaxLengths.User.Codigo);

        builder.HasData(new User(
            Ids.Seed.UserId,
            Ids.Seed.TenantId,
            null,
            "admin@email.com",
            "Admin",
            "User",
            // !Password123#
            "$2a$12$Blal/uiFIJdYsCLTMUik/egLbfg3XhbnxBC6Sb5IKz2ZYhiU/MzL2",
            "123456789",
            "0123456789",
            UserRole.Admin));

        builder.HasData(new User(
            Guid.NewGuid(),
            Ids.Seed.TenantId,
            null,
            "fabrizzio_fabiano@outlook.com",
            "Fabrizzio Fabiano",
            "Esquivel Mori",
            // !Password123#
            "$2a$12$Blal/uiFIJdYsCLTMUik/egLbfg3XhbnxBC6Sb5IKz2ZYhiU/MzL2",
            "123456789",
            "0123456789",
            UserRole.Asesorado));
    }
}