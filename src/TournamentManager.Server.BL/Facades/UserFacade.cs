using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TournamentManager.Server.DAL.Entities;
using TournamentManager.Server.DAL.UnitOfWork;
using TournamentManager.Common.Models;
using TournamentManager.Server.Auth.Models;

namespace TournamentManager.Server.BL.Facades;

public class UserFacade : CRUDFacade<UserEntity, UserModel>
{
    public UserFacade(IMapper mapper, IUnitOfWorkFactory unitOfWorkFactory) : base(mapper, unitOfWorkFactory)
    {
    }

    public async Task<UserModel?> GetByApplicationUserIdAsync(string applicationUserId)
    {
        await using var uow = UnitOfWorkFactory.Create();
        var userEntity = await uow.GetRepository<UserEntity>()
            .Get()
            .FirstOrDefaultAsync(x => x.ApplicationUserId == applicationUserId);
        
        if (userEntity is null)
            return null;
        
        return await GetAsync(userEntity.Id);
    }

    public override async Task<UserModel?> GetAsync(Guid id)
    {
        await using var uow = UnitOfWorkFactory.Create();
        var query = uow.GetRepository<UserEntity>()
            .Get()
            .Include(x => x.TeamsAsLeader)
            .Where(x => x.Id == id);

        return await Mapper.ProjectTo<UserModel>(query).SingleOrDefaultAsync().ConfigureAwait(false);
    }

    public async Task<string?> GetApplicationUserId(Guid id)
    {
        await using var uow = UnitOfWorkFactory.Create();
        var entity = await uow.GetRepository<UserEntity>()
            .Get()
            .FirstOrDefaultAsync(x => x.Id == id);

        return entity?.ApplicationUserId;
    }

    public override async Task DeleteAsync(Guid id)
    {
        // Delete related entities
        await using var uow = UnitOfWorkFactory.Create();
        
        var teamsAsMember = await uow.GetRepository<UserIsInTeamEntity>()
            .Get()
            .Where(x => x.UserId == id)
            .ToListAsync();
        
        foreach (var teamEntity in teamsAsMember)
            uow.GetRepository<UserIsInTeamEntity>().Delete(teamEntity.Id);
        
        var teamsAsLeader = await uow.GetRepository<TeamEntity>()
            .Get()
            .Where(x => x.LeaderId == id)
            .ToListAsync();

        foreach (var teamEntity in teamsAsLeader)
            uow.GetRepository<TeamEntity>().Delete(teamEntity.Id);

        await uow.CommitAsync();
        await base.DeleteAsync(id);
    }
}