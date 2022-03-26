using AutoMapper;
using Desafio.AMcom.Application.Models;
using Desafio.AMcom.Application.Queries;
using Desafio.AMcom.Domain;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Desafio.AMcom.UnitTests.Queries
{
    public class RetornarPaisesQueryTests : IDisposable
    {
        private readonly Mock<ILogger<RetornarPaisesQueryHandler>> _moqLogger;
        private readonly Mock<IMapper> _moqMapper;
        private readonly Mock<IPaisRepository> _moqPaisRepository;

        public RetornarPaisesQueryTests()
        {
            _moqLogger = new Mock<ILogger<RetornarPaisesQueryHandler>>(MockBehavior.Loose);
            _moqMapper = new Mock<IMapper>(MockBehavior.Strict);
            _moqPaisRepository = new Mock<IPaisRepository>(MockBehavior.Strict);
        }

        public void Dispose()
        {
            _moqMapper.VerifyAll();
            _moqMapper.VerifyNoOtherCalls();
            _moqPaisRepository.VerifyAll();
            _moqPaisRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Deve_Verificar_Metodo_E_Retornar_Lista_De_Paises()
        {
            // Arrange
            var command = new RetornarPaisesQuery
            { };

            IList<Pais> pais = new List<Pais>()
                { new Pais() { Gentilico = "moq-gent" }
            };

            IList<PaisModel> expectedResult = new List<PaisModel>()
                { new PaisModel() { Gentilico = "moq-gent", NomePais = "moq-pais", Sigla = "BR" }
            };

            _moqPaisRepository
                .Setup(p => p.RetornarPaisesAsync(default))
                .ReturnsAsync(pais);

            _moqMapper
                .Setup(p => p.Map<IList<PaisModel>>(pais))
                .Returns(expectedResult);

            // Act
            var result = await GetQuery().Handle(command, default);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        private RetornarPaisesQueryHandler GetQuery()
        {
            return new RetornarPaisesQueryHandler(
                _moqLogger.Object,
                _moqMapper.Object,
                _moqPaisRepository.Object
            );
        }
    }
}
