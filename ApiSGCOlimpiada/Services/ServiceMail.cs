using Coravel.Mailer.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Services
{
    public class NewUserMailable : Mailable<NewUserMailableViewModel>
    {
        private NewUserMailableViewModel _newUser;

        public NewUserMailable(NewUserMailableViewModel newUser) => this._newUser = newUser;

        public override void Build()
        {
            this.To(this._newUser)
                .From(new MailRecipient("projetomymoney@gmail.com", "Teste de envio de email"))
                .Subject($"Realizando teste de envio de email {this._newUser}")
                .View("~/Views/Mail/NewUser.cshtml", this._newUser);
        }
    }
}
