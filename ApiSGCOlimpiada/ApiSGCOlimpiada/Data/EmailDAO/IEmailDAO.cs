using ApiSGCOlimpiada.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Data.EmailDAO
{
    public interface IEmailDAO
    {
        EmailModel GetDadosSolicitacao(long idSolicitacao);
    }
}
