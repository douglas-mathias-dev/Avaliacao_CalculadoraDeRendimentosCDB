using ProcessoSeletivo_Avaliacao.Server.Controllers;

namespace ProcessoSeletivo_Avaliacao.Server.Model
{
    public class Investimento
    {
        private const double TB = 1.08;
        private const double CDI = 0.009;
        public double ValorInicial { get; set; }
        public int Prazo { get; set; }
        public double RetornoLiquido { get; set; }
        public double RetornoBruto { get; set; }

        public Investimento(double valorInicial, int prazo)
        {
            ValorInicial = valorInicial;
            Prazo = prazo;
            CalcularRetornoLiquido();
        }
        
        public void CalcularRetornoBruto()
        {
            RetornoBruto = ValorInicial * Math.Pow(1 + (CDI * TB), Prazo);
        }

        public void CalcularRetornoLiquido()
        {
            CalcularRetornoBruto();

            double rendimento = RetornoBruto - ValorInicial;
            double aliquota = 0.225;

            if (Prazo > 24)
            {
                aliquota = 0.15;
            }
            else if(Prazo > 12)
            {
                aliquota= 0.175;
            }
            else if (Prazo > 6)
            {
                aliquota= 0.20;
            }

            RetornoLiquido = ValorInicial + (rendimento * (1 - aliquota));
        }
    }
}
