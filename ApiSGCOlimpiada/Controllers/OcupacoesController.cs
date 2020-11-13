using ApiSGCOlimpiada.Data.OcupacaoDAO;
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
            {
                return NotFound(
                    new
                    {
                        Message = "Ocupação não encontrada"
                    }
                );
            }
            return new ObjectResult(ocupacao);
        }
        [HttpPost]
        public IActionResult Create([FromBody] Ocupacao ocupacao)
        {
            if (ocupacao == null)
            {
                return BadRequest(
                  new
                  {
                      Message = "Todos os campos são obrigatórios"
                  });
            }

            dao.Add(ocupacao);
            return CreatedAtRoute("GetOcupacao", new { id = ocupacao.Id }, ocupacao);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Ocupacao ocupacao, long id)
        {
            if (ocupacao == null)
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
                          Message = "Ocupação não encontrada"
                      }
                  );
            }
            Ocupacao ocupacaoUpdated = new Ocupacao();
            ocupacaoUpdated.Id = id;
            dao.Update(ocupacaoUpdated, id);
            return CreatedAtRoute("GetOcupacao", new { id = ocupacaoUpdated.Id }, ocupacaoUpdated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var ocupacao = dao.Find(id);
            if (ocupacao == null)
            {
                return NotFound(
                      new
                      {
                          Message = "Ocupação não encontrada"
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
