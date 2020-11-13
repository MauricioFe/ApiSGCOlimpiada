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
        public IActionResult GetUsuarioById(int id)
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
        [HttpPost]
        public IActionResult Create([FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest(
                    new
                    {
                        Message = "Erro interno do servidor"
                    });

            }

            dao.Add(usuario);
            return CreatedAtRoute("GetRelato", new { id = usuario.Id }, usuario);
        }
    }
}
