using ApiSGCOlimpiada.Data.ResponsavelDAO;
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
    public class ResponsaveisController : ControllerBase
    {
        private readonly IResponsavelDAO dao;

        public ResponsaveisController(IResponsavelDAO dao)
        {
            this.dao = dao;
        }
        [HttpGet]
        public IEnumerable<Responsavel> GetAll()
        {
            return dao.GetAll();
        }

        [HttpGet("{id}", Name = "GetResponsavel")]
        public IActionResult GetResponsavelById(int id)
        {
            var responsavel = dao.Find(id);
            if (responsavel == null)
            {
                return NotFound(
                    new
                    {
                        Message = "Responsavel não encontrado"
                    }
                );
            }
            return new ObjectResult(responsavel);
        }
        [HttpPost]
        public IActionResult Create([FromBody] Responsavel responsavel)
        {
            if (responsavel == null)
            {
                return BadRequest(
                  new
                  {
                      Message = "Todos os campos são obrigatórios"
                  });
            }

            dao.Add(responsavel);
            return CreatedAtRoute("GetResponsavel", new { id = responsavel.Id }, responsavel);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Responsavel responsavel, long id)
        {
            if (responsavel == null)
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
                          Message = "Responsavel não encontrado"
                      }
                  );
            }
            Responsavel responsavelUpdated = new Responsavel();
            responsavelUpdated.Id = id;
            responsavelUpdated.Nome = responsavel.Nome;
            responsavelUpdated.Cargo = responsavel.Cargo;
            responsavelUpdated.EscolaId = responsavel.EscolaId;
            dao.Update(responsavelUpdated, id);
            return CreatedAtRoute("GetResponsavel", new { id = responsavelUpdated.Id }, responsavelUpdated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var responsavel = dao.Find(id);
            if (responsavel == null)
            {
                return NotFound(
                      new
                      {
                          Message = "Responsavel não encontrado"
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
