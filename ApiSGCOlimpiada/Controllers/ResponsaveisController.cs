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
        public IActionResult GetResponsavelById(long id)
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

        [HttpGet("search", Name = "GetResponsavelBySearch")]
        public IActionResult GetResponsavelBySearch([FromQuery(Name = "search")] string search)
        {
            var responsavel = dao.FindBySearch(search);
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

            try
            {
                dao.Add(responsavel);
                return CreatedAtRoute("GetResponsavel", new { id = responsavel.Id }, responsavel);
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    Message = "Erro interno no servidor"
                });
            }
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
            try
            {
                dao.Update(responsavelUpdated, id);
                return CreatedAtRoute("GetResponsavel", new { id = responsavelUpdated.Id }, responsavelUpdated);
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    Message = "Erro interno no servidor"
                });
            }
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

            try
            {
                dao.Remove(id);
                return Ok(
                        new
                        {
                            Message = "Excluído com sucesso"
                        }
                    );
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    Message = "Erro interno no servidor"
                });
            }
        }
    }
}
