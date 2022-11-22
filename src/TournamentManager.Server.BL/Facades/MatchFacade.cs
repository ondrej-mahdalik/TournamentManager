using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TournamentManager.Server.DAL.Entities;
using TournamentManager.Server.DAL.UnitOfWork;
using TournamentManager.Common.Models;

namespace TournamentManager.Server.BL.Facades;

public class MatchFacade : CRUDFacade<MatchEntity, MatchModel>
{
    public MatchFacade(IMapper mapper, IUnitOfWorkFactory unitOfWorkFactory) : base(mapper, unitOfWorkFactory)
    {
    }

    public override async Task<IEnumerable<MatchModel>> GetAsync()
    {
        await using var uow = UnitOfWorkFactory.Create();
        var query = uow
            .GetRepository<MatchEntity>()
            .Get()
            .Include(x => x.Team1)
            .Include(x => x.Team2)
            .Include(x => x.Tournament);

        return await Mapper.ProjectTo<MatchModel>(query).ToArrayAsync().ConfigureAwait(false);
    }
    
    public override async Task<MatchModel?> GetAsync(Guid id)
    {
        await using var uow = UnitOfWorkFactory.Create();
        var entity = await uow
            .GetRepository<MatchEntity>()
            .Get()
            .Include(x => x.Team1)
            .Include(x => x.Team2)
            .Include(x => x.Tournament).FirstOrDefaultAsync(x => x.Id == id);

        return Mapper.Map<MatchModel?>(entity);
    }
}