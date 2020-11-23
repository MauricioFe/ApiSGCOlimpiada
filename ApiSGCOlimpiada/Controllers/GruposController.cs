using ApiSGCOlimpiada.Data.GrupoDAO;
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
    public class GruposController : ControllerBase
    {
        private readonly IGrupoDAO dao;

        public GruposController(IGrupoDAO dao)
        {
            this.dao = dao;
        }
        [HttpGet]
        public IEnumerable<Grupo> GetAll()
        {
            return dao.GetAll();
        }

        [HttpGet("{id}", Name = "GetGrupo")]
        public IActionResult GetGrupoById(long id)
        {
            var grupo = dao.Find(id);
            if (grupo == null)
            {
                return NotFound(
                    new
                    {
                        Message = "Grupo não encontrado"
                    }
                );
            }
            return new ObjectResult(grupo);
        }
        [HttpPost]
        public IActionResult Create([FromBody] Grupo grupo)
        {
            if (grupo == null)
            {
                return BadRequest(
                  new
                  {
                      Message = "Todos os campos são obrigatórios"
                  });
            }

            try
            {
                dao.Add(grupo);
                return CreatedAtRoute("GetGrupo", new { id = grupo.Id }, grupo);
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
        public IActionResult Put([FromBody] Grupo grupo, long id)
        {
            if (grupo == null)
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
                          Message = "Grupo não encontrado"
                      }
                  );
            }
            Grupo grupoUpdated = new Grupo();
            grupoUpdated.Id = id;
            grupoUpdated.CodigoProtheus = grupo.CodigoProtheus;
            grupoUpdated.Descricao = grupo.Descricao;
            try
            {
                dao.Update(grupoUpdated, id);
                return CreatedAtRoute("GetGrupo", new { id = grupoUpdated.Id }, grupoUpdated);
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
            var grupo = dao.Find(id);
            if (grupo == null)
            {
                return NotFound(
                      new
                      {
                          Message = "Grupo não encontrado"
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
