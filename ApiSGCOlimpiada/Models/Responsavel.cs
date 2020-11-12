using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Models
{
    public class Responsavel
    {
        public int Id { get; set; }
        public string  Nome { get; set; }
        public string  Cargo { get; set; }
        public Escola Escola { get; set; }
    }
}
