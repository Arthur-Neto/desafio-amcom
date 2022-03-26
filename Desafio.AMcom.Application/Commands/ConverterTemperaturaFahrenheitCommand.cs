using Desafio.AMcom.Application.Models;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Desafio.AMcom.Application.Commands
{
    public class ConverterTemperaturaFahrenheitCommand : IRequest<TemperaturasModel>
    {
        public double Fahrenheit { get; set; }
    }

    public class ConverterTemperaturaFahrenheitCommandHandler : AppHandlerBase<ConverterTemperaturaFahrenheitCommandHandler>, IRequestHandler<ConverterTemperaturaFahrenheitCommand, TemperaturasModel>
    {
        private readonly IMemoryCache _memoryCache;

        public ConverterTemperaturaFahrenheitCommandHandler(
            ILogger<ConverterTemperaturaFahrenheitCommandHandler> logger,
            IMemoryCache memoryCache
        ) : base(logger)
        {
            _memoryCache = memoryCache;
        }

        public async Task<TemperaturasModel> Handle(ConverterTemperaturaFahrenheitCommand request, CancellationToken cancellationToken)
        {
            var temperaturas = await _memoryCache.GetOrCreateAsync(request.Fahrenheit, entry =>
            {
                var temperaturas = new TemperaturasModel();

                try
                {
                    _logger.LogInformation("Recebida temperatura para conversão: {TemperaturaRequisicao}", request.Fahrenheit);

                    temperaturas.Fahrenheit = request.Fahrenheit;
                    temperaturas.Celsius = (request.Fahrenheit - 32.0) * 5 / 9;
                    temperaturas.Kelvin = temperaturas.Celsius + 273.15;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ocorreu um problema ao converter");
                }

                _logger.LogInformation("Resultado concluído: {TemperaturaCelsius}", temperaturas.Celsius);
                _logger.LogInformation("Resultado concluído: {TemperaturaFahrenheit}", temperaturas.Fahrenheit);
                _logger.LogInformation("Resultado concluído: {TemperaturaKelvin}", temperaturas.Kelvin);

                return Task.FromResult(temperaturas);
            });

            return temperaturas;
        }
    }
}
