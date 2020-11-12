using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Models
{
    public class Acompanhamento
    {
        public int Id { get; set; }
        public string Observacao { get; set; }
        public DateTime Date { get; set; }
        public Status Status { get; set; }
        public Usuario Usuario { get; set; }
        public SolicitacaoCompra SolicitacaoCompra { get; set; }
    }
}
