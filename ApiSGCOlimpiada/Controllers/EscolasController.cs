using ApiSGCOlimpiada.Data.EscolaDAO;
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
    public class EscolasController : ControllerBase
    {
        private readonly IEscolaDAO dao;

        public EscolasController(IEscolaDAO dao)
        {
            this.dao = dao;
        }
        [HttpGet]
        public IEnumerable<Escola> GetAll()
        {
            return dao.GetAll();
        }

        [HttpGet("{id}", Name = "GetEscola")]
        public IActionResult GetEscolaById(int id)
        {
            var escola = dao.Find(id);
            if (escola == null)
            {
                return NotFound(
                    new
                    {
                        Message = "Escola não encontrada"
                    }
                );
            }
            return new ObjectResult(escola);
        }
        [HttpPost]
        public IActionResult Create([FromBody] Escola escola)
        {
            if (escola == null)
            {
                return BadRequest(
                  new
                  {
                      Message = "Todos os campos são obrigatórios"
                  });
            }

            dao.Add(escola);
            return CreatedAtRoute("GetEscola", new { id = escola.Id }, escola);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Escola escola, long id)
        {
            if (escola == null)
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
                          Message = "Escola não encontrada"
                      }
                  );
            }
            Escola escolaUpdated = new Escola();
            escolaUpdated.Id = id;

            dao.Update(escolaUpdated, id);
            return CreatedAtRoute("GetEscola", new { id = escolaUpdated.Id }, escolaUpdated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var escola = dao.Find(id);
            if (escola == null)
            {
                return NotFound(
                      new
                      {
                          Message = "Escola não encontrada"
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
