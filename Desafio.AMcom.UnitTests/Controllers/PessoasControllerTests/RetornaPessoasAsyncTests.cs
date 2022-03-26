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

namespace Desafio.AMcom.UnitTests.Controllers.PessoasControllerTests
{
    public class RetornaPessoasAsyncTests : IDisposable
    {
        private readonly Mock<IMediator> _moqMediator;

        public RetornaPessoasAsyncTests()
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
            var query = new RetornarPessoasQuery();

            IList<PessoaModel> resultado = new List<PessoaModel>()
            {
                new PessoaModel() { Avatar = "avatar", Email = "email", Id = 1, Nome = "nome" }
            };

            _moqMediator
                .Setup(p => p.Send(query, default))
                .ReturnsAsync(resultado);

            // Act
            var result = await GetController().RetornaPessoasAsync(query, default);

            // Assert
            var okResult = result as OkObjectResult;

            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().Be(resultado);
        }

        [Fact]
        public void Deve_Verificar_Atributos()
        {
            // Assert
            typeof(PessoasController)
                .GetMethod(nameof(PessoasController.RetornaPessoasAsync))
                .Should()
                .BeDecoratedWith<HttpGetAttribute>();
        }

        private PessoasController GetController()
        {
            return new PessoasController(_moqMediator.Object);
        }
    }
}
