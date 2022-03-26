using Desafio.AMcom.Infra;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Desafio.AMcom.Application.Commands
{
    public class SalvarTemperaturaEmTxtCommand : IRequest
    {
        public double Fahrenheit { get; set; }
        public double Celsius { get; set; }
        public double Kelvin { get; set; }
    }

    public class SalvarTemperaturaEmTxtCommandHandler : AppHandlerBase<SalvarTemperaturaEmTxtCommandHandler>, IRequestHandler<SalvarTemperaturaEmTxtCommand, Unit>
    {
        public const string NOME_ARQUIVO = "temperatura.txt";

        private readonly IStreamWriterFactory _streamWriterFactory;

        public SalvarTemperaturaEmTxtCommandHandler(
            ILogger<SalvarTemperaturaEmTxtCommandHandler> logger,
            IStreamWriterFactory streamWriterFactory
        ) : base(logger)
        {
            _streamWriterFactory = streamWriterFactory;
        }

        public async Task<Unit> Handle(SalvarTemperaturaEmTxtCommand request, CancellationToken cancellationToken)
        {
            using (var file = _streamWriterFactory.GetStreamWriter(NOME_ARQUIVO))
            {
                var sb = new StringBuilder();
                sb.AppendLine(request.Kelvin.ToString());
                sb.AppendLine(request.Fahrenheit.ToString());
                sb.AppendLine(request.Celsius.ToString());

                await file.WriteLineAsync(sb, cancellationToken);
            }

            return Unit.Value;
        }
    }
}
