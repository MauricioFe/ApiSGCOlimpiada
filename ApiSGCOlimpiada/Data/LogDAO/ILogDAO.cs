using ApiSGCOlimpiada.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Data.LogDAO
{
    public interface ILogDAO
    {
        IEnumerable<Log> GetAll();
        Log Find(long id);
        bool Add(Log log);
        bool Update(Log log, long id);
        bool Remove(long id);
    }
}
