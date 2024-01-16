using AutoGlass.Application.DTOs;
using AutoGlass.Application.Results;
using System.Threading.Tasks;

namespace AutoGlass.Application.Services.Interfaces
{
    public interface IFornecedorServices
    {
        Task<RequestResult> ObterPorCodigo(int codigo);
        Task<RequestResult> Listar(string filtro);
        Task<RequestResult> Inserir(FornecedorDTO fornecedor);
        Task<RequestResult> Editar(FornecedorDTO fornecedor);
    }
}
