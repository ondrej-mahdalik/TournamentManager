using AutoMapper;
using TournamentManager.Server.DAL.Entities;
using TournamentManager.Server.DAL.UnitOfWork;
using TournamentManager.Shared.Models;

namespace TournamentManager.Server.BL.Facades;

public class UserIsInTeamFacade : CRUDFacade<UserIsInTeamEntity, UserIsInTeamModel>
{
    public UserIsInTeamFacade(IMapper mapper, IUnitOfWorkFactory unitOfWorkFactory) : base(mapper, unitOfWorkFactory)
    {

    }
}