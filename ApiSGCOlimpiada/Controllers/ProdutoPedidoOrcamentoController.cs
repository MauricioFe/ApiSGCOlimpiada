using ApiSGCOlimpiada.Data.ProdutoPedidoOrcamentoDAO;
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

        [HttpGet("{solicitacaoId}/{produtosId}", Name = "GetProdutoPedidoOrcamento")]
        public IActionResult GetProdutoPedidoOrcamentoById(long solicitacaoId, long produtosId)
        {
            var ProdutoOrcamentoPedido = dao.Find(solicitacaoId, produtosId);
            if (ProdutoOrcamentoPedido == null)
                return NotFound(new { Message = "ProdutoPedidoOrcamento não encontrado" });
            return new ObjectResult(ProdutoOrcamentoPedido);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProdutoPedidoOrcamento produtoPedidoOrcamento)
        {
            if (produtoPedidoOrcamento.SolicitacaoComprasId == 0 && produtoPedidoOrcamento.ProdutoId == 0
                && produtoPedidoOrcamento.valor == 0 && produtoPedidoOrcamento.Quantidade == 0
                && produtoPedidoOrcamento.OrcamentoId == 0)
                return BadRequest(new { Message = "Todos os campos são obrigatórios" });
            if (dao.Add(produtoPedidoOrcamento))
                return new ObjectResult(produtoPedidoOrcamento);
            return BadRequest(new { Message = "Erro interno no servidor" });
        }
        [HttpPut("{solicitacaoId}/{produtosId}/{orcamentosId}")]
        public IActionResult Put([FromBody] ProdutoPedidoOrcamento produtoPedidoOrcamento, long solicitacaoId, long produtosId, long orcamentosId)
        {
            if (produtoPedidoOrcamento.SolicitacaoComprasId == 0 && produtoPedidoOrcamento.ProdutoId == 0
                && produtoPedidoOrcamento.valor == 0 && produtoPedidoOrcamento.Quantidade == 0
                && produtoPedidoOrcamento.OrcamentoId == 0)
                return BadRequest(new { Message = "Todos os campos são obrigatórios" });

            if (dao.Find(solicitacaoId, produtosId) == null)
                return NotFound(new { Message = "ProdutoPedidoOrcamento não encontrado" });
            if (dao.Update(produtoPedidoOrcamento, solicitacaoId, produtosId, orcamentosId))
                return new ObjectResult(produtoPedidoOrcamento);

            return BadRequest(new { Message = "Erro interno no servidor" });
        }
    }
}
