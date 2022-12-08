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
    
    public async Task<IEnumerable<TeamIsParticipatingModel>> GetByUserIdAsync(Guid userId)
    {
        await using var uow = UnitOfWorkFactory.Create();
        var query = uow
            .GetRepository<TeamIsParticipatingEntity>()
            .Get()
            .Include(x => x.Tournament)
            .Include(x => x.Team)
            .Where(x => x.Team != null && (x.Team.LeaderId == userId || x.Team.Members.Any(y => y.UserId == userId)));

        return await Mapper.ProjectTo<TeamIsParticipatingModel>(query).ToArrayAsync().ConfigureAwait(false);
    }
    
}