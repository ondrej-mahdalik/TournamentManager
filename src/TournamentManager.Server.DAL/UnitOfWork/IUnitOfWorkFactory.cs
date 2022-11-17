namespace TournamentManager.Server.DAL.UnitOfWork;

public interface IUnitOfWorkFactory
{
    IUnitOfWork Create();
}
