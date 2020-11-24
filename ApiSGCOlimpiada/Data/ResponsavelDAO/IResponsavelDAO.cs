using ApiSGCOlimpiada.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Data.ResponsavelDAO
{
    public interface IResponsavelDAO
    {
        IEnumerable<Responsavel> GetAll();
        Responsavel Find(long id);
        List<Responsavel> FindBySearch(string search);
        bool Add(Responsavel responsavel);
        bool Update(Responsavel responsavel, long id);
        bool Remove(long id);
    }
}
