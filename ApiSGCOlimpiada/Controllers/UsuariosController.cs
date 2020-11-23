using ApiSGCOlimpiada.Data.UsuarioDAO;
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
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioDAO dao;

        public UsuariosController(IUsuarioDAO dao)
        {
            this.dao = dao;
        }
        [HttpGet]
        public IEnumerable<Usuario> GetAll()
        {
            return dao.GetAll();
        }

        [HttpGet("{id}", Name = "GetUsuario")]
        public IActionResult GetUsuarioById(long id)
        {
            var usuario = dao.Find(id);
            if (usuario == null)
            {
                return NotFound(
                    new
                    {
                        Message = "Usuário não encontrado"
                    }
                );
            }
            return new ObjectResult(usuario);
        }

        [HttpGet("search", Name = "GetUsuarioByName")]
        public IActionResult GetUsuarioByName([FromQuery(Name = "nome")] string nome)
        {
            var usuario = dao.FindByName(nome);
            if (usuario == null)
            {
                return NotFound(
                    new
                    {
                        Message = "Usuário não encontrado"
                    }
                );
            }
            return new ObjectResult(usuario);
        }
        [HttpPost]
        public IActionResult Create([FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest(
                  new
                  {
                      Message = "Todos os campos são obrigatórios"
                  });
            }
            try
            {
                dao.Add(usuario);
                return CreatedAtRoute("GetUsuario", new { id = usuario.Id }, usuario);
            }
            catch (Exception)
            {
                return BadRequest(
                  new
                  {
                      Message = "Erro interno no servirdor"
                  });
            }
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest(
                    new
                    {
                        Message = "Todos os campos são obrigatórios"
                    });
            }

            var usuarioLogado = dao.Login(usuario);
            if (usuarioLogado == null)
            {
                return BadRequest(
                    new
                    {
                        Message = "Erro ao realizar login. Verifique suas credenciais"
                    });
            }
            return new ObjectResult(usuarioLogado);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Usuario usuario, long id)
        {
            if (usuario == null)
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
                          Message = "Usuário não encontrado"
                      }
                  );
            }
            Usuario usuarioUpdated = new Usuario();
            usuarioUpdated.Id = id;
            usuarioUpdated.Nome = usuario.Nome;
            usuarioUpdated.Email = usuario.Email;
            usuarioUpdated.Senha = usuario.Senha;
            try
            {
                dao.Update(usuarioUpdated, id);
                return CreatedAtRoute("GetUsuario", new { id = usuarioUpdated.Id }, usuarioUpdated);
            }
            catch (Exception)
            {
                return BadRequest(
                  new
                  {
                      Message = "Erro interno no servirdor"
                  });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var usuario = dao.Find(id);
            if (usuario == null)
            {
                return NotFound(
                      new
                      {
                          Message = "Usuário não encontrado"
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
                return BadRequest(
                  new
                  {
                      Message = "Erro interno no servirdor"
                  });
            }

        }
    }
}
