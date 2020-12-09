using ApiSGCOlimpiada.Data.SolicitacaoCompraDAO;
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
        public IActionResult GetSolicitacaoById(long id)
        {
            var solicitacao = dao.Find(id);
            if (solicitacao == null)
                return NotFound(new { Message = "Solicitação de compra não encontrada" });
            return new ObjectResult(solicitacao);
        }

        [HttpPost]
        public IActionResult CreateAsync([FromBody] SolicitacaoCompra solicitacaoCompra)
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
        public IActionResult PutAsync([FromBody] SolicitacaoCompra solicitacaoCompra, long id)
        {
            if (string.IsNullOrEmpty(solicitacaoCompra.ResponsavelEntrega) && string.IsNullOrEmpty(solicitacaoCompra.Justificativa)
                && string.IsNullOrEmpty(solicitacaoCompra.Data.ToString("dd/MM/yyyy HH:mm")) && solicitacaoCompra.EscolaId == 0
                && solicitacaoCompra.TipoCompraId == 0)
                return BadRequest(new { Message = "Todos os campos são obrigatórios" });
            if (dao.Find(id) == null)
                return NotFound(new { Message = "Solicitação de compra não encontrada" });
            if (dao.Update(solicitacaoCompra, id))
                return CreatedAtRoute("GetSolicitacao", new { id = solicitacaoCompra.Id }, solicitacaoCompra);

            return BadRequest(new { Message = "Erro interno no servidor" });
        }
        [HttpPatch("{id}/notafiscal")]
        public async Task<IActionResult> AnexarNotaFiscal([FromForm] IFormFile arquivo, long id)
        {
            if (dao.Find(id) == null)
                return NotFound(new { Message = "Solicitação de compra não encontrada" });
            var fileName = await Utils.UploadUtil.UploadAnexosPdfAsync(arquivo, "AnexoOrcamentos");
            if (fileName == null)
                return BadRequest(new { Message = "Erro ao fazer upload" });
            if (dao.AnexarNotaFiscal(fileName, id))
                return new NoContentResult();

            return BadRequest(new { Message = "Erro interno no servidor" });
        }
    }
}
