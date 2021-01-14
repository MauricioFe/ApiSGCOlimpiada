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
        IEnumerable<ProdutoPedidoOrcamento> GetProdutosSolicitacao(long idSolicitacao);
        ProdutoPedidoOrcamento Find(long solicitacaoId, long produtoId);
        bool Add(ProdutoPedidoOrcamento solicitacaoCompra);
        bool Update(ProdutoPedidoOrcamento solicitacaoCompra, long solicitacaoId, long produtoId, long orcamentoId);
    }
}
