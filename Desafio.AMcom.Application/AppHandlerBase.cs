using Microsoft.Extensions.Logging;

namespace Desafio.AMcom.Application
{
    public abstract class AppHandlerBase<T>
    {
        protected readonly ILogger<T> _logger;

        protected AppHandlerBase(ILogger<T> logger)
        {
            _logger = logger;
        }
    }
}
