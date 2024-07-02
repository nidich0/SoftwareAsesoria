using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations;

public sealed class SolicitudConfiguration : IEntityTypeConfiguration<Solicitud>
{
    public void Configure(EntityTypeBuilder<Solicitud> builder)
    {
        
        builder
            .Property(user => user.NumeroTesis)
            .IsRequired()
            .HasMaxLength(MaxLengths.Solicitud.NumeroTesis);

        builder
            .Property(user => user.Afinidad)
            .IsRequired()
            .HasMaxLength(MaxLengths.Solicitud.Afinidad);

        builder.HasData(new Solicitud(
            Ids.Seed.SolicitudId,
            Ids.Seed.AsesoradoUserId,
            Ids.Seed.AsesorUserId,
            "1212312",
            "ciberseguridad",
            "Lorem ipsum",
            SolicitudStatus.Pendiente
            ));
        
    }
}