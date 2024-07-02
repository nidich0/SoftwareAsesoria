using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels.GrupoInvestigaciones;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using MediatR;

namespace CleanArchitecture.Application.Queries.GrupoInvestigaciones.GetGrupoInvestigacionById;

public sealed class GetGrupoInvestigacionByIdQueryHandler :
    IRequestHandler<GetGrupoInvestigacionByIdQuery, GrupoInvestigacionViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IGrupoInvestigacionRepository _grupoinvestigacionRepository;

    public GetGrupoInvestigacionByIdQueryHandler(IGrupoInvestigacionRepository grupoinvestigacionRepository, IMediatorHandler bus)
    {
        _grupoinvestigacionRepository = grupoinvestigacionRepository;
        _bus = bus;
    }

    public async Task<GrupoInvestigacionViewModel?> Handle(GetGrupoInvestigacionByIdQuery request, CancellationToken cancellationToken)
    {
        var grupoinvestigacion = await _grupoinvestigacionRepository.GetByIdAsync(request.GrupoInvestigacionId);

        if (grupoinvestigacion is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetGrupoInvestigacionByIdQuery),
                    $"GrupoInvestigacion with id {request.GrupoInvestigacionId} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return GrupoInvestigacionViewModel.FromGrupoInvestigacion(grupoinvestigacion);
    }
}