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
        void Add(Ocupacao ocupacao);
        void Update(Ocupacao ocupacao, long id);
        void Remove(long id);
    }
}
