using System.Globalization;

namespace Questao1
{
    public class ContaBancaria {

        public int NumeroConta { get; }
        public string Titular { get; set; }
        private double _saldo;

        public ContaBancaria(int numeroConta, string titular, double depositoInicial = 0.0)
        {
            NumeroConta = numeroConta;
            Titular = titular;
            _saldo = depositoInicial;
        }

        public double Saldo => _saldo;
        public void Deposito(double valor) => _saldo += valor;
        public void Saque(double valor)
        {
            _saldo -= valor + 3.5; // aplica a taxa de $ 3.50 por saque
        }
        public override string ToString()
        {
            return $"Conta {NumeroConta}, Titular: {Titular}, Saldo: $ {_saldo.ToString("F2")}";
        }
    }
}
