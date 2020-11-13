using ApiSGCOlimpiada.Data.LogDAO;
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
    public class LogsController : ControllerBase
    {
        private readonly ILogDAO dao;

        public LogsController(ILogDAO dao)
        {
            this.dao = dao;
        }
        [HttpGet]
        public IEnumerable<Log> GetAll()
        {
            return dao.GetAll();
        }

        [HttpGet("{id}", Name = "GetLog")]
        public IActionResult GetLogById(long id)
        {
            var log = dao.Find(id);
            if (log == null)
            {
                return NotFound(
                    new
                    {
                        Message = "Log não encontrado"
                    }
                );
            }
            return new ObjectResult(log);
        }
        [HttpPost]
        public IActionResult Create([FromBody] Log log)
        {
            if (log == null)
            {
                return BadRequest(
                  new
                  {
                      Message = "Todos os campos são obrigatórios"
                  });
            }

            dao.Add(log);
            return CreatedAtRoute("GetLog", new { id = log.Id }, log);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Log log, long id)
        {
            if (log == null)
            {
                return BadRequest(
                   new
                   {
                       Message = "Todos os campos são obrigatórios"
                   });
            }

            if (dao.Find(id) == null)
            {
                return NotFound(
                      new
                      {
                          Message = "Log não encontrado"
                      }
                  );
            }
            Log logUpdated = new Log();
            logUpdated.Id = id;
            logUpdated.Descricao = log.Descricao;
            logUpdated.UsuarioId = log.UsuarioId;
            dao.Update(logUpdated, id);
            return CreatedAtRoute("GetLog", new { id = logUpdated.Id }, logUpdated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var log = dao.Find(id);
            if (log == null)
            {
                return NotFound(
                      new
                      {
                          Message = "Log não encontrado"
                      }
                  );
            }

            dao.Remove(id);
            return Ok(
                    new
                    {
                        Message = "Excluído com sucesso"
                    }
                );
        }
    }
}
