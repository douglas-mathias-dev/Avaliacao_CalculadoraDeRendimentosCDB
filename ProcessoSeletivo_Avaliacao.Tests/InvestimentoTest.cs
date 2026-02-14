using DecimalMath;
using Microsoft.AspNetCore.Mvc;
using ProcessoSeletivo_Avaliacao.Server.Models;
using ProcessoSeletivo_Avaliacao.Server.Services;


namespace ProcessoSeletivo_Avaliacao.Tests
{
    public class InvestimentoTest
    {
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-0.01)]
        public void DeveRetornarErro_Quando_ValorInicialForMenorOuIgual0(decimal valorInicial)
        {
            var investimento = new Investimento
            {
                ValorInicial = valorInicial,
                Prazo = 10
            };

            InvestimentoService service = new (investimento);

            ProblemDetails? resultado = service.ValidarEntrada();

            
            Assert.That(resultado, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(resultado.Status, Is.EqualTo(400));
                Assert.That(resultado.Detail, Is.EqualTo("O valor inicial deve ser maior que R$ 0."));
            });
        }

        [TestCase(1000000000000000.0)]
        public void DeveRetornarErro_Quando_ValorInicialForMaiorQue999999999999999(decimal valorInicial)
        {
            var investimento = new Investimento
            {
                ValorInicial = valorInicial,
                Prazo = 10
            };

            InvestimentoService service = new (investimento);

            ProblemDetails? resultado = service.ValidarEntrada();

            Assert.That(resultado, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(resultado.Status, Is.EqualTo(400));
                Assert.That(resultado.Detail, Is.EqualTo("O valor inicial deve ser no maximo R$ 999.999.999.999.999."));
            });
        }

        [TestCase(10)]
        public void DeveRetornarNull_Quando_ValorInicialEstiverNoRangeCorreto(decimal valorInicial)
        {
            var investimento = new Investimento
            {
                ValorInicial = valorInicial,
                Prazo = 10
            };

            InvestimentoService service = new (investimento);

            ProblemDetails? resultado = service.ValidarEntrada();

            Assert.That(resultado, Is.Null);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(1)]
        public void DeveRetornarErro_Quando_PrazoForMenorOuIgual1(int prazo)
        {
            var investimento = new Investimento
            {
                ValorInicial = 10,
                Prazo = prazo
            };

            InvestimentoService service = new (investimento);

            ProblemDetails? resultado = service.ValidarEntrada();

            Assert.That(resultado, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(resultado.Status, Is.EqualTo(400));
                Assert.That(resultado.Detail, Is.EqualTo("O prazo deve ser maior que um mês."));
            });
        }

        [TestCase(1201)]
        public void DeveRetornarErro_Quando_PrazoForMaiorQue1200(int prazo)
        {
            var investimento = new Investimento
            {
                ValorInicial = 10,
                Prazo = prazo
            };

            InvestimentoService service = new (investimento);

            ProblemDetails? resultado = service.ValidarEntrada();

            Assert.That(resultado, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(resultado.Status, Is.EqualTo(400));
                Assert.That(resultado.Detail, Is.EqualTo("O prazo deve ser menor que 1200 (Cem Anos)."));
            });
        }

        [TestCase(10)]
        public void DeveRetornarNull_Quando_PrazoEstiverNoRangeCorreto(int prazo)
        {
            var investimento = new Investimento
            {
                ValorInicial = 10,
                Prazo = prazo
            };

            InvestimentoService service = new (investimento);

            ProblemDetails? resultado = service.ValidarEntrada();

            Assert.That(resultado, Is.Null);
        }

        [TestCase(10,6)]
        [TestCase(10,3)]
        [TestCase(10,2)]
        public void DeveAplicarAliquotaCorreta_Quando_PrazoForMenorOuIgualA6Meses(decimal valorInicial, int prazo)
        {
            var investimento = new Investimento
            {
                ValorInicial = valorInicial,
                Prazo = prazo
            };

            Investimento resultado = new InvestimentoService(investimento).CalcularRetorno();

            decimal retornoLiquidoCorreto = resultado.ValorInicial + ((resultado.RetornoBruto - resultado.ValorInicial) * (1 - 0.225m));

            Assert.That(retornoLiquidoCorreto, Is.EqualTo(resultado.RetornoLiquido));
        }

        [TestCase(10, 12)]
        [TestCase(10, 9)]
        [TestCase(10, 7)]
        public void DeveAplicarAliquotaCorreta_Quando_PrazoForMenorOuIgualA12Meses(decimal valorInicial, int prazo)
        {
            var investimento = new Investimento
            {
                ValorInicial = valorInicial,
                Prazo = prazo
            };

            Investimento resultado = new InvestimentoService(investimento).CalcularRetorno();

            decimal retornoLiquidoCorreto = resultado.ValorInicial + ((resultado.RetornoBruto - resultado.ValorInicial) * (1 - 0.2m));

            Assert.That(retornoLiquidoCorreto, Is.EqualTo(resultado.RetornoLiquido));
        }

        [TestCase(10, 24)]
        [TestCase(10, 18)]
        [TestCase(10, 13)]
        public void DeveAplicarAliquotaCorreta_Quando_PrazoForMenorOuIgualA24Meses(decimal valorInicial, int prazo)
        {
            var investimento = new Investimento
            {
                ValorInicial = valorInicial,
                Prazo = prazo
            };

            Investimento resultado = new InvestimentoService(investimento).CalcularRetorno();

            decimal retornoLiquidoCorreto = resultado.ValorInicial + ((resultado.RetornoBruto - resultado.ValorInicial) * (1 - 0.175m));

            Assert.That(retornoLiquidoCorreto, Is.EqualTo(resultado.RetornoLiquido));
        }

        [TestCase(10, 120)]
        [TestCase(10, 48)]
        [TestCase(10, 25)]
        public void DeveAplicarAliquotaCorreta_Quando_PrazoForMaiorQue24Meses(decimal valorInicial, int prazo)
        {
            var investimento = new Investimento
            {
                ValorInicial = valorInicial,
                Prazo = prazo
            };

            Investimento resultado = new InvestimentoService(investimento).CalcularRetorno();

            decimal retornoLiquidoCorreto = resultado.ValorInicial + ((resultado.RetornoBruto - resultado.ValorInicial) * (1 - 0.15m));

            Assert.That(retornoLiquidoCorreto, Is.EqualTo(resultado.RetornoLiquido));
        }

        [TestCase(10, 3, 0.225)]
        [TestCase(10, 9, 0.2)]
        [TestCase(10, 18, 0.175)]
        [TestCase(10, 48, 0.15)]
        [TestCase(3.33, 3, 0.225)]
        [TestCase(3.33, 9, 0.2)]
        [TestCase(3.33, 18, 0.175)]
        [TestCase(3.33, 48, 0.15)]
        public void DeveCalcularRetornosBrutoELiquidoCorretos_Quando_ValorInicialEPrazoEstiveremNoRangeCorreto(decimal valorInicial, int prazo, decimal aliquota)
        {
            var investimento = new Investimento
            {
                ValorInicial = valorInicial,
                Prazo = prazo
            };

            Investimento resultado = new InvestimentoService(investimento).CalcularRetorno();

            decimal retornoBrutoCorreto = valorInicial * DecimalEx.Pow(1 + (0.009m * 1.08m), prazo);
            decimal retornoLiquidoCorreto = valorInicial + ((retornoBrutoCorreto - valorInicial) * (1 - aliquota));

            Assert.Multiple(() =>
            {
                Assert.That(retornoBrutoCorreto, Is.EqualTo(resultado.RetornoBruto));
                Assert.That(retornoLiquidoCorreto, Is.EqualTo(resultado.RetornoLiquido));
            });
        }
    }
}