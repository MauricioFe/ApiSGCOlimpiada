using ApiSGCOlimpiada.Data.OcupacaoDAO;
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
    public class OcupacoesController : ControllerBase
    {
        private readonly IOcupacaoDAO dao;

        public OcupacoesController(IOcupacaoDAO dao)
        {
            this.dao = dao;
        }
        [HttpGet]
        public IEnumerable<Ocupacao> GetAll()
        {
            return dao.GetAll();
        }

        [HttpGet("{id}", Name = "GetOcupacao")]
        public IActionResult GetOcupacaoById(long id)
        {
            var ocupacao = dao.Find(id);
            if (ocupacao == null)
                return NotFound(new { Message = "Ocupação não encontrada" });
            return new ObjectResult(ocupacao);
        }

        [HttpGet("search", Name = "GetOcupacaoBySearch")]
        public IActionResult GetOcupacaoBySearch([FromQuery(Name = "search")] string search)
        {
            var ocupacao = dao.FindBySearch(search);
            if (ocupacao == null)
                return NotFound(new { Message = "Ocupação não encontrada" });

            return new ObjectResult(ocupacao);
        }
        [HttpPost]
        public IActionResult Create([FromBody] Ocupacao ocupacao)
        {
            if (string.IsNullOrEmpty(ocupacao.Nome) || string.IsNullOrEmpty(ocupacao.Numero))
                return BadRequest(new { Message = "Todos os campos são obrigatórios" });

            if (dao.Add(ocupacao))
                return CreatedAtRoute("GetOcupacao", new { id = ocupacao.Id }, ocupacao);
            return BadRequest(new { Message = "Erro interno no servidor" });
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Ocupacao ocupacao, long id)
        {
            if (string.IsNullOrEmpty(ocupacao.Nome) || string.IsNullOrEmpty(ocupacao.Numero))
                return BadRequest(new { Message = "Todos os campos são obrigatórios" });
            if (dao.Find(id) == null)
                return NotFound(new { Message = "Ocupação não encontrada" });
            if (dao.Update(ocupacao, id))
                return CreatedAtRoute("GetOcupacao", new { id = ocupacao.Id }, ocupacao);
            return BadRequest(new { Message = "Erro interno no servidor" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var ocupacao = dao.Find(id);
            if (ocupacao == null)
                return NotFound(new { Message = "Ocupação não encontrada" });
            if (dao.Remove(id))
                return Ok(new { Message = "Excluído com sucesso" });

            return BadRequest(new { Message = "Erro interno no servidor" });
        }
    }
}

