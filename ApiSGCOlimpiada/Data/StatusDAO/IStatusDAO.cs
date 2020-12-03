using ApiSGCOlimpiada.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Data.StatusDAO
{
    public interface IStatusDAO
    {
        IEnumerable<Status> GetAll();
        Status Find(long id);
        bool Add(Status acompanhamento);
        bool Update(Status acompanhamento, long id);
    }
}
