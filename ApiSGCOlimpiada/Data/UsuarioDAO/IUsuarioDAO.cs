using ApiSGCOlimpiada.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Data.UsuarioDAO
{
    public interface IUsuarioDAO
    {
        IEnumerable<Usuario> GetAll();
        Usuario Find(long id);
        Usuario FindByName(string Nome);
        Usuario Login(Usuario usuario);
        void Add(Usuario usuario);
        void Update(Usuario usuario, long id);
        void Remove(long id);

    }
}
