using ApiSGCOlimpiada.Data.ProdutoSolicitacoesDAO;
using ApiSGCOlimpiada.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoSolicitacoesController : ControllerBase
    {
        private readonly IProdutoSolicitacoesDAO dao;

        public ProdutoSolicitacoesController(IProdutoSolicitacoesDAO dao)
        {
            this.dao = dao;
        }

        [HttpGet]
        public IEnumerable<ProdutoSolicitacao> GetAll()
        {
            return dao.GetAll();
        }
        [HttpGet("{id}", Name = "GetProdutoSolicitacao")]
        public IActionResult FindById(int id)
        {
            var produtoSolicitacao = dao.Find(id);
            if (produtoSolicitacao == null)
                return NotFound(new { Message = "ProdutoSolicitacao não encontrado" });
            return new ObjectResult(produtoSolicitacao);
        }
        [HttpGet("{idSolicitacao}", Name = "GetProdutoSolicitacaoBySolicitacao")]
        public IActionResult FindByIdSolicitacao(int idSolicitacao)
        {
            var produtoSolicitacao = dao.FindBySolicitacao(idSolicitacao);
            if (produtoSolicitacao == null)
                return NotFound(new { Message = "ProdutoSolicitacao não encontrado" });
            return new ObjectResult(produtoSolicitacao);
        }
        [HttpPost]
        public IActionResult Create([FromBody] ProdutoSolicitacao produtoSolicitacao)
        {
            if (produtoSolicitacao.ProdutosId == 0 || produtoSolicitacao.SolicitacaoComprasId == 0)
                return BadRequest(new { Message = "Todos os campos são obrigatórios" });
            if (dao.Add(produtoSolicitacao))
                return CreatedAtRoute("GetProdutoSolicitacao", new { Id = produtoSolicitacao.Id }, produtoSolicitacao);
            return BadRequest(new { Message = "Erro interno no servidor" });
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] ProdutoSolicitacao produtoSolicitacao, int id)
        {
            if (dao.Find(id) == null)
                return NotFound(new { Message = "ProdutoSolicitacao não encontrado" });
            if (produtoSolicitacao.ProdutosId == 0 || produtoSolicitacao.SolicitacaoComprasId == 0)
                return BadRequest(new { Message = "Todos os campos são obrigatórios" });
            if (dao.Update(produtoSolicitacao, id))
                return CreatedAtRoute("GetProdutoSolicitacao", new { Id = produtoSolicitacao.Id }, produtoSolicitacao);
            return BadRequest(new { Message = "Erro interno no servidor" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (dao.Find(id) == null)
                return NotFound(new { Message = "ProdutoSolicitacao não encontrado" });
            if (dao.Remove(id))
                return Ok(new { Message = "Excluído com sucesso" });
            return BadRequest(new { Message = "Erro interno no servidor" });
        }
    }
}
