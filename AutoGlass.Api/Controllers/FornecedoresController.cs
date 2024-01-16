using AutoGlass.Application.DTOs;
using AutoGlass.Application.Services;
using AutoGlass.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AutoGlass.Api.Controllers
{

    /// <summary>
    /// Fornecedores
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class FornecedoresController : ControllerBase
    {
        private readonly IFornecedorServices _fornecedorServices;
        public FornecedoresController(IFornecedorServices fornecedorServices)
        {
            _fornecedorServices = fornecedorServices;
        }

        [HttpPost()]
        public async Task<ActionResult> Post([FromBody] FornecedorDTO fornecedorDTO)
        {
            var fornecedor = await _fornecedorServices.Inserir(fornecedorDTO);

            if (fornecedor.StatusCode == 400)
                return BadRequest(fornecedor);

            return Ok(fornecedor);
        }

        [HttpGet("{codigoFornecedor}")]
        public async Task<ActionResult> Get(int codigoFornecedor)
        {
            return Ok(await _fornecedorServices.ObterPorCodigo(codigoFornecedor));
        }

        [HttpGet()]
        public async Task<ActionResult> GetByFilter([FromQuery] string filtro)
        {
            return Ok(await _fornecedorServices.Listar(filtro));
        }


        [HttpPut("{codigoFornecedor}")]
        public async Task<ActionResult> Put([FromBody] FornecedorDTO fornecedorDTO, int codigoFornecedor)
        {
            fornecedorDTO.CodigoFornecedor = codigoFornecedor;

            var fornecedor = await _fornecedorServices.Editar(fornecedorDTO);

            if (fornecedor.StatusCode == 400)
                return BadRequest(fornecedor);

            return Ok(fornecedor);
        } 
    }
}
