using AutoGlass.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoGlass.Application.Repositories
{
    public interface IFornecedorRepository
    {
        Task<FornecedorEntity> ObterPorCodigo(int codigoFornecedor);
        Task<IEnumerable<FornecedorEntity>> Listar(string filtro);
        Task Inserir(FornecedorEntity fornecedor);
        Task Editar(FornecedorEntity fornecedor);
        Task Excluir(int codigo);
    }
}
