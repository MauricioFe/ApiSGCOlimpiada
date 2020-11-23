using ApiSGCOlimpiada.Data.UsuarioDAO;
using ApiSGCOlimpiada.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncaoController : Controller
    {
        private readonly IFuncaoDAO dao;

        public FuncaoController(IFuncaoDAO dao)
        {
            this.dao = dao;
        }
        [HttpGet]
        public IEnumerable<Funcao> GetAll()
        {
            return dao.GetAll();
        }

        [HttpGet("{id}", Name = "GetFuncao")]
        public IActionResult GetFuncaoById(long id)
        {
            var funcao = dao.Find(id);
            if (funcao == null)
            {
                return NotFound(
                    new
                    {
                        Message = "Funcao não encontrado"
                    }
                );
            }
            return new ObjectResult(funcao);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Funcao funcao)
        {
            if (funcao == null)
            {
                return BadRequest(
                  new
                  {
                      Message = "Todos os campos são obrigatórios"
                  });
            }

            try
            {
                dao.Add(funcao);
                return CreatedAtRoute("GetFuncao", new { id = funcao.Id }, funcao);
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
        public IActionResult Put([FromBody] Funcao funcao, long id)
        {
            if (funcao == null)
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
                          Message = "Funcao não encontrado"
                      }
                  );
            }
            Funcao funcaoUpdated = new Funcao();
            funcaoUpdated.Id = id;
            funcaoUpdated.funcao = funcao.funcao;
            try
            {
                dao.Update(funcaoUpdated, id);
                return CreatedAtRoute("GetFuncao", new { id = funcaoUpdated.Id }, funcaoUpdated);
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
            var funcao = dao.Find(id);
            if (funcao == null)
            {
                return NotFound(
                      new
                      {
                          Message = "Funcao não encontrado"
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
