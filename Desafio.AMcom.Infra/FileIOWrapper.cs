using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Desafio.AMcom.Infra
{
    public interface IFileIOWrapper
    {
        Task<string> ReadAllTextAsync(string path, CancellationToken cancellationToken);
    }

    public class FileIOWrapper : IFileIOWrapper
    {
        public Task<string> ReadAllTextAsync(string path, CancellationToken cancellationToken)
        {
            return File.ReadAllTextAsync(path, cancellationToken);
        }
    }
}
