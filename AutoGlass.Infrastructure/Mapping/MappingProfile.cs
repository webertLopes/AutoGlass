using AutoGlass.Application.DTOs;
using AutoGlass.Domain.Entities;
using AutoMapper;
using System.Collections.Generic;

namespace AutoGlass.Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProdutoEntity, ProdutoDTO>();
            CreateMap<ProdutoDTO, ProdutoEntity>();


            CreateMap<FornecedorEntity, FornecedorDTO>();
            CreateMap<FornecedorDTO, FornecedorEntity>();
        }
    }
}
