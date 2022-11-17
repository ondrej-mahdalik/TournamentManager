using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TournamentManager.Server.DAL.Entities;
using TournamentManager.Server.DAL.UnitOfWork;
using TournamentManager.Common.Models;

namespace TournamentManager.Server.BL.Facades;

public abstract class CRUDFacade<TEntity, TModel>
where TEntity : class, IEntity
where TModel : class, IModel
{
    protected readonly IMapper Mapper;
    protected readonly IUnitOfWorkFactory UnitOfWorkFactory;
    
    protected CRUDFacade(IMapper mapper, IUnitOfWorkFactory unitOfWorkFactory)
    {
        Mapper = mapper;
        UnitOfWorkFactory = unitOfWorkFactory;
    }
    
    public virtual async Task DeleteAsync(TModel model)
    {
        await DeleteAsync(model.Id);
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await using var uow = UnitOfWorkFactory.Create();
        uow.GetRepository<TEntity>().Delete(id);
        await uow.CommitAsync().ConfigureAwait(false);
    }

    public virtual async Task<TModel?> GetAsync(Guid id)
    {
        await using var uow = UnitOfWorkFactory.Create();
        var query = uow
            .GetRepository<TEntity>()
            .Get()
            .Where(e => e.Id == id);

        return await Mapper.ProjectTo<TModel>(query).SingleOrDefaultAsync().ConfigureAwait(false);
    }

    public virtual async Task<IEnumerable<TModel>> GetAsync()
    {
        await using var uow = UnitOfWorkFactory.Create();
        var query = uow
            .GetRepository<TEntity>()
            .Get();

        return await Mapper.ProjectTo<TModel>(query).ToArrayAsync().ConfigureAwait(false);
    }

    public virtual async Task<TModel> SaveAsync(TModel model)
    {
        await using var uow = UnitOfWorkFactory.Create();

        var entity = await uow
            .GetRepository<TEntity>()
            .InsertOrUpdateAsync(model, Mapper)
            .ConfigureAwait(false);
        await uow.CommitAsync();

        return (await GetAsync(entity.Id).ConfigureAwait(false))!;
    }
}