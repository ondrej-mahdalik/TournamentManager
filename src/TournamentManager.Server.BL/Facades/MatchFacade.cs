using AutoMapper;
using TournamentManager.Server.DAL.Entities;
using TournamentManager.Server.DAL.UnitOfWork;
using TournamentManager.Shared.Models;

namespace TournamentManager.Server.BL.Facades;

public class MatchFacade : CRUDFacade<MatchEntity, MatchModel>
{
    public MatchFacade(IMapper mapper, IUnitOfWorkFactory unitOfWorkFactory) : base(mapper, unitOfWorkFactory)
    {

    }
}