using AutoGlass.Application.Repositories;
using AutoGlass.Domain.Entities;
using AutoGlass.Infrastructure.Repositories;
using Dapper;
using Moq;
using NUnit.Framework;
using System;
using System.Data;
using System.Threading.Tasks;

namespace AutoGlass.Tests.Repositories
{
    [TestFixture]
    public class ProdutoRepositoryTests
    {
        private Mock<IDbConnection> _dbConnectionMock;
        private IProdutoRepository _produtoRepository;

        [SetUp]
        public void Initialize()
        {
            _dbConnectionMock = new Mock<IDbConnection>();
            _produtoRepository = new ProdutoRepository(_dbConnectionMock.Object);
        }

        [Test]
        public async Task Editar_ShouldUpdateProduto()
        {
            // Arrange
            var produto = new ProdutoEntity
            {
                Codigo = 1,
                Descricao = "Produto A",
                Situacao = true,
                DataFabricacao = DateTime.Now,
                DataValidade = DateTime.Now.AddYears(1),
                CodigoFornecedor = 2
            };

            _dbConnectionMock.Setup(c => c.ExecuteAsync(It.IsAny<string>(), produto, null, null, null))
                .ReturnsAsync(1);

            // Act
            await _produtoRepository.Editar(produto);

            // Assert
            _dbConnectionMock.Verify(c => c.ExecuteAsync(It.IsAny<string>(), produto, null, null, null), Times.Once);
        }

        [Test]
        public async Task ContarProdutos_ShouldReturnCount()
        {
            // Arrange
            var codigo = "1";
            var descricao = "Produto";
            var situacao = "Ativo";
            var codigoFornecedor = "1";

            _dbConnectionMock.Setup(c => c.ExecuteScalarAsync<int>(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync(5);

            // Act
            var result = await _produtoRepository.ContarProdutos(codigo, descricao, situacao, codigoFornecedor);

            // Assert
            Assert.That(result, Is.EqualTo(5));
        }
        
    }
}
