using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.Repositories.Interfaces
{
    public interface IMovimentoRepository : IRepository<Movimento>
    {
        Task<Movimento> GetAsync(string id);
    }
}
