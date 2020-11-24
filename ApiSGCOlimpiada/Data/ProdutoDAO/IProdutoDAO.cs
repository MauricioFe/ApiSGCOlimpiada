using ApiSGCOlimpiada.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Data.ProdutoDAO
{
    public interface IProdutoDAO
    {
        IEnumerable<Produto> GetAll();
        Produto Find(long codigoProtheus);
        List<Produto> FindBySearch(string search);
        bool Add(Produto produto);
        bool Update(Produto produto, long id);
        bool Remove(long id);
    }
}
