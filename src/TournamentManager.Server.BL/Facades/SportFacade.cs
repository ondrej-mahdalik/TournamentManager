using AutoMapper;
using TournamentManager.Server.DAL.Entities;
using TournamentManager.Server.DAL.UnitOfWork;
using TournamentManager.Shared.Models;

namespace TournamentManager.Server.BL.Facades;

public class SportFacade : CRUDFacade<SportEntity, SportModel>
{
    public SportFacade(IMapper mapper, IUnitOfWorkFactory unitOfWorkFactory) : base(mapper, unitOfWorkFactory)
    {

    }
}