using System;
using System.Text.Json.Serialization;

namespace AutoGlass.Application.DTOs
{
    public class ProdutoDTO
    {
        [JsonPropertyName("codigo")]
        public int Codigo { get; set; }

        [JsonPropertyName("descricao")]
        public string Descricao { get; set; }

        [JsonPropertyName("situacao")]
        public bool Situacao { get; set; }

        [JsonPropertyName("dataFabricacao")]
        public DateTime DataFabricacao { get; set; }

        [JsonPropertyName("dataValidade")]
        public DateTime DataValidade { get; set; }

        [JsonPropertyName("codigoFornecedor")]
        public int CodigoFornecedor { get; set; }

        [JsonPropertyName("fornecedor")]
        public FornecedorDTO Fornecedor { get; set; }
    }
}
