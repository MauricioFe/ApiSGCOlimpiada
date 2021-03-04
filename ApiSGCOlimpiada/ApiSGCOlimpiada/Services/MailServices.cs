using ApiSGCOlimpiada.Models;
using Coravel.Mailer.Mail;
using Coravel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace ApiSGCOlimpiada.Services
{
    public class MailServices : Mailable<EmailModel>
    {
        private EmailModel data;

        public MailServices(EmailModel data) => this.data = data;

        public override void Build()
        {

            Attachment attachment1 = new Attachment();
            attachment1.Bytes = data.orcamentoAnexos[0];
            attachment1.Name = "orcamento1.pdf";
            Attachment attachment2 = new Attachment();
            attachment2.Bytes = data.orcamentoAnexos[0];
            attachment2.Name = "orcamento2.pdf";
            Attachment attachment3 = new Attachment();
            attachment3.Bytes = data.orcamentoAnexos[0];
            attachment3.Name = "orcamento3.pdf";
            List<string> address = new List<string>();
            foreach (var item in data.Responsaveis)
            {
                address.Add(item.Email);
            }
            this.To("mauricio.lacerdaml@gmail.com")
                .From(new MailRecipient("olimpiada@gmail.com", "Envio da solicitação de compra"))
                .Attach(attachment1)
                .Attach(attachment2)
                .Attach(attachment3)
                .Subject($"Realizando teste de envio de email")
                .View("~/Views/testeEmail.cshtml", this.data);
        }
    }
}
