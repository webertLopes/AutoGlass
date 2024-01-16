using AutoGlass.Application.Repositories;
using AutoGlass.Domain.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace AutoGlass.Infrastructure.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IDbConnection _dbConnection;
        public ProdutoRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task Editar(ProdutoEntity produto)
        {
            var strQuery = @"UPDATE Produtos SET Descricao = @Descricao, Situacao = @Situacao, DataFabricacao = 
                                     @DataFabricacao, DataValidade = @DataValidade, CodigoFornecedor = @CodigoFornecedor WHERE Codigo = @Codigo";
            await _dbConnection.ExecuteAsync(strQuery, produto);
        }

        public async Task<int> ContarProdutos(string codigo, string descricao, string situacao, string codigoFornecedor)
        {
            var strQuery = new StringBuilder(@"SELECT COUNT(*) FROM Produtos WHERE 1=1");

            var parametros = new DynamicParameters();

            if (!string.IsNullOrEmpty(codigo))
            {
                strQuery.Append(" AND Codigo LIKE @Codigo");
                parametros.Add("Codigo", $"%{codigo}%");
            }

            if (!string.IsNullOrEmpty(descricao))
            {
                strQuery.Append(" AND Descricao LIKE @Descricao");
                parametros.Add("Descricao", $"%{descricao}%");
            }

            if (!string.IsNullOrEmpty(situacao))
            {
                var situacaoValor = situacao.Equals("Ativo", StringComparison.OrdinalIgnoreCase) ? "1" :
                                    situacao.Equals("Inativo", StringComparison.OrdinalIgnoreCase) ? "0" : situacao;
                strQuery.Append(" AND Situacao = @Situacao");
                parametros.Add("Situacao", situacaoValor);
            }

            if (!string.IsNullOrEmpty(codigoFornecedor))
            {
                strQuery.Append(" AND CodigoFornecedor LIKE @CodigoFornecedor");
                parametros.Add("CodigoFornecedor", $"%{codigoFornecedor}%");
            }

            return await _dbConnection.ExecuteScalarAsync<int>(strQuery.ToString(), parametros);
        }


        public async Task ExclusaoLogica(int codigo)
        {
            var strQuery = @"UPDATE Produtos SET Situacao = 0 WHERE Codigo = @Codigo";
            await _dbConnection.ExecuteAsync(strQuery, new { Codigo = codigo });
        }


        public async Task Inserir(ProdutoEntity produto)
        {
            var strQuery = @"INSERT INTO Produtos (Descricao, Situacao, DataFabricacao, DataValidade, CodigoFornecedor) 
                                    VALUES (@Descricao,@Situacao, @DataFabricacao, @DataValidade, @CodigoFornecedor)";
            await _dbConnection.ExecuteAsync(strQuery, produto);
        }

        public async Task<IEnumerable<ProdutoEntity>> Listar(int pagina, int tamanho, string codigo, string descricao, string situacao, string codigoFornecedor)
        {
            var strQuery = new StringBuilder(@"
                               SELECT P.Codigo, P.Descricao, P.Situacao, P.DataFabricacao, P.DataValidade, P.CodigoFornecedor,
                                      F.DescricaoFornecedor, F.CnpjFornecedor 
                               FROM Produtos P
                               LEFT JOIN Fornecedor F ON P.CodigoFornecedor = F.CodigoFornecedor
                               WHERE 1=1");

            var parametros = new DynamicParameters();
            int offset = (pagina - 1) * tamanho;

            if (!string.IsNullOrEmpty(codigo))
            {
                strQuery.Append(" AND Codigo LIKE @Codigo");
                parametros.Add("Codigo", $"%{codigo}%");
            }

            if (!string.IsNullOrEmpty(descricao))
            {
                strQuery.Append(" AND Descricao LIKE @Descricao");
                parametros.Add("Descricao", $"%{descricao}%");
            }

            if (!string.IsNullOrEmpty(situacao))
            {
                var situacaoValor = situacao.Equals("Ativo", StringComparison.OrdinalIgnoreCase) ? "1" :
                                    situacao.Equals("Inativo", StringComparison.OrdinalIgnoreCase) ? "0" : situacao;
                strQuery.Append(" AND Situacao = @Situacao");
                parametros.Add("Situacao", situacaoValor);
            }

            if (!string.IsNullOrEmpty(codigoFornecedor))
            {
                strQuery.Append(" AND CodigoFornecedor LIKE @CodigoFornecedor");
                parametros.Add("CodigoFornecedor", $"%{codigoFornecedor}%");
            }

            strQuery.Append(" ORDER BY Codigo LIMIT @Tamanho OFFSET @Offset");

            parametros.Add("Tamanho", tamanho);
            parametros.Add("Offset", offset);

            var produtos = await _dbConnection.QueryAsync<ProdutoEntity, FornecedorEntity, ProdutoEntity>(
                                strQuery.ToString(),
                                (produto, fornecedor) =>
                                {
                                    produto.Fornecedor = fornecedor;
                                    return produto;
                                },
                                parametros,
                                splitOn: "CodigoFornecedor");

            return produtos;
        }



        public async Task<ProdutoEntity> ObterPorCodigo(int codigo)
        {
            var strQuery = @"SELECT Codigo, Descricao, Situacao, DataFabricacao, DataValidade, CodigoFornecedor FROM Produtos WHERE Codigo = @Codigo";
            return await _dbConnection.QueryFirstOrDefaultAsync<ProdutoEntity>(strQuery, new { Codigo = codigo });
        }
    }
}
