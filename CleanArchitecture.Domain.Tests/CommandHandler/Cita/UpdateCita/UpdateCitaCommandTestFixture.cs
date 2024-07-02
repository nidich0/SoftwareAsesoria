using System;
using CleanArchitecture.Domain.Commands.Citas.CreateCita;
using CleanArchitecture.Domain.Commands.Citas.UpdateCita;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Cita.UpdateCita;

public sealed class UpdateCitaCommandTestFixture : CommandHandlerFixtureBase
{
    public UpdateCitaCommandHandler CommandHandler { get; }
    public ICitaRepository CitaRepository { get; }

    public UpdateCitaCommandTestFixture()
    {
        CitaRepository = Substitute.For<ICitaRepository>();

        CommandHandler = new UpdateCitaCommandHandler(
            Bus,
            UnitOfWork,
            NotificationHandler,
            CitaRepository,
            User);
    }

    public Entities.Cita SetupCita()
    {
        var cita = new Entities.Cita(
            Guid.NewGuid(),
            Guid.NewGuid().ToString(),
            Guid.NewGuid(),
            Guid.NewGuid());

        CitaRepository
            .GetByIdAsync(Arg.Is<Guid>(y => y == cita.Id))
            .Returns(cita);

        return cita;
    }

    public void SetupUser()
    {
        User.GetUserRole().Returns(UserRole.User);
    }

    public void SetupExistingCita(Guid id)
    {
        CitaRepository
            .GetByIdAsync(Arg.Is<Guid>(x => x == id))
            .Returns(new Entities.Cita(id, Guid.NewGuid().ToString(), Guid.NewGuid(), Guid.NewGuid()));
    }
}