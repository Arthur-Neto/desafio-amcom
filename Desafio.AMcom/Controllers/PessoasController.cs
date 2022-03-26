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
    [Route("api/pessoas")]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PessoasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Retorna lista de pessoas")]
        [SwaggerResponse(200, "Lista de pessoas, podendo ser filtrado por nome e/ou email", typeof(IList<PessoaModel>))]
        public async Task<IActionResult> RetornaPessoasAsync([FromQuery] RetornarPessoasQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }
    }
}
