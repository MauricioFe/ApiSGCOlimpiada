using ApiSGCOlimpiada.Data.ProdutoDAO;
using ApiSGCOlimpiada.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ApiSGCOlimpiada.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoDAO dao;

        public ProdutosController(IProdutoDAO dao)
        {
            this.dao = dao;
        }
        [HttpGet]
        public IEnumerable<Produto> GetAll()
        {
            return dao.GetAll();
        }

        [HttpGet("{id}", Name = "GetProduto")]
        public IActionResult GetProdutoById(long id)
        {
            var produto = dao.Find(id);
            if (produto == null)
                return NotFound(new { Message = "Produto não encontrado" });

            return new ObjectResult(produto);
        }
        [HttpGet("search", Name = "GetProdutoBySearch")]
        public IActionResult GetProdutoBySearch([FromQuery(Name = "search")] string search)
        {
            var produto = dao.FindBySearch(search);
            if (produto.Count <= 0)
                return NotFound(new { Message = "Produto não encontrado" });

            return new ObjectResult(produto);
        }
        [HttpPost]
        public IActionResult Create([FromBody] Produto produto)
        {
            if (produto.CodigoProtheus == 0 || string.IsNullOrEmpty(produto.Descricao))
                return BadRequest(new { Message = "Todos os campos são obrigatórios" });

            if (dao.Add(produto))
                return CreatedAtRoute("GetProduto", new { id = produto.Id }, produto);

            return BadRequest(new { Message = "Erro interno no servidor" });

        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Produto produto, long id)
        {
            if (produto.CodigoProtheus == 0 || string.IsNullOrEmpty(produto.Descricao))
                return BadRequest(new { Message = "Todos os campos são obrigatórios" });

            if (dao.Find(id) == null)
                return NotFound(new { Message = "Produto não encontrado" });

            Produto produtoUpdated = new Produto();
            produtoUpdated.Id = id;
            produtoUpdated.CodigoProtheus = produto.CodigoProtheus;
            produtoUpdated.Descricao = produto.Descricao;
            produtoUpdated.GrupoId = produto.GrupoId;
            if (dao.Update(produtoUpdated, id))
                return CreatedAtRoute("GetProduto", new { id = produtoUpdated.Id }, produtoUpdated);

            return BadRequest(new { Message = "Erro interno no servidor" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var produto = dao.Find(id);
            if (produto == null)
                return NotFound(new { Message = "Produto não encontrado" });

            if (dao.Remove(id))
                return Ok(new { Message = "Excluído com sucesso" });

            return BadRequest(new { Message = "Erro interno no servidor" });
        }
    }
}
