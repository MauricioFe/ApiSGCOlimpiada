using ApiSGCOlimpiada.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coravel.Mailer.Mail;

namespace ApiSGCOlimpiada.Services
{
    public class DownloadEmail : Mailable<EmailModel>
    {
        private EmailModel data;

        public DownloadEmail(EmailModel data) => this.data = data;

        public override void Build()
        {
            List<string> address = new List<string>();
            foreach (var item in data.Responsaveis)
            {
                address.Add(item.Email);
            }
            this.To("mauricio.lacerdaml@gmail.com")
                .From(new MailRecipient("olimpiada@gmail.com", "Envio da solicitação de compra"))
                .Subject($"Realizando teste de envio de email")
                .View("~/Views/testeEmail.cshtml", this.data);
        }
    }
}
