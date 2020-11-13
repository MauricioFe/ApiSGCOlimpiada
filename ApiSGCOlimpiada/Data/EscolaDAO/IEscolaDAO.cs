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
        void Add(Escola escola);
        void Update(Escola escola, long id);
        void Remove(long id);
    }
}
