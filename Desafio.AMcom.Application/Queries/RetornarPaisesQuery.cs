using AutoMapper;
using Desafio.AMcom.Application.Models;
using Desafio.AMcom.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Desafio.AMcom.Application.Queries
{
    public class RetornarPaisesQuery : IRequest<IList<PaisModel>>
    { }

    public class RetornarPaisesQueryHandler : AppHandlerBase<RetornarPaisesQueryHandler>, IRequestHandler<RetornarPaisesQuery, IList<PaisModel>>
    {
        private readonly IMapper _mapper;
        private readonly IPaisRepository _paisRepository;

        public RetornarPaisesQueryHandler(
            ILogger<RetornarPaisesQueryHandler> logger,
            IMapper mapper,
            IPaisRepository paisRepository
        ) : base(logger)
        {
            _mapper = mapper;
            _paisRepository = paisRepository;
        }

        public async Task<IList<PaisModel>> Handle(RetornarPaisesQuery request, CancellationToken cancellationToken)
        {
            var paises = await _paisRepository.RetornarPaisesAsync(cancellationToken);

            var conteudoMapeado = _mapper.Map<IList<PaisModel>>(paises);

            return conteudoMapeado;
        }
    }
}
