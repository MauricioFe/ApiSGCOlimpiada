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
        Acompanhamento Find(long id);
        bool Add(Acompanhamento acompanhamento);
        bool Update(Acompanhamento acompanhamento, long id);
    }
}
