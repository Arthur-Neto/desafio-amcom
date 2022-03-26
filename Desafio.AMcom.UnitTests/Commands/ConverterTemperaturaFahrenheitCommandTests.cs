using Desafio.AMcom.Application.Commands;
using Desafio.AMcom.Application.Models;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Desafio.AMcom.UnitTests.Commands
{
    public class ConverterTemperaturaFahrenheitCommandTests
    {
        private readonly Mock<ILogger<ConverterTemperaturaFahrenheitCommandHandler>> _moqLogger;
        private readonly IMemoryCache _moqCache;

        public ConverterTemperaturaFahrenheitCommandTests()
        {
            _moqLogger = new Mock<ILogger<ConverterTemperaturaFahrenheitCommandHandler>>(MockBehavior.Loose);
            _moqCache = new MemoryCache(new MemoryCacheOptions());
        }

        [Fact]
        public async Task Deve_Verificar_Metodo_E_Retornar_Temperaturas_Convertidas()
        {
            // Arrange
            var command = new ConverterTemperaturaFahrenheitCommand
            {
                Fahrenheit = 32
            };

            var expectedResult = new TemperaturasModel()
            {
                Celsius = 0,
                Fahrenheit = 32,
                Kelvin = 273.15
            };

            // Act
            var result = await GetCommand().Handle(command, default);

            // Assert
            result.Celsius.Should().Be(expectedResult.Celsius);
            result.Fahrenheit.Should().Be(expectedResult.Fahrenheit);
            result.Kelvin.Should().Be(expectedResult.Kelvin);
        }

        [Fact]
        public async Task Deve_Verificar_Metodo_E_Retornar_Temperaturas_Convertidas_Do_Cache()
        {
            // Arrange
            var command = new ConverterTemperaturaFahrenheitCommand
            {
                Fahrenheit = 32
            };

            var expectedResult = new TemperaturasModel()
            {
                Celsius = 0,
                Fahrenheit = 32,
                Kelvin = 273.15
            };

            _moqCache.Set(command.Fahrenheit, expectedResult);

            // Act
            var result = await GetCommand().Handle(command, default);

            // Assert
            result.Celsius.Should().Be(expectedResult.Celsius);
            result.Fahrenheit.Should().Be(expectedResult.Fahrenheit);
            result.Kelvin.Should().Be(expectedResult.Kelvin);
        }

        [Fact]
        public async Task Deve_Verificar_Metodo_E_Retornar_Temperaturas_Convertidas_Com_Valores_Corretos()
        {
            // Arrange
            var command = new ConverterTemperaturaFahrenheitCommand
            {
                Fahrenheit = 32
            };

            var expectedResult = new TemperaturasModel()
            {
                Celsius = 55,
                Fahrenheit = 66,
                Kelvin = 777
            };

            // Act
            var result = await GetCommand().Handle(command, default);

            // Assert
            result.Celsius.Should().NotBe(expectedResult.Celsius);
            result.Fahrenheit.Should().NotBe(expectedResult.Fahrenheit);
            result.Kelvin.Should().NotBe(expectedResult.Kelvin);
        }

        private ConverterTemperaturaFahrenheitCommandHandler GetCommand()
        {
            return new ConverterTemperaturaFahrenheitCommandHandler(
                _moqLogger.Object,
                _moqCache
            );
        }
    }
}
