using ApiSGCOlimpiada.Data.StatusDAO;
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
    public class StatusController : ControllerBase
    {
        private readonly IStatusDAO dao;

        public StatusController(IStatusDAO dao)
        {
            this.dao = dao;
        }
        [HttpGet]
        public IEnumerable<Status> GetAll()
        {
            return dao.GetAll();
        }

        [HttpGet("{id}", Name = "GetStatus")]
        public IActionResult GetStatusById(long id)
        {
            var status = dao.Find(id);
            if (status == null)
                return NotFound(new { Message = "Ocupação não encontrada" });
            return new ObjectResult(status);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Status status)
        {
            if (string.IsNullOrEmpty(status.Descricao))
                return BadRequest(new { Message = "Todos os campos são obrigatórios" });

            if (dao.Add(status))
                return CreatedAtRoute("GetStatus", new { id = status.Id }, status);
            return BadRequest(new { Message = "Erro interno no servidor" });
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Status status, long id)
        {
            if (string.IsNullOrEmpty(status.Descricao))
                return BadRequest(new { Message = "Todos os campos são obrigatórios" });
            if (dao.Find(id) == null)
                return NotFound(new { Message = "Ocupação não encontrada" });
            if (dao.Update(status, id))
                return CreatedAtRoute("GetStatus", new { id = status.Id }, status);
            return BadRequest(new { Message = "Erro interno no servidor" });
        }
    }
}

