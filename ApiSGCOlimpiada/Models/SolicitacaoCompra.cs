using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Models
{
    public class SolicitacaoCompra
    {
        public int Id { get; set; }
        public string ResponsavelEntrega { get; set; }
        public DateTime Data { get; set; }
        public TipoCompra TipoCompra { get; set; }
        public Escola Escola { get; set; }
        public string Justificativa { get; set; }
    }
}
