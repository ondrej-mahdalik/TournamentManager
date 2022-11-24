using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TournamentManager.Server.DAL.Entities;
using TournamentManager.Server.DAL.UnitOfWork;
using TournamentManager.Common.Models;

namespace TournamentManager.Server.BL.Facades;

public class TeamIsParticipatingFacade : CRUDFacade<TeamIsParticipatingEntity, TeamIsParticipatingModel>
{
    public TeamIsParticipatingFacade(IMapper mapper, IUnitOfWorkFactory unitOfWorkFactory) : base(mapper,
        unitOfWorkFactory)
    {
        
    }
    public override async Task<IEnumerable<TeamIsParticipatingModel>> GetAsync()
    {
        await using var uow = UnitOfWorkFactory.Create();
        var query = uow
            .GetRepository<TeamIsParticipatingEntity>()
            .Get()
            .Include(x => x.Tournament)
            .Include(x => x.Team);

        return await Mapper.ProjectTo<TeamIsParticipatingModel>(query).ToArrayAsync().ConfigureAwait(false);
    }
    
}