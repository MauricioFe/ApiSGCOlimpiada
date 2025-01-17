﻿using ApiSGCOlimpiada.Data.ProdutoPedidoOrcamentoDAO;
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
    public class ProdutoPedidoOrcamentoController : ControllerBase
    {
        private readonly IProdutoPedidoOrcamentoDAO dao;

        public ProdutoPedidoOrcamentoController(IProdutoPedidoOrcamentoDAO dao)
        {
            this.dao = dao;
        }

        [HttpGet]
        public IEnumerable<ProdutoPedidoOrcamento> GetAll()
        {
            return dao.GetAll();
        }

        [HttpGet("{id}", Name = "GetProdutoPedidoOrcamento")]
        public IActionResult GetProdutoPedidoOrcamentoById(long id)
        {
            var ProdutoOrcamentoPedido = dao.Find(id);
            if (ProdutoOrcamentoPedido == null)
                return NotFound(new { Message = "ProdutoPedidoOrcamento não encontrado" });
            return new ObjectResult(ProdutoOrcamentoPedido);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProdutoPedidoOrcamento produtoPedidoOrcamento)
        {
            if (dao.Add(produtoPedidoOrcamento))
                return new ObjectResult(produtoPedidoOrcamento);
            return BadRequest(new { Message = "Erro interno no servidor" });
        }
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] ProdutoPedidoOrcamento produtoPedidoOrcamento, long id)
        {
            if (dao.Find(id) == null)
                return NotFound(new { Message = "ProdutoPedidoOrcamento não encontrado" });
            if (dao.Update(produtoPedidoOrcamento, id))
                return new ObjectResult(produtoPedidoOrcamento);

            return BadRequest(new { Message = "Erro interno no servidor" });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if (dao.Find(id) == null)
                return NotFound(new { Message = "ProdutoPedidoOrcamento não encontrado" });
            if (dao.Remove(id))
                return Ok(new { Message = "Excluído com sucesso" });
            return BadRequest(new { Message = "Erro interno no servidor" });
        }

        [HttpGet("solicitacao/{idSolicitacao}")]
        public IEnumerable<ProdutoPedidoOrcamento> GetProdutosSolicitacao(long idSolicitacao)
        {
            return dao.GetSolicitacao(idSolicitacao);
        }
    }
}
