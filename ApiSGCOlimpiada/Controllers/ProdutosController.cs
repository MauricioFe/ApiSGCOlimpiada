using ApiSGCOlimpiada.Data.ProdutoDAO;
using ApiSGCOlimpiada.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Controllers
{
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
        public IActionResult GetProdutoById(int id)
        {
            var produto = dao.Find(id);
            if (produto == null)
            {
                return NotFound(
                    new
                    {
                        Message = "Produto não encontrado"
                    }
                );
            }
            return new ObjectResult(produto);
        }
        [HttpPost]
        public IActionResult Create([FromBody] Produto produto)
        {
            if (produto == null)
            {
                return BadRequest(
                  new
                  {
                      Message = "Todos os campos são obrigatórios"
                  });
            }

            dao.Add(produto);
            return CreatedAtRoute("GetProduto", new { id = produto.Id }, produto);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Produto produto, long id)
        {
            if (produto == null)
            {
                return BadRequest(
                   new
                   {
                       Message = "Todos os campos são obrigatórios"
                   });
            }

            if (dao.Find(id) == null)
            {
                return NotFound(
                      new
                      {
                          Message = "Produto não encontrado"
                      }
                  );
            }
            Produto produtoUpdated = new Produto();
            produtoUpdated.Id = id;
            produtoUpdated.CodigoProtheus = produto.CodigoProtheus;
            produtoUpdated.Descricao = produto.Descricao;
            produtoUpdated.GrupoId = produto.GrupoId;
            dao.Update(produtoUpdated, id);
            return CreatedAtRoute("GetProduto", new { id = produtoUpdated.Id }, produtoUpdated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var produto = dao.Find(id);
            if (produto == null)
            {
                return NotFound(
                      new
                      {
                          Message = "Produto não encontrado"
                      }
                  );
            }

            dao.Remove(id);
            return Ok(
                    new
                    {
                        Message = "Excluído com sucesso"
                    }
                );
        }
    }
}
