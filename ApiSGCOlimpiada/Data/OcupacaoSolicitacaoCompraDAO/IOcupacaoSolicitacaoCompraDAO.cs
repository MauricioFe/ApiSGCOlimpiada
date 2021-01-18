using ApiSGCOlimpiada.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Data.OcupacaoSolicitacaoCompraDAO
{
    public interface IOcupacaoSolicitacaoCompraDAO
    {
        IEnumerable<OcupacaoSolicitacaoCompra> GetAll();
        OcupacaoSolicitacaoCompra Find(long ocupacoesId, long solicitacaoCompraId);
        bool Add(OcupacaoSolicitacaoCompra ocupacaoSolicitacaoCompra);
        bool Update(OcupacaoSolicitacaoCompra ocupacaoSolicitacaoCompra, long ocupacoesId, long solicitacaoCompraId);
        bool Remove(long ocupacoesId, long solicitacaoCompraId);
        IEnumerable<OcupacaoSolicitacaoCompra> GetSolicitacaoOcupacao(long solicitacaoID);
    }
}
