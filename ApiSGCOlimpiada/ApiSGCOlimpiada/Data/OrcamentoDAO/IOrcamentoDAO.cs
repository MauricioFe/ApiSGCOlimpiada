using ApiSGCOlimpiada.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Data.OrcamentoDAO
{
    public interface IOrcamentoDAO
    {
        IEnumerable<Orcamento> GetAll();
        Orcamento Find(long id);
        bool Add(Orcamento orcamento);
        bool Update(Orcamento orcamento, long id);
        IEnumerable<Orcamento> GetOrcamentoBySolicitacao(long idSolicitacao);
    }
}
