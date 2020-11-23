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
        public IActionResult GetProdutoById(long id)
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
        [HttpGet("search", Name = "GetProdutoByProtheus")]
        public IActionResult GetProdutoByProtheus([FromQuery (Name = "codigoProtheus")]long codigoProtheus)
        {
            var produto = dao.FindByProtheus(codigoProtheus);
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

            try
            {
                dao.Add(produto);
                return CreatedAtRoute("GetProduto", new { id = produto.Id }, produto);
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    Message = "Erro interno no servidor"
                });
            }
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
            try
            {
                dao.Update(produtoUpdated, id);
                return CreatedAtRoute("GetProduto", new { id = produtoUpdated.Id }, produtoUpdated);
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    Message = "Erro interno no servidor"
                });
            }
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

            try
            {
                dao.Remove(id);
                return Ok(
                        new
                        {
                            Message = "Excluído com sucesso"
                        }
                    );
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    Message = "Erro interno no servidor"
                });
            }
        }
    }
}
