using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Desafio.AMcom.Domain
{
    public interface IPaisRepository
    {
        Task<IList<Pais>> RetornarPaisesAsync(CancellationToken cancellationToken);
        Task<IList<Pais>> RetornarPaisesPorSiglaAsync(string sigla, CancellationToken cancellationToken);
    }
}
