using ApiSGCOlimpiada.Data.EmailDAO;
using ApiSGCOlimpiada.Data.OcupacaoDAO;
using ApiSGCOlimpiada.Data.OrcamentoDAO;
using ApiSGCOlimpiada.Data.ResponsavelDAO;
using ApiSGCOlimpiada.Models;
using ApiSGCOlimpiada.Services;
using Coravel.Mailer.Mail.Interfaces;
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
    public class EnviarEmailController : ControllerBase
    {
        private readonly IMailer _mailer;
        private readonly IEmailDAO _dao;
        private readonly IOrcamentoDAO _daoOrcamento;
        private readonly IResponsavelDAO _daoResponsavel;
        private readonly IOcupacaoDAO _daoOcupacao;

        public EnviarEmailController(IMailer mailer, IEmailDAO dao, IOrcamentoDAO daoOrcamento,
            IResponsavelDAO daoResponsavel, IOcupacaoDAO daoOcupacao)
        {
            this._mailer = mailer;
            this._dao = dao;
            this._daoOrcamento = daoOrcamento;
            this._daoResponsavel = daoResponsavel;
            this._daoOcupacao = daoOcupacao;
        }
        [HttpPost("{idSolicitacao}")]
        public async Task<IActionResult> SendMail([FromBody] EmailModel dados, long idSolicitacao)
        {
            EmailModel data = _dao.GetDadosSolicitacao(idSolicitacao);
            data.CentroResponsabilidade = dados.CentroResponsabilidade;
            data.ClasseValor = dados.ClasseValor;
            data.CodUnidadeOrganizacional = dados.CodUnidadeOrganizacional;
            data.UnidadeOrganizacional = dados.UnidadeOrganizacional;
            var find = ((List<Orcamento>)_daoOrcamento.GetOrcamentoBySolicitacao(idSolicitacao)).Min(r => r.ValorTotal);
            data.Orcamento = ((List<Orcamento>)_daoOrcamento.GetOrcamentoBySolicitacao(idSolicitacao)).Find(r => r.ValorTotal == find);
            data.Responsaveis = (List<Responsavel>)_daoResponsavel.GetBySolicitacao(idSolicitacao);
            data.Ocupacoes = _daoOcupacao.GetBySolicitacao(idSolicitacao);
            await this._mailer.SendAsync(new MailServices(data));
            return Ok();
        }
    }
}
