using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProcessoSeletivo_Avaliacao.Server.Models;

namespace ProcessoSeletivo_Avaliacao.Server.Services
{
    public class InvestimentoService
    {
        private const double TB = 1.08;
        private const double CDI = 0.009;
        private Investimento _investimento { get; set; }

        public InvestimentoService(Investimento investimento)
        {
            _investimento = investimento;
        }

        public ProblemDetails ValidarEntrada()
        {
            ProblemDetails? erro = new ProblemDetails();
            if (_investimento.ValorInicial <= 0)
            {
                erro.Status =  StatusCodes.Status400BadRequest;
                erro.Title = "Valor Inicial Inválido";
                erro.Detail = "O valor inicial deve ser maior que R$ 0.";
            }

            if (_investimento.ValorInicial > 999999999999999)
            {
                erro.Status = StatusCodes.Status400BadRequest;
                erro.Title = "Valor Inicial Inválido";
                erro.Detail = "O valor inicial deve ser no maximo R$ 999.999.999.999.999.";
            }

            if (_investimento.Prazo <= 1)
            {
                erro.Status = StatusCodes.Status400BadRequest;
                erro.Title = "Prazo Inválido";
                erro.Detail = "O prazo deve ser maior que um mês.";
            }

            if (_investimento.Prazo > 1200)
            {
                erro.Status = StatusCodes.Status400BadRequest;
                erro.Title = "Prazo Inválido";
                erro.Detail = "O prazo deve ser menor que 1200 (Cem Anos).";
            }

            return erro;
        }
        public Investimento CalcularRetorno()
        {
            CalcularRetornoBruto();
            CalcularRetornoLiquido();
            return _investimento;
        }

        private void CalcularRetornoBruto()
        {
            _investimento.RetornoBruto = _investimento.ValorInicial * Math.Pow(1 + (CDI * TB), _investimento.Prazo);
        }

        private void CalcularRetornoLiquido()
        {
            double rendimento = _investimento.RetornoBruto - _investimento.ValorInicial;
            double aliquota = 0.225;

            if (_investimento.Prazo > 24)
            {
                aliquota = 0.15;
            }
            else if (_investimento.Prazo > 12)
            {
                aliquota = 0.175;
            }
            else if (_investimento.Prazo > 6)
            {
                aliquota = 0.20;
            }

            _investimento.RetornoLiquido = _investimento.ValorInicial + (rendimento * (1 - aliquota));
        }
    }
}
