using ApiSGCOlimpiada.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Data.OcupacaoDAO
{
    public interface IOcupacaoDAO
    {
        IEnumerable<Ocupacao> GetAll();
        Ocupacao Find(long id);
        List<Ocupacao> FindBySearch(string search);
        bool Add(Ocupacao ocupacao);
        bool Update(Ocupacao ocupacao, long id);
        bool Remove(long id);
    }
}
