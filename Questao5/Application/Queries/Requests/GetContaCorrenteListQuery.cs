using MediatR;
using Questao5.Domain.Entities;

namespace Questao5.Application.Queries.Requests
{
    public class GetContaCorrenteListQuery : IRequest<List<ContaCorrente>>
    {
    }
}
