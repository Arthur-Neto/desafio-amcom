using AutoMapper;
using Desafio.AMcom.Application.Models;
using Desafio.AMcom.Application.Queries;
using Desafio.AMcom.Domain;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Desafio.AMcom.UnitTests.Queries
{
    public class RetornarPessoasQueryTests : IDisposable
    {
        private readonly Mock<ILogger<RetornarPessoasQueryHandler>> _moqLogger;
        private readonly Mock<IMapper> _moqMapper;
        private readonly Mock<IHttpClientFactory> _moqHttpClientFactory;

        public RetornarPessoasQueryTests()
        {
            _moqLogger = new Mock<ILogger<RetornarPessoasQueryHandler>>(MockBehavior.Loose);
            _moqMapper = new Mock<IMapper>(MockBehavior.Strict);
            _moqHttpClientFactory = new Mock<IHttpClientFactory>(MockBehavior.Strict);
        }

        public void Dispose()
        {
            _moqMapper.VerifyAll();
            _moqMapper.VerifyNoOtherCalls();
            _moqHttpClientFactory.VerifyAll();
            _moqHttpClientFactory.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Deve_Verificar_Metodo_E_Retornar_Lista_De_Pessoas()
        {
            // Arrange
            var command = new RetornarPessoasQuery
            { };

            IList<PessoaModel> expectedResult = new List<PessoaModel>()
            {
                new PessoaModel() { Avatar = "moq-avatar", Email = "moq-email", Id = 1, Nome = "first last" }
            };

            var resultObject = new ReqResApiUsersResponse()
            {
                Data = new List<Pessoa>()
                {
                    new Pessoa() { Avatar = "moq-avatar", Email = "moq-email", FirstName = "first", LastName = "last", Id = 1 }
                }
            };

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(resultObject))
            };

            var _httpMessageHandler = new Mock<HttpMessageHandler>();
            _httpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var httpClient = new HttpClient(_httpMessageHandler.Object)
            {
                BaseAddress = new Uri("http://nonexisting.domain")
            };

            _moqHttpClientFactory
                .Setup(p => p.CreateClient(It.IsAny<string>()))
                .Returns(httpClient);

            _moqMapper
                .Setup(p => p.Map<IList<PessoaModel>>(It.IsAny<IList<Pessoa>>()))
                .Returns(expectedResult);

            // Act
            var result = await GetQuery().Handle(command, default);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task Deve_Verificar_Metodo_E_Retornar_Lista_De_Pessoas_E_Filtrar_Por_Nome()
        {
            // Arrange
            var command = new RetornarPessoasQuery
            {
                Nome = "moq-test"
            };

            IList<PessoaModel> expectedResult = new List<PessoaModel>()
            {
                new PessoaModel() { Avatar = "moq-avatar", Email = "moq-email", Id = 1, Nome = "first last" }
            };

            var resultObject = new ReqResApiUsersResponse()
            {
                Data = new List<Pessoa>()
                {
                    new Pessoa() { Avatar = "moq-avatar", Email = "moq-email", FirstName = "first", LastName = "last", Id = 1 }
                }
            };

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(resultObject))
            };

            var _httpMessageHandler = new Mock<HttpMessageHandler>();
            _httpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var httpClient = new HttpClient(_httpMessageHandler.Object)
            {
                BaseAddress = new Uri("http://nonexisting.domain")
            };

            _moqHttpClientFactory
                .Setup(p => p.CreateClient(It.IsAny<string>()))
                .Returns(httpClient);

            _moqMapper
                .Setup(p => p.Map<IList<PessoaModel>>(It.IsAny<IList<Pessoa>>()))
                .Returns(expectedResult);

            // Act
            var result = await GetQuery().Handle(command, default);

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task Deve_Verificar_Metodo_E_Retornar_Lista_De_Pessoas_E_Filtrar_Por_Email()
        {
            // Arrange
            var command = new RetornarPessoasQuery
            {
                Email = "moq-test"
            };

            IList<PessoaModel> expectedResult = new List<PessoaModel>()
            {
                new PessoaModel() { Avatar = "moq-avatar", Email = "moq-email", Id = 1, Nome = "first last" }
            };

            var resultObject = new ReqResApiUsersResponse()
            {
                Data = new List<Pessoa>()
                {
                    new Pessoa() { Avatar = "moq-avatar", Email = "moq-email", FirstName = "first", LastName = "last", Id = 1 }
                }
            };

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(resultObject))
            };

            var _httpMessageHandler = new Mock<HttpMessageHandler>();
            _httpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var httpClient = new HttpClient(_httpMessageHandler.Object)
            {
                BaseAddress = new Uri("http://nonexisting.domain")
            };

            _moqHttpClientFactory
                .Setup(p => p.CreateClient(It.IsAny<string>()))
                .Returns(httpClient);

            _moqMapper
                .Setup(p => p.Map<IList<PessoaModel>>(It.IsAny<IList<Pessoa>>()))
                .Returns(expectedResult);

            // Act
            var result = await GetQuery().Handle(command, default);

            // Assert
            result.Should().BeEmpty();
        }

        private RetornarPessoasQueryHandler GetQuery()
        {
            return new RetornarPessoasQueryHandler(
                _moqLogger.Object,
                _moqMapper.Object,
                _moqHttpClientFactory.Object
            );
        }
    }
}
