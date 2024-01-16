using System;

namespace AutoGlass.Domain.Entities
{
    public class ProdutoEntity 
    {
        public ProdutoEntity(int codigo, string descricao, bool situacao, DateTime dataFabricacao, DateTime dataValidade)
        {
            Codigo = codigo;
            Descricao = descricao;
            Situacao = situacao;
            DataFabricacao = dataFabricacao;
            DataValidade = dataValidade;
        }

        public ProdutoEntity()
        {
            
        }

        ///<summary>
        /// Codigo
        ///</summary>
        public int Codigo { get; set; }

        ///<summary>
        /// Descricao
        ///</summary>
        public string Descricao { get; set; }

        ///<summary>
        /// Situacao
        ///</summary>
        public bool Situacao { get; set; }

        ///<summary>
        /// Data Fabricacao
        ///</summary>
        public DateTime DataFabricacao { get; set; }

        ///<summary>
        /// Data Validade
        ///</summary>
        public DateTime DataValidade { get; set; }

        ///<summary>
        /// Data Validade
        ///</summary>
        public int CodigoFornecedor { get; set; }

        ///<summary>
        /// IsDataFabricacaoMenorDataValidade
        ///</summary>
        public bool IsDataFabricacaoMenorDataValidade => Validate();

        ///<summary>
        /// Fornecedor associado ao produto
        ///</summary>
        public FornecedorEntity Fornecedor { get; set; }

        ///<summary>
        /// Verifica se a data de fabricação menor que data validade
        ///</summary>
        private bool Validate()
        {
            return
                this.DataFabricacao < DataValidade;
        }
    }
}
