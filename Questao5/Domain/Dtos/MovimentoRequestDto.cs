using FluentValidation;

namespace Questao5.Domain.Dtos
{
    public class MovimentoRequestDto
    {
        public string IdRequisicao { get; set; }
        public string IdContaCorrente { get; set; }
        public double Valor { get; set; }
        public string TipoMovimento { get; set; }
    }
}
