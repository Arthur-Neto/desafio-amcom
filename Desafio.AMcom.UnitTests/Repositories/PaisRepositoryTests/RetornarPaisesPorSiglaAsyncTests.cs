using Desafio.AMcom.Domain;
using Desafio.AMcom.Infra;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Desafio.AMcom.UnitTests.Repositories.PaisRepositoryTests
{
    public class RetornarPaisesPorSiglaAsyncTests : IDisposable
    {
        private readonly Mock<IFileIOWrapper> _moqFileIOWrapper;

        public RetornarPaisesPorSiglaAsyncTests()
        {
            _moqFileIOWrapper = new Mock<IFileIOWrapper>(MockBehavior.Strict);
        }

        public void Dispose()
        {
            _moqFileIOWrapper.VerifyAll();
            _moqFileIOWrapper.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Deve_Verificar_Metodo_E_Retornar_Lista_De_Paises_Por_Sigla()
        {
            // Arrange
            IList<Pais> expectedResult = new List<Pais>()
                { new Pais() { Sigla = "BR" }
            };

            _moqFileIOWrapper
                .Setup(p => p.ReadAllTextAsync(It.IsAny<string>(), default))
                .ReturnsAsync(JsonSerializer.Serialize(expectedResult));

            // Act
            var result = await GetRepository().RetornarPaisesPorSiglaAsync("BR", default);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task Deve_Verificar_Metodo_E_Retornar_Lista_De_Paises_Vazia()
        {
            // Arrange
            IList<Pais> expectedResult = new List<Pais>()
                { new Pais() { Sigla = "BR" }
            };

            _moqFileIOWrapper
                .Setup(p => p.ReadAllTextAsync(It.IsAny<string>(), default))
                .ReturnsAsync(JsonSerializer.Serialize(expectedResult));

            // Act
            var result = await GetRepository().RetornarPaisesPorSiglaAsync("AF", default);

            // Assert
            result.Should().BeEmpty();
        }

        public IPaisRepository GetRepository()
        {
            return new PaisRepository(_moqFileIOWrapper.Object);
        }
    }
}
