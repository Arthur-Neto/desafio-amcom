using Desafio.AMcom.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Desafio.AMcom.Infra
{
    public class PaisRepository : IPaisRepository
    {
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
            var conteudoArquivo = await File.ReadAllTextAsync($"{AppDomain.CurrentDomain.BaseDirectory}\\paises.json", cancellationToken);

            return JsonSerializer.Deserialize<IList<Pais>>(conteudoArquivo);
        }
    }
}
