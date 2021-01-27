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
    public class SendMailsController : ControllerBase
    {
        private readonly IMailer _mailer;

        public SendMailsController(IMailer mailer)
        {
            this._mailer = mailer;
        }
        [HttpPost]
        public async Task<IActionResult> SendMail(NewUserMailableViewModel model, string returnUrl = null)
        {
            /* Código omitido */
            await this._mailer.SendAsync(new NewUserMailable(
                new NewUserMailableViewModel
                {
                    Name = model.Name,
                    Email = model.Email,
                    CallbackUrl = model.CallbackUrl
                }
            ));

            return Ok();

            /* Código omitido */
        }
    }
}
