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
    public class FornecedorServices : IFornecedorServices
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;

        public FornecedorServices(IFornecedorRepository fornecedorRepository, IMapper mapper)
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        public async Task<RequestResult> Editar(FornecedorDTO fornecedorDto)
        {
            var fornecedor = _mapper.Map<FornecedorEntity>(fornecedorDto);

            if (!fornecedor.IsCnpjValido) 
                 return new RequestResult().BadRequest("O Cnpj informado não é válido", fornecedor);

            await _fornecedorRepository.Editar(fornecedor);

            return new RequestResult().Ok(_mapper.Map<FornecedorDTO>(fornecedor));
        }

        public async Task<RequestResult> Inserir(FornecedorDTO fornecedorDto)
        {
            var fornecedor = _mapper.Map<FornecedorEntity>(fornecedorDto);

            if (!fornecedor.IsCnpjValido) 
                return new RequestResult().BadRequest("O Cnpj informado não é válido", fornecedor);

            await _fornecedorRepository.Inserir(fornecedor);

            return new RequestResult().Ok(_mapper.Map<FornecedorDTO>(fornecedor));
        }

        public async Task<RequestResult> Listar(string filtro)
        {
            var fornecedores = await _fornecedorRepository.Listar(filtro);

            return new RequestResult().Ok(_mapper.Map<IEnumerable<FornecedorDTO>>(fornecedores));
        }

        public async Task<RequestResult> ObterPorCodigo(int codigoFornecedor)
        {
            var fornecedor = await _fornecedorRepository.ObterPorCodigo(codigoFornecedor);

            return new RequestResult().Ok(_mapper.Map<FornecedorDTO>(fornecedor));
        }
    }
}
