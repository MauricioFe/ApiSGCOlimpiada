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
            List<Attachment> attachmentList = new List<Attachment>();

            for (int i = 0; i < data.orcamentoAnexos.Count; i++)
            {
                Attachment attachment = new Attachment();
                attachment.Bytes = data.orcamentoAnexos[i];
                attachment.Name = $"orcamento{i+1}.pdf";
                attachmentList.Add(attachment);

            }
            Attachment planilha = new Attachment();
            planilha.Bytes = data.planilha;
            planilha.Name = "dadosProdutos.xlsx";
            List<string> address = new List<string>();
            foreach (var item in data.Responsaveis)
            {
                address.Add(item.Email);
            }
            int count = attachmentList.Count;
            switch (count)
            {
                case 1:
                    this.To("mauricio.lacerdaml@gmail.com")
                        .From(new MailRecipient("olimpiada@gmail.com", "Envio da solicitação de compra"))
                        .Attach(attachmentList[0])
                        .Attach(planilha)
                        .Subject($"Realizando teste de envio de email")
                        .View("~/Views/templateEmail.cshtml", this.data);
                    break;

                case 2:
                    this.To("mauricio.lacerdaml@gmail.com")
                        .From(new MailRecipient("olimpiada@gmail.com", "Envio da solicitação de compra"))
                        .Attach(attachmentList[0])
                        .Attach(attachmentList[1])
                        .Attach(planilha)
                        .Subject($"Realizando teste de envio de email")
                        .View("~/Views/templateEmail.cshtml", this.data);
                    break;
                case 3:
                    this.To("mauricio.lacerdaml@gmail.com")
                        .From(new MailRecipient("olimpiada@gmail.com", "Envio da solicitação de compra"))
                        .Attach(attachmentList[0])
                        .Attach(attachmentList[1])
                        .Attach(attachmentList[2])
                        .Attach(planilha)
                        .Subject($"Realizando teste de envio de email")
                        .View("~/Views/templateEmail.cshtml", this.data);
                    break;
            }
        }
    }
}
