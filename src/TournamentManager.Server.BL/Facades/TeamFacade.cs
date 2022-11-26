using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TournamentManager.Server.DAL.Entities;
using TournamentManager.Server.DAL.UnitOfWork;
using TournamentManager.Common.Models;

namespace TournamentManager.Server.BL.Facades;

public class TeamFacade : CRUDFacade<TeamEntity, TeamModel>
{
    public TeamFacade(IMapper mapper, IUnitOfWorkFactory unitOfWorkFactory) : base(mapper, unitOfWorkFactory)
    {

    }

    public async Task<IEnumerable<TeamModel>> GetByUserAsync(Guid id)
    {
        await using var uow = UnitOfWorkFactory.Create();
        var query = uow.GetRepository<TeamEntity>()
            .Get()
            .Where(x => x.LeaderId == id);
        
        return await Mapper.ProjectTo<TeamModel>(query).ToArrayAsync().ConfigureAwait(false);
    }
}