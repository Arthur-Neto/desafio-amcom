using Desafio.AMcom.Application.Models;
using Desafio.AMcom.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Desafio.AMcom.Controllers
{
    [Route("api/paises")]
    [ApiController]
    public class PaisesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaisesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Retorna lista de paises")]
        [SwaggerResponse(200, "Lista de paises", typeof(IList<PaisModel>))]
        public async Task<IActionResult> RetornaPaisesAsync(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new RetornarPaisesQuery(), cancellationToken);

            return Ok(result);
        }

        [HttpGet("por-sigla/{sigla}")]
        [SwaggerOperation(Summary = "Retorna lista de paises filtrado pela sigla")]
        [SwaggerResponse(200, "Lista de paises filtrado pela sigla", typeof(IList<PaisModel>))]
        public async Task<IActionResult> RetornaPaisPorSiglaAsync(string sigla, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new RetornarPaisesPorSiglaQuery() { Sigla = sigla }, cancellationToken);

            return Ok(result);
        }
    }
}
