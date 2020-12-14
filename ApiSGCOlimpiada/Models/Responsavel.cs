using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Models
{
    public class Responsavel
    {
        public long Id { get; set; }
        public string  Nome { get; set; }
        public string  Cargo { get; set; }
        public string  Email { get; set; }
        public long EscolaId { get; set; }
        public Escola Escola { get; set; }
    }
}
