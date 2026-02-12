using Microsoft.AspNetCore.Mvc;
using ProcessoSeletivo_Avaliacao.Server.Model;

namespace ProcessoSeletivo_Avaliacao.Server.Controllers
{
    [ApiController]
    [Route("CalcularRetornoCDB")]
    public class InvestimentoController : ControllerBase
    {
       private readonly ILogger<InvestimentoController> _logger;

        public InvestimentoController(ILogger<InvestimentoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Investimento Get(double valorInicial, int prazo)
        {
            return new Investimento(valorInicial, prazo);
        }
    }
}
