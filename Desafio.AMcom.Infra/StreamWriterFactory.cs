using System.IO;

namespace Desafio.AMcom.Infra
{
    public interface IStreamWriterFactory
    {
        StreamWriter GetStreamWriter(string path);
    }

    public class StreamWriterFactory : IStreamWriterFactory
    {
        public StreamWriter GetStreamWriter(string path)
        {
            return new StreamWriter(path);
        }
    }
}
