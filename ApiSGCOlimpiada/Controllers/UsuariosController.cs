using ApiSGCOlimpiada.Data.UsuarioDAO;
using ApiSGCOlimpiada.Models;
using ApiSGCOlimpiada.Services;
using ApiSGCOlimpiada.Utils;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        [HttpGet]
        public IEnumerable<Usuario> GetAll()
        {
            return dao.GetAll();
        }
        [Authorize]
        [HttpGet("{id}", Name = "GetUsuario")]
        public IActionResult GetUsuarioById(long id)
        {
            var usuario = dao.Find(id);
            if (usuario == null)
                return NotFound(new { Message = "Usuário não encontrado" });

            return new ObjectResult(usuario);
        }
        [Authorize]
        [HttpGet("search", Name = "GetUsuarioByName")]
        public IActionResult GetUsuarioByName([FromQuery(Name = "nome")] string nome)
        {
            var usuario = dao.FindByName(nome);
            if (usuario == null)
                return NotFound(new { Message = "Usuário não encontrado" });
            return new ObjectResult(usuario);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] Usuario usuario)
        {
            if (string.IsNullOrEmpty(usuario.Nome) || string.IsNullOrEmpty(usuario.Email) || string.IsNullOrEmpty(usuario.Senha))
                return BadRequest(new { Message = "Todos os campos são obrigatórios" });
            string senhaHash = HashSenhaUtil.ComputePasswordSha256Hash(usuario.Senha);
            usuario.Senha = senhaHash;
            if (dao.Add(usuario))
                return CreatedAtRoute("GetUsuario", new { id = usuario.Id }, usuario);
            return BadRequest(new { Message = "Erro interno no servirdor" });
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] Usuario usuario)
        {
            if (string.IsNullOrEmpty(usuario.Email) || string.IsNullOrEmpty(usuario.Senha))
                return BadRequest(new { Message = "Todos os campos são obrigatórios" });

            string senhaHash = HashSenhaUtil.ComputePasswordSha256Hash(usuario.Senha);
            usuario.Senha = senhaHash;
            var usuarioLogado = dao.Login(usuario);
            if (usuarioLogado == null)
                return BadRequest(new { Message = "Erro ao realizar login. Verifique suas credenciais" });
            var token = TokenJwtServices.GerarToken(usuarioLogado);
            usuarioLogado.Senha = "";
            usuarioLogado.Token = token;
            return Ok(usuarioLogado);
        }
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Usuario usuario, long id)
        {
            if (string.IsNullOrEmpty(usuario.Nome) || string.IsNullOrEmpty(usuario.Email) || string.IsNullOrEmpty(usuario.Senha))
                return BadRequest(new { Message = "Todos os campos são obrigatórios" });
            if (dao.Find(id) == null)
                return NotFound(new { Message = "Usuário não encontrado" });
            string senhaHash = HashSenhaUtil.ComputePasswordSha256Hash(usuario.Senha);
            usuario.Senha = senhaHash;
            if (dao.Update(usuario, id))
                return CreatedAtRoute("GetUsuario", new { id = usuario.Id }, usuario);

            return BadRequest(new { Message = "Erro interno no servirdor" });
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var usuario = dao.Find(id);
            if (usuario == null)
                return NotFound(new { Message = "Usuário não encontrado" });
            if (dao.Remove(id))
                return Ok(new { Message = "Excluído com sucesso" });
            return BadRequest(new { Message = "Erro interno no servirdor" });
        }
    }
}
