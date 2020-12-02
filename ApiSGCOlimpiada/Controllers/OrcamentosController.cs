﻿using ApiSGCOlimpiada.Data.OrcamentoDAO;
using ApiSGCOlimpiada.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrcamentosController : ControllerBase
    {
        private readonly IOrcamentoDAO dao;
        public static IConfiguration _config;
        public OrcamentosController(IOrcamentoDAO dao, IConfiguration config)
        {
            this.dao = dao;
            _config = config;
        }

        [HttpGet]
        public IEnumerable<Orcamento> GetAll()
        {
            return dao.GetAll();
        }

        [HttpGet("{id}", Name = "GetOrcamento")]
        public IActionResult GetOrcamentoById(long id)
        {
            var solicitacao = dao.Find(id);
            if (solicitacao == null)
                return NotFound(new { Message = "Orçamento não encontrado" });
            return new ObjectResult(solicitacao);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Orcamento orcamento, [FromForm] IFormFile arquivo)
        {
            if (string.IsNullOrEmpty(orcamento.Fornecedor) && string.IsNullOrEmpty(orcamento.Cnpj)
                && string.IsNullOrEmpty(orcamento.Data.ToString("dd/MM/yyyy HH:mm")) && orcamento.ValorTotal == 0
                && orcamento.TotalIpi == 0 && orcamento.TotalProdutos == 0)
                return BadRequest(new { Message = "Todos os campos são obrigatórios" });
            var fileName = await Utils.UploadUtil.UploadAnexosPdfAsync(arquivo, "AnexoOrcamentos");
            if (fileName == null)
                return BadRequest(new { Message = "Erro ao fazer upload" });
            orcamento.Anexo = fileName;
            if (dao.Add(orcamento))
                return CreatedAtRoute("GetOrcamento", new { id = orcamento.Id }, orcamento);
            return BadRequest(new { Message = "Erro interno no servidor" });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromForm] Orcamento orcamento, [FromForm] IFormFile arquivo, long id)
        {
            if (string.IsNullOrEmpty(orcamento.Fornecedor) && string.IsNullOrEmpty(orcamento.Cnpj)
               && string.IsNullOrEmpty(orcamento.Data.ToString("dd/MM/yyyy HH:mm")) && orcamento.ValorTotal == 0
               && orcamento.TotalIpi == 0 && orcamento.TotalProdutos == 0)
                return BadRequest(new { Message = "Todos os campos são obrigatórios" });

            var fileName = await Utils.UploadUtil.UploadAnexosPdfAsync(arquivo, "AnexoOrcamentos");
            if (fileName == null)
                return BadRequest(new { Message = "Erro ao fazer upload" });
            orcamento.Anexo = fileName;

            if (dao.Find(id) == null)
                return NotFound(new { Message = "Orçamento não encontrado" });

            if (dao.Update(orcamento, id))
                return CreatedAtRoute("GetOrcamento", new { id = orcamento.Id }, orcamento);

            return BadRequest(new { Message = "Erro interno no servidor" });
        }
    }
}