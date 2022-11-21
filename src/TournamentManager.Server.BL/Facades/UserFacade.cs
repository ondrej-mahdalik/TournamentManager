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

    public async Task<string?> GetApplicationUserId(Guid id)
    {
        await using var uow = UnitOfWorkFactory.Create();
        var entity = await uow.GetRepository<UserEntity>()
            .Get()
            .FirstOrDefaultAsync(x => x.Id == id);

        return entity?.ApplicationUserId;
    }
}