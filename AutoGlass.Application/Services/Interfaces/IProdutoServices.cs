using AutoGlass.Application.DTOs;
using AutoGlass.Application.Results;
using System.Threading.Tasks;

namespace AutoGlass.Application.Services.Interfaces
{
    public interface IProdutoServices
    {
        Task<RequestResult> ObterPorCodigo(int codigo);
        Task<RequestResult> Listar(int pagina, int tamanho, string codigo, string descricao, string situacao, string codigoFornecedor);
        Task<RequestResult> Inserir(ProdutoDTO produto);
        Task<RequestResult> Editar(ProdutoDTO produto);
        Task ExclusaoLogica(int codigo);
    }
}
