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
        Usuario Find(int id);
        Usuario Login(Usuario usuario);
        void Add(Usuario usuario);
        void Update(Usuario usuario, int id);
        void Remove(int id);

    }
}
