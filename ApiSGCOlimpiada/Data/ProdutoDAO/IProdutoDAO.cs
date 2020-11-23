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
        Produto FindByProtheus(long codigoProtheus);
        void Add(Produto produto);
        void Update(Produto produto, long id);
        void Remove(long id);
    }
}
