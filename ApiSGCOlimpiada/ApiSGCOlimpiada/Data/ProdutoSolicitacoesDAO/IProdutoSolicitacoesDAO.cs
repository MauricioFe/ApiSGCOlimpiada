using ApiSGCOlimpiada.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Data.ProdutoSolicitacoesDAO
{
    public interface IProdutoSolicitacoesDAO
    {
        IEnumerable<ProdutoSolicitacao> GetAll();
        ProdutoSolicitacao Find(long id);
        IEnumerable<ProdutoSolicitacao> FindBySolicitacao(long solicitacaoId);
        bool Add(ProdutoSolicitacao produtoSolicitacao);
        bool Update(ProdutoSolicitacao produtoSolicitacao, long id);
        bool Remove(long id);
    }
}
