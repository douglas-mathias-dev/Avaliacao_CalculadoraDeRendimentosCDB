using Microsoft.AspNetCore.Mvc;
using ProcessoSeletivo_Avaliacao.Server.Models;

namespace ProcessoSeletivo_Avaliacao.Server.Services
{
    public class InvestimentoService
    {
        private const decimal CDI = 0.009m;
        private const decimal TB = 1.08m;
        readonly Investimento _investimento;

        public InvestimentoService(Investimento investimento)
        {
            _investimento = new Investimento()
            {
                ValorInicial = investimento.ValorInicial
                , Prazo = investimento.Prazo 
            };
        }

        public ProblemDetails? ValidarEntrada()
        {
            ProblemDetails? erro = null;
            if (_investimento.ValorInicial <= 0)
            {
                erro = new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest
                    , Title = "Valor Inicial Inválido"
                    , Detail = "O valor inicial deve ser maior que R$ 0."
                };
            }

            if (_investimento.ValorInicial > 999999999999999.99m)
            {
                erro = new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest
                    , Title = "Valor Inicial Inválido"
                    , Detail = "O valor inicial deve ser no maximo R$ 999.999.999.999.999."
                };
            }

            if (_investimento.Prazo <= 1)
            {
                erro = new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest
                    , Title = "Prazo Inválido"
                    , Detail = "O prazo deve ser maior que um mês."
                };
            }

            if (_investimento.Prazo > 1200)
            {
                erro = new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest
                    , Title = "Prazo Inválido"
                    , Detail = "O prazo deve ser menor que 1200 (Cem Anos)."
                };
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
            _investimento.RetornoBruto = _investimento.ValorInicial;
            for (int i = 0; i < _investimento.Prazo; i++)
            {
                _investimento.RetornoBruto = _investimento.RetornoBruto * (1 + (CDI * TB));
            }
        }

        private void CalcularRetornoLiquido()
        {
            decimal rendimento = _investimento.RetornoBruto - _investimento.ValorInicial;
            decimal aliquota = 0.225m;

            if (_investimento.Prazo > 24)
            {
                aliquota = 0.15m;
            }
            else if (_investimento.Prazo > 12)
            {
                aliquota = 0.175m;
            }
            else if (_investimento.Prazo > 6)
            {
                aliquota = 0.2m;
            }

            _investimento.RetornoLiquido = _investimento.ValorInicial + (rendimento * (1 - aliquota));
        }
    }
}
