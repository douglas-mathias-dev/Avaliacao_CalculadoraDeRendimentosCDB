using Microsoft.AspNetCore.Mvc;
using ProcessoSeletivo_Avaliacao.Server.Models;
using ProcessoSeletivo_Avaliacao.Server.Services;

namespace ProcessoSeletivo_Avaliacao.Server.Controllers
{
    [ApiController]
    [Route("CalcularRetornoCDB")]
    public class InvestimentoController : ControllerBase
    {
        [HttpGet]
        public ActionResult<Investimento> Get(double valorInicial, int prazo)
        {
            InvestimentoService investimentoService = new InvestimentoService
            (
                new Investimento()
                {
                    ValorInicial = valorInicial
                    , Prazo = prazo 
                }
            );

            ProblemDetails erro = investimentoService.ValidarEntrada();
            if (!string.IsNullOrEmpty(erro.Detail))
            {
                return BadRequest(erro);
            }
            
            return Ok(investimentoService.CalcularRetorno());
        }
    }
}
