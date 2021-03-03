using ApiSGCOlimpiada.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Data.AcompanhamentoDAO
{
    public interface IAcompanhamentoDAO
    {
        IEnumerable<Acompanhamento> GetAll();
        IEnumerable<Acompanhamento> GetSolicitacaoAcompanhamento();
        IEnumerable<Acompanhamento> GetSolicitacaoAcompanhamentoPendente();
        Acompanhamento FindBySolicitacaoId(long id);
        bool Add(Acompanhamento acompanhamento);
        bool Update(Acompanhamento acompanhamento, long id);
    }
}
