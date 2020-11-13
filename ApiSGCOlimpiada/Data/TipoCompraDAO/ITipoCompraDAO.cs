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
        TipoCompra Find(long id);
        void Add(TipoCompra grupo);
        void Update(TipoCompra grupo, long id);
        void Remove(long id);
    }
}
