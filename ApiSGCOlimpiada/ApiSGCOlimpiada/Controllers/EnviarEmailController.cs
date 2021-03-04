using ApiSGCOlimpiada.Data.EmailDAO;
using ApiSGCOlimpiada.Data.OcupacaoDAO;
using ApiSGCOlimpiada.Data.OrcamentoDAO;
using ApiSGCOlimpiada.Data.ProdutoPedidoOrcamentoDAO;
using ApiSGCOlimpiada.Data.ResponsavelDAO;
using ApiSGCOlimpiada.Models;
using ApiSGCOlimpiada.Services;
using Coravel.Mailer.Mail.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnviarEmailController : ControllerBase
    {
        private readonly IMailer _mailer;
        private readonly IEmailDAO _dao;
        private readonly IOrcamentoDAO _daoOrcamento;
        private readonly IResponsavelDAO _daoResponsavel;
        private readonly IOcupacaoDAO _daoOcupacao;
        private readonly IProdutoPedidoOrcamentoDAO _daoProdutoCompras;

        public EnviarEmailController(IMailer mailer, IEmailDAO dao, IOrcamentoDAO daoOrcamento,
            IResponsavelDAO daoResponsavel, IOcupacaoDAO daoOcupacao, IProdutoPedidoOrcamentoDAO daoProdutoCompras)
        {
            this._mailer = mailer;
            this._dao = dao;
            this._daoOrcamento = daoOrcamento;
            this._daoResponsavel = daoResponsavel;
            this._daoOcupacao = daoOcupacao;
            this._daoProdutoCompras = daoProdutoCompras;
        }
        [HttpPost("{idSolicitacao}")]
        public async Task<IActionResult> SendMail([FromBody] EmailModel dados, long idSolicitacao)
        {
            List<Orcamento> orcamentos = (List<Orcamento>)_daoOrcamento.GetOrcamentoBySolicitacao(idSolicitacao);
            List<ProdutoPedidoOrcamento> produtosCompras = (List<ProdutoPedidoOrcamento>)_daoProdutoCompras.GetSolicitacao(idSolicitacao);
            EmailModel data = _dao.GetDadosSolicitacao(idSolicitacao);
            data.CentroResponsabilidade = dados.CentroResponsabilidade;
            data.ClasseValor = dados.ClasseValor;
            data.CodUnidadeOrganizacional = dados.CodUnidadeOrganizacional;
            data.UnidadeOrganizacional = dados.UnidadeOrganizacional;
            var find = orcamentos.Min(r => r.ValorTotal);
            data.Orcamento = orcamentos.Find(r => r.ValorTotal == find);
            data.Responsaveis = (List<Responsavel>)_daoResponsavel.GetBySolicitacao(idSolicitacao);
            data.Ocupacoes = _daoOcupacao.GetBySolicitacao(idSolicitacao);
            foreach (var item in orcamentos)
            {
                if (!string.IsNullOrEmpty(item.Anexo))
                {
                    var path = Path.Combine($@"{Directory.GetCurrentDirectory()}\AnexoOrcamentos", item.Anexo);
                    var memory = new MemoryStream();
                    using var stream = new FileStream(path, FileMode.Open);
                    await stream.CopyToAsync(memory);
                    memory.Position = 0;
                    byte[] bacon = memory.ToArray();
                    memory.Close();
                    data.orcamentoAnexos = new List<byte[]>();
                    data.orcamentoAnexos.Add(bacon);
                }
                else
                {
                    data.orcamentoAnexos = null;
                }
            }



            await this._mailer.SendAsync(new MailServices(data));
            return Ok();
        }

        [HttpGet("teste/{idSolicitacao}")]
        public List<List<ProdutoPedidoOrcamento>> getBacon(long idSolicitacao)
        {
           
            List<ProdutoPedidoOrcamento> produtosCompras = (List<ProdutoPedidoOrcamento>)_daoProdutoCompras.GetDadosProddutoBySolicitacao(idSolicitacao);
           
            return produtosCompras2;
        }
    }
}
