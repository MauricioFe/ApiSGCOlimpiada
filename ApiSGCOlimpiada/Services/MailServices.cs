using ApiSGCOlimpiada.Models;
using Coravel.Mailer.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Services
{
    public class MailServices : Mailable<EmailModel>
    {
        private EmailModel data;

        public MailServices(EmailModel data) => this.data = data;

        public override void Build()
        {
            this.To(this.data)
                .From(new MailRecipient("projetomymoney@gmail.com", "Teste de envio de email"))
                .Subject($"Realizando teste de envio de email {this.data}")
                .View("~/Views/testeEmail.cshtml", this.data);
        }
    }
}
