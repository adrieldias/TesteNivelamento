namespace Questao5.Infrastructure.Database.UoW
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();
    }
}
