using ApiSGCOlimpiada.Models;
using Coravel.Mailer.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Services
{
    public class MailServices : Mailable<Responsavel>
    {
        private Responsavel responsaveis;

        public MailServices(Responsavel responsaveis) => this.responsaveis = responsaveis;

        public override void Build()
        {
            this.To(this.responsaveis)
                .From(new MailRecipient("projetomymoney@gmail.com", "Teste de envio de email"))
                .Subject($"Realizando teste de envio de email {this.responsaveis}")
                .View("~/Views/testeEmail.cshtml", this.responsaveis);
        }
    }
}
