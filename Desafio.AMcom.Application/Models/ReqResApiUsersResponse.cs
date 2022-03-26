using Desafio.AMcom.Domain;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Desafio.AMcom.Application.Models
{
    public class ReqResApiUsersResponse
    {
        [JsonPropertyName("data")]
        public IList<Pessoa> Data { get; set; }
    }
}
