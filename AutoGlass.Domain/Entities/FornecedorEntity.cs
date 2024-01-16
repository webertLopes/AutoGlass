namespace AutoGlass.Domain.Entities
{
    public class FornecedorEntity 
    {
        public FornecedorEntity(int codigoFornecedor, string descricaoFornecedor, string cnpjFornecedor)
        {
            CodigoFornecedor = codigoFornecedor;
            DescricaoFornecedor = descricaoFornecedor;
            CnpjFornecedor = cnpjFornecedor;
        }

        public FornecedorEntity()
        {
            
        }       

        ///<summary>
        /// Codigo Fornecedor
        ///</summary>
        public int CodigoFornecedor { get; set; }
        ///<summary>
        /// Descricao Fornecedor
        ///</summary>
        public string DescricaoFornecedor { get; set; }
        ///<summary>
        /// Cnpj Fornecedor
        ///</summary>
        public string CnpjFornecedor { get; set; }

        ///<summary>
        /// IsCnpjValido
        ///</summary>
        public bool IsCnpjValido => Validate();

        ///<summary>
        /// Verifica apenas se o cnpj possui tamanho 14 após retirar os caracteres.
        ///</summary>
        private bool Validate()
        {
            string cnpj = CnpjFornecedor
            .Replace(".", "")
            .Replace("-", "")
            .Replace("/", "").Trim();

            if (cnpj.Length != 14)
                return false;

            return true;
        }
    }
}
