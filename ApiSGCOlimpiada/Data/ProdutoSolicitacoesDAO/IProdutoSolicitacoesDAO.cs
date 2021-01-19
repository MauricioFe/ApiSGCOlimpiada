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
        ProdutoSolicitacao Find(int id);
        ProdutoSolicitacao FindBySolicitacao(int solicitacaoId);
        bool Add(ProdutoSolicitacao produtoSolicitacao);
        bool Update(ProdutoSolicitacao produtoSolicitacao, int id);
        bool Remove(int id);
    }
}
