using ApiSGCOlimpiada.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Data.EscolaDAO
{
    public interface IEscolaDAO
    {
        IEnumerable<Escola> GetAll();
        Escola Find(long id);
        bool Add(Escola escola);
        bool Update(Escola escola, long id);
        bool Remove(long id);
    }
}
