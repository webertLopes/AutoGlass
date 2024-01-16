using AutoGlass.Application.DTOs;
using AutoGlass.Application.Repositories;
using AutoGlass.Application.Results;
using AutoGlass.Application.Services.Interfaces;
using AutoGlass.Domain.Entities;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoGlass.Application.Services
{
    public class ProdutoServices : IProdutoServices
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;
        public ProdutoServices(IProdutoRepository produtoRepository, IFornecedorRepository fornecedorRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }
        public async Task<RequestResult> Editar(ProdutoDTO produtoDto)
        {
            var produto = _mapper.Map<ProdutoEntity>(produtoDto);

            if (!produto.IsDataFabricacaoMenorDataValidade)            
                return new RequestResult().BadRequest("A data de fabricação não pode ser menor que a data de validade.", produto);

            var fornecedor = await _fornecedorRepository.ObterPorCodigo(produtoDto.CodigoFornecedor);

            if (fornecedor == null)            
                return new RequestResult().BadRequest("O fornecedor especificado não existe.", produto);            

            await _produtoRepository.Editar(produto);

            return new RequestResult().Ok(_mapper.Map<ProdutoDTO>(produto));
        }

        public async Task ExclusaoLogica(int codigo)
        {
            await _produtoRepository.ExclusaoLogica(codigo);
        }

        public async Task<RequestResult> Inserir(ProdutoDTO produtoDto)
        {
            var produto = _mapper.Map<ProdutoEntity>(produtoDto);

            if (!produto.IsDataFabricacaoMenorDataValidade)
                return new RequestResult().BadRequest("A data de fabricação não pode ser menor que a data de validade.", produto);

            var fornecedor = await _fornecedorRepository.ObterPorCodigo(produtoDto.CodigoFornecedor);

            if (fornecedor == null)
                return new RequestResult().BadRequest("O fornecedor especificado não existe.", produto);

            await _produtoRepository.Inserir(produto);

            return new RequestResult().Ok(_mapper.Map<ProdutoDTO>(produto));
        }

        public async Task<RequestResult> Listar(int pagina, int tamanho, string codigo, string descricao, string situacao, string codigoFornecedor)
        {
            var produtos = await _produtoRepository.Listar(pagina, tamanho, codigo, descricao, situacao, codigoFornecedor);
            
            var totalProdutos = await _produtoRepository.ContarProdutos(codigo, descricao, situacao, codigoFornecedor);

            var paginationInfo = new RequestResult.PaginationInfo(pagina, tamanho, totalProdutos);

            return new RequestResult().Ok(_mapper.Map<IEnumerable<ProdutoDTO>>(produtos), paginationInfo);
        }


        public async Task<RequestResult> ObterPorCodigo(int codigo)
        {
            var produto = await _produtoRepository.ObterPorCodigo(codigo);

            return new RequestResult().Ok(_mapper.Map<ProdutoDTO>(produto));
        }
    }
}
