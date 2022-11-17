using AutoMapper;
using TournamentManager.Server.DAL.Entities;

namespace TournamentManager.Server.DAL.UnitOfWork;

public interface IRepository<TEntity> where TEntity : class, IEntity
{
    IQueryable<TEntity> Get();
    void Delete(Guid entityId);
    void DeleteRange(IEnumerable<Guid> entityIds);

    Task<TEntity> InsertOrUpdateAsync<TModel>(
        TModel model,
        IMapper mapper,
        CancellationToken cancellationToken = default) where TModel : class;
}
