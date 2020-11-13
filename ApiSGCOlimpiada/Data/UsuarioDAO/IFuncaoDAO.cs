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
        Funcao Find(int id);
        void Add(Funcao funcao);
        void Update(Funcao funcao, int id);
        void Remove(int id);

    }
}
