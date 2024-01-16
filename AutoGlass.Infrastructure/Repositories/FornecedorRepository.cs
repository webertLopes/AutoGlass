using AutoGlass.Application.Repositories;
using AutoGlass.Domain.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace AutoGlass.Infrastructure.Repositories
{
    public class FornecedorRepository : IFornecedorRepository
    {
        private readonly IDbConnection _dbConnection;
        public FornecedorRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task Editar(FornecedorEntity fornecedor)
        {
            var query = @"
                UPDATE Fornecedor
                SET DescricaoFornecedor = @DescricaoFornecedor, CnpjFornecedor = @CnpjFornecedor
                WHERE CodigoFornecedor = @CodigoFornecedor;";

            await _dbConnection.ExecuteAsync(query, fornecedor);
        }

        public async Task Excluir(int codigo)
        {
            var query = @"DELETE FROM Fornecedor WHERE CodigoFornecedor = @Codigo;";
            await _dbConnection.ExecuteAsync(query, new { Codigo = codigo });
        }

        public async Task Inserir(FornecedorEntity fornecedor)
        {
            var query = @"
                INSERT INTO Fornecedor (DescricaoFornecedor, CnpjFornecedor)
                VALUES (@DescricaoFornecedor, @CnpjFornecedor);";

            await _dbConnection.ExecuteAsync(query, fornecedor);
        }

        public async Task<IEnumerable<FornecedorEntity>> Listar(string filtro)
        {
            var query = @"
                  SELECT CodigoFornecedor, DescricaoFornecedor, CnpjFornecedor FROM Fornecedor
                  WHERE DescricaoFornecedor LIKE @Filtro
                  ORDER BY CodigoFornecedor;";

            return await _dbConnection.QueryAsync<FornecedorEntity>(query, new { Filtro = $"%{filtro}%" });
        }


        public async Task<FornecedorEntity> ObterPorCodigo(int codigoFornecedor)
        {
            var query = "SELECT CodigoFornecedor, DescricaoFornecedor, CnpjFornecedor FROM Fornecedor WHERE CodigoFornecedor = @codigoFornecedor";
            return await _dbConnection.QueryFirstOrDefaultAsync<FornecedorEntity>(query, new { codigoFornecedor });
        }

    }
}
