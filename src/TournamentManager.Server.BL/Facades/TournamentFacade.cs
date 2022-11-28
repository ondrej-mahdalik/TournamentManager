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
        // Deleted related entities
        await using var uow = UnitOfWorkFactory.Create();
        var matches = await uow.GetRepository<MatchEntity>()
            .Get()
            .Where(x => x.TournamentId == id)
            .ToListAsync();
        uow.GetRepository<MatchEntity>().DeleteRange(matches.Select(x => x.Id));

        var participatings = await uow.GetRepository<TeamIsParticipatingEntity>()
            .Get()
            .Where(x => x.TournamentId == id)
            .ToListAsync();
        uow.GetRepository<TeamIsParticipatingEntity>().DeleteRange(participatings.Select(x => x.Id));

        await uow.CommitAsync();
        await base.DeleteAsync(id);
    }

    public async Task<bool> CanUserViewTournament(Guid? userId, Guid tournamentId)
    {
        await using var uow = UnitOfWorkFactory.Create();
        var tournament = await uow.GetRepository<TournamentEntity>()
            .Get()
            .FirstOrDefaultAsync(x => x.Id == tournamentId);
        UserEntity? user = null;
        if (userId is not null)
            user = await uow.GetRepository<UserEntity>()
                .Get()
                .FirstOrDefaultAsync(x => x.Id == userId);

        if (tournament is null)
            return false;
        
        return tournament.IsPublic || (user?.IsAdministrator ?? false) || tournament.CreatorId == userId
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
            .FirstOrDefaultAsync(x => x.Id == id);

        return Mapper.Map<TournamentModel?>(entity);
    }
    
    public override async Task<IEnumerable<TournamentModel>> GetAsync()
    {
        await using var uow = UnitOfWorkFactory.Create();
        var query = uow
            .GetRepository<TournamentEntity>()
            .Get()
            .Include(x => x.Participatings)
            .Include(x => x.WinnerTeam);

        return await Mapper.ProjectTo<TournamentModel>(query).ToArrayAsync().ConfigureAwait(false);
    }
}