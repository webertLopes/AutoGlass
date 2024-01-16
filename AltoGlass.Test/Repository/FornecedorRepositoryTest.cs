using AutoGlass.Domain.Entities;
using AutoGlass.Infrastructure.Repositories;
using Dapper;
using Microsoft.Data.Sqlite;
using NUnit.Framework;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AutoGlass.Tests.Repositories
{
    [TestFixture]
    public class FornecedorRepositoryTests
    {
        private FornecedorRepository _repository;
        private IDbConnection _dbConnection;

        [SetUp]
        public async Task Setup()
        {
            var connectionString = "Data Source=:memory:";
            _dbConnection = new SqliteConnection(connectionString);
            _dbConnection.Open();

            // Create the table
            var createTableScriptFornecedor = @"
                  CREATE TABLE IF NOT EXISTS Fornecedor (
                      CodigoFornecedor INTEGER PRIMARY KEY AUTOINCREMENT,
                      DescricaoFornecedor TEXT,
                      CnpjFornecedor TEXT
                  );";

            await _dbConnection.ExecuteAsync(createTableScriptFornecedor);

            _repository = new FornecedorRepository(_dbConnection);
        }

        [TearDown]
        public void TearDown()
        {
            _dbConnection.Close();
        }

        [Test]
        public async Task Inserir_ShouldInsertFornecedor()
        {
            // Arrange
            var fornecedor = new FornecedorEntity
            {
                DescricaoFornecedor = "Fornecedor A",
                CnpjFornecedor = "1234567890"
            };

            // Act
            await _repository.Inserir(fornecedor);

            // Assert
            var result = await _dbConnection.QueryAsync<FornecedorEntity>("SELECT * FROM Fornecedor");
            Assert.That(result.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task ObterPorCodigo_ShouldReturnFornecedor()
        {
            // Arrange
            var fornecedor = new FornecedorEntity
            {
                DescricaoFornecedor = "Fornecedor A",
                CnpjFornecedor = "1234567890"
            };

            await _dbConnection.ExecuteAsync("INSERT INTO Fornecedor (DescricaoFornecedor, CnpjFornecedor) VALUES (@DescricaoFornecedor, @CnpjFornecedor);", fornecedor);

            // Act
            var result = await _repository.ObterPorCodigo(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.DescricaoFornecedor, Is.EqualTo(fornecedor.DescricaoFornecedor));
            Assert.That(result.CnpjFornecedor, Is.EqualTo(fornecedor.CnpjFornecedor));
        }

    }
}
