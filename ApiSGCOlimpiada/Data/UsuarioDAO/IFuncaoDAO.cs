using ApiSGCOlimpiada.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Data.UsuarioDAO
{
    public interface IFuncaoDAO
    {
        IEnumerable<Funcao> GetAll();
        Funcao Find(long id);
        bool Add(Funcao funcao);
        bool Update(Funcao funcao, long id);
        bool Remove(long id);

    }
}
