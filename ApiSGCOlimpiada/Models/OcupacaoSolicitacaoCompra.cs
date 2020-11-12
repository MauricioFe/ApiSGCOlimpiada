using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Models
{
    public class OcupacaoSolicitacaoCompra
    {
        public Ocupacao Ocupacao { get; set; }
        public SolicitacaoCompra SolicitacaoCompra { get; set; }
    }
}
