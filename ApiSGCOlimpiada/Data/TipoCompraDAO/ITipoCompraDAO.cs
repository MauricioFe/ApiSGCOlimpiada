using ApiSGCOlimpiada.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Data.TipoCompraDAO
{
    public interface ITipoCompraDAO
    {
        IEnumerable<TipoCompra> GetAll();
        List<TipoCompra> FindBySearch(string search);
        TipoCompra Find(long id);
        bool Add(TipoCompra grupo);
        bool Update(TipoCompra grupo, long id);
        bool Remove(long id);
    }
}
