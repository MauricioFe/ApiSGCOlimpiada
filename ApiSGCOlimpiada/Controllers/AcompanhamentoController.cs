using ApiSGCOlimpiada.Data.AcompanhamentoDAO;
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
    public class AcompanhamentoController : ControllerBase
    {
        private readonly IAcompanhamentoDAO dao;

        public AcompanhamentoController(IAcompanhamentoDAO dao)
        {
            this.dao = dao;
        }
        [HttpGet]
        public IEnumerable<Acompanhamento> GetAll()
        {
            return dao.GetAll();
        }

        [HttpGet("{id}", Name = "GetAcompanhamento")]
        public IActionResult GetAcompanhamentoById(long id)
        {
            var acompanhamento = dao.FindBySolicitacaoId(id);
            if (acompanhamento == null)
                return NotFound(new { Message = "Acompanhamento não encontrada" });
            return new ObjectResult(acompanhamento);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Acompanhamento acompanhamento)
        {
            if (string.IsNullOrEmpty(acompanhamento.Date.ToString()) ||
                acompanhamento.StatusId == 0 || acompanhamento.UsuarioId == 0 || acompanhamento.SolicitacaoCompraId == 0)
                return BadRequest(new { Message = "Todos os campos são obrigatórios" });

            if (dao.Add(acompanhamento))
                return CreatedAtRoute("GetAcompanhamento", new { id = acompanhamento.Id }, acompanhamento);
            return BadRequest(new { Message = "Erro interno no servidor" });
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Acompanhamento acompanhamento, long id)
        {
            if (dao.FindBySolicitacaoId(id) == null)
                return NotFound(new { Message = "Acompanhamento não encontrada" });

            if (dao.Update(acompanhamento, id))
                return CreatedAtRoute("GetAcompanhamento", new { id = acompanhamento.Id }, acompanhamento);
            return BadRequest(new { Message = "Erro interno no servidor" });
        }
        [HttpGet("solicitacao")]
        public IEnumerable<Acompanhamento> GetSolicitacaoAcompanhamento()
        {
            return dao.GetSolicitacaoAcompanhamento();
        }

        [HttpGet("solicitacao/pendente")]
        public IEnumerable<Acompanhamento> GetSolicitacaoAcompanhamentoPendente()
        {
            return dao.GetSolicitacaoAcompanhamentoPendente();
        }
    }
}

