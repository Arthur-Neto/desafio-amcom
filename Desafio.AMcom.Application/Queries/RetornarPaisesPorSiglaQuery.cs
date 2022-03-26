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
    public class RetornarPaisesPorSiglaQuery : IRequest<IList<PaisModel>>
    {
        public string Sigla { get; set; }
    }

    public class RetornarPaisesPorSiglaQueryHandler : AppHandlerBase<RetornarPaisesPorSiglaQueryHandler>, IRequestHandler<RetornarPaisesPorSiglaQuery, IList<PaisModel>>
    {
        private readonly IMapper _mapper;
        private readonly IPaisRepository _paisRepository;

        public RetornarPaisesPorSiglaQueryHandler(
            ILogger<RetornarPaisesPorSiglaQueryHandler> logger,
            IMapper mapper,
            IPaisRepository paisRepository
        ) : base(logger)
        {
            _mapper = mapper;
            _paisRepository = paisRepository;
        }

        public async Task<IList<PaisModel>> Handle(RetornarPaisesPorSiglaQuery request, CancellationToken cancellationToken)
        {
            var paises = await _paisRepository.RetornarPaisesPorSiglaAsync(request.Sigla, cancellationToken);

            var conteudoMapeado = _mapper.Map<IList<PaisModel>>(paises);

            return conteudoMapeado;
        }
    }
}
