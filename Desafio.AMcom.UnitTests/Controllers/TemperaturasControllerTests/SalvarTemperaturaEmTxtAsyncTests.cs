using Desafio.AMcom.Application.Commands;
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
    public class SalvarTemperaturaEmTxtAsyncTests : IDisposable
    {
        private readonly Mock<IMediator> _moqMediator;

        public SalvarTemperaturaEmTxtAsyncTests()
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
            var command = new SalvarTemperaturaEmTxtCommand();

            _moqMediator
                .Setup(p => p.Send(command, default))
                .ReturnsAsync(Unit.Value);

            // Act
            var result = await GetController().SalvarTemperaturaEmTxtAsync(command, default);

            // Assert
            var okResult = result as OkResult;

            okResult.StatusCode.Should().Be(200);
        }

        [Fact]
        public void Deve_Verificar_Atributos()
        {
            // Assert
            typeof(TemperaturasController)
                .GetMethod(nameof(TemperaturasController.SalvarTemperaturaEmTxtAsync))
                .Should()
                .BeDecoratedWith<HttpPostAttribute>(p => p.Template == "txt");
        }

        private TemperaturasController GetController()
        {
            return new TemperaturasController(_moqMediator.Object);
        }
    }
}
