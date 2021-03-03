using ApiSGCOlimpiada.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Data.GrupoDAO
{
    public interface IGrupoDAO
    {
        IEnumerable<Grupo> GetAll();
        List<Grupo> FindBySearch(string search);
        Grupo Find(long id);
        bool Add(Grupo grupo);
        bool Update(Grupo grupo, long id);
        bool Remove(long id);

    }
}
