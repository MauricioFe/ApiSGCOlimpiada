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
        Grupo FindBySearch(string search);
        Grupo Find(long id);
        void Add(Grupo grupo);
        void Update(Grupo grupo, long id);
        void Remove(long id);

    }
}
