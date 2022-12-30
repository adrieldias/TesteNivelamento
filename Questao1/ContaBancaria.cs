using System.Globalization;

namespace Questao1
{
    class ContaBancaria {
        public int Numero { get; set; }
        public string NomeTitular { get; set; }
        public double Saldo { get; set; }
        private readonly double _taxa = 3.5;
        public ContaBancaria(int numero, string nomeTitular, double saldo)
        {
            this.Numero = numero;
            this.NomeTitular = nomeTitular;
            this.Saldo = saldo;
        }
        public ContaBancaria(int numero, string nomeTitular)
        {
            this.Numero = numero;
            this.NomeTitular = nomeTitular;
            this.Saldo = 0;
        }
        public void Deposito(double quantia)
        {
            this.Saldo += quantia;
        }
        public void Saque(double quantia)
        {
            this.Saldo -= quantia + this._taxa;
        }
        public override string ToString()
        {
            return $"Conta {this.Numero}, Titular {this.NomeTitular}, Saldo $ {this.Saldo.ToString("0.00", CultureInfo.InvariantCulture)}";
        }
    }
}
