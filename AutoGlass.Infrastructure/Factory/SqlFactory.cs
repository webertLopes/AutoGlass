using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.IO;

namespace AutoGlass.Infrastructure.Factory
{
    public static class SqlFactory
    {
        public static IDbConnection CreateDatabase()
        {
            try
            {
                var basePath = AppDomain.CurrentDomain.BaseDirectory;

                var dbFilePath = Path.Combine(basePath, "AutoGlass.db");

                var connectionString = $"Data Source={dbFilePath};";
                var connection = new SqliteConnection(connectionString);
                connection.Open();

                InitializeSchema(connection);

                return connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao criar o banco de dados: " + ex.Message);
                throw;
            }           
        }

        private static void InitializeSchema(IDbConnection dbConnection)
        {
            var createTableScriptFornecedor = @"
                              CREATE TABLE IF NOT EXISTS Fornecedor (
                                  CodigoFornecedor INTEGER PRIMARY KEY AUTOINCREMENT,
                                  DescricaoFornecedor TEXT,
                                  CnpjFornecedor TEXT
                              );";

            var createTableScriptProdutos = @"
                             CREATE TABLE IF NOT EXISTS Produtos (
                                 Codigo INTEGER PRIMARY KEY AUTOINCREMENT,
                                 Descricao TEXT,
                                 Situacao INTEGER,
                                 DataFabricacao DATETIME,
                                 DataValidade DATETIME,
                                 CodigoFornecedor INTEGER,
                                 FOREIGN KEY (CodigoFornecedor) REFERENCES Fornecedor(CodigoFornecedor)
                             );";

            dbConnection.Execute(createTableScriptFornecedor);
            dbConnection.Execute(createTableScriptProdutos);
        }

    }
}
