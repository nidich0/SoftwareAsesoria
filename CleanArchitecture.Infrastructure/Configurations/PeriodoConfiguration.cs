using System;
using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations;

public sealed class PeriodoConfiguration : IEntityTypeConfiguration<Periodo>
{
    public void Configure(EntityTypeBuilder<Periodo> builder)
    {
        builder
            .Property(periodo => periodo.FechaInicio)
            .IsRequired();

        builder
            .Property(periodo => periodo.FechaFinal)
            .IsRequired();

        builder
            .Property(periodo => periodo.Nombre)
            .IsRequired()
            .HasMaxLength(MaxLengths.Periodo.Nombre);

        builder.HasData(new Periodo(
            Ids.Seed.PeriodoId,
            new DateOnly (2000, 12, 12),
            new DateOnly (2001, 12, 12),
            "Periodo"));
    }
}