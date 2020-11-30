using ApiSGCOlimpiada.Data.SolicitacaoCompraDAO;
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
    public class SolicitacaoCompraController : ControllerBase
    {
        private readonly ISolicitacaoCompraDAO dao;

        public SolicitacaoCompraController(ISolicitacaoCompraDAO dao)
        {
            this.dao = dao;
        }

        [HttpGet]
        public IEnumerable<SolicitacaoCompra> GetAll()
        {
            return dao.GetAll();
        }

        [HttpGet("{id}", Name = "GetSolicitacao")]
        public IActionResult GetSolicitacaoById(int id)
        {
            var solicitacao = dao.Find(id);
            if (solicitacao == null)
                return NotFound(new { Message = "Solicitação de compra não encontrada" });
            return new ObjectResult(solicitacao);
        }

        [HttpPost]
        public IActionResult Create([FromBody] SolicitacaoCompra solicitacaoCompra)
        {
            if (string.IsNullOrEmpty(solicitacaoCompra.ResponsavelEntrega) && string.IsNullOrEmpty(solicitacaoCompra.Justificativa)
                && string.IsNullOrEmpty(solicitacaoCompra.Data.ToString("dd/MM/yyyy HH:mm")) && solicitacaoCompra.EscolaId == 0
                && solicitacaoCompra.TipoCompraId == 0)
                return BadRequest(new { Message = "Todos os campos são obrigatórios" });
            if (dao.Add(solicitacaoCompra))
                return CreatedAtRoute("GetSolicitacao", new { id = solicitacaoCompra.Id }, solicitacaoCompra);
            return BadRequest(new { Message = "Erro interno no servidor" });
        }
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] SolicitacaoCompra solicitacaoCompra, int id)
        {
            if (string.IsNullOrEmpty(solicitacaoCompra.ResponsavelEntrega) && string.IsNullOrEmpty(solicitacaoCompra.Justificativa)
                && string.IsNullOrEmpty(solicitacaoCompra.Data.ToString("dd/MM/yyyy HH:mm")) && solicitacaoCompra.EscolaId == 0
                && solicitacaoCompra.TipoCompraId == 0)
                return BadRequest(new { Message = "Todos os campos são obrigatórios" });
            if (dao.Find(id) == null)
                return NotFound(new { Message = "Solicitação de compra não encontrada" });
            SolicitacaoCompra solicitacaoUpdated = new SolicitacaoCompra();
            solicitacaoUpdated.Id = id;
            solicitacaoUpdated.Data = solicitacaoCompra.Data;
            solicitacaoUpdated.Justificativa = solicitacaoCompra.Justificativa;
            solicitacaoUpdated.ResponsavelEntrega = solicitacaoCompra.ResponsavelEntrega;
            solicitacaoUpdated.TipoCompraId = solicitacaoCompra.TipoCompraId;
            solicitacaoUpdated.EscolaId = solicitacaoCompra.EscolaId;
            if (dao.Update(solicitacaoUpdated, id))
                return CreatedAtRoute("GetSolicitacao", new { id = solicitacaoCompra.Id }, solicitacaoCompra);

            return BadRequest(new { Message = "Erro interno no servidor" });
        }
    }
}
