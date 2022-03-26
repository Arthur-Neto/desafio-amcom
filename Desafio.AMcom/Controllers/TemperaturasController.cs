using Desafio.AMcom.Application.Commands;
using Desafio.AMcom.Application.Models;
using Desafio.AMcom.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace Desafio.AMcom.Controllers
{
    [Route("api/temperaturas")]
    [ApiController]
    public class TemperaturasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TemperaturasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("converter-fahrenheit")]
        [SwaggerOperation(Summary = "Converte temperatura de fahrenheit para outras")]
        [SwaggerResponse(200, "Temperaturas convertidas", typeof(TemperaturasModel))]
        public async Task<IActionResult> ConverterTemperaturaFahrenheitAsync(ConverterTemperaturaFahrenheitCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpPost("txt")]
        [SwaggerOperation(Summary = "Salva as temperaturas em um txt")]
        [SwaggerResponse(200, "Salva as temperatuas em disco")]
        public async Task<IActionResult> SalvarTemperaturaEmTxtAsync(SalvarTemperaturaEmTxtCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);

            return Ok();
        }
    }
}
