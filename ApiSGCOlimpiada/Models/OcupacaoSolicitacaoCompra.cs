using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Models
{
    public class OcupacaoSolicitacaoCompra
    {
        public int OcupacaoId { get; set; }
        public int SolicitacaoId { get; set; }
        public Ocupacao Ocupacao { get; set; }
        public SolicitacaoCompra SolicitacaoCompra { get; set; }
    }
}
