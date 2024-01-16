using AutoGlass.Application.DTOs;
using AutoGlass.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoGlass.Application.Repositories
{
    public interface IProdutoRepository
    {
        Task<ProdutoEntity> ObterPorCodigo(int codigo);
        Task<IEnumerable<ProdutoEntity>> Listar(int pagina, int tamanho, string codigo, 
            string descricao, string situacao, string codigoFornecedor);
        Task Inserir(ProdutoEntity produto);
        Task Editar(ProdutoEntity produto);
        Task ExclusaoLogica(int codigo);
        Task<int> ContarProdutos(string codigo, string descricao, string situacao, string codigoFornecedor);
    }
}
