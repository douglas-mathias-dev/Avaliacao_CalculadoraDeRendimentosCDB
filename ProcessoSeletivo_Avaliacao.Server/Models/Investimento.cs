namespace ProcessoSeletivo_Avaliacao.Server.Models
{
    public class Investimento
    {
        public decimal ValorInicial { get; set; }
        public int Prazo { get; set; }
        public decimal RetornoLiquido { get; set; }
        public decimal RetornoBruto { get; set; }
    }
}
