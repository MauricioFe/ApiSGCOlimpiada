using ApiSGCOlimpiada.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Data.SolicitacaoCompraDAO
{
    public interface ISolicitacaoCompraDAO
    {
        IEnumerable<SolicitacaoCompra> GetAll();
        SolicitacaoCompra Find(int id);
        bool Add(SolicitacaoCompra solicitacaoCompra);
        bool Update(SolicitacaoCompra solicitacaoCompra);
    }
}
