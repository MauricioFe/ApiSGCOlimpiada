using ApiSGCOlimpiada.Data.EmailDAO;
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

        public EnviarEmailController(IMailer mailer, IEmailDAO dao, IOrcamentoDAO daoOrcamento, IResponsavelDAO daoResponsavel)
        {
            this._mailer = mailer;
            this._dao = dao;
            this._daoOrcamento = daoOrcamento;
            this._daoResponsavel = daoResponsavel;
        }
        [HttpPost("{idSolicitacao}")]
        public async Task<IActionResult> SendMail([FromBody] EmailModel dados, long idSolicitacao)
        {
            EmailModel data = _dao.GetDadosSolicitacao(idSolicitacao);
            data.CentroResponsabilidade = dados.CentroResponsabilidade;
            data.ClasseValor = dados.ClasseValor;
            data.CodUnidadeOrganizacional = dados.CodUnidadeOrganizacional;
            data.UnidadeOrganizacional = dados.UnidadeOrganizacional;
            data.Orcamentos = (List<Orcamento>)_daoOrcamento.GetOrcamentoBySolicitacao(idSolicitacao);
            data.Responsaveis = (List<Responsavel>)_daoResponsavel.GetBySolicitacao(idSolicitacao);
            await this._mailer.SendAsync(new MailServices(dados));
            return Ok();
        }
    }
}
