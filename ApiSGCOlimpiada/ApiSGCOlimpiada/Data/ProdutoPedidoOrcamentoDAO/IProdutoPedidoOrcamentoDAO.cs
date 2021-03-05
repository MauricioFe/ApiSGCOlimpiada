using ApiSGCOlimpiada.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Data.ProdutoPedidoOrcamentoDAO
{
    public interface IProdutoPedidoOrcamentoDAO
    {
        IEnumerable<ProdutoPedidoOrcamento> GetAll();
        IEnumerable<ProdutoPedidoOrcamento> GetSolicitacao(long idSolicitacao);
        ProdutoPedidoOrcamento Find(long id);
        bool Add(ProdutoPedidoOrcamento solicitacaoCompra);
        bool Update(ProdutoPedidoOrcamento solicitacaoCompra, long id);
        bool Remove(long id);
        IEnumerable<Planilha> GetDadosProdutoBySolicitacao(long idSolicitacao);
    }
}
