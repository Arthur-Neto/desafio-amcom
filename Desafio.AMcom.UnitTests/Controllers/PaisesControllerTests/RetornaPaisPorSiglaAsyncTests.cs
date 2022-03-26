using Desafio.AMcom.Application.Models;
using Desafio.AMcom.Application.Queries;
using Desafio.AMcom.Controllers;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Desafio.AMcom.UnitTests.Controllers.PaisesControllerTests
{
    public class RetornaPaisPorSiglaAsyncTests : IDisposable
    {
        private readonly Mock<IMediator> _moqMediator;

        public RetornaPaisPorSiglaAsyncTests()
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
            IList<PaisModel> resultado = new List<PaisModel>()
            {
                new PaisModel() { Gentilico = "moq-gent", NomePais = "moq-pais", Sigla = "moq-sigla" }
            };

            _moqMediator
                .Setup(p => p.Send(It.IsAny<RetornarPaisesPorSiglaQuery>(), default))
                .ReturnsAsync(resultado);

            // Act
            var result = await GetController().RetornaPaisPorSiglaAsync("moq-sigla", default);

            // Assert
            var okResult = result as OkObjectResult;

            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().Be(resultado);
        }

        [Fact]
        public void Deve_Verificar_Atributos()
        {
            // Assert
            typeof(PaisesController)
                .GetMethod(nameof(PaisesController.RetornaPaisPorSiglaAsync))
                .Should()
                .BeDecoratedWith<HttpGetAttribute>();
        }

        private PaisesController GetController()
        {
            return new PaisesController(_moqMediator.Object);
        }
    }
}
