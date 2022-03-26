using System.Text.Json.Serialization;

namespace Desafio.AMcom.Domain
{
    public class Pais
    {
        [JsonPropertyName("gentilico")]
        public string Gentilico { get; set; }

        [JsonPropertyName("nome_pais")]
        public string NomePais { get; set; }

        [JsonPropertyName("nome_pais_int")]
        public string NomePaisInt { get; set; }

        [JsonPropertyName("sigla")]
        public string Sigla { get; set; }
    }
}
