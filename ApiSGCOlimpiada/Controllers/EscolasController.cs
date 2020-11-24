﻿using ApiSGCOlimpiada.Data.EscolaDAO;
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
        public IActionResult GetEscolaById(long id)
        {
            var escola = dao.Find(id);
            if (escola == null)
                return NotFound(new { Message = "Escola não encontrada" });
            return new ObjectResult(escola);
        }
        [HttpPost]
        public IActionResult Create([FromBody] Escola escola)
        {
            if (escola == null)
                return BadRequest(new { Message = "Todos os campos são obrigatórios" });
            if (dao.Add(escola))
                return CreatedAtRoute("GetEscola", new { id = escola.Id }, escola);
            return BadRequest(new { Message = "Erro interno no servidor" });
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Escola escola, long id)
        {
            if (escola == null)
                return BadRequest(new { Message = "Todos os campos são obrigatórios" });

            if (dao.Find(id) == null)
                return NotFound(new { Message = "Escola não encontrada" });
            Escola escolaUpdated = new Escola();
            escolaUpdated.Id = id;
            escolaUpdated.Nome = escola.Nome;
            escolaUpdated.Bairro = escola.Bairro;
            escolaUpdated.Cep = escola.Cep;
            escolaUpdated.Cidade = escola.Cidade;
            escolaUpdated.Estado = escola.Estado;
            escolaUpdated.Logradouro = escola.Logradouro;
            escolaUpdated.Numero = escola.Numero;
            if (dao.Update(escolaUpdated, id))
                return CreatedAtRoute("GetEscola", new { id = escolaUpdated.Id }, escolaUpdated);
            return BadRequest(new { Message = "Erro interno no servidor" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var escola = dao.Find(id);
            if (escola == null)
                return NotFound(new { Message = "Escola não encontrada" });
            if (dao.Remove(id))
                return Ok(new { Message = "Excluído com sucesso" });
            return BadRequest(new { Message = "Erro interno no servidor" });
        }
    }
}
