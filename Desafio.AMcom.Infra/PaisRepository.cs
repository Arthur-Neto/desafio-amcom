using Desafio.AMcom.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Desafio.AMcom.Infra
{
    public class PaisRepository : IPaisRepository
    {
        private readonly IFileIOWrapper _fileIOWrapper;

        public PaisRepository(IFileIOWrapper fileIOWrapper)
        {
            _fileIOWrapper = fileIOWrapper;
        }

        public Task<IList<Pais>> RetornarPaisesAsync(CancellationToken cancellationToken)
        {
            return BuscarArquivoEDeserializaAsync(cancellationToken);
        }

        public async Task<IList<Pais>> RetornarPaisesPorSiglaAsync(string sigla, CancellationToken cancellationToken)
        {
            var paises = await BuscarArquivoEDeserializaAsync(cancellationToken);

            return paises.Where(p => p.Sigla == sigla).ToList();
        }

        private async Task<IList<Pais>> BuscarArquivoEDeserializaAsync(CancellationToken cancellationToken)
        {
            var conteudoArquivo = await _fileIOWrapper.ReadAllTextAsync($"{AppDomain.CurrentDomain.BaseDirectory}//paises.json", cancellationToken);

            return JsonSerializer.Deserialize<IList<Pais>>(conteudoArquivo);
        }
    }
}
