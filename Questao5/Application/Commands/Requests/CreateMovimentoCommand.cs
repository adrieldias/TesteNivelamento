using FluentValidation;
using MediatR;
using Questao5.Domain.Dtos;
using Questao5.Domain.Entities;

namespace Questao5.Application.Commands.Requests
{
    public class CreateMovimentoCommand : IRequest<ResponseDto>
    {
        public string IdRequisicao { get; set; }
        public string IdContaCorrente { get; set; }
        public double Valor { get; set; }
        public string TipoMovimento { get; set; }
        

        public CreateMovimentoCommand(string idRequisicao, string idContaCorrente, double valor, string tipoMovimento)
        {
            IdRequisicao = idRequisicao;
            IdContaCorrente = idContaCorrente;
            Valor = valor;
            TipoMovimento = tipoMovimento;            
        }
    }
}
