using Questao5.Domain.Dtos;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.Repositories.Interfaces
{
    public interface IContaCorrenteRepository : IRepository<ContaCorrente>
    {
        Task<SaldoDto> ConsultarSaldo(string idContaCorrente);
    }
}
