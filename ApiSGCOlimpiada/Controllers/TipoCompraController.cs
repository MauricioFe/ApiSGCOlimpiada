using ApiSGCOlimpiada.Data.TipoCompraDAO;
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
    public class TipoComprasController : ControllerBase
    {
        private readonly ITipoCompraDAO dao;

        public TipoComprasController(ITipoCompraDAO dao)
        {
            this.dao = dao;
        }
        [HttpGet]
        public IEnumerable<TipoCompra> GetAll()
        {
            return dao.GetAll();
        }

        [HttpGet("{id}", Name = "GetTipoCompra")]
        public IActionResult GetTipoCompraById(long id)
        {
            var tipoCompra = dao.Find(id);
            if (tipoCompra == null)
            {
                return NotFound(
                    new
                    {
                        Message = "Tipo de compra não encontrado"
                    }
                );
            }
            return new ObjectResult(tipoCompra);
        }

        [HttpGet("search", Name = "GetTipoCompraSearch")]
        public IActionResult GetTipoCompraBySearch([FromQuery(Name = "search")] string search)
        {
            var tipoCompra = dao.FindBySearch(search);
            if (tipoCompra.Count <= 0)
            {
                return NotFound(
                    new
                    {
                        Message = "Tipo de compra não encontrado"
                    }
                );
            }
            return new ObjectResult(tipoCompra);
        }
        [HttpPost]
        public IActionResult Create([FromBody] TipoCompra tipoCompra)
        {
            if (tipoCompra == null)
            {
                return BadRequest(
                  new
                  {
                      Message = "Todos os campos são obrigatórios"
                  });
            }

            try
            {
                dao.Add(tipoCompra);
                return CreatedAtRoute("GetTipoCompra", new { id = tipoCompra.Id }, tipoCompra);
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
        public IActionResult Put([FromBody] TipoCompra tipoCompra, long id)
        {
            if (tipoCompra == null)
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
                          Message = "Tipo de compra não encontrado"
                      }
                  );
            }
            TipoCompra tipoCompraUpdated = new TipoCompra();
            tipoCompraUpdated.Id = id;
            tipoCompraUpdated.Descricao = tipoCompra.Descricao;
            try
            {
                dao.Update(tipoCompraUpdated, id);
                return CreatedAtRoute("GetTipoCompra", new { id = tipoCompraUpdated.Id }, tipoCompraUpdated);
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
            var tipoCompra = dao.Find(id);
            if (tipoCompra == null)
            {
                return NotFound(
                      new
                      {
                          Message = "Tipo de compra não encontrado"
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
