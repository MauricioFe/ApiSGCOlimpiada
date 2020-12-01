using ApiSGCOlimpiada.Data.OrcamentoDAO;
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
    public class OrcamentosController : ControllerBase
    {
        private readonly IOrcamentoDAO dao;

        public OrcamentosController(IOrcamentoDAO dao)
        {
            this.dao = dao;
        }

        [HttpGet]
        public IEnumerable<Orcamento> GetAll()
        {
            return dao.GetAll();
        }

        [HttpGet("{id}", Name = "GetOrcamento")]
        public IActionResult GetOrcamentoById(long id)
        {
            var solicitacao = dao.Find(id);
            if (solicitacao == null)
                return NotFound(new { Message = "Orçamento não encontrado" });
            return new ObjectResult(solicitacao);
        }

        [HttpPost]
        public IActionResult Create([FromForm] Orcamento orcamento)
        {
            if (string.IsNullOrEmpty(orcamento.Fornecedor) && string.IsNullOrEmpty(orcamento.Cnpj)
                && string.IsNullOrEmpty(orcamento.Data.ToString("dd/MM/yyyy HH:mm")) && orcamento.ValorTotal == 0
                && orcamento.TotalIpi == 0 && orcamento.TotalProdutos == 0)
                return BadRequest(new { Message = "Todos os campos são obrigatórios" });
            if (dao.Add(orcamento))
                return CreatedAtRoute("GetOrcamento", new { id = orcamento.Id }, orcamento);
            return BadRequest(new { Message = "Erro interno no servidor" });
        }
        [HttpPut("{id}")]
        public IActionResult Put([FromForm] Orcamento orcamento, long id)
        {
            if (string.IsNullOrEmpty(orcamento.Fornecedor) && string.IsNullOrEmpty(orcamento.Cnpj)
               && string.IsNullOrEmpty(orcamento.Data.ToString("dd/MM/yyyy HH:mm")) && orcamento.ValorTotal == 0
               && orcamento.TotalIpi == 0 && orcamento.TotalProdutos == 0)
                return BadRequest(new { Message = "Todos os campos são obrigatórios" });
            if (dao.Find(id) == null)
                return NotFound(new { Message = "Orçamento não encontrado" });
            if (dao.Update(orcamento, id))
                return CreatedAtRoute("GetOrcamento", new { id = orcamento.Id }, orcamento);

            return BadRequest(new { Message = "Erro interno no servidor" });
        }
    }
}
