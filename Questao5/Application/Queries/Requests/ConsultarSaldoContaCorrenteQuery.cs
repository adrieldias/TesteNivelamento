using MediatR;
using Questao5.Domain.Dtos;
using Questao5.Domain.Entities;

namespace Questao5.Application.Queries.Requests
{
    public class ConsultarSaldoContaCorrenteQuery : IRequest<ResponseDto>
    {
        public string Id { get; set; }
    }
}
