using ApiSGCOlimpiada.Data.OcupacaoSolicitacaoCompraDAO;
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
    public class OcupacaoSolicitacaoCompraController : ControllerBase
    {
        private readonly IOcupacaoSolicitacaoCompraDAO dao;

        public OcupacaoSolicitacaoCompraController(IOcupacaoSolicitacaoCompraDAO dao)
        {
            this.dao = dao;
        }

        [HttpGet]
        public IEnumerable<OcupacaoSolicitacaoCompra> GetAll()
        {
            return dao.GetAll();
        }

        [HttpGet("{ocupacoesId}/{solicitacaoId}", Name = "GetOcupacoesSolicitacao")]
        public IActionResult GetSolicitacaoById(long ocupacoesId, long solicitacaoId)
        {
            var solicitacao = dao.Find(ocupacoesId, solicitacaoId);
            if (solicitacao == null)
                return NotFound(new { Message = "Solicitação de compra não encontrada" });
            return new ObjectResult(solicitacao);
        }

        [HttpPost]
        public IActionResult CreateAsync([FromBody] OcupacaoSolicitacaoCompra ocupacaoSolicitacaoCompra)
        {
            if (ocupacaoSolicitacaoCompra.SolicitacaoId == 0 && ocupacaoSolicitacaoCompra.OcupacaoId == 0)
                return BadRequest(new { Message = "Todos os campos são obrigatórios" });
            if (dao.Add(ocupacaoSolicitacaoCompra))
                return new ObjectResult(ocupacaoSolicitacaoCompra);
            return BadRequest(new { Message = "Erro interno no servidor" });
        }
        [HttpPut("{ocupacoesId}/{solicitacaoId}")]
        public IActionResult PutAsync([FromBody] OcupacaoSolicitacaoCompra ocupacaoSolicitacaoCompra, long ocupacoesId, long solicitacaoId)
        {
            if (ocupacaoSolicitacaoCompra.SolicitacaoId == 0 && ocupacaoSolicitacaoCompra.OcupacaoId == 0)
                return BadRequest(new { Message = "Todos os campos são obrigatórios" });
            if (dao.Find(ocupacoesId, solicitacaoId) == null)
                return NotFound(new { Message = "Solicitação de compra não encontrada" });
            if (dao.Update(ocupacaoSolicitacaoCompra, ocupacoesId, solicitacaoId))
                return new ObjectResult(ocupacaoSolicitacaoCompra);
            return BadRequest(new { Message = "Erro interno no servidor" });
        }
    }
}
