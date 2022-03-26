using Desafio.AMcom.Application.Models;
using Desafio.AMcom.Application.Queries;
using Desafio.AMcom.Controllers;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Desafio.AMcom.UnitTests.Controllers.TemperaturasControllerTests
{
    public class ConverterTemperaturaFahrenheitAsyncTests : IDisposable
    {
        private readonly Mock<IMediator> _moqMediator;

        public ConverterTemperaturaFahrenheitAsyncTests()
        {
            _moqMediator = new Mock<IMediator>(MockBehavior.Strict);
        }

        public void Dispose()
        {
            _moqMediator.VerifyAll();
        }

        [Fact]
        public async Task Deve_Verificar_Metodo_E_Retornar_OkResult_Quando_Retorno_Do_Mediator_For_Sucesso()
        {
            // Arrange
            var command = new ConverterTemperaturaFahrenheitCommand() { Fahrenheit = 20 };

            var resultado = new TemperaturasModel()
            {
                Celsius = 0,
                Fahrenheit = 20,
                Kelvin = 0
            };

            _moqMediator
                .Setup(p => p.Send(command, default))
                .ReturnsAsync(resultado);

            // Act
            var result = await GetController().ConverterTemperaturaFahrenheitAsync(command, default);

            // Assert
            var okResult = result as OkObjectResult;

            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().Be(resultado);
        }

        [Fact]
        public void Deve_Verificar_Atributos()
        {
            // Assert
            typeof(TemperaturasController)
                .GetMethod(nameof(TemperaturasController.ConverterTemperaturaFahrenheitAsync))
                .Should()
                .BeDecoratedWith<HttpPostAttribute>(p => p.Template == "converter-fahrenheit");
        }

        private TemperaturasController GetController()
        {
            return new TemperaturasController(_moqMediator.Object);
        }
    }
}
