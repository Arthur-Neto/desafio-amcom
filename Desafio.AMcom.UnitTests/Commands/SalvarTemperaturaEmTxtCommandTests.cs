using Desafio.AMcom.Application.Commands;
using Desafio.AMcom.Infra;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Desafio.AMcom.UnitTests.Commands
{
    public class SalvarTemperaturaEmTxtCommandTests : IDisposable
    {
        private readonly Mock<ILogger<SalvarTemperaturaEmTxtCommandHandler>> _moqLogger;
        private readonly Mock<IStreamWriterFactory> _moqFactory;

        public SalvarTemperaturaEmTxtCommandTests()
        {
            _moqLogger = new Mock<ILogger<SalvarTemperaturaEmTxtCommandHandler>>(MockBehavior.Loose);
            _moqFactory = new Mock<IStreamWriterFactory>(MockBehavior.Strict);
        }

        public void Dispose()
        {
            _moqFactory.VerifyAll();
            _moqFactory.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Deve_Verificar_Metodo_E_Verificar_Se_StreamWriter_Foi_Criado()
        {
            // Arrange
            var command = new SalvarTemperaturaEmTxtCommand
            {
                Fahrenheit = 32
            };

            _moqFactory
                .Setup(p => p.GetStreamWriter(SalvarTemperaturaEmTxtCommandHandler.NOME_ARQUIVO))
                .Returns(new StreamWriter(SalvarTemperaturaEmTxtCommandHandler.NOME_ARQUIVO));

            // Act
            await GetCommand().Handle(command, default);

            // Assert
            _moqFactory.Verify(p => p.GetStreamWriter(SalvarTemperaturaEmTxtCommandHandler.NOME_ARQUIVO), Times.Once);
        }

        private SalvarTemperaturaEmTxtCommandHandler GetCommand()
        {
            return new SalvarTemperaturaEmTxtCommandHandler(
                _moqLogger.Object,
                _moqFactory.Object
            );
        }
    }
}
