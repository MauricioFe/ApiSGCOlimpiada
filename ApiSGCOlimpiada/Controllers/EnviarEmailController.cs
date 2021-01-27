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

        public EnviarEmailController(IMailer mailer)
        {
            this._mailer = mailer;
        }
        [HttpPost]
        public async Task<IActionResult> SendMail(EmailModel dados)
        {

            await this._mailer.SendAsync(new MailServices(dados));
            return Ok();
        }
    }
}
