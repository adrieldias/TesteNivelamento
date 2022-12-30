using MediatR;
using Microsoft.EntityFrameworkCore;
using Questao5.Application.Queries.Requests;
using Questao5.Domain.Dtos;
using Questao5.Domain.Entities;
using Questao5.Domain.Resources;
using Questao5.Infrastructure.Database.Repositories.Interfaces;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Questao5.Application.Handlers
{
    public class ConsultarSaldoContaCorrenteHandler : IRequestHandler<ConsultarSaldoContaCorrenteQuery, ResponseDto>
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepository;

        public ConsultarSaldoContaCorrenteHandler(IContaCorrenteRepository contaCorrenteRepository)
        {
            _contaCorrenteRepository = contaCorrenteRepository;
        }

        public async Task<ResponseDto> Handle(ConsultarSaldoContaCorrenteQuery query, CancellationToken cancellationToken)
        {
            var contaCorrente = await _contaCorrenteRepository.GetAsync(query.Id);
            if (contaCorrente == null)
                return new ResponseDto(false, MessagesResource.INVALID_ACCOUNT);
            if (!contaCorrente.IsValid())
                return new ResponseDto(false, MessagesResource.INACTIVE_ACCOUNT);
            var saldo = await _contaCorrenteRepository.ConsultarSaldo(query.Id);
            return new ResponseDto(true, saldo);
        }
    }
}
