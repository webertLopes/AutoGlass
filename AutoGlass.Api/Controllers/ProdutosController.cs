using AutoGlass.Application.DTOs;
using AutoGlass.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AutoGlass.Api.Controllers
{
    /// <summary>
    /// Produtos
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoServices _produtoServices;
        public ProdutosController(IProdutoServices produtoServices)
        {
            _produtoServices = produtoServices;
        }

        [HttpPost()]
        public async Task<ActionResult> Post([FromBody] ProdutoDTO produtoDTO)
        {
            var produto = await _produtoServices.Inserir(produtoDTO);

            if (produto.StatusCode == 400)            
                return BadRequest(produto);

            return Ok(produto);
        }

        [HttpGet("{codigo}")]
        public async Task<ActionResult> Get(int codigo)
        {
            var produto = await _produtoServices.ObterPorCodigo(codigo);

            return Ok(produto);
        }

        [HttpGet()]
        public async Task<ActionResult> GetByFilter([FromQuery] int pagina, int tamanho, string codigo, string descricao, string situacao, string codigoFornecedor)
        {
            var produtos = await _produtoServices.Listar(pagina, tamanho, codigo, descricao, situacao, codigoFornecedor);

            return Ok(produtos);
        }


        [HttpPut("{codigo}")]
        public async Task<ActionResult> Put([FromBody] ProdutoDTO produtoDTO, int codigo)
        {
            produtoDTO.Codigo = codigo;

            var produto = await _produtoServices.Editar(produtoDTO);

            if (produto.StatusCode == 400)
                return BadRequest(produto);

            return Ok(produto);
        }


        [HttpDelete()]
        public async Task<ActionResult> Delete([FromQuery] int codigo)
        {
            await _produtoServices.ExclusaoLogica(codigo);           

            return NoContent();
        }
    }
}
