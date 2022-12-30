namespace Questao5.Domain.Dtos
{
    public class SaldoDto
    {
        public int NumeroContaCorrente { get; set; }
        public string TitularContaCorrente { get; set; }
        public DateTime DataConsulta { get; set; }
        public double Saldo { get; set; }
    }
}
