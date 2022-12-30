using MediatR;
using Questao5.Application.Queries.Requests;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.Repositories.Interfaces;

namespace Questao5.Application.Handlers
{
    public class GetContaCorrenteListHandler : IRequestHandler<GetContaCorrenteListQuery, List<ContaCorrente>>
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepository;

        public GetContaCorrenteListHandler(IContaCorrenteRepository contaCorrenteRepository)
        {
            _contaCorrenteRepository = contaCorrenteRepository;
        }

        public async Task<List<ContaCorrente>> Handle(GetContaCorrenteListQuery query, CancellationToken cancellationToken)
        {
            return await _contaCorrenteRepository.GetAllAsync();
        }
    }
}
