using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.Repositories.Interfaces
{
    public interface IIdempotenciaRepository : IRepository<Idempotencia>
    {
        Task<Idempotencia> GetAsync(string id);
    }
}
