using System.Text.Json.Serialization;

namespace AutoGlass.Application.DTOs
{
    public class FornecedorDTO
    {
        [JsonPropertyName("codigoFornecedor")]
        public int CodigoFornecedor { get; set; }

        [JsonPropertyName("descricaoFornecedor")]
        public string DescricaoFornecedor { get; set; }

        [JsonPropertyName("cnpjFornecedor")]
        public string CnpjFornecedor { get; set; }
    }
}
