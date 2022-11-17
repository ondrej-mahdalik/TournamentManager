using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TournamentManager.Server.DAL.Entities;
using TournamentManager.Server.DAL.UnitOfWork;
using TournamentManager.Shared.Models;

namespace TournamentManager.Server.BL.Facades;

public class TournamentFacade : CRUDFacade<TournamentEntity, TournamentModel>
{
    public TournamentFacade(IMapper mapper, IUnitOfWorkFactory unitOfWorkFactory) : base(mapper, unitOfWorkFactory)
    {

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
}