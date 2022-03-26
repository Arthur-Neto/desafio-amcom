using AutoMapper;
using Desafio.AMcom.Application.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Desafio.AMcom.Application.Queries
{
    public class RetornarPessoasQuery : IRequest<IList<PessoaModel>>
    {
        public string Nome { get; set; }
        public string Email { get; set; }
    }

    public class RetornarPessoasQueryHandler : AppHandlerBase<RetornarPessoasQueryHandler>, IRequestHandler<RetornarPessoasQuery, IList<PessoaModel>>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;
        private readonly HttpRequestMessage _httpMessage;

        public RetornarPessoasQueryHandler(
            ILogger<RetornarPessoasQueryHandler> logger,
            IMapper mapper,
            IHttpClientFactory httpClientFactory
        ) : base(logger)
        {
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;

            _httpMessage = new HttpRequestMessage(
            HttpMethod.Get,
            "https://reqres.in/api/users?page=2"
            );
        }

        public async Task<IList<PessoaModel>> Handle(RetornarPessoasQuery request, CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient("reqres");
            var response = await httpClient.GetAsync("api/users?page=2", cancellationToken);

            var responseContent = await response.Content.ReadAsStringAsync();
            var deserializedContent = JsonSerializer.Deserialize<ReqResApiUsersResponse>(responseContent);

            var pessoas = _mapper.Map<IList<PessoaModel>>(deserializedContent.Data);

            if (string.IsNullOrEmpty(request.Email) is false)
            {
                pessoas = pessoas.Where(x => x.Email.Contains(request.Email)).ToList();
            }

            if (string.IsNullOrEmpty(request.Nome) is false)
            {
                pessoas = pessoas.Where(x => x.Nome.Contains(request.Nome)).ToList();
            }

            return pessoas;
        }
    }
}
