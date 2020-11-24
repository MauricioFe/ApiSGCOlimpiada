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
        List<Usuario> FindByName(string Nome);
        Usuario Login(Usuario usuario);
        bool Add(Usuario usuario);
        bool Update(Usuario usuario, long id);
        bool Remove(long id);

    }
}
