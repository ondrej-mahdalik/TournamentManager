using System.Text.RegularExpressions;
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
            .Include(x => x.Team2);

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

    public async Task<IEnumerable<MatchModel>> GetByTournamentIdAsync(Guid id)
    {
        await using var uow = UnitOfWorkFactory.Create();
        var query = uow
            .GetRepository<MatchEntity>()
            .Get()
            .Include(x => x.Team1)
            .Include(x => x.Team2)
            .Where(x => x.TournamentId == id);

        return await Mapper.ProjectTo<MatchModel>(query).ToArrayAsync().ConfigureAwait(false);
    }

    public async Task<bool> InsertManyAsync(List<MatchModel> matches)
    {
        await using var uow = UnitOfWorkFactory.Create();
        var repository = uow
            .GetRepository<MatchEntity>();

        try
        {
            foreach (var match in matches)
            {
                await repository.InsertOrUpdateAsync(match, Mapper).ConfigureAwait(false);
            }

            await uow.CommitAsync();
            return true;
        }
        catch
        {
            repository.DeleteRange(matches.Select(x => x.Id));
            await uow.CommitAsync();
            return false;
        }
    }
    
    public async Task<bool> InsertOrUpdateManyAsync(List<MatchModel> matches)
    {
        await using var uow = UnitOfWorkFactory.Create();
        var repository = uow
            .GetRepository<MatchEntity>();

        try
        {
            foreach (var match in matches)
            {
                await repository.InsertOrUpdateAsync(match, Mapper).ConfigureAwait(false);
            }

            await uow.CommitAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
    public async Task<List<MatchModel>> GetByUserIdAsync(Guid userId)
    {
        await using var uow = UnitOfWorkFactory.Create();
        var query = uow
            .GetRepository<MatchEntity>()
            .Get()
            .Include(x => x.Team1)
            .Include(x => x.Team2)
            .Include(x => x.Tournament)
            .Where(x => x.Team2 != null && x.Team1 != null && (x.Team1.LeaderId == userId || x.Team1.Members.Any(y => y.Id == userId)
                    || x.Team2.LeaderId == userId || x.Team2.Members.Any(y => y.Id == userId)));

        return await Mapper.ProjectTo<MatchModel>(query).ToListAsync().ConfigureAwait(false);
    }
}