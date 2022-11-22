using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TournamentManager.Server.DAL.Entities;
using TournamentManager.Server.DAL.UnitOfWork;
using TournamentManager.Common.Models;

namespace TournamentManager.Server.BL.Facades;

public class TournamentFacade : CRUDFacade<TournamentEntity, TournamentModel>
{
    public TournamentFacade(IMapper mapper, IUnitOfWorkFactory unitOfWorkFactory) : base(mapper, unitOfWorkFactory)
    {

    }

    public override async Task DeleteAsync(Guid id)
    {
        // Load related entities to support client cascade delete
        await using var uow = UnitOfWorkFactory.Create(); 
        _ = await uow.GetRepository<MatchEntity>()
            .Get()
            .Where(x => x.TournamentId == id)
            .ToListAsync();
        _ = await uow.GetRepository<TeamIsParticipatingEntity>()
            .Get()
            .Where(x => x.TournamentId == id)
            .ToListAsync();
        
        await base.DeleteAsync(id);
    }

    public async Task<bool> CanUserViewTournament(Guid userId, Guid tournamentId)
    {
        await using var uow = UnitOfWorkFactory.Create();
        var tournament = await uow.GetRepository<TournamentEntity>()
            .Get()
            .FirstOrDefaultAsync(x => x.Id == tournamentId);
        var user = await uow.GetRepository<UserEntity>()
            .Get()
            .FirstOrDefaultAsync(x => x.Id == userId);

        if (tournament is null || user is null)
            return false;
        
        return tournament.IsPublic || user.IsAdministrator || tournament.CreatorId == userId
               || await uow.GetRepository<TeamIsParticipatingEntity>()
                   .Get()
                   .AnyAsync(x => x.Team != null && x.TournamentId == tournamentId && x.Team.Members.Any(y => y.UserId == userId));
    }
    
    public override async Task<TournamentModel?> GetAsync(Guid id)
    {
        await using var uow = UnitOfWorkFactory.Create();
        var entity = await uow
            .GetRepository<TournamentEntity>()
            .Get()
            .Include(x => x.Matches)
            .ThenInclude(x => x.Team1)
            .Include(x => x.Matches)
            .ThenInclude(x => x.Team2)
            .FirstOrDefaultAsync(x => x.Id == id);

        return Mapper.Map<TournamentModel?>(entity);
    }
    
    public override async Task<IEnumerable<TournamentModel>> GetAsync()
    {
        await using var uow = UnitOfWorkFactory.Create();
        var query = uow
            .GetRepository<TournamentEntity>()
            .Get().Include(x => x.Participatings);

        return await Mapper.ProjectTo<TournamentModel>(query).ToArrayAsync().ConfigureAwait(false);
    }
}