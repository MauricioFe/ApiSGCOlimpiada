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
            List<string> address = new List<string>();
            address.Add(data.Email);
            this.To(address)
                .From(new MailRecipient("olimpiada@gmail.com", "Envio da solicitação de compra"))
                .Subject($"Realizando teste de envio de email")
                .View("~/Views/testeEmail.cshtml", this.data);
        }
    }
}
